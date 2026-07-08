using System;
using System.Globalization;

namespace DevForge.Lib.Tools
{
    public static class TextExt
    {
        public static string TrimOrNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static long ParseHex(string text, long defVal)
        {
            if (long.TryParse(text, NumberStyles.HexNumber, null, out var res))
                return res;
            return defVal;
        }
    }
}