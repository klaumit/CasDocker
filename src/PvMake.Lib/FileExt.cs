using System;
using System.IO;
using System.Collections.Generic;
using F = Pva2cpa.Lib.Files;

// ReSharper disable InlineOutVariableDeclaration
// ReSharper disable AssignNullToNotNullAttribute

namespace PvMake.Lib
{
    public static class FileExt
    {
        public static string GetAssDir(string name, bool create, Type type = null)
        {
            var myType = type ?? typeof(FileExt);
            var myAss = myType.Assembly;
            var myDll = Path.GetFullPath(myAss.Location);
            var myDir = Path.GetDirectoryName(myDll);
            var subDir = Path.Combine(myDir, name);
            return F.GetDir(subDir, create);
        }

        public static string GetEnvDir(string name, bool create)
        {
            var folder = Environment.GetEnvironmentVariable(name);
            return F.GetDir(folder, create);
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
            File.WriteAllText(file, text, TextExt.Win);
        }
    }
}