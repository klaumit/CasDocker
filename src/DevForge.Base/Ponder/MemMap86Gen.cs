using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable ConvertIfStatementToNullCoalescingExpression

namespace DevForge.Lib.Ponder
{
    public static class MemMap86Gen
    {
        public const int DefaultBank = 3;
        public static readonly int[] Segments = { 0x6000, 0x7000 };
        public const int SegmentSize = 0x10000;
        public const int AddrStart = 0x0100;

        public static IEnumerable<int> GetAddresses()
        {
            for (var addr = AddrStart; addr <= 0x010E; addr += 2)
                yield return addr;
            for (var addr = 0x0110; addr <= 0x011E; addr += 2)
                yield return addr;
        }

        public static uint Get86Address(this PvBuff call)
        {
            var addrIndex = (call.Src - AddrStart) / 2;
            var segIndex = call.Seg == Segments[1] ? 1 : 0;
            return (uint)(((uint)addrIndex * 2 + segIndex) * SegmentSize + call.Off);
        }

        public static IEnumerable<string> Print86Hex(this PvBuff buff)
        {
            var array = buff.Bytes;
            if (array == null) array = new byte[0];
            return MemMapGen.PrintHexDump(buff.Get86Address(), array);
        }

        public static List<PvBuff> GenerateCalls(int maxChunkSize)
        {
            var calls = new List<PvBuff>();
            foreach (var addr in GetAddresses())
            foreach (var seg in Segments)
            {
                var offset = 0;
                while (offset < SegmentSize)
                {
                    var remaining = SegmentSize - offset;
                    var length = Math.Min(maxChunkSize, remaining);
                    calls.Add(new PvBuff
                    {
                        Src = (ushort)addr, Bank = DefaultBank, Seg = (ushort)seg,
                        Off = (ushort)offset, Size = (ushort)length
                    });
                    offset += length;
                }
            }
            return calls;
        }
    }
}