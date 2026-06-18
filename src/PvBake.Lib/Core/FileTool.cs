using PvBake.Lib.Models;
using static PvBake.Addins; 

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake
{
    public static class FileTool
    {
        public static IFile Detect(string file)
        {
            return ReadX86AddIn(file);
        }
    }
}