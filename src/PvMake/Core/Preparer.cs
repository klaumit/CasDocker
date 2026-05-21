using System;
using System.IO;
using PvMake.Lib;
using W = PvMake.Lib.Writing;
using M = PvMake.Lib.Making;
using S = PvMake.Lib.Siming;
using B = PvMake.Core.Bases;
using System.Collections.Generic;

// ReSharper disable InlineOutVariableDeclaration

namespace PvMake.Core
{
    public static class Preparer
    {
        public static void Run(IOptions o)
        {
            B.LoadAndPrepareProject(o);

            foreach (var item in B.sdks)
            {
                var sdk = item.Sdk;
                var isHitachi = KnowIt.IsHitachi(sdk);
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                Console.WriteLine(" * {0}", sdk);
                if (isHitachi)
                    PrepareHitachi(sdkDir, B.proj, B.inputDir);
                else
                    PrepareIntel(sdkDir, B.proj, B.inputDir, item);
            }

            Console.WriteLine("Done.");
        }

        private static void PrepareIntel(string sdkDir, Project proj, string inputDir, Model m)
        {
            var cDir = FileExt.GetDir(Path.Combine(sdkDir, "C"), false);
            var pDir = FileExt.GetDir(Path.Combine(cDir, proj.AppName), true);

            var zipFile = Path.Combine(B.archRepo, "LSIJ_proj" + ".tar.gz");
            ZipExt.Uncompress(zipFile, pDir);

            var foundFiles = FileExt.FindAllFiles(inputDir);
            SortedSet<string> hFiles;
            foundFiles.TryGetValue(".h", out hFiles);
            SortedSet<string> cFiles;
            foundFiles.TryGetValue(".c", out cFiles);
            SortedSet<string> bFiles;
            foundFiles.TryGetValue(".bmp", out bFiles);

            FileExt.GetDir(Path.Combine(pDir, "ForDEBUG"), true);
            FileExt.GetDir(Path.Combine(cDir, "User_Bin"), true);
            FileExt.GetDir(Path.Combine(pDir, "OBJ"), true);

            var ccDir = FileExt.GetDir(Path.Combine(pDir, "C"), true);
            W.ReWrite(cFiles, ccDir, false, cDir);

            var hhDir = FileExt.GetDir(Path.Combine(pDir, "H"), true);
            W.ReWrite(hFiles, hhDir, false, cDir);

            var miDir = FileExt.GetDir(Path.Combine(pDir, "MENUICON"), true);
            W.ReCopy(bFiles, miDir, cDir);

            var mFile = Path.Combine(pDir, "Makefile");
            FileExt.WriteWin(mFile, M.CreateMakeFile(proj, hFiles, cFiles));

            var simDir = Path.Combine(sdkDir, "SIM");
            var cpjTpl = Path.Combine(simDir, m.Mod + ".CPJ");
            W.ReCopy(new[] { cpjTpl }, pDir, cDir);

            var root = Path.GetDirectoryName(sdkDir);
            var sFile = Path.Combine(pDir, m.Mod + ".CPJ");
            Locating.FixText(sFile,
                Tuple.Create(@"C:\CASIO", root),
                Tuple.Create(@"\SAMPLE.BIN", '\\' + proj.AppName + ".BIN")
            );
        }

        private static void PrepareHitachi(string sdkDir, Project proj, string inputDir)
        {
            var pDir = FileExt.GetDir(Path.Combine(sdkDir, proj.AppName), true);

            var zipFile = Path.Combine(B.archRepo, "SHC_proj" + ".tar.gz");
            ZipExt.Uncompress(zipFile, pDir);

            var foundFiles = FileExt.FindAllFiles(inputDir);
            SortedSet<string> hFiles;
            foundFiles.TryGetValue(".h", out hFiles);
            SortedSet<string> cFiles;
            foundFiles.TryGetValue(".c", out cFiles);
            SortedSet<string> bFiles;
            foundFiles.TryGetValue(".bmp", out bFiles);

            FileExt.GetDir(Path.Combine(pDir, "Debug"), true);
            FileExt.GetDir(Path.Combine(pDir, "User_Bin"), true);
            FileExt.GetDir(Path.Combine(pDir, "Release"), true);

            var ccDir = FileExt.GetDir(Path.Combine(pDir, "SRC"), true);
            W.ReWrite(cFiles, ccDir, true, sdkDir);

            var hhDir = FileExt.GetDir(Path.Combine(pDir, "DEF"), true);
            W.ReWrite(hFiles, hhDir, true, sdkDir);

            var miDir = FileExt.GetDir(Path.Combine(pDir, "ICON"), true);
            W.ReCopy(bFiles, miDir, sdkDir);

            var mFile = Path.Combine(pDir, "sources.def");
            FileExt.WriteWin(mFile, M.CreateSrcDefFile(proj, cFiles));

            var sFile = Path.Combine(pDir, "PV3S1600.dlp");
            FileExt.WriteWin(sFile, S.CreatePv3Dlp(proj));
        }
    }
}