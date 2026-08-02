using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevForge.UI.Core;
using DevForge.UI.Tools;

// ReSharper disable ConvertToUsingDeclaration

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

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long Pos { get; internal set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_parent == null)
				_parent = this.FindParent<IHexView>();
			if (_parent == null)
				return;
			using (var font = Font.SetMonospace(10))
			using (var brush = new SolidBrush(Color.Black))
			{
				var g = e.Graphics;
				var size = 20;
				var margin = 10;
				var y = 0;
				foreach (var line in _parent.GetLines(Pos, 24))
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