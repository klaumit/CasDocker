using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

// ReSharper disable AssignNullToNotNullAttribute

namespace PvMake.Lib
{
    public static class FileExt
    {
        public static string GetDir(string folder, bool create)
        {
            if (string.IsNullOrWhiteSpace(folder)) return null;
            folder = Environment.ExpandEnvironmentVariables(folder ?? "");
            var dir = Path.GetFullPath(folder);
            if (create && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        public static string GetAssDir(string name, bool create, Type type = null)
        {
            var myType = type ?? typeof(FileExt);
            var myAss = myType.Assembly;
            var myDll = Path.GetFullPath(myAss.Location);
            var myDir = Path.GetDirectoryName(myDll);
            var subDir = Path.Combine(myDir, name);
            return GetDir(subDir, create);
        }

        public static string GetEnvDir(string name, bool create)
        {
            var folder = Environment.GetEnvironmentVariable(name);
            return GetDir(folder, create);
        }

        public static IEnumerable<string> Find(string root, string name)
        {
            var so = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(root, name, so);
            return files;
        }

        public static SortedDictionary<string, SortedSet<string>> FindAllFiles(string folder)
        {
            const SearchOption so = SearchOption.AllDirectories;
            var files = Directory.GetFiles(folder, "*", so);
            var dict = new SortedDictionary<string, SortedSet<string>>();
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file).ToLowerInvariant();
                SortedSet<string> list;
                if (!dict.TryGetValue(ext, out list))
                    dict[ext] = list = new SortedSet<string>();
                list.Add(file);
            }
            return dict;
        }

        public static void WriteWin(string file, IEnumerable<string> lines)
        {
            var text = string.Join("\r\n", lines);
            File.WriteAllText(file, text, Encoding.ASCII);
        }
    }
}