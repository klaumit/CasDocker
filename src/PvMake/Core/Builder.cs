using System;
using System.IO;
using System.Linq;
using PvMake.Lib;
using B = PvMake.Core.Bases;
using P = PvMake.Lib.Patching;
using K = PvMake.Lib.KnowIt;

// ReSharper disable InlineOutVariableDeclaration

namespace PvMake.Core
{
    public static class Builder
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
                    CompileHitachi(sdkDir, B.proj, B.inputDir);
                else if (K.IsIntel(sdk))
                    CompileIntel(sdkDir, B.proj, B.inputDir);

                sdk = item.Sdk;
                if (K.IsClassPad(sdk))
                {
                    var projDir = Path.Combine(sdkDir, B.proj.AppName);
                    var pvaFile = FileExt.Find(projDir, "*.pva").FirstOrDefault();
                    var tgtDir = Path.Combine(B.pvPrefix, sdk);
                    P.PostCompilePad(pvaFile, tgtDir);
                }
            }

            Console.WriteLine("Done.");
        }

        private static void CompileIntel(string sdkDir, Project proj, string inputDir)
        {
            var cDir = Path.Combine(sdkDir, "C");
            var pDir = Path.Combine(cDir, proj.AppName);
            var mBat = Path.Combine(pDir, "mk.bat");
            ProcExt.Start(mBat, pDir, null, sec: 30);
        }

        private static void CompileHitachi(string sdkDir, Project proj, string inputDir)
        {
            var pDir = Path.Combine(sdkDir, proj.AppName);
            var mBat = Path.Combine(pDir, "BuildAll.bat");
            ProcExt.Start(mBat, pDir, null, sec: 30);
        }
    }
}