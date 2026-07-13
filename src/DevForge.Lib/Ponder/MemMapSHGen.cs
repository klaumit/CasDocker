using System.Collections.Generic;

namespace DevForge.Lib.Ponder
{
    public static class MemMapSHGen
    {
        public const int MaxChunkSize = 64;
        public const uint AddrStart = 0x8C000000;

        public static IEnumerable<uint> GetAddresses()
        {
            for (uint i = 0; i < 1000; i++)
                yield return AddrStart + (MaxChunkSize * i);
        }

        public static long GetSHAddress(this PvBuff call)
        {
            return (call.Seg << 16) | call.Off;
        }

        public static IEnumerable<string> PrintSHHex(this PvBuff buff)
        {
            var array = buff.Bytes;
            if (array == null) array = new byte[0];
            return MemMapGen.PrintHexDump(buff.GetSHAddress(), array);
        }

        public static List<PvBuff> GenerateCalls()
        {
            var calls = new List<PvBuff>();
            foreach (var addr in GetAddresses())
            {
                var length = MaxChunkSize;
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