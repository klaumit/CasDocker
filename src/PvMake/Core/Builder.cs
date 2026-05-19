using System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PvMake.Lib;
using System.IO;
using W = PvMake.Lib.Writing;
using M = PvMake.Lib.Making;
using S = PvMake.Lib.Siming;
using B = PvMake.Core.Bases;

namespace PvMake.Core
{
    public static class Builder
    {
        public static void Run(IOptions o)
        {
            B.LoadAndPrepareProject(o);

            foreach (var sdk in B.sdks)
            {
                var isHitachi = KnowIt.IsHitachi(sdk);
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                if (isHitachi)
                    PrepareHitachi(sdkDir, B.proj, B.inputDir);
                else
                    PrepareIntel(sdkDir, B.proj, B.inputDir);
            }

            Console.WriteLine("Done.");
        }

        private static void PrepareIntel(string sdkDir, Project proj, string inputDir)
        {
            var cDir = FileExt.GetDir(Path.Combine(sdkDir, "C"), false);
            var pDir = FileExt.GetDir(Path.Combine(cDir, proj.AppName), true);

            var zipFile = Path.Combine(B.archRepo, "LSIJ_proj" + ".tar.gz");
            ZipExt.Uncompress(zipFile, pDir);

            var foundFiles = FileExt.FindAllFiles(inputDir);
            foundFiles.TryGetValue(".h", out var hFiles);
            foundFiles.TryGetValue(".c", out var cFiles);
            foundFiles.TryGetValue(".bmp", out var bFiles);

            _ = FileExt.GetDir(Path.Combine(pDir, "ForDEBUG"), true);
            _ = FileExt.GetDir(Path.Combine(cDir, "User_Bin"), true);
            _ = FileExt.GetDir(Path.Combine(pDir, "OBJ"), true);

            var ccDir = FileExt.GetDir(Path.Combine(pDir, "C"), true);
            W.ReWrite(cFiles, ccDir);

            var hhDir = FileExt.GetDir(Path.Combine(pDir, "H"), true);
            W.ReWrite(hFiles, hhDir);

            var miDir = FileExt.GetDir(Path.Combine(pDir, "MENUICON"), true);
            W.ReCopy(bFiles, miDir);

            var mFile = Path.Combine(pDir, "Makefile");
            FileExt.WriteWin(mFile, M.CreateMakeFile(proj, hFiles, cFiles));
        }

        private static void PrepareHitachi(string sdkDir, Project proj, string inputDir)
        {
            var pDir = FileExt.GetDir(Path.Combine(sdkDir, proj.AppName), true);

            var zipFile = Path.Combine(B.archRepo, "SHC_proj" + ".tar.gz");
            ZipExt.Uncompress(zipFile, pDir);

            var foundFiles = FileExt.FindAllFiles(inputDir);
            foundFiles.TryGetValue(".h", out var hFiles);
            foundFiles.TryGetValue(".c", out var cFiles);
            foundFiles.TryGetValue(".bmp", out var bFiles);

            _ = FileExt.GetDir(Path.Combine(pDir, "Debug"), true);
            _ = FileExt.GetDir(Path.Combine(pDir, "User_Bin"), true);
            _ = FileExt.GetDir(Path.Combine(pDir, "Release"), true);

            var ccDir = FileExt.GetDir(Path.Combine(pDir, "SRC"), true);
            W.ReWrite(cFiles, ccDir, true);

            var hhDir = FileExt.GetDir(Path.Combine(pDir, "DEF"), true);
            W.ReWrite(hFiles, hhDir, true);

            var miDir = FileExt.GetDir(Path.Combine(pDir, "ICON"), true);
            W.ReCopy(bFiles, miDir);

            var mFile = Path.Combine(pDir, "sources.def");
            FileExt.WriteWin(mFile, M.CreateSrcDefFile(proj, cFiles));

            var sFile = Path.Combine(pDir, "PV3S1600.dlp");
            FileExt.WriteWin(sFile, S.CreatePv3Dlp(proj));
        }
    }
}