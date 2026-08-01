using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevForge.Lib.Hex;
using DevForge.Tools;
using DevForge.UI.Core;
using DevForge.UI.Resources;
using DevForge.UI.Tools;
using F = System.IO.File;

namespace DevForge.UI.Views
{
	public partial class HxdForm : Form, IHexView
	{
		private string _name;
		private int _middleScroll;

		public HxdForm()
		{
			InitializeComponent();
		}

		public string File { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			_middleScroll = hexScroll.Value;
			Icon = ResExt.GetStream("app.ico").ToIcon();
			_name = Path.GetFileNameWithoutExtension(File);
			Text = _name;
			SetRanges();
		}

		private void SetRanges()
		{
			rangeBox.Font = Fonts.SetMonospace(rangeBox.Font);
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

		private Dictionary<ScrollEventType, int> _scrolls
			= new Dictionary<ScrollEventType, int>();

		private void hexScroll_Scroll(object sender, ScrollEventArgs e)
		{
			if (_scrolls.ContainsKey(e.Type))
				return;
			_scrolls[e.Type] = e.NewValue;
			if (!_scrolls.ContainsKey(ScrollEventType.ThumbTrack)
				&& e.NewValue != e.OldValue)
				_scrolls[ScrollEventType.ThumbTrack] = e.OldValue;
			if (e.Type == ScrollEventType.EndScroll)
			{
				var start = _scrolls[ScrollEventType.ThumbTrack];
				var end = _scrolls[ScrollEventType.EndScroll];
				var diff = end - start;
				e.NewValue = _middleScroll;
				_scrolls.Clear();
				hexScroll_Scroll(diff);
			}
		}

		private void hexScroll_Scroll(int diff)
		{
			Debug.WriteLine(" SCROLL => " + diff);
		}
	}
}