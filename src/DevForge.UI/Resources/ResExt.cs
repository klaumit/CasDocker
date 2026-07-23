using System.IO;
using System.Drawing;

namespace DevForge.UI.Resources
{
    public static class ResExt
    {
        public static Stream GetStream(string name)
        {
            var type = typeof(ResExt);
            var asm = type.Assembly;
            var fqn = type.Namespace + "." + name;
            var stream = asm.GetManifestResourceStream(fqn);
            return stream;
        }

        public static Bitmap ToImage(this Stream stream)
        {
            var img = new Bitmap(stream);
            return img;
        }

        public static Icon ToIcon(this Stream stream)
        {
            var img = new Icon(stream);
            return img;
        }
    }
}