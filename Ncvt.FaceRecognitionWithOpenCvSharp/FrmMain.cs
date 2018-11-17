using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FROCS.Application;
using FROCS.Application.Common;
using FROCS.Application.Dtos;
using FROCS.Core;
using FROCS.Core.Models;
using FROCS.EntityFramework.Data;
using FROCS.EntityFramework.Repository;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.UserInterface;
using Stepon.FaceRecognization.Recognization;

namespace Ncvt.FaceRecognitionWithOpenCvSharp
{
    public partial class FrmMain : Form
    {
        private int _currentCameraIndex = 0;       // 当前摄像头索引
        private bool _isRunning = false;           // 运行状态
        private VideoCapture _capture;             // 视频采集设备
        private static Mat _receivedImage; //      // 接收到的图像
        private readonly long _fps = 1000 / 30;    // 画面每秒传输帧数，1000毫秒（1秒）

        private Task _run;                      // 用于运行视频采集及图像处理线程
        private bool _shouldShot;               // 是否从视频中捕捉人脸添加到人脸库
        private readonly Stopwatch _watch = new Stopwatch();  // 计时器，计算处理一帧消耗的时间

        private FaceDetectionService _faceDetectionService;   // 人脸处理服务类
        private PersonFaceRepository _personFaceRepository;   // 人脸仓储类
        private List<PersonFace> _cache;        // 返回所有人脸集合
        private static Bitmap _frameImage;      // 显示到视频监视区的图像

        private bool _rotateFlip = true;         // 是否左右翻转画面，摄像头设为true, 视频设为false 


        //private static Mat _receivedImage;
        //const string Move = @"f:\01hadoop介绍1.avi";
        //private VideoCapture _capture = new VideoCapture(Move);

        // private BackgroundWorker _worker;
        public FrmMain()
        {
            InitializeComponent();
        }
        int sleepTime = (int)(1000 / 30);


        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitMenu();

        }

        private void Init()
        {
            _faceDetectionService = new FaceDetectionService();
            _personFaceRepository = new PersonFaceRepository();
            _cache = _personFaceRepository.GetAllPersonFaces();
        }


        /// <summary>
        /// 初始化菜单，添加当前电脑连接的摄像头
        /// </summary>
        private void InitMenu()
        {
            var _cameraDeviceManager = new WebCameraDeviceManager();
            var _cameras = _cameraDeviceManager.GetCameraDeviceList();
            if (_cameras.Count > 0)
            {
                foreach (var camera in _cameras)
                {
                    ToolStripItem cameraMenu = new ToolStripMenuItem();
                    cameraMenu.Text = string.Format("{0}：{1}", camera.Index + 1, camera.Title);
                    cameraMenu.Tag = camera.Index;
                    cameraMenu.Click += new EventHandler(cameraMenuItem_Click);
                    tsmiVideoCapture.DropDownItems.Add(cameraMenu);
                }
            }
            else
            {
                MessageBox.Show("当前电脑没有检测到可用摄像头！");
            }

            tsmiStop.Enabled = false;                // 禁用 停止 菜单
            tsmiAddFaceFromVideo.Enabled = false;    // 禁用 从视频添加人脸菜单
        }


