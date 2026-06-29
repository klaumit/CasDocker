using System;
using System.IO;
using PvBake.Lib.Tools;
using PvBake.Lib.API;

// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Core
{
    public static class SimExtractor
    {
        public static void Run(IOptions o)
        {
            var inRoot = Path.GetFullPath(o.InputDir);
            var outRoot = Path.GetFullPath(o.OutputDir);
            Console.WriteLine($"Input  = {inRoot}");
            Console.WriteLine($"Output = {outRoot}");

            const SearchOption so = SearchOption.AllDirectories;
            var files = Directory.GetFiles(inRoot, "*.cpj", so);

            foreach (var file in files)
            {
                var local = Files.GetRelativePath(inRoot, file);
                var name = Path.GetFileNameWithoutExtension(file).Replace("Plus", "P");
                Console.WriteLine($" * [{name,-7}] {local}");

                var cpj = IniExt.ReadProject(file);
                var biosLocal = Files.GetRelativePath(inRoot, cpj.biosFile);
                var applLocal = Files.GetRelativePath(inRoot, cpj.applFile);
                Console.WriteLine($"    - ({TextExt.ToByteSize(cpj.biosOffs),6}) {biosLocal}");
                Console.WriteLine($"    - ({TextExt.ToByteSize(cpj.applOffs),6}) {applLocal}");

                var biosArr = File.ReadAllBytes(cpj.biosFile);
                var applArr = File.ReadAllBytes(cpj.applFile);
                var array = new byte[cpj.biosOffs + cpj.applOffs + applArr.Length];
                Array.Fill<byte>(array, 0xFF);
                Array.Copy(biosArr, 0, array, cpj.biosOffs, biosArr.Length);
                Array.Copy(applArr, 0, array, cpj.applOffs, applArr.Length);

                var target = Path.GetFullPath(Path.Combine(outRoot, $"{name}.bin"));
                Console.WriteLine($"      --> {target}");
                File.WriteAllBytes(target, array);
            }

            Console.WriteLine("Done.");
        }
    }
}