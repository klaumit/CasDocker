using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PvMake.Tools
{
    public static class FileExt
    {
        public static string GetDir(string? folder)
        {
            folder = Environment.ExpandEnvironmentVariables(folder ?? "");
            var dir = Path.GetFullPath(folder);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        public static void WriteWin(string file, IEnumerable<string> lines)
        {
            var text = string.Join("\r\n", lines);
            File.WriteAllText(file, text, Encoding.ASCII);
        }

        public static SortedDictionary<string, SortedSet<string>> FindAllFiles(string folder)
        {
            const SearchOption so = SearchOption.AllDirectories;
            var files = Directory.GetFiles(folder, "*", so);
            var dict = new SortedDictionary<string, SortedSet<string>>();
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file).ToLowerInvariant();
                if (!dict.TryGetValue(ext, out var list))
                    dict[ext] = list = [];
                list.Add(file);
            }
            return dict;
        }
    }
}