using System;
using System.IO;
using PvMake.Lib;
using W = PvMake.Lib.Writing;
using M = PvMake.Lib.Making;
using A = PvMake.Lib.Assembling;
using S = PvMake.Lib.Siming;
using B = PvMake.Core.Bases;
using System.Collections.Generic;
using K = PvMake.Lib.KnowIt;
using FileExt = Pva2cpa.Lib.Files;
using FileEx = PvMake.Lib.FileExt;

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
                Console.WriteLine(" * {0}", sdk);
                if (K.IsClassPad(sdk))
                    sdk = B.ClassPadAlias;

                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                if (K.IsHitachi(sdk))
                    PrepareHitachi(sdkDir, B.proj, B.inputDir);
                else if (K.IsIntel(sdk))
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

            var foundFiles = FileEx.FindAllFiles(inputDir);
            SortedSet<string> hFiles;
            foundFiles.TryGetValue(".h", out hFiles);
            SortedSet<string> cFiles;
            foundFiles.TryGetValue(".c", out cFiles);
            SortedSet<string> bFiles;
            foundFiles.TryGetValue(".bmp", out bFiles);

            FileExt.GetDir(Path.Combine(pDir, "ForDEBUG"), true);
            FileExt.GetDir(Path.Combine(cDir, "User_Bin"), true);
            FileExt.GetDir(Path.Combine(pDir, "OBJ"), true);

            var hit = new HitMe { PatchHit = false };
            
            var ccDir = FileExt.GetDir(Path.Combine(pDir, "C"), true);
            W.ReWrite(cFiles, ccDir, hit, cDir);

            var hhDir = FileExt.GetDir(Path.Combine(pDir, "H"), true);
            W.ReWrite(hFiles, hhDir, hit, cDir);

            var miDir = FileExt.GetDir(Path.Combine(pDir, "MENUICON"), true);
            W.ReCopy(bFiles, miDir, cDir);

            var mFile = Path.Combine(pDir, "Makefile");
            FileEx.WriteWin(mFile, M.CreateMakeFile(proj, hFiles, cFiles));

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

            var foundFiles = FileEx.FindAllFiles(inputDir);
            SortedSet<string> hFiles;
            foundFiles.TryGetValue(".h", out hFiles);
            SortedSet<string> cFiles;
            foundFiles.TryGetValue(".c", out cFiles);
            SortedSet<string> bFiles;
            foundFiles.TryGetValue(".bmp", out bFiles);

            FileExt.GetDir(Path.Combine(pDir, "Debug"), true);
            FileExt.GetDir(Path.Combine(pDir, "User_Bin"), true);
            FileExt.GetDir(Path.Combine(pDir, "Release"), true);

            var hit = new HitMe { PatchHit = true };

            var ccDir = FileExt.GetDir(Path.Combine(pDir, "SRC"), true);
            W.ReWrite(cFiles, ccDir, hit, sdkDir);

            var hhDir = FileExt.GetDir(Path.Combine(pDir, "DEF"), true);
            W.ReWrite(hFiles, hhDir, hit, sdkDir);

            var miDir = FileExt.GetDir(Path.Combine(pDir, "ICON"), true);
            W.ReCopy(bFiles, miDir, sdkDir);

            var mFile = Path.Combine(pDir, "sources.def");
            FileEx.WriteWin(mFile, M.CreateSrcDefFile(proj, cFiles));

            var sFile = Path.Combine(pDir, "PV3S1600.dlp");
            FileEx.WriteWin(sFile, S.CreatePv3Dlp(proj));

            if (hit.InlineAsm) A.FixHitachiAsm(pDir);
        }
    }
}