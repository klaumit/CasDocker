using System;
using System.IO;
using System.Linq;
using System.Text;

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

        public static string GetSafeStr(this BinaryReader reader, int count, Encoding enc = null)
        {
            var bytes = reader.GetSafeBytes(count);
            if (bytes == null)
                return null;
            var endPos = Array.IndexOf(bytes, (byte)0);
            var maxLen = endPos >= 0 ? endPos : count;
            enc ??= Encoding.ASCII;

            Console.Write("'" + string.Join("|", bytes.Select(t => (int)t)) + "'");

            var txt = enc.GetString(bytes, 0, maxLen).TrimOrNull();

            Console.WriteLine("'" + txt + "'");

            return txt;
        }

        public static byte[] CopyZero(this byte[] array, int max)
        {
            var zero = new byte[array.Length];
            Array.Copy(array, 0, zero, 0, max);
            return zero;
        }
    }
}