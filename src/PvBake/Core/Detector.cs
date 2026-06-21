using System;
using System.IO;
using PvBake.Lib.Tools;
using PvBake.Lib.API;
using PvBake.Lib.Core;
using PvBake.Lib.Models;

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

                if (FileTool.Read(file) is not { } fo)
                    continue;

                var json = JsonTool.ToJson(fo, format: true);
                Console.WriteLine(json);

                var target = Path.GetFullPath(Path.Combine(outRoot, $"{name}"));
                Console.WriteLine($"      --> {target}");
                using (var stream = File.Create(target))
                    FileTool.Write(fo, stream);

                var targetH1 = Path.GetFullPath(Path.Combine(outRoot, $"{name}.1.hex"));
                Printer.PrintXxd(target, targetH1);
                var targetH2 = Path.GetFullPath(Path.Combine(outRoot, $"{name}.2.hex"));
                Printer.PrintHxd(target, targetH2);

                if (fo is { } bmpFo)
                {
                    var xame = name.Replace(".bin", ".bmp");
                    var xarget = Path.GetFullPath(Path.Combine(outRoot, $"{xame}"));
                    Console.WriteLine($"      --> {xarget}");
                    using (var stream = File.Create(xarget))
                        Icons.SaveX86Bmp((Icon)bmpFo, stream);

                    var xargetH1 = Path.GetFullPath(Path.Combine(outRoot, $"{xame}.1.hex"));
                    Printer.PrintXxd(xarget, xargetH1);
                    var xargetH2 = Path.GetFullPath(Path.Combine(outRoot, $"{xame}.2.hex"));
                    Printer.PrintHxd(xarget, xargetH2);
                }
            }

            Console.WriteLine("Done.");
        }
    }
}