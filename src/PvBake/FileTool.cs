using System;
using System.IO;
using System.Text;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvRanger
{
    public static class FileTool
    {
        public static IFile? Detect(string file)
        {
            return ReadX86AddIn(file);
        }

        private static AddIn ReadX86AddIn(string file)
        {
            var enc = Encoding.ASCII;
            using var stream = File.OpenRead(file);
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
            var o = new AddIn
            {
                Sig = magic, Model = model, HeadVersion = headVer, Status = status,
                Mode = mode, Name = addInName, Length = addInLen, AppCompiled = appCompiled,
                AppVersion = appVer, LibCompiled = libCompiled, LibVersion = libVer,
                MenuIcon = offsIcon, ListIcon = offsLIcon, Comment = comment
            };
            return o;
        }
    }
}