using System.Linq;
using System.Collections.Generic;
using System;

namespace DevForge.Lib.Ponder
{
    public static class MemMapSHGen
    {
        public const uint AddrStart = 0x8C000000;

        private static IEnumerable<uint> Iter(uint start, int size, int count = 1024 * 16)
        {
            for (uint i = 0; i < count; i++)
                yield return (uint)(start + (size * i));
        }

        public static IEnumerable<uint> GetAddresses(int maxChunkSize)
        {
            return new uint[0] { }
                // .Concat(Iter(0x00000000, maxChunkSize)) TLB Error!!
                .Concat(Iter(0x10000000, maxChunkSize, 100)) /* 1002D000 error */
                // .Concat(Iter(0x20000000, maxChunkSize)) TLB Error!!
                // .Concat(Iter(0x30000000, maxChunkSize)) TLB Error!!
                // .Concat(Iter(0x40000000, maxChunkSize)) TLB Error!!
                // .Concat(Iter(0x50000000, maxChunkSize)) TLB Error!!
                .Concat(Iter(0x60000000, maxChunkSize, 100)) /* 60004000 error */
                // .Concat(Iter(0x70000000, maxChunkSize)) TLB Error!!
                .Concat(Iter(0x80000000, maxChunkSize))
                .Concat(Iter(0x8A000000, maxChunkSize))
                .Concat(Iter(0x8B000000, maxChunkSize, 100))
                .Concat(Iter(0x8C000000, maxChunkSize))
                .Concat(Iter(0x8C024800, maxChunkSize))
                .Concat(Iter(0x8C400000, maxChunkSize))
                .Concat(Iter(0x8CC00000, maxChunkSize))
                .Concat(Iter(0x8D000000, maxChunkSize))
                .Concat(Iter(0x8E000000, maxChunkSize))
                .Concat(Iter(0x8F000000, maxChunkSize))
                .Concat(Iter(0x90000000, maxChunkSize))
                .Concat(Iter(0xA0000000, maxChunkSize))
                .Concat(Iter(0xB0000000, maxChunkSize))
                // .Concat(Iter(0xC0000000, maxChunkSize)) TLB Error!!
                .Concat(Iter(0xD0000000, maxChunkSize, 100)) /* D003D000 error */
                .Concat(Iter(0xE0000000, maxChunkSize))
                .Concat(Iter(0xF0000000, maxChunkSize))
                ;
        }

        public static uint GetSHAddress(this PvBuff call)
        {
            return ((uint)call.Seg << 16) | call.Off;
        }

        public static IEnumerable<string> PrintSHHex(this PvBuff buff)
        {
            var array = buff.Bytes;
            if (array == null) array = new byte[0];
            return MemMapGen.PrintHexDump(buff.GetSHAddress(), array);
        }

        public static List<PvBuff> GenerateCalls(int maxChunkSize)
        {
            var calls = new List<PvBuff>();
            foreach (var addr in GetAddresses(maxChunkSize))
            {
                var length = maxChunkSize;
                ushort seg = (ushort)(addr >> 16);
                ushort off = (ushort)(addr & 0xFFFF);
                calls.Add(new PvBuff
                {
                    Seg = seg, Off = off, Size = (ushort)length
                });
            }
            return calls;
        }
    }
}