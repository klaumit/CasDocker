using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using PvBake.Lib.Encodings;

namespace PvBake.Lib.Tools
{
    public static class ValueTool
    {
        public static string ToHexString(this byte[] bytes, bool lower = false,
            string sp = null, int? max = null, bool rotate = false)
        {
            var bld = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                if (i >= max) break;
                if (i + 1 >= max) rotate = false;
                bld.Append(bytes[i + (rotate ? 1 : 0)].ToString("X2"));
                if (i + 1 >= max) break;
                bld.Append(bytes[i + (rotate ? 0 : 1)].ToString("X2"));
                if (sp != null) bld.Append(sp);
            }
            var txt = bld.ToString();
            if (lower) txt = txt.ToLowerInvariant();
            return txt.TrimEnd();
        }

        public static string TrimOrNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static string AsHex(this byte[] bytes)
        {
            return bytes == null ? null : bytes.ToHexString();
        }

        public static T? AsEnum<T>(this string txt) where T : struct, Enum
        {
            if (txt == null)
                return null;
            if (!Enum.TryParse<T>(txt, true, out var val))
                return null;
            if (!Enum.IsDefined(typeof(T), val))
                return null;
            return val;
        }

        public static DateTime? AsDate(this string txt)
        {
            if (txt == null)
                return null;
            if (txt.Length != 8)
                return null;
            var a = txt.Substring(0, 4);
            if (!int.TryParse(a, out var aa))
                return null;
            var b = txt.Substring(4, 2);
            if (!int.TryParse(b, out var bb))
                return null;
            var c = txt.Substring(6, 2);
            if (!int.TryParse(c, out var cc))
                return null;
            var val = new DateTime(aa, bb, cc);
            return val;
        }

        public static TimeSpan? AsTime(this string txt)
        {
            if (txt == null)
                return null;
            if (txt.Length != 4)
                return null;
            var a = txt.Substring(0, 2);
            if (!int.TryParse(a, out var aa))
                return null;
            var b = txt.Substring(2, 2);
            if (!int.TryParse(b, out var bb))
                return null;
            var val = new TimeSpan(aa, bb, 0);
            return val;
        }

        public static byte[] AsAscii(this string val, bool endMark = false, int? length = null)
        {
            if (endMark && !string.IsNullOrWhiteSpace(val))
            {
                TextExt.AddZeroMark(ref val);
            }
            var res = val.TryAsPvRus(0, val?.Length ?? 0, out var err);
            if (err == 0)
            {
                res = res?.Concat(new byte[] { 0x00, 0x00 }).ToArray();
            }
            else
            {
                var enc = Encoding.ASCII;
                res = enc.GetBytes(val ?? string.Empty);
            }
            if (length != null)
            {
                const byte ff = 0xFF;
                res = res.Pad(length.Value, ff);
            }
            return res;
        }

        public static byte[] AsAscii<T>(this T? val) where T : struct, Enum
        {
            return val.ToString().AsAscii();
        }

        public static byte[] AsAscii(this Version ver)
        {
            return $"{ver.Major:D2}{ver.Minor:D2}".AsAscii();
        }

        public static byte[] AsAscii(this DateTime dt)
        {
            return $"{dt.Year:D4}{dt.Month:D2}{dt.Day:D2}{dt.Hour:D2}{dt.Minute:D2}".AsAscii();
        }

        public static Version AsVer(this string txt)
        {
            if (txt == null)
                return null;
            if (txt.Length != 4)
                return null;
            var a = txt.Substring(0, 2);
            if (!int.TryParse(a, out var aa))
                return null;
            var b = txt.Substring(2, 2);
            if (!int.TryParse(b, out var bb))
                return null;
            var val = new Version(aa, bb);
            return val;
        }

        public static DateTime? AsDateTime(this Tuple<string, string> t)
        {
            var myDate = t.Item1.AsDate();
            var myTime = t.Item2.AsTime();
            if (myDate == null || myTime == null)
                return null;
            var val = myDate.Value.Date + myTime.Value;
            return val;
        }

        public static string FixStr(this string txt)
        {
            return txt?.Replace((char)63, ' ').Trim();
        }

        public static string ReadTxtFile(string file)
        {
            var enc = Encoding.UTF8;
            var txt = File.ReadAllText(file, enc);
            return txt;
        }

        public static string Search(string root, string file)
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

        public static byte[] Pad(this byte[] bytes, int size, byte bit)
        {
            var array = new byte[size];
            for (var i = 0; i < array.Length; i++) array[i] = bit;
            if (bytes != null)
                Array.Copy(bytes, 0, array, 0, bytes.Length);
            return array;
        }
    }
}