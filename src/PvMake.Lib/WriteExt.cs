using System;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PvMake.Lib;
using System.IO;

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
                Console.WriteLine($"    + {name} ({bytes.Length} B) => {lTgt}");
            }
        }

        public static void ReWrite(IEnumerable<string> files, string dest, bool patchHit, string root)
        {
            if (files == null)
                return;
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var lines = new List<string>();
                using (var input = new StreamReader(file, Encoding.ASCII))
                {

                }
                FileExt.WriteWin(tgt, lines);
                var lTgt = Path.GetFullPath(tgt).Replace(root, ".");
                Console.WriteLine($"    + {name} ({lines.Count} L) => {lTgt}");
            }
        }
    }
}