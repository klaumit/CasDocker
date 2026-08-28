using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// ReSharper disable UseStringInterpolation
// ReSharper disable TooWideLocalVariableScope
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Lib
{
    public static class Writing
    {
        public static void ReCopy(IEnumerable<string> files, string dest, string root)
        {
            if (files == null)
                return;
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var bytes = File.ReadAllBytes(file);
                File.WriteAllBytes(tgt, bytes);
                var lTgt = Path.GetFullPath(tgt).Replace(root, ".");
                Console.WriteLine("    + {0} ({1} B) => {2}", name, bytes.Length, lTgt);
            }
        }

        private static IDictionary<string, string> GetSymbols(bool patchHit)
        {
            var symbols = new Dictionary<string, string>();
            if (patchHit)
            {
                symbols["FAR"] = " ";
                symbols["PADE"] = ",\t0x00";
                symbols["B@@"] = "(byte)";
                symbols["C@"] = "(char*)";
                symbols["B@"] = "(byte *)";
                symbols["FF@"] = " 0xffffffff";
            }
            else
            {
                symbols["FAR"] = " far ";
                symbols["PADE"] = "";
                symbols["B@@"] = "";
                symbols["C@"] = "";
                symbols["B@"] = "";
                symbols["FF@"] = " 0xffff";
            }
            return symbols;
        }

        public static void ReWrite(IEnumerable<string> files, string dest, HitMe hit, string root)
        {
            if (files == null)
                return;
            var symbols = GetSymbols(hit.PatchHit);
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var lines = new List<string>();
                string input;
                using (var reader = new StreamReader(file, Encoding.ASCII))
                    input = reader.ReadToEnd();
                string tmp;
                foreach (var iLine in input.Split('\n'))
                {
                    var line = iLine.TrimEnd('\r');
                    foreach (var item in symbols)
                    {
                        var key = item.Key;
                        var val = item.Value;
                        tmp = string.Format(" {0} ", key);
                        if (line.Contains(tmp))
                            line = line.Replace(tmp, val);
                    }
                    if (hit.PatchHit && line.Contains("#pragma inline_asm"))
                        hit.InlineAsm = true;
                    lines.Add(line);
                }
                FileExt.WriteWin(tgt, lines);
                var lTgt = Path.GetFullPath(tgt).Replace(root, ".");
                Console.WriteLine("    + {0} ({1} L) => {2}", name, lines.Count, lTgt);
            }
        }
    }
}
