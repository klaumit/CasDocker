using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DevForge.Lib.Hex;
using DevForge.UI.Resources;
using DevForge.Lib.Visual;

namespace DevForge.UI.Views
{
	public partial class MapForm : Form
	{
		private string _name;

		public MapForm()
		{
			InitializeComponent();
			KeyPreview = true;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string File { get; set; }

		private void MapForm_Load(object sender, EventArgs e)
		{
			Icon = ResExt.GetStream("app.ico").ToIcon();
			_name = Path.GetFileNameWithoutExtension(File);
			Text = _name;
			var bs = SetImage();
			Text += " [" + bs + "]";
			Resize += MapForm_Resize;
		}

		private void MapForm_Resize(object sender, EventArgs e)
		{
			SetImage();
		}

		private string SetImage(bool force = false)
		{
			string bs;
			var xxd = new XxdFile(File);
			var imgFile = xxd.Render(force, out bs);
			var w = mapImgBox.Width;
			var h = mapImgBox.Height;
			var image = Image.FromFile(imgFile).ScaleTo(w, h);
			mapImgBox.Image = image;
			return bs;
		}

		private void MapForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				SetImage(force: true);
			}
		}
	}
}