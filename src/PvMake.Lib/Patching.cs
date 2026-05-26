using System;
using System.IO;
using B = System.Buffer;

// ReSharper disable RedundantExplicitArrayCreation
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

            var data = File.ReadAllBytes(pvaFile);
            data[0x40] = 0x00;
            data[0x41] = 0x02;

            var checkSum = CalcChecksum32(data);
            var tmp = BitConverter.GetBytes(checkSum);
            var chck = new byte[] { tmp[0], tmp[2], tmp[1], tmp[3] };

            var foot = new byte[]
            {
                0x43, 0x41, 0x53, 0x49, 0x4F, 0x20, 0x43, 0x4C,
                0x41, 0x53, 0x53, 0x20, 0x50, 0x41, 0x44, 0x20,
                0x41, 0x44, 0x44, 0x49, 0x4E, 0x20, 0x44, 0x41,
                0x54, 0x41, 0x20, 0x20, 0x20, 0x20, 0x30, 0x00
            };

            var final = new byte[data.Length + foot.Length + 16];
            B.BlockCopy(data, 0, final, 0, data.Length);
            B.BlockCopy(foot, 0, final, data.Length, foot.Length);
            B.BlockCopy(chck, 0, final, data.Length + foot.Length, chck.Length);

            File.WriteAllBytes(tgtFile, final);
            Console.WriteLine("    => '{0}' created [0x{1:X8}]!", Path.GetFileName(tgtFile), checkSum);
        }

        private static uint CalcChecksum32(byte[] data)
        {
            uint checksum = 0;
            foreach (var b in data)
                checksum += b;
            return checksum;
        }
    }
}