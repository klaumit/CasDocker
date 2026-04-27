using System;
using System.Threading.Tasks;

namespace PvMake
{
    internal static class PreProc
    {
        public static async Task Run(Options o)
        {
            var inputDir = FileExt.GetDir(o.InputDir);
            var outputDir = FileExt.GetDir(o.OutputDir);
            Console.WriteLine($"Input  = {inputDir}");
            Console.WriteLine($"Output = {outputDir}");
        }
    }
}