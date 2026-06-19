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
                var name = Path.GetFileName(file);
                Console.WriteLine($" * {local}");

                if (FileTool.Detect(file) is not { } fo)
                    continue;

                var json = JsonTool.ToJson(fo, format: true);
                Console.WriteLine(json);

                var target = Path.GetFullPath(Path.Combine(outRoot, $"{name}"));
                Console.WriteLine($"      --> {target}");
                // File.WriteAllBytes(target, array);

                var targetH1 = Path.GetFullPath(Path.Combine(outRoot, $"{name}.1.hex"));
                Printer.PrintXxd(file, targetH1);
                var targetH2 = Path.GetFullPath(Path.Combine(outRoot, $"{name}.2.hex"));
                Printer.PrintHxd(file, targetH2);
            }

            Console.WriteLine("Done.");
        }
    }
}