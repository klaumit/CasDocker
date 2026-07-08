namespace DevForge.Lib.High
{
    public sealed class PvBuff
    {
        public ushort Src { get; set; }
        public byte Bank { get; set; }
        public ushort Seg { get; set; }
        public ushort Off { get; set; }
        public ushort Size { get; set; }
        public byte[] Hex { get; set; }
    }
}