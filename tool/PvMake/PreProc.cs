using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SoftCircuits.IniFileParser;

namespace PvMake
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

            var utf = Encoding.UTF8;
            var ini = new IniFile();
            await ini.LoadAsync(Path.Combine(inputDir, prj), utf);

            var appS = ini.GetSectionSettings(app);
            foreach (var item in appS)
            {
                Console.WriteLine(item);
            }
        }
    }
}