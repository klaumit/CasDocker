using System;
using System.IO;
using PvBake.Lib.Tools;
using PvBake.Lib.API;
using PvBake.Lib.Core;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Core
{
    public static class Detector
    {
        public static void Run(IOptions o)
        {
            var inRoot = Path.GetFullPath(o.InputDir);
            var outRoot = Path.GetFullPath(o.OutputDir);
            Console.WriteLine($"Input  = {inRoot}");
            Console.WriteLine($"Output = {outRoot}");

            const SearchOption so = SearchOption.AllDirectories;
            var files = Directory.GetFiles(inRoot, "*.bin", so);

            foreach (var file in files)
            {
                var local = Files.GetRelativePath(inRoot, file);
                Console.WriteLine($" * {local}");

                if (FileTool.Read(file) is not { } fo)
                    continue;

                var fExt = Path.GetExtension(file);
                var name = fo.GetName() ?? Path.GetFileNameWithoutExtension(file);
                name = $"{name}{fExt}".Replace(' ', '_').Replace("!", "");

                var target = Path.GetFullPath(Path.Combine(outRoot, name));
                Console.WriteLine($"      --> {target}");
                using (var stream = File.Create(target))
                    FileTool.Write(fo, stream);

                if (JsonTool.ToJson(fo, format: true) is { } jsonText)
                {
                    var jsonName = name.Replace(".bin", ".json");
                    var jsonPath = Path.GetFullPath(Path.Combine(outRoot, $"{jsonName}"));
                    File.WriteAllText(jsonPath, jsonText, TextExt.Utf);
                    Console.WriteLine($"      --> {jsonPath}");
                }

                if (fo.SaveAsBmp() is { } bmpBytes)
                {
                    var bIdx = 0;
                    foreach (var bmpItem in bmpBytes)
                    {
                        var bmpName = name.Replace(".bin", $"_{++bIdx}.bmp");
                        var bmpPath = Path.GetFullPath(Path.Combine(outRoot, $"{bmpName}"));
                        File.WriteAllBytes(bmpPath, bmpItem);
                        Console.WriteLine($"      --> {bmpPath}");
                    }
                }
            }

            Console.WriteLine("Done.");
        }

        private static void WriteHex(string outRoot, string name, string target)
        {
            var targetH1 = Path.GetFullPath(Path.Combine(outRoot, $"{name}.1.hex"));
            Printer.PrintXxd(target, targetH1);
            var targetH2 = Path.GetFullPath(Path.Combine(outRoot, $"{name}.2.hex"));
            Printer.PrintHxd(target, targetH2);
        }
    }
}