using System.IO;
using System.Text;
using PvBake.Lib.Models;
using PvBake.Lib.Tools;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class Icons
    {
        internal static Icon ReadX86Icon(string file)
        {
            var info = new FileInfo(file);
            if (info.Length is < 83 or > 173)
                return null;
            using var stream = File.OpenRead(file);
            return LoadX86Icon(stream);
        }

        internal static bool SaveX86Icon(Icon a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            b.Write(a.Width ?? 0);
            b.Write(a.Height ?? 0);
            return true;
        }

        internal static Icon LoadX86Icon(Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryReader(stream, enc);
            if (b.GetSafeUInt16() is not { } width)
                return null;
            if (b.GetSafeUInt16() is not { } height)
                return null;
            var o = new Icon
            {
                Width = width, Height = height
            };
            return o;
        }
    }
}