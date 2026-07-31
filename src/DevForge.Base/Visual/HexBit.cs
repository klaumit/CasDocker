namespace DevForge.Lib.Visual
{
    public sealed class HexBit
    {
        public HexBit(uint addr, byte[] raw, int idx)
        {
            Addr = addr;
            Raw = raw;
            Idx = idx;
        }

        public uint Addr { get; private set; }
        public byte[] Raw { get; private set; }
        public int Idx { get; private set; }
    }
}