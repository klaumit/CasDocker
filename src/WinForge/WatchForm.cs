using System;
using System.Windows.Forms;
using MemForge.Lib;

namespace WinFinder
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
		}
	}
}