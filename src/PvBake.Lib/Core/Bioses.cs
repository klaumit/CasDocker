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
            var enc = Encoding.ASCII;
            using var stream = File.OpenRead(file);
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
            var o = new Bios
            {
                Sig = magic, Model = model, Length = biosLen
            };
            return o;
        }
    }
}