using System;
using System.Globalization;
using System.Linq;
using ByteSizeLib;

namespace DevForge.Lib.Tools
{
    public static class TextExt
    {
        public static string TrimOrNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static string CleanTrim(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Replace('\0', ' ').Trim();
        }

        public static long ParseHex(string text, long defVal)
        {
        	if (string.IsNullOrWhiteSpace(text))
        	    return defVal;
        	const string tmp = "0x";
        	if (text.StartsWith(tmp))
        		text = text.Replace(tmp, "");
        	long res;
            if (long.TryParse(text, NumberStyles.HexNumber, null, out res))
                return res;
            return defVal;
        }

        public static string FixPath(string path)
        {
            return path.Replace('?', '-');
        }

        public static string ToHexString(this byte[] bytes, bool lower = false, int? split = null)
        {
            if (bytes == null) return null;
            return string.Join("", bytes.Select((b, i) =>
            {
                var txt = b.ToString(lower ? "x2" : "X2");
                if (split != null && i != 0 && i % split == 0)
                    txt = " " + txt;
                return txt;
            }));
        }

        public static byte[] FromHexString(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return null;
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return bytes;
        }

        public static string ToByteSize(double bytes)
        {
            var size = ByteSize.FromBytes(bytes);
            var txt = size.ToString();
            if (txt == " b")
                txt = "0 b";
            return txt;
        }
        
        public static string ToStr(long number)
        {
            var txt = string.Format("{0:D}", number);;
            return txt;
        }
    }
}