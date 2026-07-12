namespace DevForge
{
    partial class MainForm
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
			this.quitBtn = new System.Windows.Forms.Button();
			this.tryFind1Btn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tryFind2Btn = new System.Windows.Forms.Button();
			this.fakeBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// quitBtn
			// 
			this.quitBtn.Location = new System.Drawing.Point(156, 37);
			this.quitBtn.Name = "quitBtn";
			this.quitBtn.Size = new System.Drawing.Size(96, 33);
			this.quitBtn.TabIndex = 0;
			this.quitBtn.Text = "Exit the app";
			this.quitBtn.UseVisualStyleBackColor = true;
			this.quitBtn.Click += new System.EventHandler(this.quitBtn_Click);
			// 
			// tryFind1Btn
			// 
			this.tryFind1Btn.Location = new System.Drawing.Point(29, 30);
			this.tryFind1Btn.Name = "tryFind1Btn";
			this.tryFind1Btn.Size = new System.Drawing.Size(96, 21);
			this.tryFind1Btn.TabIndex = 1;
			this.tryFind1Btn.Text = "Try to find USB";
			this.tryFind1Btn.UseVisualStyleBackColor = true;
			this.tryFind1Btn.Click += new System.EventHandler(this.tryFind1Btn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.fakeBtn);
			this.groupBox1.Controls.Add(this.tryFind2Btn);
			this.groupBox1.Controls.Add(this.tryFind1Btn);
			this.groupBox1.Controls.Add(this.quitBtn);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(283, 103);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Monitor";
			// 
			// tryFind2Btn
			// 
			this.tryFind2Btn.Location = new System.Drawing.Point(29, 57);
			this.tryFind2Btn.Name = "tryFind2Btn";
			this.tryFind2Btn.Size = new System.Drawing.Size(96, 21);
			this.tryFind2Btn.TabIndex = 2;
			this.tryFind2Btn.Text = "Try to find Serial";
			this.tryFind2Btn.UseVisualStyleBackColor = true;
			this.tryFind2Btn.Click += new System.EventHandler(this.tryFind2Btn_Click);
			// 
			// fakeBtn
			// 
			this.fakeBtn.Location = new System.Drawing.Point(131, 74);
			this.fakeBtn.Name = "fakeBtn";
			this.fakeBtn.Size = new System.Drawing.Size(29, 23);
			this.fakeBtn.TabIndex = 3;
			this.fakeBtn.Text = "T";
			this.fakeBtn.UseVisualStyleBackColor = true;
			this.fakeBtn.Click += new System.EventHandler(this.fakeBtn_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(305, 128);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "DevForge";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button quitBtn;
        private System.Windows.Forms.Button tryFind1Btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button tryFind2Btn;
		private System.Windows.Forms.Button fakeBtn;
	}
}

