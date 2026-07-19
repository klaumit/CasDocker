using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MemForge.Lib;
using Vanara.PInvoke;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable RedundantExplicitArrayCreation

namespace MemForge
{
	public sealed class NoteIcon
	{
		internal Dictionary<uint, List<Tuple<HWND, string>>> windows;

		internal NotifyIcon noteIcon;
		private ContextMenuStrip noteMenu;

        private ProcWatcher _watcher;
		
		public NoteIcon()
		{
			noteIcon = new NotifyIcon();
			noteMenu = new ContextMenuStrip();
			noteMenu.Items.AddRange(InitializeMenu());
			
			noteIcon.DoubleClick += IconDoubleClick;
            var nis = ResTool.GetStream(typeof(NoteIcon), "Resources", "app.ico");
            noteIcon.Icon = new Icon(nis);
			noteIcon.ContextMenuStrip = noteMenu;

			windows = new Dictionary<uint, List<Tuple<HWND, string>>>();
            _watcher = new ProcWatcher(OnSimStarted, Defaults.Sim86, Defaults.SimSh);
		}
		
		private ToolStripItem[] InitializeMenu()
		{
			var menu = new ToolStripMenuItem[] {
                new ToolStripMenuItem("Kill all", null, menuKillClick),
				new ToolStripMenuItem("About", null, menuAboutClick),
				new ToolStripMenuItem("Exit", null, menuExitClick)
			};
			return menu;
		}

        private void OnSimStarted(object sender, uint pid, string name)
        {
            var title = "Process found!";
            var text = string.Format("{0} [{1}]", name, pid);
            noteIcon.ShowBalloonTip(500, title, text, ToolTipIcon.Info);

            FindMyWindows(pid);
        }

        private void FindMyWindows(uint pid)
        {
			windows[pid] = WindowExt.GetTopLevelWindows(pid);
        }
        
        private void menuKillClick(object sender, EventArgs e)
        {
            ProcExt.KillAll(Defaults.Sim86);
            ProcExt.KillAll(Defaults.SimSh);
        }

		private void menuAboutClick(object sender, EventArgs e)
		{
            MessageBox.Show("MemForge 1.0", "Info", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		private void menuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void IconDoubleClick(object sender, EventArgs e)
		{
		}
	}
}