using FROCS.Application;
using FROCS.Application.Common;
using FROCS.Application.Dtos;
using FROCS.Core;
using FROCS.Core.Configuration;
using FROCS.Core.Models;
using FROCS.EntityFramework.Repository;
using Stepon.FaceRecognization.Recognization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ncvt.FaceRecognitionWithOpenCvSharp
{
    public partial class FrmFacePhoto : Form
    {
        public PersonFaceFeature PersonFaceFeature { get; set; }
        // List<FaceFeatureImageThumb> _lists;
        // PersonFaceService _personFaceService;
        PersonFaceRepository _personFaceRepository;
        FaceDetectionService _faceDetectionService;

        Feature _feature;
        Bitmap _faceImage;      // 完整的图片
        Bitmap _faceThumbnail;  // 人脸截图
        Bitmap _singleImage;    // 单张图片
        bool _single = false;   // 是否单个图像，对应的是从摄像头添加人脸
        bool _hasFace = false;  // 包含有效人脸时为true
        string[] _images;  // 由父窗体传来的图片文件名列表
        int _count = 0;  // 图片数量
        int _faceCount = 0; // 人脸数量
        int i = 0;       // 图片处理计数器
        public FrmFacePhoto()
        {
            InitializeComponent();
            _personFaceRepository = new PersonFaceRepository();
            _faceDetectionService = new FaceDetectionService();

        }

        public FrmFacePhoto(string[] images) : this()
        {
            _images = images;
            _count = _images.Count();
        }


        public FrmFacePhoto(Bitmap image) : this()
        {
            _singleImage = image;
            _count = 1;
            _single = true;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Enabled = false;
            if (_hasFace)
            {
                PersonFace _personFace = new PersonFace();
                _personFace.Name = txtPersonName.Text;
                _personFace.Position = txtPosition.Text;
                _personFace.Description = txtDescription.Text;
                _personFace.SerialNumber = txtSerialNumber.Text;

                if (!Directory.Exists(AppConfigurations.FaceImagesPath))
                    Directory.CreateDirectory(AppConfigurations.FaceImagesPath);

                var imageFileName = string.Format("{0}.jpg", System.Guid.NewGuid().ToString());

                var thumbImageFileName = Path.Combine(AppConfigurations.FaceImagesPath, imageFileName);

                // 保存人脸截图缩略图
                _faceThumbnail.Save(thumbImageFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                _personFace.ImageUrl = thumbImageFileName;
                _personFace.FaceFeature = _feature.FeatureData;



                // 查找相似度0.5以上的人脸
                var face = _faceDetectionService.FindPersonFaceByFeatureFirstOrDefault(_feature);
                if (face == null)
                {
                    var result = _personFaceRepository.InsertPersionFace(_personFace);
                    _faceCount++;
                }
                else
                {
                    FrmReplaceFaceFeature frff = new FrmReplaceFaceFeature(_personFace, face.PersonFace, face.Similar);

                    if (frff.ShowDialog() == DialogResult.OK)
                    {
                        _personFace.Id = face.PersonFace.Id;
                        var rep = _personFaceRepository.UpdatePersonFace(_personFace);
                        MessageBox.Show("人脸更新成功！");
                    }

                    frff.Dispose();
                }

            }
            btnOK.Enabled = true;
            i++;
            if (i < _count)
            {
                ShowFaceImage(i);
            }
            else
            {
                MessageBox.Show("成功添加人脸特征数量：" + _faceCount);
                this.DialogResult = DialogResult.OK;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("成功添加人脸特征数量：" + _faceCount);
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmFacePhoto_Load(object sender, EventArgs e)
        {
            tsslFaceCount.Text = string.Format("待识别人脸特征图片数量：{0}   |   ", _count);

            if (_single)
            {
                pictureBox1.Image = _singleImage;
                var locateResult = _faceDetectionService.FaceLocateResult(_singleImage);
                if (locateResult.FaceCount == 1)
                {
                    btnOK.Text = "添加人脸";
                    _feature = _faceDetectionService.GetFaceFeature(_singleImage, locateResult);
                    _faceThumbnail = PictureProcess.GetThumbnail(_singleImage, _feature);
                    picThumbnail.Image = _faceThumbnail;
                    _hasFace = true;
                }
                else
                {
                    var lbShowText = new Label();   // 使用 Label 在图片中显示文字
                    lbShowText.Text = string.Format("图片无有效人脸\n\n请跳过");
                    lbShowText.Font = new Font("宋体", 14, FontStyle.Bold);
                    lbShowText.ForeColor = Color.Red;
                    lbShowText.Location = new Point(0, 00);
                    lbShowText.Width = 150;
                    lbShowText.Height = 150;
                    lbShowText.TextAlign = ContentAlignment.MiddleCenter;
                    lbShowText.Parent = picThumbnail;
                    btnOK.Text = "跳过";

                    _hasFace = false;
                }

                tsslAddedToLibrary.Text = string.Format("已经添加到人脸库数量：{0}   |   ", _faceCount);
                tsslCurrentNumber.Text = string.Format("当前处理图片序号：{0}   ", i + 1);

                txtPersonName.Text = string.Empty;
                txtPosition.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtDescription.Text = string.Empty;

                txtPersonName.Enabled = _hasFace;
                txtPosition.Enabled = _hasFace;
                txtSerialNumber.Enabled = _hasFace;
                txtDescription.Enabled = _hasFace;
            }
            else
            {
                ShowFaceImage(i);
            }


        }

        /// <summary>
        /// 关闭窗体前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmFacePhoto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_faceImage != null)
            {
                _faceImage.Dispose();
            }
            if (_faceThumbnail != null)
            {
                _faceThumbnail.Dispose();
            }

            if (_feature != null)
            {
                _feature.Dispose();
            }
            _faceDetectionService.Dispose();
            _personFaceRepository.Dispose();
        }

     

        /// <summary>
        /// 根据图片数组的下标载入并显示图片
        /// </summary>
        /// <param name="no">图片的下标</param>
        private void ShowFaceImage(int no)
        {
            var imageName = _images[no];
            _faceImage = new Bitmap(Image.FromFile(imageName));
            pictureBox1.Image = _faceImage;
            var locateResult = _faceDetectionService.FaceLocateResult(_faceImage);
            if (locateResult.FaceCount == 1)
            {
                btnOK.Text = "添加人脸";
                _feature = _faceDetectionService.GetFaceFeature(_faceImage, locateResult);
                _faceThumbnail = PictureProcess.GetThumbnail(_faceImage, _feature);
                picThumbnail.Image = _faceThumbnail;
                _hasFace = true;
            }
            else
            {
                var lbShowText = new Label();   // 使用 Label 在图片中显示文字
                lbShowText.Text = string.Format("图片无有效人脸\n\n请跳过");
                lbShowText.Font = new Font("宋体", 14, FontStyle.Bold);
                lbShowText.ForeColor = Color.Red;
                lbShowText.Location = new Point(0, 00);
                lbShowText.Width = 150;
                lbShowText.Height = 150;
                lbShowText.TextAlign = ContentAlignment.MiddleCenter;
                lbShowText.Parent = picThumbnail;
                btnOK.Text = "跳过";

                _hasFace = false;
            }

            tsslAddedToLibrary.Text = string.Format("已经添加到人脸库数量：{0}   |   ", _faceCount);
            tsslCurrentNumber.Text = string.Format("当前处理图片序号：{0}   ", i + 1);

            // 来自文件的时候，文件名默认为人脸名
            if (_single)
            {
                txtPersonName.Text = string.Empty;
            }
            else
            {
                // var info = new FileInfo(imageName);   // 获取文件名
                // var fileName = info.Name.Replace(info.Extension, ""); // 去掉扩展名
                txtPersonName.Text = System.IO.Path.GetFileNameWithoutExtension(imageName);  // 直接获取主文件名
            }
            txtPosition.Text = string.Empty;
            txtSerialNumber.Text = string.Empty;
            txtDescription.Text = string.Empty;

            txtPersonName.Enabled = _hasFace;
            txtPosition.Enabled = _hasFace;
            txtSerialNumber.Enabled = _hasFace;
            txtDescription.Enabled = _hasFace;
        }

    }
}
