using System;
using System.IO;
using PvMake.Lib;
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






            throw new NotImplementedException();
        }

        private static void CompileHitachi(string sdkDir, Project proj, string inputDir)
        {






            throw new NotImplementedException();
        }
    }
}