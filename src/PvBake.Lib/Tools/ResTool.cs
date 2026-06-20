using System;
using System.IO;
using System.Text;

namespace PvBake.Lib.Tools
{
    public static class ResTool
    {
        public static string GetEmbeddedText(this Type type, string name)
        {
            var ass = typeof(ResTool).Assembly;
            var nsp = type.Namespace;
            var full = $"{nsp}.{name}";
            using var stream = ass.GetManifestResourceStream(full);
            if (stream == null)
                return null;
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var text = reader.ReadToEnd();
            return text;
        }
    }
}