
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
			this.showMapBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// rangeBox
			// 
			this.rangeBox.FormattingEnabled = true;
			this.rangeBox.Location = new System.Drawing.Point(16, 21);
			this.rangeBox.Name = "rangeBox";
			this.rangeBox.Size = new System.Drawing.Size(253, 329);
			this.rangeBox.TabIndex = 1;
			this.rangeBox.SelectedIndexChanged += new System.EventHandler(this.rangeBox_SelectedIndexChanged);
			// 
			// saveAsBtn
			// 
			this.saveAsBtn.Location = new System.Drawing.Point(757, 394);
			this.saveAsBtn.Name = "saveAsBtn";
			this.saveAsBtn.Size = new System.Drawing.Size(92, 23);
			this.saveAsBtn.TabIndex = 2;
			this.saveAsBtn.Text = "Save as binary";
			this.saveAsBtn.UseVisualStyleBackColor = true;
			this.saveAsBtn.Click += new System.EventHandler(this.saveAsBtn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rangeBox);
			this.groupBox1.Location = new System.Drawing.Point(665, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(286, 367);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ranges";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.hexScroll);
			this.groupBox2.Controls.Add(this.hexPanel);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(647, 545);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Data";
			// 
			// hexScroll
			// 
			this.hexScroll.Location = new System.Drawing.Point(612, 28);
			this.hexScroll.Maximum = 99;
			this.hexScroll.Name = "hexScroll";
			this.hexScroll.Size = new System.Drawing.Size(20, 498);
			this.hexScroll.TabIndex = 1;
			this.hexScroll.Value = 45;
			this.hexScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hexScroll_Scroll);
			// 
			// hexPanel
			// 
			this.hexPanel.BackColor = System.Drawing.Color.White;
			this.hexPanel.Location = new System.Drawing.Point(17, 28);
			this.hexPanel.Name = "hexPanel";
			this.hexPanel.Size = new System.Drawing.Size(592, 498);
			this.hexPanel.TabIndex = 0;
			// 
			// showMapBtn
			// 
			this.showMapBtn.Location = new System.Drawing.Point(757, 423);
			this.showMapBtn.Name = "showMapBtn";
			this.showMapBtn.Size = new System.Drawing.Size(92, 23);
			this.showMapBtn.TabIndex = 5;
			this.showMapBtn.Text = "Show minimap";
			this.showMapBtn.UseVisualStyleBackColor = true;
			this.showMapBtn.Click += new System.EventHandler(this.showMapBtn_Click);
			// 
			// HxdForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(965, 569);
			this.Controls.Add(this.showMapBtn);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.saveAsBtn);
			this.Name = "HxdForm";
			this.Text = "HxdForm";
			this.Load += new System.EventHandler(this.Form_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HxdForm_KeyUp);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.HxdForm_PreviewKeyDown);
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
		private System.Windows.Forms.Button showMapBtn;
	}
}