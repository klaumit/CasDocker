using System.IO;
using System.Text;
using PvBake.Lib.Models;
using PvBake.Lib.Tools;

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
            using var stream = File.OpenRead(file);
            return LoadX86Dump(stream);
        }

        internal static bool SaveX86Dump(Dump a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            b.Write(ByteTool.Allocate(0xFF, a.Length));
            stream.Position = 0;
            if (a.Bios is { } bios)
            {
                Bioses.SaveX86Bios(bios, new StayStream(stream));
            }
            if (a.AddIns is { } addIns)
            {
                foreach (var pair in addIns)
                {
                    var offset = pair.Key;
                    stream.Position = offset;
                    var addIn = pair.Value;
                    AddIns.SaveX86AddIn(addIn, new StayStream(stream));
                }
            }
            return true;
        }

        internal static Dump LoadX86Dump(Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryReader(stream, enc);

            var dumpLen = (int)stream.Length;
            var o = new Dump { Length = dumpLen };

            var pos = stream.Position;
            if (Bioses.LoadX86Bios(new StayStream(stream)) is { } bios)
                o.Bios = bios;
            else
                stream.Position = pos;

            const int step = 512;
            for (var i = 0; i < stream.Length; i += step)
            {
                stream.Position = i;
                if (AddIns.LoadX86AddIn(new StayStream(stream)) is { } addIn)
                    (o.AddIns ??= new()).Add(i, addIn);
            }

            return o;
        }
    }
}