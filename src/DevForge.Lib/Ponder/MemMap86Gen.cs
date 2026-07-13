using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable ConvertIfStatementToNullCoalescingExpression

namespace DevForge.Lib.Ponder
{
    public static class MemMap86Gen
    {
        public const int MaxChunkSize = 64;
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

        public static long Get86Address(this PvBuff call)
        {
            var addrIndex = (call.Src - AddrStart) / 2;
            var segIndex = call.Seg == Segments[1] ? 1 : 0;
            return ((long)addrIndex * 2 + segIndex) * SegmentSize + call.Off;
        }

        public static IEnumerable<string> Print86Hex(this PvBuff buff)
        {
            var array = buff.Bytes;
            if (array == null) array = new byte[0];

        public static IEnumerable<string> PrintHexDump(long address, byte[] data, int len = 16)
        {
            for (var rowStart = 0; rowStart < data.Length; rowStart += len)
            {
                var rowLen = Math.Min(len, data.Length - rowStart);
                var rowAddress = address + rowStart;
                var line = new StringBuilder();
                line.Append(rowAddress.ToString("X8")).Append(": ");
                for (var i = 0; i < len; i += 2)
                {
                    line.Append(i < rowLen ? data[rowStart + i].ToString("x2") : "  ");
                    line.Append(i + 1 < rowLen
                        ? data[rowStart + i + 1].ToString("x2")
                        : i + 1 < len
                            ? "  "
                            : "");
                    line.Append(' ');
                }
                line.Append(' ');
                for (var i = 0; i < rowLen; ++i)
                {
                    var b = data[rowStart + i];
                    line.Append(b >= 0x20 && b <= 0x7E ? (char)b : '.');
                }
                yield return line.ToString();
            return MemMapGen.PrintHexDump(buff.Get86Address(), array);
        }

        public static List<PvBuff> GenerateCalls()
        {
            var calls = new List<PvBuff>();
            foreach (var addr in GetAddresses())
            foreach (var seg in Segments)
            {
                var offset = 0;
                while (offset < SegmentSize)
                {
                    var remaining = SegmentSize - offset;
                    var length = Math.Min(MaxChunkSize, remaining);
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