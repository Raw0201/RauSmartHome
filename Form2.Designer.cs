namespace RauSmartHome
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.pbxCameraImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCameraImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxCameraImage
            // 
            this.pbxCameraImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxCameraImage.Location = new System.Drawing.Point(0, 0);
            this.pbxCameraImage.Name = "pbxCameraImage";
            this.pbxCameraImage.Size = new System.Drawing.Size(784, 561);
            this.pbxCameraImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxCameraImage.TabIndex = 0;
            this.pbxCameraImage.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pbxCameraImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor de cámara";
            ((System.ComponentModel.ISupportInitialize)(this.pbxCameraImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxCameraImage;
    }
}