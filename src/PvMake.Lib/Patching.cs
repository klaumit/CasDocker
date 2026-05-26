using System;
using System.IO;

// ReSharper disable UseStringInterpolation

namespace PvMake.Lib
{
    public static class Patching
    {
        public static void PostCompilePad(string pvaFile, string tgtDir)
        {
            if (string.IsNullOrWhiteSpace(pvaFile))
            {
                Console.Error.WriteLine("No PVA to be found for patching!");
                return;
            }

            var ubDir = FileExt.GetDir(Path.Combine(tgtDir, "User_Bin"), true);
            var pvaName = Path.GetFileNameWithoutExtension(pvaFile);
            var tgtFile = Path.Combine(ubDir, string.Format("{0}.cpa", pvaName));
            var pvaBytes = File.ReadAllBytes(pvaFile);

            var cpaBytes = pvaBytes;



            File.WriteAllBytes(tgtFile, cpaBytes);
            Console.WriteLine("    => '{0}' created!", Path.GetFileName(tgtFile));
        }
    }
}