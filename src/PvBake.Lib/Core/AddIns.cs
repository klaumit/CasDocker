using System;
using System.IO;
using System.Linq;
using System.Text;
using PvBake.Lib.Models;
using PvBake.Lib.Tools;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class AddIns
    {
        internal static AddIn ReadX86AddIn(string file)
        {
            var info = new FileInfo(file);
            if (info.Length is < 2867 or > 126 * 1024)
                return null;
            using var stream = File.OpenRead(file);
            return LoadX86AddIn(stream);
        }

        internal static bool SaveX86AddIn(AddIn a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            b.Write(a.Sig);
            b.Write(a.Model.AsAscii());
            b.Write(a.HeadVersion.AsAscii());
            b.Write(a.Status.GetValueOrDefault());
            b.Write(a.Mode.GetValueOrDefault());
            b.Write(a.Name.AsAscii(length: 16, endMark: true));
            b.Write(a.Length.GetValueOrDefault());
            b.Write(a.AppCompiled.GetValueOrDefault().AsAscii());
            b.Write(a.AppVersion.AsAscii());
            b.Write(a.LibCompiled.GetValueOrDefault().AsAscii());
            b.Write(a.LibVersion.AsAscii());
            b.Write(a.MenuIcon.GetValueOrDefault());
            b.Write(a.ListIcon.GetValueOrDefault());
            b.Write(a.Comment.AsAscii(length: 64, endMark: true));
            b.Write(a.Payload);
            return true;
        }

        internal static AddIn LoadX86AddIn(Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryReader(stream, enc);
            if (b.GetSafeBytes(8) is not { } magic)
                return null;
            if (magic.AsHex() != "00FF434153494F03")
                return null;
            if (b.GetSafeStr(4) is not { } modelStr)
                return null;
            if (modelStr.AsEnum<Model>() is not { } model)
                throw new InvalidOperationException(modelStr);
            if (b.GetSafeStr(4) is not { } headVerStr)
                return null;
            if (headVerStr.AsVer() is not { } headVer)
                return null;
            if (b.GetSafeUInt16() is not { } status)
                return null;
            if (b.GetSafeUInt16() is not { } mode)
                return null;
            if (b.GetSafeStr(16).FixStr() is not { } addInName)
                return null;
            if (b.GetSafeUInt32() is not { } addInLen)
                return null;
            if (b.GetSafeStr(8) is not { } compDateStr)
                return null;
            if (b.GetSafeStr(4) is not { } compTimeStr)
                return null;
            if (Tuple.Create(compDateStr, compTimeStr).AsDateTime() is not { } appCompiled)
                return null;
            if (b.GetSafeStr(4) is not { } versionStr)
                return null;
            if (versionStr.AsVer() is not { } appVer)
                return null;
            if (b.GetSafeStr(8) is not { } libDateStr)
                return null;
            if (b.GetSafeStr(4) is not { } libTimeStr)
                return null;
            if (Tuple.Create(libDateStr, libTimeStr).AsDateTime() is not { } libCompiled)
                return null;
            if (b.GetSafeStr(4) is not { } libVerStr)
                return null;
            if (libVerStr.AsVer() is not { } libVer)
                return null;
            if (b.GetSafeUInt32() is not { } offsIcon)
                return null;
            if (b.GetSafeUInt32() is not { } offsLIcon)
                return null;
            if (b.GetSafeStr(64).FixStr() is not { } comment)
                return null;
            var restLen = (int)addInLen - 144;
            if (b.GetSafeBytes(restLen) is not { } pyl)
                return null;
            var rest = stream.Length - stream.Position;
            if (rest <= 6 * 1024)
            {
                if (b.GetSafeBytes(14) is { } ffs && ffs.All(x => x == 0xff))
                {
                    pyl = pyl.Concat(ffs).ToArray();
                    if (b.GetSafeBytes((int)(rest - ffs.Length)) is { } ext)
                        pyl = pyl.Concat(ext).ToArray();
                }
            }
            var o = new AddIn
            {
                Sig = magic, Model = model, HeadVersion = headVer, Status = status,
                Mode = mode, Name = addInName, Length = addInLen, AppCompiled = appCompiled,
                AppVersion = appVer, LibCompiled = libCompiled, LibVersion = libVer,
                MenuIcon = offsIcon, ListIcon = offsLIcon, Comment = comment, Payload = pyl
            };
            return o;
        }
    }
}