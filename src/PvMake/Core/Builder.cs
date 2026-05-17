using System;
using System.Collections.Generic;
using PvMake.Lib;
using System.IO;
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
            var mFile = Path.Combine(pDir, "Makefile");
            FileExt.WriteWin(mFile, Making.CreateMakeFile(proj,
                new List<string>(), new List<string>()));
        }

        private static void PrepareHitachi(string sdkDir, Project proj)
        {

            // var pDir = FileExt.GetDir(Path.Combine(cDir, proj.AppName), true);
        }
    }
}