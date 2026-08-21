using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevForge.Lib.Common;
using DevForge.Lib.Hex;
using DevForge.Lib.High;
using DevForge.Lib.Messages.Impl;
using DevForge.Lib.Ponder;
using DevForge.Lib.Tools;
using DevForge.UI.Resources;

// ReSharper disable UseCollectionExpression
// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable LocalizableElement

namespace DevForge.Views
{
    public partial class DeviceForm : Form
    {
        private DeviceFoundArgs _args;
        private PvInfo _info;
        private JsonLines _log;
        private StreamWriter _got;
        private SortedDictionary<uint, Read> _reads;
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
            if (e.Message == null)
                return;
            var ts = e.Stamp.ToString("u").TrimEnd('Z');
            statusLbl.Text = "[" + ts + "] (" + e.Message.Kind + ") " + e.Message.Length + " bytes";
            _log.Write(e.Message);

            GetMode gm;
            if ((gm = e.Message as GetMode) != null)
            {
                Action gmA = () =>
                {
                    gm.Unpack(out var kind, out var code, out var stat, out var ptr);
                    mCodeTb.Text = "0x" + code.ToString("X4");
                    mStatTb.Text = "0x" + stat.ToString("X4");
                    jumpBox.Text = "0x" + ptr.ToString("X8");
                };
                Invoke(gmA);
            }

            Read r;
            if (((r = e.Message as Read) != null) && _reads != null)
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

        private void Form_Load(object sender, EventArgs e)
        {
            Icon = ResExt.GetStream("app.ico").ToIcon();
            picBox.Image = ResExt.GetStream("device.png").ToImage();
            var dev = _args.Device;
            Text = dev.Name + " - " + "DevForge";
            Apply(_args.Hello, _args.Stamp);
            Device_Message(dev, new GotMessageArgs { Stamp = _args.Stamp, Message = _args.Hello });
            UpdateCustomTxt();
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

        private void SendGetMode(byte val)
        {
            var dev = _args.Device;
            var hex = new byte[] { val }.ToHexString();
            dev.Send(new GetMode(hex));
        }

        private void gcmBtn_Click(object sender, EventArgs e) { SendGetMode(2); }
        private void glmBtn_Click(object sender, EventArgs e) { SendGetMode(1); }

        private void SendJumpo(byte val)
        {
            var dev = _args.Device;
            var code = mCodeTb.Text.Replace("0x", "");
            var stat = mStatTb.Text.Replace("0x", "");
            var txt = string.Format("{0:X2}|{1}|{2}", val, code, stat);
            dev.Send(new Jumpo(txt));
        }

        private void osClBtn_Click(object sender, EventArgs e) { SendJumpo(5); }

        private void SendLive()
        {
            var dev = _args.Device;
            var aliveMs = waitUpd.Value;
            var hex = new byte[] { (byte)aliveMs }.ToHexString();
            dev.Send(new Alive(hex));
        }

        private void keepLiveBtn_Click(object sender, EventArgs e)
        {
            SendLive();
        }

        private string LenHex { get { return ((int)msgLenDw.Value).ToString("X2"); } }

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

        private long GetJumpTo()
        {
            return TextExt.ParseHex(jumpBox.Text, 0);
        }

        private void jumpToBtn_Click(object sender, EventArgs e)
        {
            var dev = _args.Device;
            var hex = string.Format("{0:x8}", (uint)GetJumpTo());
            var seg = hex.Substring(0, 4);
            var off = hex.Substring(4, 4);
            if (_info.Cpu == PvCpu.X86)
            {
                var args = string.Join("|", new[] { "0000", "00", seg, off, "" });
                dev.Send(new Jump(args));
            }
            else if (_info.Cpu == PvCpu.SH3)
            {
                var args = string.Join("|", new[] { "0000", "00", seg, off, "" });
                dev.Send(new Jump(args));
            }
        }

        private int GetPkgLen()
        {
        	return (int)msgLenDw.Value;
        }
        
        private void backupBtn_Click(object sender, EventArgs e)
        {
        	var maxChunkSize = GetPkgLen();
            if (_info.Cpu == PvCpu.X86)
            {
            	var calls = MemMap86Gen.GenerateCalls(maxChunkSize);
                _reads = calls.ToDict(k => k.Get86Address(), v => new Read(v));
            }
            else if (_info.Cpu == PvCpu.SH3)
            {
            	var calls = MemMapSHGen.GenerateCalls(maxChunkSize);
                _reads = calls.ToDict(k => k.GetSHAddress(), v => new Read(v));
            }
            DoBackup();
        }

        private void DoBackup()
        {
            var xxdFile = GetLogName(_info, ".xxd");
            if (_got != null)
            {
                _got.Flush();
                _got.Dispose();
                _got = null;
            }
            _packStill = _reads.Count;
            _packGot = 0;
            var xxd = new XxdFile(xxdFile);
            xxd.ReadLines();
            var allKeys = _reads.Keys.ToArray();
            foreach (var range in xxd.Loop())
            {
                foreach (var addr in range.Intersect(allKeys))
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
                gotStiLbl.Text = TextExt.ToStr(_packGot)+" / "+
                	             TextExt.ToStr(_packStill);
            };
            Invoke(action);
        }
        
        private void FromBoxTextChanged(object sender, EventArgs e)
        {
        	UpdateCustomTxt();
        }
        
        private void ToBoxTextChanged(object sender, EventArgs e)
        {
        	UpdateCustomTxt();        	
        }
        
        private void UpdateCustomTxt()
        {
        	var diff = GetCustomLimits();
        	var txt = "Try "+ TextExt.ToByteSize(diff);
        	customBtn.Text = txt;
        }
        
        private long GetCustomLimits()
        {
        	var from = GetCustomFrom();
        	var to = GetCustomTo();
        	var diff = to - from;
        	return diff;
        }

        private long GetCustomTo()
        {
            return TextExt.ParseHex(toBox.Text, 0);
        }

        private long GetCustomFrom()
        {
        	return TextExt.ParseHex(fromBox.Text, 0);
        }

        private void CustomBtnClick(object sender, EventArgs e)
        {
            var maxChunkSize = GetPkgLen();
            var from = (uint)GetCustomFrom();
            var to = (uint)GetCustomTo();
            var range = Ranges.Create(from, to);
            if (_info.Cpu == PvCpu.X86)
            {
                var addrs = range.Iterate(maxChunkSize);
                var calls = MemMap86Gen.GenerateCalls(maxChunkSize, addrs);
                _reads = calls.ToDict(k => k.Get86Address(), v => new Read(v));
            }
            else if (_info.Cpu == PvCpu.SH3)
            {
                var addrs = range.Iterate(maxChunkSize);
                var calls = MemMapSHGen.GenerateCalls(maxChunkSize, addrs);
                _reads = calls.ToDict(k => k.GetSHAddress(), v => new Read(v));
            }
            DoBackup();
        }
        
        private void PurgeBtnClick(object sender, EventArgs e)
        {
        	_reads.Clear();
        }
	}
}
