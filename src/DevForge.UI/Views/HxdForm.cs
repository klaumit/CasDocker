using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevForge.Lib.Hex;
using DevForge.UI.Resources;
using F = System.IO.File;

namespace DevForge.UI.Views
{
	public partial class HxdForm : Form
	{
		private string _name;

		public HxdForm()
		{
			InitializeComponent();
		}

		public string File { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			Icon = ResExt.GetStream("app.ico").ToIcon();
			_name = Path.GetFileNameWithoutExtension(File);
			Text = _name;
			SetRanges();
		}

		private void SetRanges()
		{
			rangeBox.Items.Clear();
			var xxd = new XxdFile(File);
			xxd.ReadLines();
			var ranges = xxd.Stats.Info.Ranges;
			foreach (var range in ranges)
				rangeBox.Items.Add(range.Value);
		}

		private void saveAsBtn_Click(object sender, EventArgs e)
		{
			var ext = ".bin";
			var dest = _name + ext;
			using (var dialog = new SaveFileDialog
			{
				FileName = dest, DefaultExt = ext,
				Filter = "Binary files (.bin)|*.bin"
			})
			{
				if (dialog.ShowDialog(this) != DialogResult.OK)
					return;
				var file = dialog.FileName;
				if (string.IsNullOrWhiteSpace(file))
					return;
				using (var input = F.OpenText(File))
				using (var output = F.Create(file))
				{
					foreach (var line in XxdFile.ReadHexLines(input))
					{
						var array = line.GetRaw();
						output.Write(array, 0, array.Length);
					}
				}
				Process.Start(file);
			}		
		}
	}
}