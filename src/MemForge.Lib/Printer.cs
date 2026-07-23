using System.IO;
using System.Text;

namespace MemForge.Lib
{
    public static class Printer
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

        public static void PrintXxd(string inFile, string outFile)
        {
            using (var reader = File.OpenRead(inFile))
            using (var writer = File.CreateText(outFile))
            {
                const int size = 16;
                var array = new byte[size];
                var adr = 00000000;
                int got;
                while ((got = reader.Read(array, 0, array.Length)) >= 1)
                {
                    var middle = array.ToHexString(lower: true, sp: " ", max: got)
                        .PadRight(39, ' ');
                    var text = PrintAscii(array, got);
                    var line = string.Format("{0:x8}: {1}  {2}", adr, middle, text);
                    writer.WriteLine(line);
                    adr += got;
                }
                writer.Flush();
            }
        }

        private static string PrintAscii(byte[] array, int? max)
        {
            var bld = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                if (i >= max) break;
                var bit = array[i];
                var let = bit >= 0x20 && bit <= 0x7e ? (char)bit : '.';
                bld.Append(let);
            }
            return bld.ToString();
        }
    }
}