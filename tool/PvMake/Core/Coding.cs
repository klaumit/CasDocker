using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PvMake.Tools;

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Coding
    {
        public static void ReCopy(IEnumerable<string>? files, string dest)
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

        public static void ReWrite(IEnumerable<string>? files, string dest, bool patchHit = false)
        {
            if (files == null)
                return;
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var iLines = File.ReadAllLines(file, Encoding.ASCII);
                var lines = new List<string>();
                string tmp;
                foreach (var iLine in iLines)
                {
                    var line = iLine;
                    if (line.Contains(tmp = "byte far "))
                        line = line.Replace(tmp, "byte ");
                    if (line.Contains(tmp = "<stdrom.h>"))
                        line = line.Replace(tmp, "\"string.h\"");
                    if (line.Contains(tmp = " far "))
                        line = line.Replace(tmp, " ");
                    if (line.Contains(tmp = "== 0xffff "))
                        line = line.Replace(tmp, "== 0xffffffff ");
                    if (line.Contains(tmp = "IB_PFONT"))
                        line = line.Replace(tmp, "(byte)IB_PFONT");
                    if (line.Contains(tmp = "t_tbl") && !line.Contains("cpy("))
                        line = line.Replace(tmp, "(char*)t_tbl");
                    if (line.Contains(tmp = ",\""))
                        line = line.Replace(tmp, ",(byte *)\"");
                    lines.Add(line);
                }
                FileExt.WriteWin(tgt, lines);
                Console.WriteLine($"    + {name} ({lines.Count} L) => {tgt}");
            }
        }
    }
}