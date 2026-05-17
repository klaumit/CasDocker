using System;
using System.IO;
using System.Linq;
using PvMake.Lib;
using B = PvMake.Core.Bases;

// ReSharper disable PossibleNullReferenceException

namespace PvMake.Core
{
    public static class Simulator
    {
        public static void Run(IOptions o)
        {
            B.LoadAndPrepareProject(o);

            foreach (var sdk in B.sdks)
            {
                var isHitachi = KnowIt.IsHitachi(sdk);
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                var exeName = isHitachi ? "CASIO SimSH.exe" : "Sim3022.exe";
                var exe = FileExt.Find(sdkDir, exeName).FirstOrDefault();
                var lbl = Path.GetFileNameWithoutExtension(exe)
                    .Split(new[] { ' ' }, 2).Last();
                Console.WriteLine(" * Starting {0} of {1}...", lbl, sdk);
                if (isHitachi)
                {
                    RegVb(sdkDir);
                }
                ProcExt.Start(exe, sdkDir, null);
            }

            Console.WriteLine("Done.");
        }

        private static void RegVb(string sdkDir)
        {
            const string regMark = "vb6.txt";
            var regMarkF = Path.Combine(sdkDir, regMark);
            var reg = File.Exists(regMarkF);
            if (reg)
                return;
            var ocxs = FileExt.Find(sdkDir, "*.OCX").OrderBy(x => x).ToArray();
            foreach (var ocx in ocxs)
                ProcExt.Start("regsvr32", sdkDir, ocx);
            File.WriteAllText(regMarkF, "done");
        }
    }
}