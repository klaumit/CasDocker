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

                if (FileTool.Detect(file) is { } fo)
                {
                    Console.WriteLine(JsonTool.ToJson(fo, format: true));
                }
            }

            Console.WriteLine("Done.");
        }
    }
}