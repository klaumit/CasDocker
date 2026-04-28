using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PvMake.Resources;
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

            var (d2M, m2D) = ResTool.ReadDirToModel();


            Console.WriteLine(JsonSerializer.Serialize(d2M));
            Console.WriteLine(JsonSerializer.Serialize(m2D));
            
            

            var appS = ini.GetSectionSettings(app);
            foreach (var item in appS)
            {
                Console.WriteLine(item);
            }
        }
    }
}