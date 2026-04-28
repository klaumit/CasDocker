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

        public static void WriteTo(string file, IEnumerable<string> lines)
        {
            File.WriteAllLines(file, lines, Encoding.UTF8);
        }
    }
}