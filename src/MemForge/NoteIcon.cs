using System;
using System.Drawing;
using System.Windows.Forms;
using MemForge.Lib;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable RedundantExplicitArrayCreation

namespace MemForge
{
	public sealed class NoteIcon
	{
		internal NotifyIcon noteIcon;
		private ContextMenuStrip noteMenu;
		
		public NoteIcon()
		{
			noteIcon = new NotifyIcon();
			noteMenu = new ContextMenuStrip();
			noteMenu.Items.AddRange(InitializeMenu());
			
			noteIcon.DoubleClick += IconDoubleClick;
            var nis = ResTool.GetStream(typeof(NoteIcon), "Resources", "app.ico");
            noteIcon.Icon = new Icon(nis);
			noteIcon.ContextMenuStrip = noteMenu;
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

        private void menuKillClick(object sender, EventArgs e)
        {
            ProcExt.KillAll("Sim3022");
            ProcExt.KillAll("CASIO SimSH");
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