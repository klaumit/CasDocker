using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PvMake.Lib
{
    public static class Locating
    {
        public static void FixPaths(string dir, string root)
        {
            var name = Path.GetFileName(dir);
            var kind = KnowIt.CheckKind(name);
            switch (kind)
            {
                case KnowIt.Known.ModelX86:
                    FixModelX86(root, dir); 
                    break;
                case KnowIt.Known.CompilerX86:
                    FixCompilerX86(root, dir);
                    break;
                case KnowIt.Known.ModelSH3:
                    FixModelSH3(root, dir); 
                    break;
            }
        }
             
        private static void FixModelSH3(string root, string dir)
        {
            var files = FileExt.FindAllFiles(dir);

            SortedSet<string> defs;
            if (!files.TryGetValue(".def", out defs))
                defs = new SortedSet<string>();

            var shDir = Path.Combine(root, "shc");
            foreach (var def in defs)
                FixText(def, Tuple.Create(@"c:\pvshcom\shc", shDir));

            SortedSet<string> bats;
            if (!files.TryGetValue(".bat", out bats))
                bats = new SortedSet<string>();

            foreach (var bat in bats)
                FixText(bat, 
                    Tuple.Create(@"c:\pvshcom\shc", shDir),
                    Tuple.Create(@"c:\casio", root)
                );
        }

        private static void FixModelX86(string root, string dir)
        {
            var files = FileExt.FindAllFiles(dir);

            SortedSet<string> dats;
            if (!files.TryGetValue(".dat", out dats))
                dats = new SortedSet<string>();

            var lsijDir = Path.Combine(root, "lsij");
            foreach (var dat in dats)
                FixText(dat, Tuple.Create(@"C:\lsij\lsic86pv", lsijDir));

            SortedSet<string> bats;
            if (!files.TryGetValue(".bat", out bats))
                bats = new SortedSet<string>();

            foreach (var bat in bats)
                FixText(bat,
                    Tuple.Create(@"C:\lsij\lsic86pv", lsijDir),
                    Tuple.Create(@"C:\CASIO", root)
                );
        }

        private static void FixCompilerX86(string root, string dir)
        {
            var files = FileExt.FindAllFiles(dir);

            SortedSet<string> empt;
            if (!files.TryGetValue("", out empt))
                empt = new SortedSet<string>();

            var lsijDir = Path.Combine(root, "lsij");
            foreach (var emp in empt)
                FixText(emp, Tuple.Create(@"C:\lsij\lsic86pv", lsijDir));
        }

        private static void FixText(string file, params Tuple<string, string>[] replaces)
        {
            var src = File.ReadAllLines(file, Encoding.ASCII);
            var dst = new List<string>();
            bool dirty = false;
            foreach (var line in src)
            {
                string nLine = line;
                foreach (var repl in replaces)
                {
                    var term = repl.Item1;
                    var word = repl.Item2;
                    nLine = nLine.Replace(term, word);
                }                
                if (!line.Equals(nLine))
                    dirty = true;
                dst.Add(nLine);
            }
            if (!dirty)
                return;
            FileExt.WriteWin(file, dst);
        }
    }
}