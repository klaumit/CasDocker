using System.Drawing;

namespace DevForge.UI.Tools
{
	public static class Fonts
	{
		internal static Font SetMonospace(this Font font, int? size = null)
		{
			var family = FontFamily.GenericMonospace;
			var fSize = size ?? font.Size;
			var res = new Font(family, fSize);
			return res;
		}
	}
}