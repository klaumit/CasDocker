using DevForge.Lib.API;

// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Ponder
{
    public sealed class PvBuff : IPvBuff
    {
        public ushort Src { get; set; }
        public byte Bank { get; set; }
        public ushort Seg { get; set; }
        public ushort Off { get; set; }
        public ushort Size { get; set; }
        public byte[] Bytes { get; set; }

        public override string ToString()
        {
            var hl = Bytes == null ? "" : " -> " + Bytes.Length;
            return string.Format("PvBuff(0x{0:X4}, {1}, 0x{2:X4}, 0x{3:X4}, {4}{5})",
                Src, Bank, Seg, Off, Size, hl);
        }
    }
}