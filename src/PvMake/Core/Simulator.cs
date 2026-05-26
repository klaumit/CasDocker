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

            foreach (var item in B.sdks)
            {
                var sdk = item.Sdk;
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                var isHitachi = KnowIt.IsHitachi5(sdk);
                var isIntel = KnowIt.IsIntel5(sdk);
                var isClassy = KnowIt.IsClassPad(sdk);
                var exeName = isHitachi ? "CASIO SimSH.exe"
                    : isIntel ? "Sim3022.exe"
                    : isClassy ? "ClassPad300.exe"
                    : null;
                var simExt = (isHitachi || isClassy)
                    ? "*.dlp"
                    : "*.cpj";

                Driving.KillAll(exeName);

                var exe = FileExt.Find(sdkDir, exeName).FirstOrDefault();
                var lbl = Path.GetFileNameWithoutExtension(exe)
                    .Split(new[] { ' ' }, 2).Last();
                Console.WriteLine(" * Starting {0} of {1}...", lbl, sdk);
                if (isHitachi)
                {
                    RegVb(sdkDir);
                }
                ProcExt.Start(exe, sdkDir, null);

                if (isHitachi)
                {
                    var projDir = Path.Combine(B.pvPrefix, sdk, B.proj.AppName);
                    var simFile = Directory.GetFiles(projDir, simExt).First();
                    Driving.OpenInHitachi(simFile);
                }
                else
                {
                    var projDir = Path.Combine(B.pvPrefix, sdk, "C", B.proj.AppName);
                    var simFile = Directory.GetFiles(projDir, simExt).First();
                    Driving.OpenInIntel(simFile);
                }
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