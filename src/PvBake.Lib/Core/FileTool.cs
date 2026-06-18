using PvBake.Lib.Models;
using static PvBake.Lib.Core.Addins;
using static PvBake.Lib.Core.Bioses; 

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class FileTool
    {
        public static IFile Detect(string file)
        {
            if (ReadX86AddIn(file) is { } addIn)
                return addIn;
            if (ReadX86Bios(file) is { } bios)
                return bios;
            return null;
        }
    }
}