using System;
using System.IO;
using System.Threading.Tasks;
using PvMake.Models;
using PvMake.Resources;
using PvMake.Tools;
using static PvMake.Core.Making;
using static PvMake.Core.Siming;
using static PvMake.Models.KnowIt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PvMake.Tools;
using SimpleTextPreprocessor;
using SimpleTextPreprocessor.ExpressionSolver;
using SimpleTextPreprocessor.IncludeResolver;

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class PreProc
    {
        public static void ReCopy(IEnumerable<string>? files, string dest)
        {
            if (files == null)
                return;
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var bytes = File.ReadAllBytes(file);
                File.WriteAllBytes(tgt, bytes);
                Console.WriteLine($"    + {name} ({bytes.Length} B) => {tgt}");
            }
        }

        public static async Task Run(Options o)
        {
            var inputDir = FileExt.GetDir(o.InputDir);
            var outputDir = FileExt.GetDir(o.OutputDir);
            Console.WriteLine($"Input  = {inputDir}");
            Console.WriteLine($"Output = {outputDir}");

            var ini = await IniExt.ReadFile(Path.Combine(inputDir, Project.Fn));
            var proj = new Project(ini);

            var (d2M, m2D) = ResTool.ReadDirToModel();
            foreach (var (dirName, _) in d2M)
            {
                var dir = FileExt.GetDir(Path.Combine(outputDir, dirName));
                var local = Path.GetRelativePath(outputDir, dir);
                Console.WriteLine($" * {local}");

                if (IsHitachi(dirName))
                    RunForHitachi(proj, inputDir, dir);
                else
                    RunForIntel(proj, inputDir, dir);
            }

            Console.WriteLine("Done.");
        }

        private static void RunForHitachi(Project p, string inputDir, string dir)
        {
            var pDir = FileExt.GetDir(Path.Combine(dir, p.AppName!));

            var foundFiles = FileExt.FindAllFiles(inputDir);
            foundFiles.TryGetValue(".h", out var hFiles);
            foundFiles.TryGetValue(".c", out var cFiles);
            foundFiles.TryGetValue(".bmp", out var bFiles);

            var modelExtra = ResTool.GetDir("PV3S1600");
            var modelFiles = FileExt.FindAllFiles(modelExtra);
            modelFiles.TryGetValue(".mak", out var mFiles);

            var e1File = Path.Combine(pDir, "PV3S1600.dlr");
            FileExt.WriteWin(e1File, CreatePv3Dlr());

            var e2File = Path.Combine(pDir, "PV3S1600.dlp");
            FileExt.WriteWin(e2File, CreatePv3Dlp(p.AppTitle!, p.AppName!));

            var e3File = Path.Combine(pDir, "PV3S1600.dlw");
            FileExt.WriteWin(e3File, CreatePv3Dlw());

            var mkFile = Path.Combine(pDir, "sources.def");
            FileExt.WriteWin(mkFile, CreateSrcDefFile(p.AppTitle!, p.AppVer!, cFiles));

            var mbFile = Path.Combine(pDir, "BuildAll.bat");
            FileExt.WriteWin(mbFile, CreateBuildBat());

            _ = FileExt.GetDir(Path.Combine(pDir, "debug"));
            _ = FileExt.GetDir(Path.Combine(pDir, "user_bin"));

            var prjM = FileExt.GetDir(Path.Combine(pDir, "ICON"));
            Coding.ReCopy(bFiles, prjM);
            var prjC = FileExt.GetDir(Path.Combine(pDir, "SRC"));
            Coding.ReWrite(cFiles, prjC, true);
            var prjH = FileExt.GetDir(Path.Combine(pDir, "DEF"));
            Coding.ReWrite(hFiles, prjH, true);
            var prjS = FileExt.GetDir(Path.Combine(pDir, "make"));
            Coding.ReCopy(mFiles, prjS);
        }

        private static void RunForIntel(Project p, string inputDir, string dir)
        {
            var cDir = FileExt.GetDir(Path.Combine(dir, "C"));
            var pDir = FileExt.GetDir(Path.Combine(cDir, p.AppName!));

            var foundFiles = FileExt.FindAllFiles(inputDir);
            foundFiles.TryGetValue(".h", out var hFiles);
            foundFiles.TryGetValue(".c", out var cFiles);
            foundFiles.TryGetValue(".bmp", out var bFiles);

            var mkFile = Path.Combine(pDir, "Makefile");
            FileExt.WriteWin(mkFile, CreateMakeFile(p.AppTitle!, p.AppVer!, hFiles, cFiles));

            var mbFile = Path.Combine(pDir, "mk.bat");
            FileExt.WriteWin(mbFile, Making.CreateMakeBat());

            _ = FileExt.GetDir(Path.Combine(pDir, "ForDEBUG"));
            _ = FileExt.GetDir(Path.Combine(pDir, "OBJ"));

            var prjM = FileExt.GetDir(Path.Combine(pDir, "MENUICON"));
            Coding.ReCopy(bFiles, prjM);
            var prjC = FileExt.GetDir(Path.Combine(pDir, "C"));
            Coding.ReWrite(cFiles, prjC);
            var prjH = FileExt.GetDir(Path.Combine(pDir, "H"));
            Coding.ReWrite(hFiles, prjH);
        }
    }
}