        /// <summary>
        /// 摄像头选择菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;  // 获取当前菜单项
            _currentCameraIndex = (int)item.Tag;
            if (_isRunning)
            {
                //StopVideoCapture();            // 先停止原来的视频采集              
            }
            // StartVideoCapture(_currentCameraIndex);   // 启动新的视频采集
        }


        /// <summary>
        /// 从照片添加人脸库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddFaceFromPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();       // 打开文件对话框
            openFile.Filter = "JPEG|*.jpg;*.jpeg|位图文件|*.bmp";  // 打开文件的类型
            openFile.Multiselect = true;                          // 是否允许打开多张图片


            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (openFile.FileNames.Count() > 0)
                {
                    var images = openFile.FileNames;
                    AddFaceFeatureToLibrary(images);
                }
  
            }
        }


        /// <summary>
        /// 从多个图片添加人脸
        /// </summary>
        /// <param name="images">图片文件名集合</param>
        private void AddFaceFeatureToLibrary(string[] images)
        {
            FrmFacePhoto ffp = new FrmFacePhoto(images);
            if (ffp.ShowDialog() == DialogResult.OK)
            {
                if(_cache!=null)
                _cache = _personFaceRepository.GetAllPersonFaces();  //添加人脸后重新载入人脸库数据
            }
            ffp.Dispose();
        }

        /// <summary>
        /// 从图像中识别并添加人脸到库
        /// </summary>
        /// <param name="_image"></param>
        private void AddFaceFeatureToLibrary(Bitmap _image)
        {
            FrmFacePhoto ffp = new FrmFacePhoto(_image);
            if (ffp.ShowDialog() == DialogResult.OK)
            {
                if (_cache != null)
                    _cache = _personFaceRepository.GetAllPersonFaces();  //添加人脸后重新载入人脸库数据
            }
            ffp.Dispose();
        }


        private void tsmiStart_Click(object sender, EventArgs e)
        {
            StartVideoCapture(_currentCameraIndex);
            tsmiStart.Enabled = false;
            tsmiStop.Enabled = true;
            tsmiAddFaceFromVideo.Enabled = true;
        }

        private void tsmiStop_Click(object sender, EventArgs e)
        {

            tsmiStart.Enabled = true;
            tsmiStop.Enabled = false;
            tsmiAddFaceFromVideo.Enabled = false;
        }

        private void StartVideoCapture(int camIndex = 0)
        {
            Init();
            if (_capture != null && _capture.CaptureType == CaptureType.Camera && _currentCameraIndex == camIndex)
            {
                return;
            }
            if (_receivedImage == null)
            {
                _receivedImage = new Mat();
            }

            _capture = new VideoCapture(camIndex);  // 实例化指定摄像头 
            _currentCameraIndex = camIndex;

            // 设置摄像头的分辨率为1280*720，小于此分辨率时会按摄像头最高分辨率工作
            _capture.Set(CaptureProperty.FrameWidth, 1920);
            _capture.Set(CaptureProperty.FrameHeight, 1080);

            _isRunning = true;   // 这一行要放在线程启动之前，不然while(_isRunning) 为False，无法循环

            _run = new Task(VideoFrameCaptured);   // 实例化视频采集捕抓线程
            _run.Start();   // 启动线程


            tsmiStart.Enabled = false;
            tsmiStop.Enabled = true;

        }

        /// <summary>
        /// 视频捕获
        /// </summary>
        private void VideoFrameCaptured()
        {
            while (_isRunning)
            {
                _watch.Start();
                if(_capture != null && _capture.CvPtr != IntPtr.Zero)
                {
                    try
                    {
                        if (_receivedImage == null || _receivedImage.CvPtr == IntPtr.Zero)
                        {
                            _receivedImage = new Mat();
                        }

                        bool success = _capture.Read(_receivedImage);  // 读取1帧图像到 _receivedImage
                        if (!success)
                        {
                            // toolStripStatusLabel2.Text = "Producer: null frame from live camera, continue!";
                        }

                        _frameImage = _receivedImage.ToBitmap();  // 摄像头读到的数据转为Bitmap

                        if (_rotateFlip)
                        {
                            // 图像进行水平翻转(使用摄像头时需要这句，视频文件时注释掉，或是加判断进行控制)
                            _frameImage.RotateFlip(RotateFlipType.Rotate180FlipY);
                        }

                        if (_shouldShot)
                        {
                            AddFaceFeatureToLibrary(_frameImage.Clone(new Rectangle(0, 0, _frameImage.Width, _frameImage.Height),
                                _frameImage.PixelFormat));
                            _shouldShot = false;
                        }

                        picVideoImage.Image = _frameImage;  // 显示图片
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }



                _watch.Stop();   // 停止计时
                var runtime = _watch.ElapsedMilliseconds;  // 获取当前运行的总时间
            }
        }


        /// <summary>
        /// 从视频添加到人脸库菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddFaceFromVideo_Click(object sender, EventArgs e)
        {
            _shouldShot = true;   // 视频捕获人脸开启
        }
    }
}
