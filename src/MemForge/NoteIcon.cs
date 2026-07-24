using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MemForge.Lib;
using Vanara.PInvoke;
using DevForge.Lib.Setup;
using U = DevForge.Tools.Utils;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable RedundantExplicitArrayCreation
// ReSharper disable LocalizableElement

namespace MemForge
{
	public sealed class NoteIcon
	{
		private readonly Lazy<DeviceHub> _hub = new Lazy<DeviceHub>();

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
			Init();
		}

		private void Init()
		{
			U.Main = CreateDummy();
			noteIcon.Disposed += (o, e) => U.OnExiting(o, 
				new FormClosingEventArgs(CloseReason.FormOwnerClosing, false));
			_hub.Value.NewDevice += U.OnNewDevice;
			_hub.Value.StartMemory();
		}

		private Control CreateDummy()
		{
			var ctrl = new Form { Text = "Dummy window" };
			ctrl.CreateControl();
			ctrl.Show();
			ctrl.Shown += (o, e) => ctrl.Visible = false;
			return ctrl;
		}

		private ToolStripItem[] InitializeMenu()
		{
			var menu = new ToolStripMenuItem[] {
				new ToolStripMenuItem("Dump all", null, menuDumpClick),
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

			windows[pid] = WindowExt.GetTopLevelWindows(pid);
			StartReadingProc(pid);
        }

        private void StartReadingProc(uint pid)
        {
			MemAbstract.FindInSim(pid);
        }

		private void menuDumpClick(object sender, EventArgs e)
		{
			foreach (var pid in windows.Keys)
			{
				MemReader.WriteFullDump(pid);
			}
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