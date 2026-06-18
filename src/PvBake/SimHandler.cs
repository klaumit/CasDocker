using System;
using System.IO;

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
                var local = Files.GetRelativePath(root, file);
                var name = Path.GetFileNameWithoutExtension(file).Replace("Plus", "P");
                Console.WriteLine($" * [{name,-7}] {local}");

                var cpj = IniExt.ReadProject(file);
                var biosLocal = Files.GetRelativePath(root, cpj.biosFile);
                var applLocal = Files.GetRelativePath(root, cpj.applFile);
                Console.WriteLine($"    - ({TextExt.ToByteSize(cpj.biosOffs),6}) {biosLocal}");
                Console.WriteLine($"    - ({TextExt.ToByteSize(cpj.applOffs),6}) {applLocal}");

                var biosArr = File.ReadAllBytes(cpj.biosFile);
                var applArr = File.ReadAllBytes(cpj.applFile);
                var array = new byte[cpj.biosOffs + cpj.applOffs + applArr.Length];
                Array.Copy(biosArr, 0, array, cpj.biosOffs, biosArr.Length);
                Array.Copy(applArr, 0, array, cpj.applOffs, applArr.Length);

                var target = Path.GetFullPath($"{name}.bin");
                Console.WriteLine($"      --> {target}");
                File.WriteAllBytes(target, array);
            }

            Console.WriteLine("Done.");
        }
    }
}