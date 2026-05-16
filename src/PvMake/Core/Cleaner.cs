using System;
using System.IO;
using System.Linq;
using PvMake.Lib;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    public static class Cleaner
    {
        public static void Run(IOptions o)
        {
            var inputDir = FileExt.GetDir(o.InputDir, false);
            Console.WriteLine("Source => {0}", inputDir);

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

            Console.WriteLine("Done.");
        }
    }
}