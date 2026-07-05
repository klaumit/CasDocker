using System;
using System.Linq;
using System.Windows.Forms;
using DevForge.Resources;
using DevForge.Lib.API;
using DevForge.Lib.Common;
using DevForge.Lib.Messages.Impl;
using Msg = DevForge.Lib.Messages.Message;
using DevForge.Lib.High;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable LocalizableElement

namespace DevForge
{
    public partial class DeviceForm : Form
    {
        private DeviceFoundArgs _args;

        public DeviceForm()
        {
            InitializeComponent();
        }

        public DeviceForm(DeviceFoundArgs args) : this()
        {
            _args = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = ResExt.GetStream("app.ico").ToIcon();
            picBox.Image = ResExt.GetStream("device.png").ToImage();
            var dev = _args.Device;
            Text = dev.Name + " - " + "DevForge";
            Apply(_args.Hello, _args.Stamp);
        }

        private void Apply(Hello hello, DateTime stamp)
        {
            if (hello != null)
            {
                var info = hello.AsInfo();
                if (info != null)
                {
                    chipLbl.Text = info.Chip + "";
                    areaLbl.Text = info.Area + "";
                    cpuLbl.Text = info.Cpu + "";
                    memLbl.Text = info.Mem + "";
                    commLbl.Text = (info.Comm + "").TrimStart('_');
                    appLbl.Text = info.App;
                    var ots = info.Ver.OsDate.ToString("u");
                    osDtLbl.Text = ots.Split(new[] { ' ' }, 2).First();
                    osVerLbl.Text = info.Ver.OsVer + "";
                }
            }
            var dts = stamp.ToString("u").TrimEnd('Z');
            dtLbl.Text = dts.Split(new[] { ' ' }, 2).Last();
        }
    }
}