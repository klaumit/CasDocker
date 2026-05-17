using System;
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
                    PrepareHitachi(sdkDir);
                else
                    PrepareIntel(sdkDir);
            }

            Console.WriteLine("Done.");
        }

        private static void PrepareIntel(string sdkDir)
        {
            throw new NotImplementedException();
        }

        private static void PrepareHitachi(string sdkDir)
        {
            throw new NotImplementedException();
        }
    }
}