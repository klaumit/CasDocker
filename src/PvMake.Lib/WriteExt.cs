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

        public static void ReWrite(SortedSet<string> cFiles, string ccDir, bool b)
        {


            
            
            




            throw new NotImplementedException();
        }
    }
}