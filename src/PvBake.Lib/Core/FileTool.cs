using System.IO;
using PvBake.Lib.Models;
using static PvBake.Lib.Core.AddIns;
using static PvBake.Lib.Core.Bioses;
using static PvBake.Lib.Core.Dumps;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class FileTool
    {
        public static IFile Read(string file)
        {
            if (ReadX86Dump(file) is { } dump)
                return dump;
            if (ReadX86Bios(file) is { } bios)
                return bios;
            if (ReadX86AddIn(file) is { } addIn)
                return addIn;
            return null;
        }

        public static bool Write(IFile file, Stream stream)
        {
            // if (file is Dump { } dump)
            //    return SaveX86Dump(dump, stream);
            if (file is Bios { } bios)
                return SaveX86Bios(bios, stream);
            if (file is AddIn { } addIn)
                return SaveX86AddIn(addIn, stream);
            return false;
        }
    }
}