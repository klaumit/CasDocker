using System;
using System.Collections.Generic;
using DevForge.Lib.API;

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

        public static uint Get86Address(this IPvBuff call)
        {
            var addrIdx = (call.Src - AddrStart) / 2;
            var segIdx = call.Seg == Segments[1] ? 1 : 0;
            return (uint)(((uint)addrIdx * 2 + segIdx) * SegmentSize + call.Off);
        }

        public static PvBuff From86Address(this uint spaceAddr, byte bank = 3, ushort size = 64)
        {
            var bucket = spaceAddr / SegmentSize;
            var off = spaceAddr % SegmentSize;
            var segIdx = (int)(bucket % 2);
            var addrIdx = bucket / 2;
            var seg = (ushort)Segments[segIdx];
            var src = (ushort)(addrIdx * 2 + AddrStart);
            return new PvBuff { Src = src, Bank = bank, Seg = seg, Off = (ushort)off, Size = size };
        }

        public static IEnumerable<string> Print86Hex(this PvBuff buff)
        {
            var array = buff.Bytes;
            if (array == null) array = new byte[0];
            return MemMapGen.PrintHexDump(buff.Get86Address(), array);
        }

        public static IEnumerable<PvBuff> GenerateCalls(int maxChunkSize)
        {
            foreach (var addr in GetAddresses())
            foreach (var seg in Segments)
            {
                var offset = 0;
                while (offset < SegmentSize)
                {
                    var remaining = SegmentSize - offset;
                    var length = Math.Min(maxChunkSize, remaining);
                    yield return new PvBuff
                    {
                        Src = (ushort)addr, Bank = DefaultBank, Seg = (ushort)seg,
                        Off = (ushort)offset, Size = (ushort)length
                    };
                    offset += length;
                }
            }
        }

        public static IEnumerable<PvBuff> GenerateCalls(int maxChunkSize,
                                                IEnumerable<uint> addresses)
        {
            foreach (var addr in addresses)
            {
                var length = (ushort)maxChunkSize;
                var buff = addr.From86Address(DefaultBank, length);
                yield return buff;
            }
        }
	}
}