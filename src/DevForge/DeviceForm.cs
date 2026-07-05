using System;
using System.Windows.Forms;
using DevForge.Resources;
using DevForge.Lib.API;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable LocalizableElement

namespace DevForge
{
    public partial class DeviceForm : Form
    {
        private ICommDevice _dev;

        public DeviceForm()
        {
            InitializeComponent();
        }

        public DeviceForm(ICommDevice dev)
            : this()
        {
            _dev = dev;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = ResExt.GetStream("app.ico").ToIcon();
            Text = _dev.Name + " - " + "DevForge";
        }
    }
}