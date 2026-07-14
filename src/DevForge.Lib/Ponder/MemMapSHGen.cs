using System.Collections.Generic;

namespace DevForge.Lib.Ponder
{
    public static class MemMapSHGen
    {
        public const uint AddrStart = 0x8C000000;

        public static IEnumerable<uint> GetAddresses(int maxChunkSize)
        {
            for (uint i = 0; i < 1024 * 16; i++)
                yield return (uint)(0x8C000000 + (maxChunkSize * i));

            for (uint i = 0; i < 1024; i++)
                yield return (uint)(0x8C400000 + (maxChunkSize * i));

            for (uint i = 0; i < 1024; i++)
                yield return (uint)(0x8CC00000 + (maxChunkSize * i));

            for (uint i = 0; i < 1024; i++)
                yield return (uint)(0xA0000000 + (maxChunkSize * i));

            // TLB Error in 0xC0000000 !

            for (uint i = 0; i < 1024; i++)
                yield return (uint)(0xD0000000 + (maxChunkSize * i));
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