using System.IO;
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
                var middle = array.ToHexString(lower: true, sp: " ", max: got);
                var line = $"{adr:x8}: {middle}";
                writer.WriteLine(line);
                adr += got;
            }
            writer.Flush();
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