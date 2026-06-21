using System.IO;
using System.Text;
using PvBake.Lib.Models;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class Icons
    {
        internal static Icon ReadX86Icon(string file)
        {
            var info = new FileInfo(file);
            if (info.Length is < 2867 or > 126 * 1024)
                return null;
            using var stream = File.OpenRead(file);
            return LoadX86Icon(stream);
        }

        internal static bool SaveX86Icon(Icon a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            return true;
        }

        internal static Icon LoadX86Icon(Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryReader(stream, enc);
            var o = new Icon
            {

            };
            return o;
        }
    }
}