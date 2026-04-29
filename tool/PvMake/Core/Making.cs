using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Making
    {
        internal static IEnumerable<string> CreateMakeFile(string target, string ver,
            IEnumerable<string>? hs, IEnumerable<string>? cs)
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
            list.Add($"VERSION = {ver}");
            list.Add("");
            list.Add("#== MenuIcon (Xsize=45dot,Ysize=28dot) ==");
            list.Add("MICON = menuicon\\icon.bmp");
            list.Add("");
            list.Add("#== ListMenuIcon (Xsize=27dot,Ysize=20dot) ==");
            list.Add("LICON = menuicon\\Licon.bmp");
            list.Add("");
            list.Add("#== CompileObjectFile ==");
            var cTxt = string.Join("  \\\n\t\t", cs?.Reverse().Select(c =>
                $"$(ODIR)\\{Path.GetFileName(c).Replace(".c", ".obj")}") ?? []);
            list.Add($"APLOBJS =\t{cTxt}");
            list.Add("");
            list.Add("#== IncludeHeaderFile ==");
            var hTxt = string.Join(" \\\n\t\t", hs?.Reverse().Select(h =>
                $"$(HDIR)\\{Path.GetFileName(h)}") ?? []);
            list.Add($"HEADFILE = \t{hTxt}");
            list.Add("");
            list.Add("### ----------------------------------------- ###");
            list.Add("");
            list.Add(@"include ..\COM_LNK\MakeSDK.2");
            list.Add("");
            list.Add("");
            list.Add("");
            return list;
        }

        internal static IEnumerable<string> CreateMakeBat()
        {
            var list = new List<string>();
            list.Add("@echo off");
            list.Add(@"CALL ..\..\PATHSET.BAT");
            list.Add(@"@echo\");
            list.Add("@echo --- MAKE START ---");
            list.Add("kmmake PMODEL=1");
            list.Add("if exist make.i del make.i");
            list.Add("@echo --- MAKE START (ForDEBUG)---");
            list.Add("kmmake PMODEL=1 DEBUG=1 > err");
            list.Add("if exist make.i del make.i");
            list.Add("pause");
            list.Add("");
            list.Add("");
            return list;
        }
    }
}