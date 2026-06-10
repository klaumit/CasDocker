namespace Pva2cpa
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
            this.exitBtn = new System.Windows.Forms.Button();
            this.dropBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dropBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(187, 278);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 0;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // dropBox
            // 
            this.dropBox.Controls.Add(this.label1);
            this.dropBox.Location = new System.Drawing.Point(12, 12);
            this.dropBox.Name = "dropBox";
            this.dropBox.Size = new System.Drawing.Size(250, 250);
            this.dropBox.TabIndex = 1;
            this.dropBox.TabStop = false;
            this.dropBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dropBox_DragDrop);
            this.dropBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.dropBox_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drop your PVA here!";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 315);
            this.Controls.Add(this.dropBox);
            this.Controls.Add(this.exitBtn);
            this.Name = "MainForm";
            this.Text = "PVA to CPA Converter";
            this.dropBox.ResumeLayout(false);
            this.dropBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.GroupBox dropBox;
        private System.Windows.Forms.Label label1;
    }
}