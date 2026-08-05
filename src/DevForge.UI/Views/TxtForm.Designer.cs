
namespace DevForge.UI.Views
{
	partial class TxtForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.findLstBx = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.findLstBx);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(247, 488);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Findings";
			// 
			// findLstBx
			// 
			this.findLstBx.FormattingEnabled = true;
			this.findLstBx.Location = new System.Drawing.Point(12, 19);
			this.findLstBx.Name = "findLstBx";
			this.findLstBx.Size = new System.Drawing.Size(223, 459);
			this.findLstBx.TabIndex = 0;
			this.findLstBx.SelectedIndexChanged += new System.EventHandler(this.findLstBx_SelectedIndexChanged);
			// 
			// TxtForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(247, 488);
			this.Controls.Add(this.groupBox1);
			this.Name = "TxtForm";
			this.Text = "TxtForm";
			this.Load += new System.EventHandler(this.TxtForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox findLstBx;
	}
}
