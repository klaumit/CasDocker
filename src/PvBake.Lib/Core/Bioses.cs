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
            b.Write(a.Payload[0]);
            b.Write(OsHeadMark.GetPlainBytes());
            b.Write(a.Payload[1]);
            b.Write(a.Compiled.GetValueOrDefault().AsAscii(noTime: true));
            b.Write(a.SwModel.AsAscii());
            b.Write(a.Payload[2]);
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
            if (b.GetSafeBytes(65388) is not { } pyl1)
                return null;
            if (b.GetSafeStr(8) is not { } headMrk)
                return null;
            if (headMrk is not OsHeadMark)
                return null;
            if (b.GetSafeBytes(8) is not { } pyl2)
                return null;
            if (b.GetSafeStr(8) is not { } compDateStr)
                return null;
            if (compDateStr.AsDate() is not { } bioCompiled)
                return null;
            if (b.GetSafeStr(4) is not { } sModelStr)
                return null;
            if (sModelStr.AsEnum<Model>() is not { } sModel)
                throw new InvalidOperationException(sModelStr);
            var biosLen = Math.Min(128 * 1024, (int)stream.Length);
            var restLen = biosLen - OsHeadSize;
            if (b.GetSafeBytes(restLen) is not { } pyl3)
                return null;
            var o = new Bios
            {
                Sig = magic, Model = model, Length = (uint)biosLen,
                Compiled = bioCompiled, SwModel = sModel,
                Payload = [pyl1, pyl2, pyl3]
            };
            return o;
        }

        public const int OsHeadSize = 65428;
        public const string OsHeadMark = "OSHEADER";
    }
}