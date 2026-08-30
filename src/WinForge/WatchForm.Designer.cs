
namespace WinForge
{
	partial class WatchForm
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
			this.components = new System.ComponentModel.Container();
			this.closeBtn = new System.Windows.Forms.Button();
			this.sim86Tb = new System.Windows.Forms.TextBox();
			this.simShTb = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.clocker = new System.Windows.Forms.Timer(this.components);
			this.delayNd = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.start86Btn = new System.Windows.Forms.Button();
			this.startShBtn = new System.Windows.Forms.Button();
			this.stop86Btn = new System.Windows.Forms.Button();
			this.stopShBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.delayNd)).BeginInit();
			this.SuspendLayout();
			// 
			// closeBtn
			// 
			this.closeBtn.Location = new System.Drawing.Point(351, 270);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 25);
			this.closeBtn.TabIndex = 0;
			this.closeBtn.Text = "Close";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// sim86Tb
			// 
			this.sim86Tb.Location = new System.Drawing.Point(75, 28);
			this.sim86Tb.Name = "sim86Tb";
			this.sim86Tb.Size = new System.Drawing.Size(55, 20);
			this.sim86Tb.TabIndex = 1;
			// 
			// simShTb
			// 
			this.simShTb.Location = new System.Drawing.Point(207, 28);
			this.simShTb.Name = "simShTb";
			this.simShTb.Size = new System.Drawing.Size(55, 20);
			this.simShTb.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Sim86";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(155, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 14);
			this.label2.TabIndex = 4;
			this.label2.Text = "SimSH";
			// 
			// clocker
			// 
			this.clocker.Interval = 3000;
			this.clocker.Tick += new System.EventHandler(this.clocker_Tick);
			// 
			// delayNd
			// 
			this.delayNd.Location = new System.Drawing.Point(343, 29);
			this.delayNd.Maximum = new decimal(new int[] {
									5000,
									0,
									0,
									0});
			this.delayNd.Name = "delayNd";
			this.delayNd.Size = new System.Drawing.Size(55, 20);
			this.delayNd.TabIndex = 5;
			this.delayNd.ValueChanged += new System.EventHandler(this.delayNd_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(292, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Delay";
			// 
			// start86Btn
			// 
			this.start86Btn.Location = new System.Drawing.Point(75, 54);
			this.start86Btn.Name = "start86Btn";
			this.start86Btn.Size = new System.Drawing.Size(55, 23);
			this.start86Btn.TabIndex = 7;
			this.start86Btn.Text = "Start";
			this.start86Btn.UseVisualStyleBackColor = true;
			this.start86Btn.Click += new System.EventHandler(this.Start86BtnClick);
			// 
			// startShBtn
			// 
			this.startShBtn.Location = new System.Drawing.Point(207, 54);
			this.startShBtn.Name = "startShBtn";
			this.startShBtn.Size = new System.Drawing.Size(55, 23);
			this.startShBtn.TabIndex = 8;
			this.startShBtn.Text = "Start";
			this.startShBtn.UseVisualStyleBackColor = true;
			this.startShBtn.Click += new System.EventHandler(this.StartShBtnClick);
			// 
			// stop86Btn
			// 
			this.stop86Btn.Location = new System.Drawing.Point(75, 83);
			this.stop86Btn.Name = "stop86Btn";
			this.stop86Btn.Size = new System.Drawing.Size(55, 23);
			this.stop86Btn.TabIndex = 9;
			this.stop86Btn.Text = "Stop";
			this.stop86Btn.UseVisualStyleBackColor = true;
			this.stop86Btn.Click += new System.EventHandler(this.Stop86BtnClick);
			// 
			// stopShBtn
			// 
			this.stopShBtn.Location = new System.Drawing.Point(207, 83);
			this.stopShBtn.Name = "stopShBtn";
			this.stopShBtn.Size = new System.Drawing.Size(55, 23);
			this.stopShBtn.TabIndex = 10;
			this.stopShBtn.Text = "Stop";
			this.stopShBtn.UseVisualStyleBackColor = true;
			this.stopShBtn.Click += new System.EventHandler(this.StopShBtnClick);
			// 
			// WatchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 308);
			this.Controls.Add(this.stopShBtn);
			this.Controls.Add(this.stop86Btn);
			this.Controls.Add(this.startShBtn);
			this.Controls.Add(this.start86Btn);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.delayNd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.simShTb);
			this.Controls.Add(this.sim86Tb);
			this.Controls.Add(this.closeBtn);
			this.Name = "WatchForm";
			this.Text = "WatchForm";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.delayNd)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button stopShBtn;
		private System.Windows.Forms.Button stop86Btn;
		private System.Windows.Forms.Button startShBtn;
		private System.Windows.Forms.Button start86Btn;

		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.TextBox sim86Tb;
		private System.Windows.Forms.TextBox simShTb;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Timer clocker;
		private System.Windows.Forms.NumericUpDown delayNd;
		private System.Windows.Forms.Label label3;
	}
}

