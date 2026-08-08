namespace DevForge.Views
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
			this.testReadBtn = new System.Windows.Forms.Button();
			this.statusSrp = new System.Windows.Forms.StatusStrip();
			this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
			this.msgLenDw = new System.Windows.Forms.NumericUpDown();
			this.backupBtn = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.delayDown = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.gotLbl = new System.Windows.Forms.Label();
			this.stillLbl = new System.Windows.Forms.Label();
			this.gotStiLbl = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.cstGrpBox = new System.Windows.Forms.GroupBox();
			this.customBtn = new System.Windows.Forms.Button();
			this.toBox = new System.Windows.Forms.TextBox();
			this.fromBox = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.purgeBtn = new System.Windows.Forms.Button();
			this.waitUpd = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.jumpBox = new System.Windows.Forms.TextBox();
			this.jumpToBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
			this.infoBox.SuspendLayout();
			this.statusSrp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.msgLenDw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.delayDown)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.cstGrpBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.waitUpd)).BeginInit();
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
			this.closeBtn.Location = new System.Drawing.Point(421, 182);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 23);
			this.closeBtn.TabIndex = 2;
			this.closeBtn.Text = "Send quit";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// keepLiveBtn
			// 
			this.keepLiveBtn.Location = new System.Drawing.Point(421, 153);
			this.keepLiveBtn.Name = "keepLiveBtn";
			this.keepLiveBtn.Size = new System.Drawing.Size(75, 23);
			this.keepLiveBtn.TabIndex = 3;
			this.keepLiveBtn.Text = "Keep alive";
			this.keepLiveBtn.UseVisualStyleBackColor = true;
			this.keepLiveBtn.Click += new System.EventHandler(this.keepLiveBtn_Click);
			// 
			// testReadBtn
			// 
			this.testReadBtn.Location = new System.Drawing.Point(24, 128);
			this.testReadBtn.Name = "testReadBtn";
			this.testReadBtn.Size = new System.Drawing.Size(75, 23);
			this.testReadBtn.TabIndex = 4;
			this.testReadBtn.Text = "Test read";
			this.testReadBtn.UseVisualStyleBackColor = true;
			this.testReadBtn.Click += new System.EventHandler(this.testReadBtn_Click);
			// 
			// statusSrp
			// 
			this.statusSrp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
			this.statusSrp.Location = new System.Drawing.Point(0, 331);
			this.statusSrp.Name = "statusSrp";
			this.statusSrp.Size = new System.Drawing.Size(508, 22);
			this.statusSrp.TabIndex = 6;
			this.statusSrp.Text = "statusStrip1";
			// 
			// statusLbl
			// 
			this.statusLbl.Name = "statusLbl";
			this.statusLbl.Size = new System.Drawing.Size(22, 17);
			this.statusLbl.Text = "---";
			// 
			// msgLenDw
			// 
			this.msgLenDw.Location = new System.Drawing.Point(30, 161);
			this.msgLenDw.Maximum = new decimal(new int[] {
            66560,
            0,
            0,
            0});
			this.msgLenDw.Name = "msgLenDw";
			this.msgLenDw.Size = new System.Drawing.Size(66, 20);
			this.msgLenDw.TabIndex = 7;
			this.msgLenDw.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
			// 
			// backupBtn
			// 
			this.backupBtn.Location = new System.Drawing.Point(176, 19);
			this.backupBtn.Name = "backupBtn";
			this.backupBtn.Size = new System.Drawing.Size(75, 23);
			this.backupBtn.TabIndex = 8;
			this.backupBtn.Text = "Start";
			this.backupBtn.UseVisualStyleBackColor = true;
			this.backupBtn.Click += new System.EventHandler(this.backupBtn_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(31, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Delay (ms):";
			// 
			// delayDown
			// 
			this.delayDown.Location = new System.Drawing.Point(96, 19);
			this.delayDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.delayDown.Name = "delayDown";
			this.delayDown.Size = new System.Drawing.Size(68, 20);
			this.delayDown.TabIndex = 10;
			this.delayDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(31, 52);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(27, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Got:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(105, 52);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(26, 13);
			this.label11.TabIndex = 12;
			this.label11.Text = "Still:";
			// 
			// gotLbl
			// 
			this.gotLbl.AutoSize = true;
			this.gotLbl.Location = new System.Drawing.Point(31, 74);
			this.gotLbl.Name = "gotLbl";
			this.gotLbl.Size = new System.Drawing.Size(16, 13);
			this.gotLbl.TabIndex = 13;
			this.gotLbl.Text = "---";
			// 
			// stillLbl
			// 
			this.stillLbl.AutoSize = true;
			this.stillLbl.Location = new System.Drawing.Point(105, 74);
			this.stillLbl.Name = "stillLbl";
			this.stillLbl.Size = new System.Drawing.Size(16, 13);
			this.stillLbl.TabIndex = 14;
			this.stillLbl.Text = "---";
			// 
			// gotStiLbl
			// 
			this.gotStiLbl.AutoSize = true;
			this.gotStiLbl.Location = new System.Drawing.Point(176, 73);
			this.gotStiLbl.Name = "gotStiLbl";
			this.gotStiLbl.Size = new System.Drawing.Size(16, 13);
			this.gotStiLbl.TabIndex = 16;
			this.gotStiLbl.Text = "---";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.delayDown);
			this.groupBox1.Controls.Add(this.backupBtn);
			this.groupBox1.Controls.Add(this.gotStiLbl);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.stillLbl);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.gotLbl);
			this.groupBox1.Location = new System.Drawing.Point(122, 117);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(270, 104);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Backup";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(193, 52);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(32, 13);
			this.label13.TabIndex = 17;
			this.label13.Text = "Num:";
			// 
			// cstGrpBox
			// 
			this.cstGrpBox.Controls.Add(this.customBtn);
			this.cstGrpBox.Controls.Add(this.toBox);
			this.cstGrpBox.Controls.Add(this.fromBox);
			this.cstGrpBox.Controls.Add(this.label15);
			this.cstGrpBox.Controls.Add(this.label14);
			this.cstGrpBox.Location = new System.Drawing.Point(122, 227);
			this.cstGrpBox.Name = "cstGrpBox";
			this.cstGrpBox.Size = new System.Drawing.Size(270, 91);
			this.cstGrpBox.TabIndex = 18;
			this.cstGrpBox.TabStop = false;
			this.cstGrpBox.Text = "Custom";
			// 
			// customBtn
			// 
			this.customBtn.Location = new System.Drawing.Point(161, 22);
			this.customBtn.Name = "customBtn";
			this.customBtn.Size = new System.Drawing.Size(90, 23);
			this.customBtn.TabIndex = 16;
			this.customBtn.Text = "---";
			this.customBtn.UseVisualStyleBackColor = true;
			this.customBtn.Click += new System.EventHandler(this.CustomBtnClick);
			// 
			// toBox
			// 
			this.toBox.Location = new System.Drawing.Point(60, 53);
			this.toBox.Name = "toBox";
			this.toBox.Size = new System.Drawing.Size(80, 20);
			this.toBox.TabIndex = 15;
			this.toBox.Text = "0xA1000000";
			this.toBox.TextChanged += new System.EventHandler(this.ToBoxTextChanged);
			// 
			// fromBox
			// 
			this.fromBox.Location = new System.Drawing.Point(60, 24);
			this.fromBox.Name = "fromBox";
			this.fromBox.Size = new System.Drawing.Size(80, 20);
			this.fromBox.TabIndex = 14;
			this.fromBox.Text = "0xA0000000";
			this.fromBox.TextChanged += new System.EventHandler(this.FromBoxTextChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(23, 56);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(16, 13);
			this.label15.TabIndex = 13;
			this.label15.Text = "to";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(23, 27);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(27, 13);
			this.label14.TabIndex = 12;
			this.label14.Text = "from";
			// 
			// purgeBtn
			// 
			this.purgeBtn.Location = new System.Drawing.Point(421, 244);
			this.purgeBtn.Name = "purgeBtn";
			this.purgeBtn.Size = new System.Drawing.Size(75, 23);
			this.purgeBtn.TabIndex = 19;
			this.purgeBtn.Text = "Purge";
			this.purgeBtn.UseVisualStyleBackColor = true;
			this.purgeBtn.Click += new System.EventHandler(this.PurgeBtnClick);
			// 
			// waitUpd
			// 
			this.waitUpd.Location = new System.Drawing.Point(30, 223);
			this.waitUpd.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.waitUpd.Name = "waitUpd";
			this.waitUpd.Size = new System.Drawing.Size(61, 20);
			this.waitUpd.TabIndex = 20;
			this.waitUpd.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(27, 200);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(55, 13);
			this.label16.TabIndex = 21;
			this.label16.Text = "Alive (ms):";
			// 
			// jumpBox
			// 
			this.jumpBox.Location = new System.Drawing.Point(15, 299);
			this.jumpBox.Name = "jumpBox";
			this.jumpBox.Size = new System.Drawing.Size(80, 20);
			this.jumpBox.TabIndex = 23;
			this.jumpBox.Text = "0x8C06ED4C";
			// 
			// jumpToBtn
			// 
			this.jumpToBtn.Location = new System.Drawing.Point(15, 272);
			this.jumpToBtn.Name = "jumpToBtn";
			this.jumpToBtn.Size = new System.Drawing.Size(53, 21);
			this.jumpToBtn.TabIndex = 24;
			this.jumpToBtn.Text = "Jump to";
			this.jumpToBtn.UseVisualStyleBackColor = true;
			this.jumpToBtn.Click += new System.EventHandler(this.jumpToBtn_Click);
			// 
			// DeviceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(508, 353);
			this.Controls.Add(this.jumpToBtn);
			this.Controls.Add(this.jumpBox);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.waitUpd);
			this.Controls.Add(this.purgeBtn);
			this.Controls.Add(this.cstGrpBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.msgLenDw);
			this.Controls.Add(this.statusSrp);
			this.Controls.Add(this.testReadBtn);
			this.Controls.Add(this.keepLiveBtn);
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.infoBox);
			this.Controls.Add(this.picBox);
			this.Name = "DeviceForm";
			this.Text = "DeviceForm";
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
			this.infoBox.ResumeLayout(false);
			this.infoBox.PerformLayout();
			this.statusSrp.ResumeLayout(false);
			this.statusSrp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.msgLenDw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.delayDown)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.cstGrpBox.ResumeLayout(false);
			this.cstGrpBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.waitUpd)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        private System.Windows.Forms.Button purgeBtn;
        private System.Windows.Forms.Button customBtn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox fromBox;
        private System.Windows.Forms.TextBox toBox;
        private System.Windows.Forms.GroupBox cstGrpBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label gotStiLbl;

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
		private System.Windows.Forms.Button testReadBtn;
		private System.Windows.Forms.StatusStrip statusSrp;
		private System.Windows.Forms.ToolStripStatusLabel statusLbl;
		private System.Windows.Forms.NumericUpDown msgLenDw;
		private System.Windows.Forms.Button backupBtn;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown delayDown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label gotLbl;
		private System.Windows.Forms.Label stillLbl;
		private System.Windows.Forms.NumericUpDown waitUpd;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox jumpBox;
		private System.Windows.Forms.Button jumpToBtn;
	}
}