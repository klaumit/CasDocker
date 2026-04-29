using System;
using System.IO;
using System.Threading.Tasks;
using PvMake.Tools;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Cleaner
    {
        public static Task Run(Options o)
        {
            var outputDir = FileExt.GetDir(o.OutputDir);
            Console.WriteLine($"Output = {outputDir}");

            var toDelete = FileExt.FindAllFiles(outputDir)
                .Copy(
                    [
                        ".abs", ".dbg", ".hex", ".lin", ".map", ".pva"
                    ],
                    [
                        ("", x => x.EndsWith("err") || x.EndsWith("fin")),
                        (".bin", x => !x.Contains("SIM") && !x.Contains("APLALL")),
                        (".obj", x => !x.Contains("Com_") && !x.Contains("SYSTEM"))
                    ]
                );

            foreach (var (_, files) in toDelete)
            {
                foreach (var file in files)
                {
                    if (File.Exists(file))
                    {
                        Console.WriteLine($" * {file}");
                        File.Delete(file);
                    }
                }
            }

            Console.WriteLine("Done.");
            return Task.CompletedTask;
        }
    }
}