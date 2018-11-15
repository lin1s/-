namespace Ncvt.FaceRecognitionWithOpenCvSharp
{
    partial class ControlFace
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbFaceName = new System.Windows.Forms.Label();
            this.lbFaceGender = new System.Windows.Forms.Label();
            this.lbFaceAge = new System.Windows.Forms.Label();
            this.lbFaceCreationTime = new System.Windows.Forms.Label();
            this.lbFaceSimilar = new System.Windows.Forms.Label();
            this.lbFacePositoin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(187, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 150);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // lbFaceName
            // 
            this.lbFaceName.AutoSize = true;
            this.lbFaceName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFaceName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbFaceName.Location = new System.Drawing.Point(355, 12);
            this.lbFaceName.Name = "lbFaceName";
            this.lbFaceName.Size = new System.Drawing.Size(75, 19);
            this.lbFaceName.TabIndex = 2;
            this.lbFaceName.Text = "label1";
            // 
            // lbFaceGender
            // 
            this.lbFaceGender.AutoSize = true;
            this.lbFaceGender.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFaceGender.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbFaceGender.Location = new System.Drawing.Point(355, 77);
            this.lbFaceGender.Name = "lbFaceGender";
            this.lbFaceGender.Size = new System.Drawing.Size(56, 16);
            this.lbFaceGender.TabIndex = 3;
            this.lbFaceGender.Text = "label1";
            // 
            // lbFaceAge
            // 
            this.lbFaceAge.AutoSize = true;
            this.lbFaceAge.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFaceAge.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbFaceAge.Location = new System.Drawing.Point(355, 108);
            this.lbFaceAge.Name = "lbFaceAge";
            this.lbFaceAge.Size = new System.Drawing.Size(56, 16);
            this.lbFaceAge.TabIndex = 4;
            this.lbFaceAge.Text = "label1";
            // 
            // lbFaceCreationTime
            // 
            this.lbFaceCreationTime.AutoSize = true;
            this.lbFaceCreationTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFaceCreationTime.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbFaceCreationTime.Location = new System.Drawing.Point(355, 139);
            this.lbFaceCreationTime.Name = "lbFaceCreationTime";
            this.lbFaceCreationTime.Size = new System.Drawing.Size(56, 16);
            this.lbFaceCreationTime.TabIndex = 5;
            this.lbFaceCreationTime.Text = "label1";
            // 
            // lbFaceSimilar
            // 
            this.lbFaceSimilar.AutoSize = true;
            this.lbFaceSimilar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbFaceSimilar.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFaceSimilar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbFaceSimilar.Location = new System.Drawing.Point(135, 140);
            this.lbFaceSimilar.Name = "lbFaceSimilar";
            this.lbFaceSimilar.Size = new System.Drawing.Size(76, 22);
            this.lbFaceSimilar.TabIndex = 6;
            this.lbFaceSimilar.Text = "label1";
            // 
            // lbFacePositoin
            // 
            this.lbFacePositoin.AutoSize = true;
            this.lbFacePositoin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFacePositoin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbFacePositoin.Location = new System.Drawing.Point(355, 46);
            this.lbFacePositoin.Name = "lbFacePositoin";
            this.lbFacePositoin.Size = new System.Drawing.Size(56, 16);
            this.lbFacePositoin.TabIndex = 7;
            this.lbFacePositoin.Text = "label1";
            // 
            // ListFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.lbFacePositoin);
            this.Controls.Add(this.lbFaceSimilar);
            this.Controls.Add(this.lbFaceCreationTime);
            this.Controls.Add(this.lbFaceAge);
            this.Controls.Add(this.lbFaceGender);
            this.Controls.Add(this.lbFaceName);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ListFace";
            this.Size = new System.Drawing.Size(552, 175);
            this.Load += new System.EventHandler(this.ListFace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbFaceName;
        private System.Windows.Forms.Label lbFaceGender;
        private System.Windows.Forms.Label lbFaceAge;
        private System.Windows.Forms.Label lbFaceCreationTime;
        private System.Windows.Forms.Label lbFaceSimilar;
        private System.Windows.Forms.Label lbFacePositoin;
    }
}
