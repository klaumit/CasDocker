using System.Collections.Generic;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Making
    {
        internal static IEnumerable<string> CreateMakeFile(string target)
        {
            var list = new List<string>();
            list.Add("#Makefile for PocketViewer Sample Program");
            list.Add("");
            list.Add(@"include ..\COM_LNK\MakeSDK.1");
            list.Add("");
            list.Add("### -------- Define Make Application -------- ###");
            list.Add("");
            list.Add("#== TargetName ==");
            list.Add("TARGET  = " + target);
            list.Add("");
            list.Add("#== Program Name ==");
            list.Add($"PROGNAME = \"{target}\"");
            list.Add("");
            list.Add("#== ProgramVersion(EX. 0100->Ver1.00) ==");
            list.Add("");
            list.Add("#== MenuIcon (Xsize=45dot,Ysize=28dot) ==");
            list.Add("");
            list.Add("#== ListMenuIcon (Xsize=27dot,Ysize=20dot) ==");
            list.Add("");
            list.Add("#== CompileObjectFile ==");
            list.Add("");
            list.Add("#== IncludeHeaderFile ==");
            list.Add("");
            list.Add("### ----------------------------------------- ###");
            list.Add("");
            list.Add(@"include ..\COM_LNK\MakeSDK.2");
            list.Add("");
            return list;
        }
    }
}