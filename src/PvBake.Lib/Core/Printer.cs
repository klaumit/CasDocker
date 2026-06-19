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
                var line = $"{adr:x8}: {ValueTool.ToHexString(array, lower: true, space: "?")}";
                writer.WriteLine(line);
                adr += got;
            }
            writer.Flush();
        }

        public static void PrintHxd(string inFile, string outFile)
        {
            using var reader = File.OpenRead(inFile);
            using var writer = File.CreateText(outFile);
            var size = 16;
            var adr = 0000000;

            
            
         
            

            // 0000000 ff00 4143 4953 034f 345a 3638 3130 3030






        }
    }
}