using System.Drawing;
using System.Windows.Forms;
using DevForge.UI.Tools;

namespace DevForge.UI.Views
{
	public partial class HxdPanel : Panel
	{
		public HxdPanel()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (var font = Fonts.SetMonospace(Font))
			using (var brush = new SolidBrush(Color.Black))
			{
				var margin = 10;
				var size = 20;
				var count = 16;
				var g = e.Graphics;
				var x = 0;
				var y = 0;
				for (var i = 0; i < 1000; i++)
				{
					var txt = string.Format("{0:X2} ", (byte)i);
					var xPos = margin + x * size;
					var yPos = margin + y * size;
					g.DrawString(txt, font, brush, xPos, yPos);
					x++;
					if (x >= count)
					{
						y++;
						x = 0;
					}
					if (y >= count)
						break;
				}
			}
		}
	}
}