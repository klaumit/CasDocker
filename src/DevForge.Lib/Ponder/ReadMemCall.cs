namespace DevForge.Lib.Ponder
{
    public sealed class ReadMemCall
    {
        public ushort Addr { get; set; }
        public byte Bank { get; set; }
        public ushort Seg { get; set; }
        public ushort Off { get; set; }
        public ushort Len { get; set; }

        public override string ToString()
        {
            return $"ReadMem(0x{Addr:X4}, {Bank}, 0x{Seg:X4}, 0x{Off:X4}, {Len})";
        }
    }
}