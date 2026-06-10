using System;
using System.IO;

namespace Pva2cpa.Lib
{
    public static class Files
    {
        public static string GetDir(string folder, bool create)
        {
            if (Strings.IsNullOrWhiteSpace(folder)) return null;
            folder = Environment.ExpandEnvironmentVariables(folder ?? "");
            var dir = Path.GetFullPath(folder);
            if (create && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}