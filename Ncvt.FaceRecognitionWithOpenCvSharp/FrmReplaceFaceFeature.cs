using FROCS.Application.Dtos;
using FROCS.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ncvt.FaceRecognitionWithOpenCvSharp
{
    public partial class FrmReplaceFaceFeature : Form
    {
        public PersonFace NewFace { get; set; }
        public PersonFace OldFace { get; set; }
        public float Similar { get; set; }
        public FrmReplaceFaceFeature()
        {
            InitializeComponent();
        }

        public FrmReplaceFaceFeature(PersonFace _newFace, PersonFace _oldFace, float Similar) : this()
        {
            this.NewFace = _newFace;
            this.OldFace = _oldFace;
            this.Similar = Similar * 100;
        }

        private void FrmReplaceFaceFeature_Load(object sender, EventArgs e)
        {
            lbSimilar.Text = string.Format("{0:N}%", Similar);
            picNewFaceImage.ImageLocation = NewFace.ImageUrl;
            lbNewDescription.Text = NewFace.Description;
            lbNewName.Text = NewFace.Name;
            lbNewPosition.Text = NewFace.Position;
            lbNewSerialNumber.Text = NewFace.SerialNumber;

            picOldFaceImage.ImageLocation = OldFace.ImageUrl;
            lbOldDescription.Text = OldFace.Description;
            lbOldName.Text = OldFace.Name;
            lbOldPosition.Text = OldFace.Position;
            lbOldSerialNumber.Text = OldFace.SerialNumber;
            lbCreationTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", OldFace.CreationTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
