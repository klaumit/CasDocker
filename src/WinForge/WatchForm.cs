using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MemForge.Lib;
using WinFinder;
using SE = MemForge.Lib.SysExt;

namespace WinForge
{
	public partial class WatchForm : Form
	{
		public WatchForm()
		{
			InitializeComponent();
		}

		private void Quit()
		{
			clocker.Enabled = false;
			Close();
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Quit();
		}

		private void clocker_Tick(object sender, EventArgs e)
		{
			var sim86 = ProcExt.Find("Sim3022");
			if (sim86 != null)
			{
				var pid = (uint)sim86.Id;
				sim86Tb.Text = pid.ToString("X4");

				Monitoring.ReadReg86(pid);
			}
			var simSh = ProcExt.Find("CASIO SimSH");
			if (simSh != null)
			{
				var pid = (uint)simSh.Id;
				simShTb.Text = pid.ToString("X4");

				Monitoring.ReadRegSh(pid);
			}
		}

		private void delayNd_ValueChanged(object sender, EventArgs e)
		{
			clocker.Interval = (int)delayNd.Value;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			clocker_Tick(sender, e);
			delayNd.Value = clocker.Interval;
			clocker.Enabled = true;
			FindExes();
		}

		private Dictionary<string, string> _exes;

		private void FindExes()
		{
			var root = SE.GetSrcRoot();
			_exes = SE.GetSimExes(root);
		}

		private void Start86BtnClick(object sender, EventArgs e)
		{
			if (_exes.TryGetValue("Sim86", out var exe))
				ProcExt.Start(exe);
		}

		private void StartShBtnClick(object sender, EventArgs e)
		{
			if (_exes.TryGetValue("SimSH", out var exe))
				ProcExt.Start(exe);
		}
	}
}