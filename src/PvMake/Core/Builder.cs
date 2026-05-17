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

                Console.WriteLine(" ? " + isHitachi + " -> " + sdkDir);
            }

            Console.WriteLine("Done.");
        }
    }
}