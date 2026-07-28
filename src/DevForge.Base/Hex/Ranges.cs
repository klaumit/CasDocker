using System.Collections.Generic;
using System.Linq;
using DevForge.Lib.Messages.Impl;
using DevForge.Lib.Tools;

namespace DevForge.Lib.Hex
{
    public static class Ranges
    {
        public static IntRange Create(string from, string to)
        {
            var beg = (uint)TextExt.ParseHex(from, 0);
            var end = (uint)TextExt.ParseHex(to, 0);
            return Create(beg, end);
        }

        public static IntRange Create(uint beg, uint end)
        {
            var len = end - beg;
            var obj = new IntRange { Off = beg, Len = len };
            return obj;
        }

        public static IEnumerable<uint> Iterate(this IntRange range, int step)
        {
            var addr = range.Off ?? 0;
            var count = range.Len / step;
            for (var i = 0; i < count; i++)
            {
                yield return addr;
                addr = (uint)(addr + step);
            }
        }

        public static IEnumerable<IntRange> Loop(this XxdFile xxd)
        {
            foreach (var range in xxd.Stats.Info.Ranges)
                yield return range.Value;
        }

		public static IEnumerable<uint> Intersect(this IntRange range, IEnumerable<uint> values)
		{
            var found = values.SkipWhile(k => k < range.Off).TakeWhile(k => k <= range.Next);
            return found;
		}
	}
}