using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using DevForge.Lib.Comb;
using DevForge.UI.Resources;

namespace DevForge.UI.Views
{
	public partial class TxtForm : Form
	{
		private string _name;

		public TxtForm()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string File { get; set; }

		private void TxtForm_Load(object sender, EventArgs e)
		{
			Icon = ResExt.GetStream("app.ico").ToIcon();
			_name = Path.GetFileNameWithoutExtension(File);
			Text = _name;
			SetFindings();
		}

		private void SetFindings()
		{
			findLstBx.Items.Clear();
			var items = Searcher.FindNeedle(File, Needles.PvAplHed);
			foreach (var item in items)
			{
				findLstBx.Items.Add(item);
			}
		}
	}
}