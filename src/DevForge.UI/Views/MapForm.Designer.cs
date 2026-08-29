
namespace DevForge.UI.Views
{
	partial class MapForm
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
			this.mapImgBox = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.mapImgBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mapImgBox
			// 
			this.mapImgBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mapImgBox.Location = new System.Drawing.Point(12, 19);
			this.mapImgBox.Name = "mapImgBox";
			this.mapImgBox.Size = new System.Drawing.Size(512, 512);
			this.mapImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mapImgBox.TabIndex = 0;
			this.mapImgBox.TabStop = false;
			this.mapImgBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapImgBox_MouseUp);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mapImgBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(537, 544);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Mini map";
			// 
			// MapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 544);
			this.Controls.Add(this.groupBox1);
			this.Name = "MapForm";
			this.Text = "MapForm";
			this.Load += new System.EventHandler(this.MapForm_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MapForm_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.mapImgBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.PictureBox mapImgBox;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}