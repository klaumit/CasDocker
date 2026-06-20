using System;
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
                var text = PrintAscii(array, got);
                var line = $"{adr:x8}: {middle}  {text}";
                writer.WriteLine(line);
                adr += got;
            }
            writer.Flush();
        }

        private static string PrintAscii(byte[] array, int? max)
        {
            var bld = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                if (i >= max) break;
                var bit = array[i];
                var let = bit is >= 0x20 and <= 0x7e ? (char)bit : '.';
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
                if (got % 2 != 0) array = array.CopyZero(got++);
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