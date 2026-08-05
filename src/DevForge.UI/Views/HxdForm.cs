using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevForge.Lib.Hex;
using DevForge.UI.Core;
using DevForge.UI.Resources;
using DevForge.UI.Tools;
using F = System.IO.File;
using System.ComponentModel;

namespace DevForge.UI.Views
{
	public partial class HxdForm : Form, IHexView
	{
		private string _name;
		private int _middleScroll;
		private long _length;

		public HxdForm()
		{
			InitializeComponent();
			KeyPreview = true;
		}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
			rangeBox.Font = rangeBox.Font.SetMonospace();
			rangeBox.Items.Clear();
			var xxd = new XxdFile(File);
			xxd.ReadLines();
			var info = xxd.Stats.Info;
			_length = info.Pos;
			var ranges = info.Ranges;
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
				ScrollRelative(diff);
			}
		}

		public IEnumerable<XxdLine> GetLines(long pos, int count)
		{
			var xxd = new XxdFile(File);
			using (var reader = xxd.OpenReader(pos))
			{
				var lines = XxdFile.ReadHexLines(reader);
				foreach (var line in lines.Take(count))
				{
					yield return line;
				}
			}
		}

		private void rangeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var range = rangeBox.SelectedItem as IntRange;
			if (range == null)
				return;
			ScrollAbsolute(range.Pos);
		}

		private void ScrollAbsolute(long pos)
		{
			hexPanel.Pos = pos;
			hexPanel.Invalidate();
		}

		private void ScrollRelative(int diff)
		{
			hexPanel.Pos += diff * (67 + 2);
			hexPanel.Invalidate();
		}

		private void HxdForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.PageDown)
				ScrollRelative(50);
			else if (e.KeyCode == Keys.PageUp)
				ScrollRelative(-50);
			else if (e.KeyCode == Keys.Down)
				ScrollRelative(1);
			else if (e.KeyCode == Keys.Up)
				ScrollRelative(-1);
		}

		internal void OnMapClick(double pro)
		{
			const int perLine = 67 + 2;
			var newPos = (pro / 100.0) * _length;
			var mult = (long)(newPos / perLine);
			var fixPos = mult * perLine;
			ScrollAbsolute(fixPos);
		}

		internal void OnPosClick(int lineNo)
		{
			const int perLine = 67 + 2;
			var pos = perLine * lineNo;
			ScrollAbsolute(pos);
		}

		private void showMapBtn_Click(object sender, EventArgs e)
		{
			var map = new MapForm() { File = File };
			map.Show(this);
		}

		private void findingsBtn_Click(object sender, EventArgs e)
		{
			var fnd = new TxtForm() { File = File };
			fnd.Show(this);
		}
	}
}