using System;
using System.IO;
using System.Threading.Tasks;
using PvMake.Resources;
using PvMake.Tools;
using static PvMake.Core.Making;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class PreProc
    {
        private const string App = "Application";
        private const string Nom = "Name";
        private const string Tit = "Title";
        private const string Ver = "Version";
        private const string Prj = "project.ini";

        public static async Task Run(Options o)
        {
            var inputDir = FileExt.GetDir(o.InputDir);
            var outputDir = FileExt.GetDir(o.OutputDir);
            Console.WriteLine($"Input  = {inputDir}");
            Console.WriteLine($"Output = {outputDir}");

            var ini = await IniExt.ReadFile(Path.Combine(inputDir, Prj));
            var appName = ini.GetSetting(App, Nom);
            var appTitle = ini.GetSetting(App, Tit);
            var appVer = ini.GetSetting(App, Ver);

            var (d2M, m2D) = ResTool.ReadDirToModel();
            foreach (var (dirName, _) in d2M)
            {
                var dir = FileExt.GetDir(Path.Combine(outputDir, dirName));
                var local = Path.GetRelativePath(outputDir, dir);
                Console.WriteLine($" * {local}");

                var cDir = FileExt.GetDir(Path.Combine(dir, "C"));
                var pDir = FileExt.GetDir(Path.Combine(cDir, appName!));

                var foundFiles = FileExt.FindAllFiles(inputDir);
                foundFiles.TryGetValue(".h", out var hFiles);
                foundFiles.TryGetValue(".c", out var cFiles);
                foundFiles.TryGetValue(".bmp", out var bFiles);

                var mkFile = Path.Combine(pDir, "Makefile");
                FileExt.WriteWin(mkFile, CreateMakeFile(appTitle!, appVer!, hFiles, cFiles));

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

            Console.WriteLine("Done.");
        }
    }
}