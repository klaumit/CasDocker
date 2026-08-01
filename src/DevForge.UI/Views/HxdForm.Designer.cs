
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.hexScroll = new System.Windows.Forms.VScrollBar();
			this.hexPanel = new DevForge.UI.Views.HxdPanel();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// rangeBox
			// 
			this.rangeBox.FormattingEnabled = true;
			this.rangeBox.Location = new System.Drawing.Point(16, 19);
			this.rangeBox.Name = "rangeBox";
			this.rangeBox.Size = new System.Drawing.Size(237, 316);
			this.rangeBox.TabIndex = 1;
			// 
			// saveAsBtn
			// 
			this.saveAsBtn.Location = new System.Drawing.Point(73, 381);
			this.saveAsBtn.Name = "saveAsBtn";
			this.saveAsBtn.Size = new System.Drawing.Size(92, 23);
			this.saveAsBtn.TabIndex = 2;
			this.saveAsBtn.Text = "Save as";
			this.saveAsBtn.UseVisualStyleBackColor = true;
			this.saveAsBtn.Click += new System.EventHandler(this.saveAsBtn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rangeBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(268, 351);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ranges";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.hexScroll);
			this.groupBox2.Controls.Add(this.hexPanel);
			this.groupBox2.Location = new System.Drawing.Point(286, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(647, 524);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Data";
			// 
			// hexScroll
			// 
			this.hexScroll.Location = new System.Drawing.Point(612, 28);
			this.hexScroll.Maximum = 99;
			this.hexScroll.Name = "hexScroll";
			this.hexScroll.Size = new System.Drawing.Size(20, 477);
			this.hexScroll.TabIndex = 1;
			this.hexScroll.Value = 45;
			this.hexScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hexScroll_Scroll);
			// 
			// hexPanel
			// 
			this.hexPanel.BackColor = System.Drawing.Color.White;
			this.hexPanel.Location = new System.Drawing.Point(17, 28);
			this.hexPanel.Name = "hexPanel";
			this.hexPanel.Size = new System.Drawing.Size(592, 477);
			this.hexPanel.TabIndex = 0;
			// 
			// HxdForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(945, 548);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.saveAsBtn);
			this.Name = "HxdForm";
			this.Text = "HxdForm";
			this.Load += new System.EventHandler(this.Form_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListBox rangeBox;
		private System.Windows.Forms.Button saveAsBtn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private DevForge.UI.Views.HxdPanel hexPanel;
		private System.Windows.Forms.VScrollBar hexScroll;
	}
}