using PvBake.Lib.Models;
using static PvBake.Lib.Core.AddIns;
using static PvBake.Lib.Core.Bioses;
using static PvBake.Lib.Core.Dumps;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class FileTool
    {
        public static IFile Detect(string file)
        {
            if (ReadX86Dump(file) is { } dump)
                return dump;
            if (ReadX86Bios(file) is { } bios)
                return bios;
            if (ReadX86AddIn(file) is { } addIn)
                return addIn;
            return null;
        }
    }
}