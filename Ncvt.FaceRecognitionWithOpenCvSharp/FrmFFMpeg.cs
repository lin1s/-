using NReco.VideoConverter;
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
    public partial class FrmFFMpeg : Form
    {
        public FrmFFMpeg()
        {
            InitializeComponent();
        }

        private void FrmFFMpeg_Load(object sender, EventArgs e)
        {

        }


        private MemoryStream outputStream;
        private ConvertLiveMediaTask task;
        private void Init()
        {
            var ffmpeg =new FFMpegConverter();
            outputStream = new MemoryStream();
            
        }

    }
}
