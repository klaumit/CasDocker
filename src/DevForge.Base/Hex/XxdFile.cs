using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using System.Linq;
using System.Threading;
using DevForge.Lib.API;
using DevForge.Lib.Messages;
using System.Linq;
using System.Text;
using DevForge.Lib.Tools;
using Newtonsoft.Json;
using F = System.IO.File;

// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Common
{
    public sealed class XxdFile
    {
        public string File { get; }
        public XxdStat Stats { get; }

        public XxdFile(string file)
        {
            File = file;
            var ext = Path.GetExtension(file);
            var info = file.Replace(ext, ".info");
            Stats = new XxdStat(info);
        }

        public void ReadLines()
        {
            var enc = Encoding.UTF8;
            using (var reader = new StreamReader(File, enc))
            {
                Stats.Read();
                var offset = Stats.Info.Pos;
                if (offset >= 1)
                {
                    var core = reader.BaseStream;
                    core.Seek(offset, SeekOrigin.Begin);
                }
                if (Stats.Info.Ranges == null)
                    Stats.Info.Ranges = new SortedDictionary<uint, IntRange>();
                var dict = Stats.Info.Ranges;
                IntRange last = null;
                foreach (var line in ReadHexLines(reader))
                {
                    var ir = new IntRange { Off = line.Addr, Len = (uint?)line.Raw?.Length };
                    var off = ir.Off ?? 0;
                    if (last != null)
                    {
                        if (last.Next == off)
                        {
                            last.Len += ir.Len;
                            continue;
                        }
                        if (last.Off == off && last.Len == ir.Len)
                            continue;
                    }
                    dict[off] = last = ir;
                }
                Optimize(dict);
                WriteStats(reader, Stats);
            }
        }

        private static void WriteStats(StreamReader stream, XxdStat stats)
        {
            var core = stream.BaseStream;
            var pos = core.Position;
            stats.Info.Pos = pos;
            stats.Write();
        }

        private static IEnumerable<XxdLine> ReadHexLines(StreamReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var tmp = line.Split(':', 2);
                var addr = uint.Parse(tmp[0], NumberStyles.HexNumber);
                tmp = tmp[1].Split("  ", 2);
                var mid = tmp[0].Replace(" ", "").Trim();
                var raw = TextExt.FromHexString(mid);
                var txt = tmp[1];
                var obj = new XxdLine { Addr = addr, Raw = raw, Txt = txt };
                yield return obj;
            }
        }

        private static void Optimize(SortedDictionary<uint, IntRange> dict)
        {
            IntRange last = null;
            foreach (var pair in dict.ToArray())
            {
                var key = pair.Key;
                var current = pair.Value;
                if (last != null)
                {
                    if (last.Next > current.Off && current.Next >= last.Next)
                    {
                        dict.Remove(key);
                        last.Len = current.Next - last.Off;
                    }
                }
                last = pair.Value;
            }
        }

        public override string ToString()
        {
            var name = Path.GetFileNameWithoutExtension(File);
            return string.Format("[XXD] '{0}'", name);
        }
    }
}