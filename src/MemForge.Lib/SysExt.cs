using System;
using System.IO;

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
    }
}