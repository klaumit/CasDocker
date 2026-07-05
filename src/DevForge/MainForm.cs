using System;
using System.Windows.Forms;
using DevForge.Lib.Common;
using DevForge.Resources;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable LocalizableElement

namespace DevForge
{
    public partial class MainForm : Form
    {
        private readonly Lazy<DeviceHub> _hub = new Lazy<DeviceHub>();

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
            Icon = ResExt.GetStream("app.ico").ToIcon();
            imgBox.Image = ResExt.GetStream("device.png").ToImage();

            _hub.Value.NewDevice += OnNewDevice;
            _hub.Value.StartOnce();
        }

        private void OnNewDevice(object s, DeviceFoundArgs e)
        {
            Console.WriteLine(" " + s + " = " + e);
        }
    }
}