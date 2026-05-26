using System;
using System.IO;
using System.Linq;
using PvMake.Lib;
using B = PvMake.Core.Bases;
using K = PvMake.Lib.KnowIt;

namespace PvMake.Core
{
    public static class Uploader
    {
        public static void Run(IOptions o)
        {
            B.LoadAndPrepareProject(o);

            foreach (var item in B.sdks)
            {
                var sdk = item.Sdk;
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                var exeName = K.IsHitachi5(sdk) ? "FTM.exe"
                    : K.IsIntel5(sdk) ? "PVM.exe"
                    : K.IsClassPad(sdk) ? "FA-CP1.exe"
                    : null;
                var exe = FileExt.Find(sdkDir, exeName).FirstOrDefault();
                var lbl = Path.GetFileNameWithoutExtension(exe);
                Console.WriteLine(" * Starting {0} of {1}...", lbl, sdk);
                ProcExt.Start(exe, sdkDir, null);
            }

            Console.WriteLine("Done.");
        }
    }
}