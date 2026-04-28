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

        public static string[] Find(string root, string filter)
        {
            var files = Directory.GetFiles(root, filter, SearchOption.AllDirectories);
            return files;
        }
    }
}