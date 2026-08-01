using System.Drawing;
using System.Windows.Forms;
using DevForge.UI.Core;
using DevForge.UI.Tools;

namespace DevForge.UI.Views
{
	public partial class HxdPanel : UserControl
	{
		public HxdPanel()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

		private IHexView _parent;

		public long Pos { get; internal set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_parent == null)
				_parent = GuiExt.FindParent<IHexView>(this);
			if (_parent == null)
				return;
			using (var font = Fonts.SetMonospace(Font, 10))
			using (var brush = new SolidBrush(Color.Black))
			{
				var g = e.Graphics;
				var size = 20;
				var margin = 10;
				var y = 0;
				foreach (var line in _parent.GetLines(Pos, 30))
				{
					var d = string.Format("{0}  {1}   {2}",
						line.Addr, line.Raw, line.Txt);
					var xPos = margin;
					var yPos = margin + y * size;
					g.DrawString(d, font, brush, xPos, yPos);
					y++;
				}
			}
		}
	}
}