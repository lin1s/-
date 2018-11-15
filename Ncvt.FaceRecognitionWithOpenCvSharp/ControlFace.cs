using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FROCS.Core.Models;

namespace Ncvt.FaceRecognitionWithOpenCvSharp
{
    public partial class ControlFace : UserControl
    {
        public PersonFace MyProperty { get; set; }
        public ControlFace()
        {
            InitializeComponent();
        }

        private void ListFace_Load(object sender, EventArgs e)
        {

        }
    }
}
