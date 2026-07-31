using System;
using System.Windows.Forms;
using DevForge.UI.Resources;

namespace DevForge.UI.Views
{
	public partial class HxdForm : Form
	{
		public HxdForm()
		{
			InitializeComponent();
		}

		private void Form_Load(object sender, EventArgs e)
		{
			Icon = ResExt.GetStream("app.ico").ToIcon();
		}
	}
}