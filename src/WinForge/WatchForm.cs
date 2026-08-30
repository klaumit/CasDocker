using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MemForge.Lib;
using WinFinder;
using SE = MemForge.Lib.SysExt;

// ReSharper disable InlineOutVariableDeclaration

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
			var sim86Pid = sim86 == null ? null : (uint?)sim86.Id;
			sim86Tb.Text = sim86Pid?.ToString("X4");
			Monitoring.ReadReg86(sim86Pid);

			var simSh = ProcExt.Find("CASIO SimSH");
			var simShPid = simSh == null ? null : (uint?)simSh.Id;
			simShTb.Text = simShPid?.ToString("X4");
			Monitoring.ReadRegSh(simShPid);
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
			string exe;
			if (_exes.TryGetValue("Sim86", out exe))
				ProcExt.Start(exe);
		}

		private void StartShBtnClick(object sender, EventArgs e)
		{
			string exe;
			if (_exes.TryGetValue("SimSH", out exe))
				ProcExt.Start(exe);
		}

		private void Stop86BtnClick(object sender, EventArgs e)
		{
			ProcExt.KillAll(ProcExt.GetByPid(Tooly.ParseUInt32(sim86Tb.Text)));
		}

		private void StopShBtnClick(object sender, EventArgs e)
		{
			ProcExt.KillAll(ProcExt.GetByPid(Tooly.ParseUInt32(simShTb.Text)));
		}
	}
}