using System.IO;
using System.Text;
using PvBake.Lib.Models;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class Dumps
    {
        internal static Dump ReadX86Dump(string file)
        {
            var info = new FileInfo(file);
            if (info.Length is < 1000 * 1024 or > 1300 * 1024)
                return null;
            var enc = Encoding.ASCII;
            using var stream = File.OpenRead(file);
            using var b = new BinaryReader(stream, enc);

            // TODO

            var dumpLen = (int)stream.Length;
            var o = new Dump
            {
                Length = dumpLen
            };
            return o;
        }
    }
}