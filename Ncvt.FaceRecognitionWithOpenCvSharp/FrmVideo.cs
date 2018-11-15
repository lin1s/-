using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.UserInterface;
using System;
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
using NReco.VideoConverter;

namespace Ncvt.FaceRecognitionWithOpenCvSharp
{
    public partial class FrmVideo : Form
    {
        public FrmVideo()
        {
            InitializeComponent();
        }

        private void FrmVideo_Load(object sender, EventArgs e)
        {
            //_pictureBoxIpl1 = pictureBoxIpl1;
           
        }

        // private PictureBoxIpl _pictureBoxIpl1;
        private bool isRunning = false;

        private VideoCapture _capture = new VideoCapture("f:\\01hadoop介绍1.avi");

        private void button1_Click(object sender, EventArgs e)
        {
            isRunning = !isRunning;
            pictureBox1.Refresh();
        }
      
        private void btnStop_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isRunning)
            {
                Mat image = new Mat();
                _capture.Read(image);
                if (image.Empty())
                {
                    isRunning = !isRunning;
                }
                else
                {
                    int sleeptime = (int)(1000 / _capture.Fps);
                    pictureBox1.BackgroundImage = image.ToBitmap();
                    Cv2.WaitKey(sleeptime);
                    image.Dispose();
                }
            }
        }
    }
}
