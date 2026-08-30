using System.Drawing;
using System.Globalization;

namespace WinForge
{
    public static class Tooly
    {
        public static uint? ParseUInt32(string text)
        {
            return uint.TryParse(text, NumberStyles.HexNumber, null, out var res)
                ? res
                : (uint?)null;
        }

        public static Font GetMonospace(this Font font)
        {
            var family = FontFamily.GenericMonospace;
            var res = new Font(family, font.Size);
            return res;
        }
    }
}