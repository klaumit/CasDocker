using System.IO;
using System.Drawing;

namespace DevForge.Resources
{
    public static class ResExt
    {
        internal static Stream GetStream(string name)
        {
            var type = typeof(ResExt);
            var asm = type.Assembly;
            var fqn = type.Namespace + "." + name;
            var stream = asm.GetManifestResourceStream(fqn);
            return stream;
        }

        internal static Bitmap ToImage(this Stream stream)
        {
            var img = new Bitmap(stream);
            return img;
        }

        internal static Icon ToIcon(this Stream stream)
        {
            var img = new Icon(stream);
            return img;
        }
    }
}