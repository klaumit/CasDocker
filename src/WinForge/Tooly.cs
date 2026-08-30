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
    }
}