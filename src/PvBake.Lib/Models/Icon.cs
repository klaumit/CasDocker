namespace PvBake.Lib.Models
{
    public sealed class Icon : IFile
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public bool[] Pixels { get; set; }
        public uint Length => (uint)(4 + ((Width + 7) / 8) * Height);

        public bool GetPixel(int x, int y) => Pixels[y * Width + x];
    }
}