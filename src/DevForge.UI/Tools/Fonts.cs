using System.Drawing;

namespace DevForge.UI.Tools
{
	public static class Fonts
	{
		internal static Font SetMonospace(this Font font)
		{
			var family = FontFamily.GenericMonospace;
			var size = font.Size;
			var res = new Font(family, size);
			return res;
		}
	}
}