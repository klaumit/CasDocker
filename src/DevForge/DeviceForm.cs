using System;
using System.Windows.Forms;
using DevForge.Resources;
using DevForge.Lib.Common;
using DevForge.Lib.Messages.Impl;

// ReSharper disable UseCollectionExpression
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
                    chipLbl.Text = info.Chip.GetEnumStr();
                    areaLbl.Text = info.Area.GetEnumStr();
                    cpuLbl.Text = info.Cpu.GetEnumStr();
                    memLbl.Text = info.Mem.GetEnumStr();
                    commLbl.Text = info.Comm.GetEnumStr();
                    appLbl.Text = info.App;
                    osDtLbl.Text = info.Ver.OsDate.GetDateStr();
                    osVerLbl.Text = info.Ver.OsVer.GetVerStr();
                }
            }
            dtLbl.Text = stamp.GetTimeStr();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            SendClose();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SendClose()
        {
            var dev = _args.Device;
            dev.Send(new Quit("Please stop. Now."));
        }

        private void SendLive()
        {
            var dev = _args.Device;
            dev.Send(new Alive("1E"));
        }

        private void keepLiveBtn_Click(object sender, EventArgs e)
        {
            SendLive();
        }

		private void todoBtn_Click(object sender, EventArgs e)
		{
            var dev = _args.Device;
            var args = string.Join("|",
                new[] { "0110", "03", "6000", "0000", "32", "" }
            );
            dev.Send(new Read(args));
        }

		private void todaBtn_Click(object sender, EventArgs e)
		{
            var dev = _args.Device;
            var args = string.Join("|",
                new[] { "0000", "00", "8C00", "0000", "32", "" }
            );
            dev.Send(new Read(args));
        }
    }
}