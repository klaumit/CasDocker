using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MemForge.Lib;
using SE = MemForge.Lib.SysExt;
using M = WinFinder.Monitoring;

// ReSharper disable UseNullPropagation
// ReSharper disable LocalizableElement
// ReSharper disable UseStringInterpolation
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
			sim86Tb.Text = string.Format("{0:X4}", sim86Pid);
			SetList(M.ReadReg86(sim86Pid));

			var simSh = ProcExt.Find("CASIO SimSH");
			var simShPid = simSh == null ? null : (uint?)simSh.Id;
			simShTb.Text = string.Format("{0:X4}", simShPid);
			SetList(M.ReadRegSh(simShPid));
		}

		private void SetList(RegShShim sim)
		{
			regShList.Items.Clear();
			if (sim == null)
				return;
			var regs = sim.ReadRegs();
			if (regs == null)
				return;
			foreach (var pair in regs)
				regShList.Items.Add(pair.Key + " = " + pair.Value);
		}

		private void SetList(Reg86Shim sim)
		{
			reg86List.Items.Clear();
			if (sim == null)
				return;
			var regs = sim.ReadRegs();
			if (regs == null)
				return;
			foreach (var pair in regs)
				reg86List.Items.Add(pair.Key + " = " + pair.Value);
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
			SetFonts();
		}

		private void SetFonts()
		{
			reg86List.Font = reg86List.Font.GetMonospace();
			regShList.Font = regShList.Font.GetMonospace();
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

		private void Dump86BtnClick(object sender, EventArgs e)
		{
			var sim86Pid = Tooly.ParseUInt32(sim86Tb.Text);
			if (sim86Pid == null)
				return;
			var f = M.DumpMem(sim86Pid, Path.GetFullPath("sim86.txt"));
			MessageBox.Show(f, "Dump 86");
		}

		private void DumpShBtnClick(object sender, EventArgs e)
		{
			var simShPid = Tooly.ParseUInt32(simShTb.Text);
			if (simShPid == null)
				return;
			var f = M.DumpMem(simShPid, Path.GetFullPath("simSH.txt"));
			MessageBox.Show(f, "Dump SH");
		}
	}
}