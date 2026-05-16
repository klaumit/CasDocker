using System;
using System.IO;
using System.Linq;
using PvMake.Lib;
using B = PvMake.Core.Bases;

namespace PvMake.Core
{
    public static class Uploader
    {
        public static void Run(IOptions o)
        {
            Bases.LoadAndPrepareProject(o);

            foreach (var sdk in B.sdks)
            {
                var isHitachi = KnowIt.IsHitachi(sdk);
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                var exeName = isHitachi ? "FTM.exe" : "PVM.exe";
                var exe = FileExt.Find(sdkDir, exeName).FirstOrDefault();
                var lbl = Path.GetFileNameWithoutExtension(exe);
                Console.WriteLine(" * Starting {0} of {1}...", lbl, sdk);
                ProcExt.Start(exe, sdkDir, null);
            }

            Console.WriteLine("Done.");
        }
    }
}