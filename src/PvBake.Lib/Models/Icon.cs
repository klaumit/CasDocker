using Newtonsoft.Json;

namespace PvBake.Lib.Models
{
    public sealed class Icon : IFile
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        [JsonIgnore]
        public bool[] Pixels { get; set; }
        public uint Length => (uint)(4 + GetRowBytes() * Height);

        public int GetRowBytes() => (Width + 7) / 8;
    }
}