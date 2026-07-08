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
			this.osVerLbl = new System.Windows.Forms.Label();
			this.osDtLbl = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.appLbl = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.memLbl = new System.Windows.Forms.Label();
			this.cpuLbl = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.commLbl = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.areaLbl = new System.Windows.Forms.Label();
			this.chipLbl = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dtLbl = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.closeBtn = new System.Windows.Forms.Button();
			this.keepLiveBtn = new System.Windows.Forms.Button();
			this.todoBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
			this.infoBox.SuspendLayout();
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
			this.infoBox.Controls.Add(this.osVerLbl);
			this.infoBox.Controls.Add(this.osDtLbl);
			this.infoBox.Controls.Add(this.label8);
			this.infoBox.Controls.Add(this.label10);
			this.infoBox.Controls.Add(this.appLbl);
			this.infoBox.Controls.Add(this.label12);
			this.infoBox.Controls.Add(this.memLbl);
			this.infoBox.Controls.Add(this.cpuLbl);
			this.infoBox.Controls.Add(this.label6);
			this.infoBox.Controls.Add(this.label7);
			this.infoBox.Controls.Add(this.commLbl);
			this.infoBox.Controls.Add(this.label9);
			this.infoBox.Controls.Add(this.areaLbl);
			this.infoBox.Controls.Add(this.chipLbl);
			this.infoBox.Controls.Add(this.label4);
			this.infoBox.Controls.Add(this.label2);
			this.infoBox.Controls.Add(this.dtLbl);
			this.infoBox.Controls.Add(this.label1);
			this.infoBox.Location = new System.Drawing.Point(105, 12);
			this.infoBox.Name = "infoBox";
			this.infoBox.Size = new System.Drawing.Size(391, 99);
			this.infoBox.TabIndex = 1;
			this.infoBox.TabStop = false;
			this.infoBox.Text = "Info";
			// 
			// osVerLbl
			// 
			this.osVerLbl.AutoSize = true;
			this.osVerLbl.Location = new System.Drawing.Point(312, 70);
			this.osVerLbl.Name = "osVerLbl";
			this.osVerLbl.Size = new System.Drawing.Size(16, 13);
			this.osVerLbl.TabIndex = 19;
			this.osVerLbl.Text = "---";
			// 
			// osDtLbl
			// 
			this.osDtLbl.AutoSize = true;
			this.osDtLbl.Location = new System.Drawing.Point(312, 48);
			this.osDtLbl.Name = "osDtLbl";
			this.osDtLbl.Size = new System.Drawing.Size(16, 13);
			this.osDtLbl.TabIndex = 17;
			this.osDtLbl.Text = "---";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(258, 70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(43, 13);
			this.label8.TabIndex = 18;
			this.label8.Text = "OS ver:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(258, 48);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(37, 13);
			this.label10.TabIndex = 16;
			this.label10.Text = "OS dt:";
			// 
			// appLbl
			// 
			this.appLbl.AutoSize = true;
			this.appLbl.Location = new System.Drawing.Point(312, 25);
			this.appLbl.Name = "appLbl";
			this.appLbl.Size = new System.Drawing.Size(16, 13);
			this.appLbl.TabIndex = 15;
			this.appLbl.Text = "---";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(258, 25);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(29, 13);
			this.label12.TabIndex = 14;
			this.label12.Text = "App:";
			// 
			// memLbl
			// 
			this.memLbl.AutoSize = true;
			this.memLbl.Location = new System.Drawing.Point(193, 70);
			this.memLbl.Name = "memLbl";
			this.memLbl.Size = new System.Drawing.Size(16, 13);
			this.memLbl.TabIndex = 13;
			this.memLbl.Text = "---";
			// 
			// cpuLbl
			// 
			this.cpuLbl.AutoSize = true;
			this.cpuLbl.Location = new System.Drawing.Point(193, 48);
			this.cpuLbl.Name = "cpuLbl";
			this.cpuLbl.Size = new System.Drawing.Size(16, 13);
			this.cpuLbl.TabIndex = 11;
			this.cpuLbl.Text = "---";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(139, 70);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(33, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Mem:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(139, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "CPU:";
			// 
			// commLbl
			// 
			this.commLbl.AutoSize = true;
			this.commLbl.Location = new System.Drawing.Point(193, 25);
			this.commLbl.Name = "commLbl";
			this.commLbl.Size = new System.Drawing.Size(16, 13);
			this.commLbl.TabIndex = 9;
			this.commLbl.Text = "---";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(139, 25);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(39, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "Comm:";
			// 
			// areaLbl
			// 
			this.areaLbl.AutoSize = true;
			this.areaLbl.Location = new System.Drawing.Point(70, 70);
			this.areaLbl.Name = "areaLbl";
			this.areaLbl.Size = new System.Drawing.Size(16, 13);
			this.areaLbl.TabIndex = 7;
			this.areaLbl.Text = "---";
			// 
			// chipLbl
			// 
			this.chipLbl.AutoSize = true;
			this.chipLbl.Location = new System.Drawing.Point(70, 48);
			this.chipLbl.Name = "chipLbl";
			this.chipLbl.Size = new System.Drawing.Size(16, 13);
			this.chipLbl.TabIndex = 5;
			this.chipLbl.Text = "---";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Area:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Chipset:";
			// 
			// dtLbl
			// 
			this.dtLbl.AutoSize = true;
			this.dtLbl.Location = new System.Drawing.Point(70, 25);
			this.dtLbl.Name = "dtLbl";
			this.dtLbl.Size = new System.Drawing.Size(16, 13);
			this.dtLbl.TabIndex = 3;
			this.dtLbl.Text = "---";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Seen at:";
			// 
			// closeBtn
			// 
			this.closeBtn.Location = new System.Drawing.Point(317, 143);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 23);
			this.closeBtn.TabIndex = 2;
			this.closeBtn.Text = "Send quit";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// keepLiveBtn
			// 
			this.keepLiveBtn.Location = new System.Drawing.Point(157, 143);
			this.keepLiveBtn.Name = "keepLiveBtn";
			this.keepLiveBtn.Size = new System.Drawing.Size(75, 23);
			this.keepLiveBtn.TabIndex = 3;
			this.keepLiveBtn.Text = "Keep alive";
			this.keepLiveBtn.UseVisualStyleBackColor = true;
			this.keepLiveBtn.Click += new System.EventHandler(this.keepLiveBtn_Click);
			// 
			// todoBtn
			// 
			this.todoBtn.Location = new System.Drawing.Point(24, 155);
			this.todoBtn.Name = "todoBtn";
			this.todoBtn.Size = new System.Drawing.Size(75, 23);
			this.todoBtn.TabIndex = 4;
			this.todoBtn.Text = "TODO";
			this.todoBtn.UseVisualStyleBackColor = true;
			this.todoBtn.Click += new System.EventHandler(this.todoBtn_Click);
			// 
			// DeviceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(508, 203);
			this.Controls.Add(this.todoBtn);
			this.Controls.Add(this.keepLiveBtn);
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.infoBox);
			this.Controls.Add(this.picBox);
			this.Name = "DeviceForm";
			this.Text = "DeviceForm";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
			this.infoBox.ResumeLayout(false);
			this.infoBox.PerformLayout();
			this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.GroupBox infoBox;
        private System.Windows.Forms.Label dtLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label chipLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label areaLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label memLbl;
        private System.Windows.Forms.Label cpuLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label commLbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label osVerLbl;
        private System.Windows.Forms.Label osDtLbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label appLbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button keepLiveBtn;
		private System.Windows.Forms.Button todoBtn;
	}
}