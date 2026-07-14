using System;
using System.Linq;
using System.Windows.Forms;
using DevForge.Resources;
using DevForge.Lib.Common;
using DevForge.Lib.Messages.Impl;
using DevForge.Lib.High;
using DevForge.Lib.Tools;
using DevForge.Lib.Ponder;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using System.Threading;

// ReSharper disable UseCollectionExpression
// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable LocalizableElement

namespace DevForge
{
    public partial class DeviceForm : Form
    {
        private DeviceFoundArgs _args;
        private PvInfo _info;
        private JsonLines _log;
        private StreamWriter _got;
        private Dictionary<uint, Read> _reads;
        private long _packGot;
        private long _packStill;
        private int _packSize = 64;

        public DeviceForm()
        {
            InitializeComponent();
        }

        public DeviceForm(DeviceFoundArgs args) : this()
        {
            _args = args;
            var dev = args.Device;
            dev.NewMessage += Device_Message;
        }

        private void Device_Message(object sender, GotMessageArgs e)
        {
            var ts = e.Stamp.ToString("u").TrimEnd('Z');
            statusLbl.Text = "[" + ts + "] (" + e.Message.Kind + ") " + e.Message.Length + " bytes";
            _log.Write(e.Message);

            if (e.Message is Read r && _reads != null)
            {
                var buff = r.AsBuff();
                if (buff.Bytes == null && _args.Device.Name == "FakeDevice")
                {
                    buff.Bytes = new byte[] { 1, 2, 3, 4, 5 };
                }
                var addr = buff.GetAddress(_info.Cpu);
                _reads.Remove(addr);
                foreach (var hex in buff.PrintHex(_info.Cpu))
                    _got.WriteLine(hex);
                _got.Flush();
                MarkOne();
                var waitMs = (double)delayDown.Value;
                var wait = TimeSpan.FromMilliseconds(waitMs);
                Thread.Sleep(wait);
                SendTopRead();
            }
        }

        private void SendTopRead()
        {
            if (_reads != null && _reads.Count >= 1)
            {
                var read = _reads.FirstOrDefault().Value;
                _args.Device.Send(read);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = ResExt.GetStream("app.ico").ToIcon();
            picBox.Image = ResExt.GetStream("device.png").ToImage();
            var dev = _args.Device;
            Text = dev.Name + " - " + "DevForge";
            Apply(_args.Hello, _args.Stamp);
            Device_Message(dev, new GotMessageArgs { Stamp = _args.Stamp, Message = _args.Hello });
        }

        private void Apply(Hello hello, DateTime stamp)
        {
            if (hello != null)
            {
                var info = hello.AsInfo();
                if (info != null)
                {
                    _log = new JsonLines(GetLogName(info, ".log"));
                    chipLbl.Text = info.Chip.GetEnumStr();
                    areaLbl.Text = info.Area.GetEnumStr();
                    cpuLbl.Text = info.Cpu.GetEnumStr();
                    memLbl.Text = info.Mem.GetEnumStr();
                    commLbl.Text = info.Comm.GetEnumStr();
                    appLbl.Text = info.App;
                    osDtLbl.Text = info.Ver.OsDate.GetDateStr();
                    osVerLbl.Text = info.Ver.OsVer.GetVerStr();
                    _info = info;
                }
            }
            dtLbl.Text = stamp.GetTimeStr();
        }

        private string GetLogName(PvInfo info, string end)
        {
            var name = info.Chip + "_" + GetLogName(info.Area) + "_" + info.Mem + "_v" +
                info.Ver.OsVer.GetVerStr() + "_" + info.Ver.OsDate.GetDateStr() + end;
            return TextExt.FixPath(name);
        }

        private string GetLogName(PvArea area)
        {
            switch (area)
            {
                case PvArea.Europe: return "EU";
                case PvArea.America: return "US";
                default: return "?";
            }
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

        private string LenHex => ((int)msgLenDw.Value).ToString("X2");

        private void testReadBtn_Click(object sender, EventArgs e)
        {
            var dev = _args.Device;
            if (_info.Cpu == PvCpu.X86)
            {
                var args = string.Join("|", new[] { "0110", "03", "6000", "0000", LenHex, "" });
                dev.Send(new Read(args));
            }
            else if (_info.Cpu == PvCpu.SH3)
            {
                var args = string.Join("|", new[] { "0000", "00", "8C00", "0040", LenHex, "" });
                dev.Send(new Read(args));
            }
        }

        private void backupBtn_Click(object sender, EventArgs e)
        {
            var maxChunkSize = (int)msgLenDw.Value;
            if (_info.Cpu == PvCpu.X86)
            {
                _reads = MemMap86Gen.GenerateCalls(maxChunkSize)
                    .ToDictionary(k => k.Get86Address(), v => new Read(v));
            }
            else if (_info.Cpu == PvCpu.SH3)
            {
                _reads = MemMapSHGen.GenerateCalls(maxChunkSize)
                    .ToDictionary(k => k.GetSHAddress(), v => new Read(v));
            }
            var xxdFile = GetLogName(_info, ".xxd");
            if (_got != null)
            {
                _got.Flush();
                _got.Dispose();
                _got = null;
            }
            _packStill = _reads.Count;
            _packGot = 0;
            var existing = File.Exists(xxdFile) ? File.ReadAllLines(xxdFile, Encoding.UTF8) : new string[0];
            foreach (var line in existing)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var tmp = line.Split(new[] { ':' }, 2);
                var addr = uint.Parse(tmp[0], NumberStyles.HexNumber);
                if (_reads.ContainsKey(addr))
                {
                    _reads.Remove(addr);
                    MarkOne(update: false);
                }
            }
            _got = File.AppendText(xxdFile);
            SendTopRead();
        }

        private void MarkOne(bool update = true)
        {
            _packStill = _reads.Count;
            _packGot++;
            if (!update) return;
            Action action = () =>
            {
                stillLbl.Text = TextExt.ToByteSize(_packStill * _packSize);
                gotLbl.Text = TextExt.ToByteSize(_packGot * _packSize);
            };
            Invoke(action);
        }
    }
}