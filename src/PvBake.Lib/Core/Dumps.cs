using System;
using System.IO;
using System.Linq;
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
            if (info.Length is < 895 * 1024 or > 1300 * 1024)
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
            if (a.Blobs is { } blobs)
            {
                foreach (var pair in blobs)
                {
                    var offset = pair.Key;
                    stream.Position = offset;
                    var blob = pair.Value;
                    stream.Write(blob.Data, 0, blob.Data.Length);
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
            var lastEnd = -1;
            Tuple<int, Blob> lastBlob = null;
            for (var i = (int)stream.Position; i <= stream.Length; i += step)
            {
                stream.Position = i;
                if (stream.Position <= lastEnd)
                    continue;
                if (AddIns.LoadX86AddIn(new StayStream(stream)) is { } addIn)
                {
                    (o.AddIns ??= new()).Add(i, addIn);
                    lastEnd = (int)stream.Position;
                    continue;
                }
                stream.Position = i;
                var raw = b.GetSafeBytes(step);
                if (raw == null) continue;
                if (raw.All(x => x == 0xFF)) continue;
                if (lastBlob != null)
                {
                    var lastBlobIdx = lastBlob.Item1;
                    var lastBlobLen = (int)(lastBlob.Item2.Length ?? 0);
                    var diff = i - (lastBlobIdx + lastBlobLen);
                    if (diff == 0)
                    {
                        var lastBlobObj = lastBlob.Item2;
                        lastBlobObj.Length = (uint)(lastBlobLen + raw.Length);
                        lastBlobObj.Data = lastBlobObj.Data.Concat(raw).ToArray();
                        continue;
                    }
                }
                var blob = new Blob { Length = (uint)raw.Length, Data = raw };
                var bt = lastBlob = Tuple.Create(i, blob);
                (o.Blobs ??= new()).Add(bt.Item1, bt.Item2);
            }

            return o;
        }
    }
}