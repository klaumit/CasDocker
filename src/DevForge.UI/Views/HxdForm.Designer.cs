
namespace DevForge.UI.Views
{
	partial class HxdForm
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
			this.rangeBox = new System.Windows.Forms.ListBox();
			this.saveAsBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rangeBox
			// 
			this.rangeBox.FormattingEnabled = true;
			this.rangeBox.Location = new System.Drawing.Point(108, 110);
			this.rangeBox.Name = "rangeBox";
			this.rangeBox.Size = new System.Drawing.Size(393, 420);
			this.rangeBox.TabIndex = 1;
			// 
			// saveAsBtn
			// 
			this.saveAsBtn.Location = new System.Drawing.Point(613, 456);
			this.saveAsBtn.Name = "saveAsBtn";
			this.saveAsBtn.Size = new System.Drawing.Size(92, 23);
			this.saveAsBtn.TabIndex = 2;
			this.saveAsBtn.Text = "Save as";
			this.saveAsBtn.UseVisualStyleBackColor = true;
			this.saveAsBtn.Click += new System.EventHandler(this.saveAsBtn_Click);
			// 
			// HxdForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(801, 650);
			this.Controls.Add(this.saveAsBtn);
			this.Controls.Add(this.rangeBox);
			this.Name = "HxdForm";
			this.Text = "HxdForm";
			this.Load += new System.EventHandler(this.Form_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListBox rangeBox;
		private System.Windows.Forms.Button saveAsBtn;
	}
}