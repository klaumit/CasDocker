using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace PvRanger
{
    public static class ValueTool
    {
        public static string? TrimOrNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static string? AsHex(this byte[]? bytes)
        {
            return bytes == null ? null : Convert.ToHexString(bytes);
        }

        public static T? AsEnum<T>(this string? txt) where T : struct, Enum
        {
            if (txt == null)
                return null;
            if (!Enum.TryParse<T>(txt, true, out var val))
                return null;
            if (!Enum.IsDefined(val))
                return null;
            return val;
        }

        public static DateTime? AsDate(this string? txt)
        {
            if (txt == null)
                return null;
            if (txt.Length != 8)
                return null;
            var a = txt[..4];
            if (!int.TryParse(a, out var aa))
                return null;
            var b = txt[4..6];
            if (!int.TryParse(b, out var bb))
                return null;
            var c = txt[6..8];
            if (!int.TryParse(c, out var cc))
                return null;
            var val = new DateTime(aa, bb, cc);
            return val;
        }

        public static TimeSpan? AsTime(this string? txt)
        {
            if (txt == null)
                return null;
            if (txt.Length != 4)
                return null;
            var a = txt[..2];
            if (!int.TryParse(a, out var aa))
                return null;
            var b = txt[2..4];
            if (!int.TryParse(b, out var bb))
                return null;
            var val = new TimeSpan(aa, bb, 0);
            return val;
        }

        public static Version? AsVer(this string? txt)
        {
            if (txt == null)
                return null;
            if (txt.Length != 4)
                return null;
            var a = txt[..2];
            if (!int.TryParse(a, out var aa))
                return null;
            var b = txt[2..4];
            if (!int.TryParse(b, out var bb))
                return null;
            var val = new Version(aa, bb);
            return val;
        }

        public static DateTime? AsDateTime(this (string date, string time) t)
        {
            var myDate = t.date.AsDate();
            var myTime = t.time.AsTime();
            if (myDate == null || myTime == null)
                return null;
            var val = myDate.Value.Date + myTime.Value;
            return val;
        }

        public static string? FixStr(this string? txt)
        {
            return txt?.Replace((char)63, ' ').Trim();
        }

        public static string ReadTxtFile(string file)
        {
            var enc = Encoding.UTF8;
            var txt = File.ReadAllText(file, enc);
            return txt;
        }

        public static string? Search(string? root, string file)
        {
            file = FixPaths(file);
            var current = root ?? string.Empty;
            do
            {
                var full = Path.Combine(current, file);
                if (File.Exists(full))
                    return full;
            } while ((current = Path.GetDirectoryName(current)!) != null);
            return null;
        }

        public static string FixPaths(string path)
        {
            var c = Path.DirectorySeparatorChar;
            return path.Replace('\\', c).Replace('/', c);
        }

        public static int ParseHex(this string txt)
        {
            return int.Parse(txt, NumberStyles.HexNumber);
        }
    }
}