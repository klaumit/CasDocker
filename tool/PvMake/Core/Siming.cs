using System.Collections.Generic;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Siming
    {
        internal static IEnumerable<string> CreatePv3Dlp(string title, string name)
        {
            var list = new List<string>();
            list.Add("[DLSimProject]");
            list.Add($"Name={title} (PVS1600)");
            list.Add("Model=..\\SIM\\PV-S1600.dlm");
            list.Add("SourcePath=.");
            list.Add("MemoryPath=..\\SIM\\MEM");
            list.Add("");
            list.Add("[Program1]");
            list.Add($"Program=user_bin\\{name}.abs");
            list.Add($"Debug=user_bin\\{name}.dbg");
            list.Add("LoadAddress=00000000:00000000");
            list.Add("");
            list.Add("[LoadInternal]");
            list.Add($"Line0=user_bin\\{name}.pva");
            list.Add("");
            return list;
        }

        internal static IEnumerable<string> CreatePv3Dlr()
        {
            var list = new List<string>();
            list.Add("[DLSimRunSpace]");
            list.Add("");
            return list;
        }

        internal static IEnumerable<string> CreatePv3Dlw()
        {
            var list = new List<string>();
            list.Add("[DLSimWorkSpace]");
            list.Add("");
            list.Add("[_1]");
            list.Add("Type=5");
            list.Add("Order=6");
            list.Add("Top=0");
            list.Add("Left=0");
            list.Add("Height=5610");
            list.Add("Width=7785");
            list.Add("State=0");
            list.Add("Flags=00000000");
            list.Add("OptionA=0");
            list.Add("");
            list.Add("[_2]");
            list.Add("Type=1");
            list.Add("Order=1");
            list.Add("Top=0");
            list.Add("Left=7800");
            list.Add("Height=3930");
            list.Add("Width=2970");
            list.Add("State=0");
            list.Add("Flags=00000001");
            list.Add("OptionA=0");
            list.Add("");
            list.Add("[_3]");
            list.Add("Type=6");
            list.Add("Order=2");
            list.Add("Top=3930");
            list.Add("Left=7800");
            list.Add("Height=2415");
            list.Add("Width=2970");
            list.Add("State=0");
            list.Add("Flags=00000001");
            list.Add("OptionA=0");
            list.Add("");
            list.Add("[_4]");
            list.Add("Type=7");
            list.Add("Order=5");
            list.Add("Top=5610");
            list.Add("Left=0");
            list.Add("Height=2400");
            list.Add("Width=7785");
            list.Add("State=0");
            list.Add("Flags=00000000");
            list.Add("OptionA=0");
            list.Add("");
            list.Add("[_5]");
            list.Add("Type=8");
            list.Add("Order=4");
            list.Add("Top=8010");
            list.Add("Left=0");
            list.Add("Height=1920");
            list.Add("Width=7785");
            list.Add("State=0");
            list.Add("Flags=00000000");
            list.Add("OptionA=0");
            list.Add("");
            list.Add("[_6]");
            list.Add("Type=3");
            list.Add("Order=0");
            list.Add("Top=0");
            list.Add("Left=10785");
            list.Add("Height=6345");
            list.Add("Width=2235");
            list.Add("State=0");
            list.Add("Flags=00000000");
            list.Add("OptionA=0");
            list.Add("");
            list.Add("[_7]");
            list.Add("Type=2");
            list.Add("Order=3");
            list.Add("Top=6345");
            list.Add("Left=7800");
            list.Add("Height=3585");
            list.Add("Width=5220");
            list.Add("State=0");
            list.Add("Flags=00000000");
            list.Add("OptionA=0");
            return list;
        }
    }
}