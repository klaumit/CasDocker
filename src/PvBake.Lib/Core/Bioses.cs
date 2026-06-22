using System;
using System.IO;
using System.Linq;
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
            if (info.Length is < 111 * 1024 or > 129 * 1024)
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
            if (b.GetSafeBytes(65388) is not { } pay1)
                return null;
            if (b.GetSafeStr(8) is not { } headMrk)
                return null;
            if (headMrk is not "OSHEADER")
                return null;
            if (b.GetSafeBytes(8) is not { } pay2)
                return null;
            if (b.GetSafeStr(8) is not { } compDateStr)
                return null;
            Console.WriteLine(compDateStr);
            if (compDateStr.AsDate() is not { } bioCompiled)
                return null;
            if (b.GetSafeStr(4) is not { } sModelStr)
                return null;
            Console.WriteLine(sModelStr);
            if (sModelStr.AsEnum<Model>() is not { } sModel)
                throw new InvalidOperationException(sModelStr);
            var biosLen = Math.Min(128 * 1024, (int)stream.Length);
            var restLen = biosLen - 12;
            if (b.GetSafeBytes(restLen) is not { } pyl)
                return null;
            var o = new Bios
            {
                Sig = magic, Model = model, Length = (uint)biosLen,
                Compiled = bioCompiled, SwModel = sModel, Payload = pyl
            };
            return o;
        }
    }
}