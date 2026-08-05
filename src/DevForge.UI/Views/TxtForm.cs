using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using DevForge.Lib.Comb;
using DevForge.UI.Resources;
using DevForge.UI.Tools;
using System.Linq;
using N = DevForge.Lib.Comb.Needles;
using S = DevForge.Lib.Comb.Searcher;

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
			findLstBx.Font = findLstBx.Font.SetMonospace(size: 11);
			findLstBx.Items.Clear();
			var it = S.FindNeedle(File, N.PvAplHed).GroupBy(i => i.Absolute);
			foreach (var item in it)
				findLstBx.Items.Add(item.First());
		}

		private void findLstBx_SelectedIndexChanged(object sender, EventArgs e)
		{
			var fnd = findLstBx.SelectedItem as TxtMatch;
			if (fnd == null)
				return;

			var owner = (HxdForm)Owner;
			owner.OnPosClick(Math.Max(0, fnd.LineNo - 1));
		}
	}
}