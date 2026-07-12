using System;
using System.Globalization;
using System.Linq;

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

        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null) return null;
            return string.Join("", bytes.Select(b => b.ToString("X2")));
        }

        public static byte[] FromHexString(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return null;
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return bytes;
        }
    }
}