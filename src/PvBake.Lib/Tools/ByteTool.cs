using System;
using System.IO;
using System.Linq;
using System.Text;
using PvBake.Lib.Encodings;

namespace PvBake.Lib.Tools
{
    public static class ByteTool
    {
        public static uint? GetSafeUInt32(this BinaryReader reader)
        {
            try
            {
                return reader.ReadUInt32();
            }
            catch (Exception)
            {
                // Ignore!
            }
            return null;
        }

        public static ushort? GetSafeUInt16(this BinaryReader reader)
        {
            try
            {
                return reader.ReadUInt16();
            }
            catch (Exception)
            {
                // Ignore!
            }
            return null;
        }

        public static byte[] GetSafeBytes(this BinaryReader reader, int count)
        {
            try
            {
                var array = reader.ReadBytes(count);
                if (array.Length == count)
                    return array;
            }
            catch (Exception)
            {
                // Ignore!
            }
            return null;
        }

        public static string GetSafeStr(this BinaryReader reader, int count)
        {
            var bytes = reader.GetSafeBytes(count);
            if (bytes == null)
                return null;
            var endPos = Array.IndexOf(bytes, (byte)0);
            var maxLen = endPos >= 0 ? endPos : count;
            var enc = Encoding.ASCII;
            var txt = enc.GetString(bytes, 0, maxLen).TrimOrNull();
            if (txt.StartsWith("??"))
            {
                var t = bytes.TryAsPvRus(0, maxLen, out var err).TrimOrNull();
                if (!string.IsNullOrWhiteSpace(t) && err == 0) txt = t;
            }
            return txt;
        }

        public static byte[] CopyZero(this byte[] array, int max)
        {
            var zero = new byte[array.Length];
            Array.Copy(array, 0, zero, 0, max);
            return zero;
        }

        public static byte[] Allocate(this byte value, int count)
        {
            var array = Enumerable.Repeat(value, count).ToArray();
            return array;
        }
    }
}