namespace DevForge
{
    partial class DeviceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.picBox = new System.Windows.Forms.PictureBox();
            this.infoBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(12, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(87, 99);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(105, 12);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(405, 99);
            this.infoBox.TabIndex = 1;
            this.infoBox.TabStop = false;
            this.infoBox.Text = "Info";
            // 
            // DeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 339);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.picBox);
            this.Name = "DeviceForm";
            this.Text = "DeviceForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.GroupBox infoBox;
    }
}