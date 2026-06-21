namespace PvBake.Lib.Models
{
    public sealed class Icon : IFile
    {
        public ushort? Width { get; set; }
        public ushort? Height { get; set; }
        public byte[] Pixels { get; set; }
    }
}