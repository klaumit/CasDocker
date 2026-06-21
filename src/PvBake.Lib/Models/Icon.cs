namespace PvBake.Lib.Models
{
    public sealed class Icon : IFile
    {
        public byte? Width { get; set; }
        public byte? Height { get; set; }
        public byte[] Pixels { get; set; }
    }
}