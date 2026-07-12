// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Ponder
{
    public sealed class PvBuff
    {
        public ushort Addr { get; set; }
        public byte Bank { get; set; }
        public ushort Seg { get; set; }
        public ushort Off { get; set; }
        public ushort Len { get; set; }
        public byte[] Hex { get; set; }

        public override string ToString()
        {
            var hl = Hex == null ? "" : " -> " + Hex.Length + " bytes";
            return string.Format("PvBuff(0x{0:X4}, {1}, 0x{2:X4}, 0x{3:X4}, {4}{5})",
                Addr, Bank, Seg, Off, Len, hl);
        }
    }
}