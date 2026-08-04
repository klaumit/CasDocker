
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Comb
{
    public sealed class TxtMatch
    {
        public string Address { get; set; }
        public int Offset { get; set; }
        public uint Absolute { get; set; }

        public override string ToString()
        {
            return string.Format("{0} +{1:d2} ({2:x8})", Address, Offset, Absolute);
        }
    }
}