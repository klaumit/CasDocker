using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace MemForge
{
	public sealed class NotificationIcon
	{
		internal NotifyIcon noteIcon;
		private ContextMenu noteMenu;
		
		public NotificationIcon()
		{
			noteIcon = new NotifyIcon();
			noteMenu = new ContextMenu(InitializeMenu());
			
			noteIcon.DoubleClick += IconDoubleClick;
			var resources = new ComponentResourceManager(typeof(NotificationIcon));
			noteIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			noteIcon.ContextMenu = noteMenu;
		}
		
		private MenuItem[] InitializeMenu()
		{
			MenuItem[] menu = new MenuItem[] {
				new MenuItem("About", menuAboutClick),
				new MenuItem("Exit", menuExitClick)
			};
			return menu;
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