using System;
using System.IO;
using System.Threading.Tasks;
using PvMake.Models;
using PvMake.Resources;
using PvMake.Tools;
using static PvMake.Core.Making;
using static PvMake.Models.KnowIt;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class PreProc
    {
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

            var mkFile = Path.Combine(pDir, "sources.def");
            FileExt.WriteWin(mkFile, CreateSrcDefFile(p.AppTitle!, p.AppVer!, cFiles));

            var mbFile = Path.Combine(pDir, "BuildAll.bat");
            FileExt.WriteWin(mbFile, CreateBuildBat());

            _ = FileExt.GetDir(Path.Combine(pDir, "debug"));
            _ = FileExt.GetDir(Path.Combine(pDir, "user_bin"));

            var prjM = FileExt.GetDir(Path.Combine(pDir, "ICON"));
            Coding.ReCopy(bFiles, prjM);
            var prjC = FileExt.GetDir(Path.Combine(pDir, "SRC"));
            Coding.ReWrite(cFiles, prjC);
            var prjH = FileExt.GetDir(Path.Combine(pDir, "DEF"));
            Coding.ReWrite(hFiles, prjH);
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