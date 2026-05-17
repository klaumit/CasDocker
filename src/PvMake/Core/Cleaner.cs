using System;
using System.IO;
using PvMake.Lib;
using System.Collections.Generic;
using B = PvMake.Core.Bases;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    public static class Cleaner
    {
        public static void Run(IOptions o)
        {
            B.LoadAndPrepareProject(o);

            foreach (var sdk in B.sdks)
            {
                var sdkDir = Path.Combine(B.pvPrefix, sdk);
                CleanDir(sdkDir);
            }

            Console.WriteLine("Done.");
        }

        private static void CleanDir(string inputDir)
        {
            var toDelete = FileExt.FindAllFiles(inputDir)
                .Copy(
                    new List<string>()
                    {
                        ".abs", ".dbg", ".hex", ".lin", ".map", ".pva"
                    },
                    new List<KeyValuePair<string, Func<string, bool>>>
                    {
                        new KeyValuePair<string, Func<string, bool>>
                            ("", x => x.EndsWith("err") || x.EndsWith("fin")),
                        new KeyValuePair<string, Func<string, bool>>
                            (".bin", x => !x.Contains("SIM") && !x.Contains("APLALL")),
                        new KeyValuePair<string, Func<string, bool>>
                            (".obj", x => !x.Contains("Com_") && !x.Contains("SYSTEM"))
                    }
                );

            foreach (var item in toDelete)
            {
                var files = item.Value;
                foreach (var file in files)
                {
                    if (File.Exists(file))
                    {
                        Console.WriteLine(" * {0}", file);
                        File.Delete(file);
                    }
                }
            }
        }
    }
}