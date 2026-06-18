using System;
using System.IO;
using PvBake.Lib.Tools;
using PvBake.Lib.API;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Core
{
    public static class Detector
    {
        public static void Run(IOptions o)
        {
            var root = Path.GetFullPath(o.InputDir);
            Console.WriteLine($"Root = {root}");

            const SearchOption so = SearchOption.AllDirectories;
            var files = Directory.GetFiles(root, "*.bin", so);

            foreach (var file in files)
            {
                if (FileTool.Detect(file) is { } fo)
                {
                    Console.WriteLine($" * {file}");
                    Console.WriteLine(JsonTool.ToJson(fo));
                }
            }

            Console.WriteLine("Done.");
        }
    }
}