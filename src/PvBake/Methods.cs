using System;
using System.IO;
using PvBake.Lib.Tools;

namespace PvBake
{
    internal static class Methods
    {
        private static void FindAndDetectAll(string[] args)
        {
            var root = Environment.CurrentDirectory;
            root = Path.Combine(root, "demo");
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