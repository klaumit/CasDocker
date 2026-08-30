using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MemForge.Lib
{
    public static class SysExt
    {
        public static string GetSrcRoot()
        {
            var folder = Environment.CurrentDirectory;
            var term = Path.DirectorySeparatorChar + "src";
            var tmp = folder.Split(new[] { term }, 2, StringSplitOptions.None);
            var full = Path.GetFullPath(tmp[0]);
            return full;
        }

        public static Dictionary<string, string> GetSimExes(string root)
        {
            var l = Path.DirectorySeparatorChar;
            // var pt = string.Format("src{0}PvMake{0}bin{0}Debug{0}net40{0}_pv", l);
            var pt = string.Format("src{0}win_build{0}_pv", l);
            var dir = Path.Combine(root, pt);
            const SearchOption so = SearchOption.AllDirectories;
            var dict = new Dictionary<string, string>();
            foreach (var file in Directory.EnumerateFiles(dir, "*Sim*.exe", so))
            {
                var name = Path.GetFileNameWithoutExtension(file).Split(' ').Last();
                var key = name.Replace("3022", "86");
                dict[key] = file;
            }
            return dict;
        }
    }
}