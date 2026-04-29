using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Making
    {
        internal static IEnumerable<string> CreateSrcDefFile(string target, string ver,
            IEnumerable<string>? cs)
        {
            var list = new List<string>();
            list.Add("#########################################################################");
            list.Add("#	Application Independent Definitions");
            list.Add("#########################################################################");
            list.Add("");
            list.Add("#########################################################################");
            list.Add("#	Application							#");
            list.Add("");
            list.Add("# use this name when a script creates *.pva,*.abs and *.dbg");
            list.Add($"TARGET={target}");
            list.Add("");
            list.Add("# PV Application Title (This name is displayed on PV Menu)");
            list.Add($"TITLE=\"{target}\"");
            list.Add("");
            list.Add("# ProgramVersion(EX. 0100->Ver1.00)");
            list.Add($"VERSION = {ver}");
            list.Add("");
            list.Add(@"# Application Sources (src\*.c)");
            var i = 0;
            foreach (var c in cs?.Reverse() ?? [])
            {
                var cFn = Path.GetFileNameWithoutExtension(c);
                list.Add($"FILE{i++}={cFn}");
            }
            list.Add("");
            list.Add("#									#");
            list.Add("#########################################################################");
            return list;
        }

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

        internal static IEnumerable<string> CreateBuildBat()
        {
            var list = new List<string>();
            list.Add("echo off");
            list.Add(@"CALL ..\PathSET.BAT");
            list.Add(@"CALL ..\TOOLS\BuildAll.BAT > build.log");
            list.Add("if ERRORLEVEL 1 goto builderr");
            list.Add("goto batchexit");
            list.Add(":builderr");
            list.Add("echo ############################");
            list.Add("echo       Build Error!!");
            list.Add("echo   Please Read \"Build.Log\"");
            list.Add("echo ############################");
            list.Add("pause");
            list.Add(":batchexit");
            return list;
        }
    }
}