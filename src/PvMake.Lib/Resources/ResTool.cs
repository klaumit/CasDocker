using System.IO;
using System.Text;

// ReSharper disable ConvertToUsingDeclaration

namespace PvMake.Lib.Resources
{
    public static class ResTool
    {
        public static Stream GetStream(string name)
        {
            var type = typeof(ResTool);
            var asm = type.Assembly;
            var full = type.Namespace + "." + name;
            return asm.GetManifestResourceStream(full);
        }

        public static string GetText(string name)
        {
            var enc = Encoding.UTF8;
            using (var stream = GetStream(name))
            {
                if (stream == null) return null;
                using (var reader = new StreamReader(stream, enc))
                {
                    var text = reader.ReadToEnd();
                    return text.TrimOrNull();
                }
            }
        }
    }
}