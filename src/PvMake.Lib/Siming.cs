using System.Collections.Generic;

// ReSharper disable UseStringInterpolation
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Lib
{
    public static class Siming
    {
        public static IEnumerable<string> CreatePv3Dlp(Project p)
        {
            var title = p.AppTitle;
            var name = p.AppName;

            var list = new List<string>();
            list.Add("[DLSimProject]");
            list.Add(string.Format("Name={0} (PVS1600)", title));
            list.Add("Model=..\\SIM\\PV-S1600.dlm");
            list.Add("SourcePath=.");
            list.Add("MemoryPath=..\\SIM\\MEM");
            list.Add("");
            list.Add("[Program1]");
            list.Add(string.Format("Program=user_bin\\{0}.abs", name));
            list.Add(string.Format("Debug=user_bin\\{0}.dbg", name));
            list.Add("LoadAddress=00000000:00000000");
            list.Add("");
            list.Add("[LoadInternal]");
            list.Add(string.Format("Line0=user_bin\\{0}.pva", name));
            list.Add("");
            return list;
        }   
    }
}