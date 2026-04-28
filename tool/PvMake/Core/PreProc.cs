using System;
using System.IO;
using System.Threading.Tasks;
using PvMake.Tools;

namespace PvMake.Core
{
    internal static class PreProc
    {
        private const string app = "Application";
        private const string tit = "Title";
        private const string ver = "Version";
        private const string prj = "project.ini";

        public static async Task Run(Options o)
        {
            var inputDir = FileExt.GetDir(o.InputDir);
            var outputDir = FileExt.GetDir(o.OutputDir);
            Console.WriteLine($"Input  = {inputDir}");
            Console.WriteLine($"Output = {outputDir}");

            var ini = await IniExt.ReadFile(Path.Combine(inputDir, prj));

            var appS = ini.GetSectionSettings(app);
            foreach (var item in appS)
            {
                Console.WriteLine(item);
            }
        }
    }
}