using System;
using System.IO;
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

		public string File { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			Icon = ResExt.GetStream("app.ico").ToIcon();
			var name = Path.GetFileNameWithoutExtension(File);
			Text = name;
		}
	}
}