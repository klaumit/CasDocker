using System;
using System.IO;

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
    }
}