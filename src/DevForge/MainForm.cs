using System;
using System.Windows.Forms;
using DevForge.Lib.Setup;
using DevForge.Resources;
using U = DevForge.Tools.Utils;

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

            U.Main = this;
            FormClosing += U.OnExiting;
            _hub.Value.NewDevice += U.OnNewDevice;
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

		private void fakeBtn_Click(object sender, EventArgs e)
		{
            _hub.Value.StartFake();
        }
    }
}