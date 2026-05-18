using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable UseStringInterpolation
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Lib
{
    public static class Making
    {
        public static IEnumerable<string> CreateSrcDefFile(Project p,
            IEnumerable<string> cs)
        {
            var target = p.AppName;
            var title = p.AppTitle;
            var ver = p.AppVer;

            var list = new List<string>();
            list.Add("#########################################################################");
            list.Add("#	Application Independent Definitions");
            list.Add("#########################################################################");
            list.Add("");
            list.Add("#########################################################################");
            list.Add("#	Application							#");
            list.Add("");
            list.Add("# use this name when a script creates *.pva,*.abs and *.dbg");
            list.Add(string.Format("TARGET={0}", target));
            list.Add("");
            list.Add("# PV Application Title (This name is displayed on PV Menu)");
            list.Add(string.Format("TITLE=\"{0}\"", title));
            list.Add("");
            list.Add("# ProgramVersion(EX. 0100->Ver1.00)");
            list.Add(string.Format("VERSION = {0}", ver));
            list.Add("");
            list.Add(@"# Application Sources (src\*.c)");
            var i = 0;
            foreach (var c in cs.Reverse())
            {
                var cFn = Path.GetFileNameWithoutExtension(c);
                list.Add(string.Format("FILE{0}={1}", i++, cFn));
            }
            list.Add("");
            list.Add("#									#");
            list.Add("#########################################################################");
            return list;
        }

        public static IEnumerable<string> CreateMakeFile(Project p,
            IEnumerable<string> hs, IEnumerable<string> cs)
        {
            var target = p.AppName;
            var title = p.AppTitle;
            var ver = p.AppVer;

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
            list.Add(string.Format("PROGNAME = \"{0}\"", title));
            list.Add("");
            list.Add("#== ProgramVersion(EX. 0100->Ver1.00) ==");
            list.Add(string.Format("VERSION = {0}", ver));
            list.Add("");
            list.Add("#== MenuIcon (Xsize=45dot,Ysize=28dot) ==");
            list.Add("MICON = menuicon\\icon.bmp");
            list.Add("");
            list.Add("#== ListMenuIcon (Xsize=27dot,Ysize=20dot) ==");
            list.Add("LICON = menuicon\\Licon.bmp");
            list.Add("");
            list.Add("#== CompileObjectFile ==");
            var cTxt = string.Join("  \\\n\t\t", cs.Reverse().Select(c =>
                string.Format("$(ODIR)\\{0}", Path.GetFileName(c).Replace(".c", ".obj"))));
            list.Add(string.Format("APLOBJS =\t{0}", cTxt));
            list.Add("");
            list.Add("#== IncludeHeaderFile ==");
            var hTxt = string.Join(" \\\n\t\t", hs.Reverse().Select(h =>
                string.Format("$(HDIR)\\{0}", Path.GetFileName(h))));
            list.Add(string.Format("HEADFILE = \t{0}", hTxt));
            list.Add("");
            list.Add("### ----------------------------------------- ###");
            list.Add("");
            list.Add(@"include ..\COM_LNK\MakeSDK.2");
            list.Add("");
            list.Add("");
            list.Add("");
            return list;
        }
    }
}