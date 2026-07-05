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

            FormClosing += Utils.OnExiting;
            _hub.Value.NewDevice += Utils.OnNewDevice;
            tryFind1Btn_Click(sender, e);
            tryFind2Btn_Click(sender, e);
        }

        private void tryFind1Btn_Click(object sender, EventArgs e)
        {
            _hub.Value.StartModern();
        }

        private void tryFind2Btn_Click(object sender, EventArgs e)
        {
            _hub.Value.StartLegacy();
        }
    }
}