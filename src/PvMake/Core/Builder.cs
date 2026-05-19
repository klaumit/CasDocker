using System;
using System.IO;
using PvMake.Lib;
using W = PvMake.Lib.Writing;
using M = PvMake.Lib.Making;
using S = PvMake.Lib.Siming;
using B = PvMake.Core.Bases;
using System.Diagnostics;

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
                Console.WriteLine(" * {0}", sdk);
                if (isHitachi)
                    CompileHitachi(sdkDir, B.proj, B.inputDir);
                else
                    CompileIntel(sdkDir, B.proj, B.inputDir);
            }

            Console.WriteLine("Done.");
        }

        private static void CompileIntel(string sdkDir, Project proj, string inputDir)
        {
            var cDir = Path.Combine(sdkDir, "C");
            var pDir = Path.Combine(cDir, proj.AppName);
            var mBat = Path.Combine(pDir, "mk.bat");
            var proc = ProcExt.Start(mBat, pDir, null);
            Console.WriteLine(proc);
        }

        private static void CompileHitachi(string sdkDir, Project proj, string inputDir)
        {
            var pDir = Path.Combine(sdkDir, proj.AppName);
            var mBat = Path.Combine(pDir, "BuildAll.bat");
            var proc = ProcExt.Start(mBat, pDir, null);
            Console.WriteLine(proc);
        }
    }
}