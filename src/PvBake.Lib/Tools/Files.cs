using System;
using System.IO;

namespace PvRanger
{
    public class Files
    {
        public static string GetRelativePath(string relativeTo, string path)
        {
            var dc = Path.DirectorySeparatorChar;
            var relativeUri = new Uri(relativeTo);
            var fileUri = new Uri(path);
            var relUri = relativeUri.MakeRelativeUri(fileUri).ToString();
            return Uri.UnescapeDataString(relUri).Replace('/', dc);
        }
    }
}