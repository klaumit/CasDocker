using System;
using System.Collections.Generic;
using PvMake.Lib;
using System.IO;
using static PvMake.Lib.Making;
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
                    PrepareHitachi(sdkDir, B.proj);
                else
                    PrepareIntel(sdkDir, B.proj);
            }

            Console.WriteLine("Done.");
        }

        private static void PrepareIntel(string sdkDir, Project proj)
        {
            var cDir = FileExt.GetDir(Path.Combine(sdkDir, "C"), false);
            var pDir = FileExt.GetDir(Path.Combine(cDir, proj.AppName), true);

            var zipFile = Path.Combine(B.archRepo, "LSIJ_proj" + ".tar.gz");
            ZipExt.Uncompress(zipFile, pDir);

            var mFile = Path.Combine(pDir, "Makefile");
            FileExt.WriteWin(mFile, CreateMakeFile(proj,
                new List<string>(), new List<string>()));
        }

        private static void PrepareHitachi(string sdkDir, Project proj)
        {
            var pDir = FileExt.GetDir(Path.Combine(sdkDir, proj.AppName), true);

            var zipFile = Path.Combine(B.archRepo, "SHC_proj" + ".tar.gz");
            ZipExt.Uncompress(zipFile, pDir);

            var mFile = Path.Combine(pDir, "sources.def");
            FileExt.WriteWin(mFile, CreateSrcDefFile(proj, new List<string>()));
        }
    }
}