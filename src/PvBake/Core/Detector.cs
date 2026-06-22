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
            var files = Directory.GetFiles(inRoot, "*.*", so);

            foreach (var file in files)
            {
                var local = Files.GetRelativePath(inRoot, file);
                Console.WriteLine($" * {local}");

                if (FileTool.Read(file) is not { } fo)
                    continue;

                WriteOne(fo, outRoot, file);
            }

            Console.WriteLine("Done.");
        }

        private static void WriteOut(IFile fo, string outRoot, string file)
        {
            var fExt = fo.GetExt() ?? Path.GetExtension(file);
            var name = fo.GetName() ?? Path.GetFileNameWithoutExtension(file);
            name = $"{name}{fExt}".Replace(' ', '_').Replace("!", "");

            var target = Path.GetFullPath(Path.Combine(outRoot, name));
            Console.WriteLine($"      --> {target}");
            using (var stream = File.Create(target))
                FileTool.Write(fo, stream);

            WriteHex(outRoot, name, target);

            if (JsonTool.ToJson(fo, format: true) is { } jsonText)
            {
                var jsonName = name.Replace(fExt, ".json");
                var jsonPath = Path.GetFullPath(Path.Combine(outRoot, $"{jsonName}"));
                File.WriteAllText(jsonPath, jsonText, TextExt.Utf);
                Console.WriteLine($"      --> {jsonPath}");
            }

            if (fo.SaveAsBmp() is { } bmpBytes)
            {
                var bIdx = 0;
                foreach (var bmpItem in bmpBytes)
                {
                    var bmpName = name.Replace(fExt, $"_{++bIdx}.bmp");
                    var bmpPath = Path.GetFullPath(Path.Combine(outRoot, $"{bmpName}"));
                    File.WriteAllBytes(bmpPath, bmpItem);
                    Console.WriteLine($"      --> {bmpPath}");
                }
            }
        }

        private static void WriteOne(IFile file, string outRoot, string iFile)
        {
            var baseName = Path.GetFileNameWithoutExtension(iFile);
            if (file is Dump { } dump)
            {
                WriteOut(dump, outRoot, iFile);
                WriteOne(dump.Bios, outRoot, $"{baseName}_bios");
                foreach (var (key, val) in dump.AddIns)
                    WriteOne(val, outRoot, $"{baseName}_a{key}");
                return;
            }
            if (file is Bios { } bios)
            {
                WriteOut(bios, outRoot, iFile);
                return;
            }
            if (file is AddIn { } addIn)
            {
                WriteOut(addIn, outRoot, iFile);
                return;
            }
            throw new InvalidOperationException(file.GetType().FullName);
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