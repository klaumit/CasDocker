using System.IO;
using System.Text;
using PvBake.Lib.Tools;

namespace PvBake.Lib.Core
{
    public static class Printer
    {
        public static void PrintXxd(string inFile, string outFile)
        {
            using var reader = File.OpenRead(inFile);
            using var writer = File.CreateText(outFile);
            const int size = 16;
            var array = new byte[size];
            var adr = 00000000;
            int got;
            while ((got = reader.Read(array, 0, array.Length)) >= 1)
            {
                var middle = array.ToHexString(lower: true, sp: " ", max: got)
                    .PadRight(39, ' ');
                var text = PrintAscii(array);
                var line = $"{adr:x8}: {middle}  {text}";
                writer.WriteLine(line);
                adr += got;
            }
            writer.Flush();
        }

        private static string PrintAscii(byte[] array)
        {
            var enc = Encoding.ASCII;
            var bld = new StringBuilder();
            foreach (var item in array)
            {
                var bit = enc.GetChars([item])[0];
                var let = '.';
                if (char.IsDigit(bit))
                    let = bit;
                else if (char.IsLetter(bit))
                    let = bit;
                bld.Append(let);
            }
            return bld.ToString();
        }

        public static void PrintHxd(string inFile, string outFile)
        {
            using var reader = File.OpenRead(inFile);
            using var writer = File.CreateText(outFile);
            const int size = 16;
            var array = new byte[size];
            var adr = 00000000;
            int got;
            while ((got = reader.Read(array, 0, array.Length)) >= 1)
            {
                var middle = array.ToHexString(lower: true, sp: " ", max: got, rotate: true);
                var line = $"{adr:x7} {middle}";
                writer.WriteLine(line.PadRight(47, ' '));
                adr += got;
            }
            var last = $"{adr:x7}";
            writer.WriteLine(last);
            writer.Flush();
        }
    }
}