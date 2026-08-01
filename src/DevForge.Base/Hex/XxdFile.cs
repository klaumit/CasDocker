using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using DevForge.Lib.Tools;
using F = System.IO.File;

// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Hex
{
    public sealed class XxdFile
    {
        public string File { get; private set; }
        public XxdStat Stats { get; private set; }

        public XxdFile(string file)
        {
            File = file;
            var ext = Path.GetExtension(file);
            var info = file.Replace(ext, ".info");
            Stats = new XxdStat(info);
        }

        public StreamReader OpenReader(long? pos = null)
        {
            var enc = Encoding.UTF8;
            if (!F.Exists(File)) F.WriteAllBytes(File, new byte[0]);
            var reader = new StreamReader(File, enc);
            if (pos != null) reader.BaseStream.Position = pos.Value;
            return reader;
        }

        public void ReadLines()
        {
            using (var reader = OpenReader())
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
                    var addr = line.GetAddr();
                    var bytes = line.GetRaw();
                    var len = (uint)bytes.Length;
                    var ir = new IntRange { Off = addr, Len = len };
                    if (line.Pos != null)
                        ir.Pos = line.Pos.Value;
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

        public static IEnumerable<XxdLine> ReadHexLines(StreamReader reader)
        {
            var pos = 0L;
            int len;
            string line;
            while ((line = reader.ReadLineWithLen(out len)) != null)
            {
                var tmp = line.Split(new[] { ':' }, 2);
                var addr = tmp[0];
                tmp = tmp[1].Split(new[] { "  " }, 2, StringSplitOptions.None);
                var raw = tmp[0];
                var txt = tmp[1];
                var obj = new XxdLine { Addr = addr, Raw = raw, Txt = txt };
                obj.Pos = pos;
                yield return obj;
                pos += len;
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