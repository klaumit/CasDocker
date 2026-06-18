using PvBake.Lib.Models;
using static PvBake.Lib.Core.Addins; 

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class FileTool
    {
        public static IFile Detect(string file)
        {
            return ReadX86AddIn(file);
        }
    }
}