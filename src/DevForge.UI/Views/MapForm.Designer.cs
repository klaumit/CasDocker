
namespace DevForge.UI.Views
{
	partial class MapForm
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
			this.mapImgBox = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.mapImgBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mapImgBox
			// 
			this.mapImgBox.Location = new System.Drawing.Point(12, 19);
			this.mapImgBox.Name = "mapImgBox";
			this.mapImgBox.Size = new System.Drawing.Size(512, 512);
			this.mapImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mapImgBox.TabIndex = 0;
			this.mapImgBox.TabStop = false;
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
			((System.ComponentModel.ISupportInitialize)(this.mapImgBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox mapImgBox;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}