using System;
using System.IO;
using System.Linq;
using PvMake.Lib;
using System.Collections.Generic;
using System.Text;

namespace PvMake.Core
{
    internal static class Bases
    {
        private static Project proj;
        private static string[] mods;
        internal static string[] sdks;
        internal static string pvPrefix;

        internal static void LoadAndPrepareProject(IOptions o)
        {
            var inputDir = FileExt.GetDir(o.InputDir, false);
            Console.WriteLine("Source   => {0}", inputDir);

            var prjFile = Path.Combine(inputDir, Project.Fn);
            proj = IniExt.ReadProj(prjFile);
            Console.WriteLine("Project  => {1} v{2} ({0})", proj.AppName, proj.AppTitle, proj.AppVer);

            mods = proj.ForModels;
            Console.WriteLine("Models   => {0}", string.Join(" ", mods));

            var dir2M = Models.ReadDirToModel();
            sdks = mods.Select(dir2M.FindDir).ToArray();
            Console.WriteLine("SDKs     => {0}", string.Join(" ", sdks));

            var archives = Archives.ReadList();
            var archRepo = FileExt.GetAssDir("Archives", false);
            pvPrefix = Prefixes.GetPvPrefix();
            Console.WriteLine("Scratch  => {0}", pvPrefix);

            foreach (var sdk in sdks)
            {
                var shown = false;
                var toExtract = archives.Iter(sdk).ToArray();
                foreach (var value in toExtract)
                {
                    var zipDest = Path.Combine(pvPrefix, value.Name);
                    if (Directory.Exists(zipDest))
                        continue;
                    var zipFile = Path.Combine(archRepo, value.Name + ".tar.gz");
                    if (!shown)
                    {
                        shown = true;
                        Console.WriteLine(" * Processing {0}...", sdk);
                    }
                    Console.WriteLine("    * Setting up {0}...", value.Name);
                    ZipExt.Uncompress(zipFile, pvPrefix);
                }
            }
        }
    }
}