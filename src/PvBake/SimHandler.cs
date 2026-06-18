using System;
using System.IO;
using Humanizer;

namespace PvRanger
{
    internal static class SimHandler
    {
        public static void ExtractSim()
        {
            var root = "/home/john/Coding/NetProjects/CasMirror";
            Console.WriteLine($"Root = {root}");

            const SearchOption so = SearchOption.AllDirectories;
            var files = Directory.GetFiles(root, "*.cpj", so);

            foreach (var file in files)
            {
                var local = Path.GetRelativePath(root, file);
                var name = Path.GetFileNameWithoutExtension(file).Replace("Plus", "P");
                Console.WriteLine($" * [{name,-7}] {local}");

                var iniData = IniExt.ReadFile(file);
                var iniDir = Path.GetDirectoryName(file);
                var group = iniData["CSGROUP5"];
                var biosFile = ValueTool.Search(iniDir, group["CHIPFILE0"])!;
                var biosOffs = group["CHIPOFFSET0"].ParseHex();
                var applFile = ValueTool.Search(iniDir, group["CHIPFILE1"])!;
                var applOffs = group["CHIPOFFSET1"].ParseHex();

                var biosLocal = Path.GetRelativePath(root, biosFile);
                var applLocal = Path.GetRelativePath(root, applFile);
                Console.WriteLine($"    - ({ByteSize.FromBytes(biosOffs),6}) {biosLocal}");
                Console.WriteLine($"    - ({ByteSize.FromBytes(applOffs),6}) {applLocal}");

                var biosArr = File.ReadAllBytes(biosFile);
                var applArr = File.ReadAllBytes(applFile);
                var array = new byte[biosOffs + applOffs + applArr.Length];
                Array.Copy(biosArr, 0, array, biosOffs, biosArr.Length);
                Array.Copy(applArr, 0, array, applOffs, applArr.Length);

                var target = Path.GetFullPath($"{name}.bin");
                Console.WriteLine($"      --> {target}");
                File.WriteAllBytes(target, array);
            }

            Console.WriteLine("Done.");
        }
    }
}