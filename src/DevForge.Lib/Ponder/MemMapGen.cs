using System;
using System.Collections.Generic;

namespace DevForge.Lib.Ponder
{
    public static class MemMap86Gen
    {
        public const int MaxChunkSize = 64;
        public const int DefaultBank = 3;
        public static readonly int[] Segments = { 0x6000, 0x7000 };
        public const int SegmentSize = 0x10000;

        public static IEnumerable<int> GetAddresses()
        {
            for (var addr = 0x0100; addr <= 0x010E; addr += 2)
                yield return addr;
            for (var addr = 0x0110; addr <= 0x011E; addr += 2)
                yield return addr;
        }

        public static List<ReadMemCall> GenerateCalls()
        {
            var calls = new List<ReadMemCall>();
            foreach (var addr in GetAddresses())
            foreach (var seg in Segments)
            {
                var offset = 0;
                while (offset < SegmentSize)
                {
                    var remaining = SegmentSize - offset;
                    var length = Math.Min(MaxChunkSize, remaining);
                    calls.Add(new ReadMemCall
                    {
                        Addr = (ushort)addr, Bank = DefaultBank, Seg = (ushort)seg,
                        Off = (ushort)offset, Len = (ushort)length
                    });
                    offset += length;
                }
            }
            return calls;
        }
    }
}