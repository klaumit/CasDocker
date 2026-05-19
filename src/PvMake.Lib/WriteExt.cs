using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class WriteExt
    {
        public static void ReCopy(IEnumerable<string> files, string dest)
        {
            if (files == null)
                return;
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var bytes = File.ReadAllBytes(file);
                File.WriteAllBytes(tgt, bytes);
                Console.WriteLine($"    + {name} ({bytes.Length} B) => {tgt}");
            }
        }
    }
}