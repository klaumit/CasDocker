using System;
using System.Windows.Forms;
using DevForge.Resources;

namespace DevForge
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Quit()
        {
            Environment.Exit(0);
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = ResExt.ToIcon(ResExt.GetStream("app.ico"));
            imgBox.Image = ResExt.ToImage(ResExt.GetStream("device.png"));

            Utils.X1();
            Utils.X2();
        }
    }
}