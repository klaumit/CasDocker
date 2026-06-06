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
                var exeName = K.IsHitachi(sdk) ? "FTM.exe"
                    : K.IsIntel(sdk) ? "PVM.exe"
                    : K.IsClassPad(sdk) ? "ClassPad Add-In Installer.exe"
                    : null;
                var exe = FileExt.Find(sdkDir, exeName).FirstOrDefault();
                var lbl = Path.GetFileNameWithoutExtension(exe);
                Console.WriteLine(" * Starting {0} of {1}...", lbl, sdk);
                ProcExt.New(exe, sdkDir).Start();
            }

            Console.WriteLine("Done.");
        }
    }
}