using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DevForge.Lib.Comb;
using DevForge.Lib.Hex;
using DevForge.UI.Resources;
using DevForge.Lib.Visual;

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



			Searcher.FindNeedle("?", Needles.PvAplHed);
			
			


		}
	}
}