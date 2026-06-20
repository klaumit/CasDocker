using System;
using System.IO;
using System.Text;
using PvBake.Lib.Models;
using PvBake.Lib.Tools;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class Bioses
    {
        internal static Bios ReadX86Bios(string file)
        {
            var info = new FileInfo(file);
            if (info.Length is < 127 * 1024 or > 129 * 1024)
                return null;
            using var stream = File.OpenRead(file);
            return LoadX86Bios(stream);
        }

        internal static bool SaveX86Bios(Bios a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            b.Write(a.Sig);
            b.Write(a.Model.AsAscii());
            b.Write(a.Payload);
            return true;
        }

        internal static Bios LoadX86Bios(Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryReader(stream, enc);
            if (b.GetSafeBytes(8) is not { } magic)
                return null;
            if (magic.AsHex() != "45BA434153494F03")
                return null;
            if (b.GetSafeStr(4) is not { } modelStr)
                return null;
            if (modelStr.AsEnum<Model>() is not { } model)
                throw new InvalidOperationException(modelStr);
            const int biosLen = 128 * 1024;
            const int restLen = biosLen - 12;
            if (b.GetSafeBytes(restLen) is not { } pyl)
                return null;
            var o = new Bios
            {
                Sig = magic, Model = model, Length = biosLen, Payload = pyl
            };
            return o;
        }
    }
}