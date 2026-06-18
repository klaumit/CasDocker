namespace PvBake.Lib.Models
{
    public sealed class Bios : IFile
    {
        public byte[] Sig { get; set; }
        public Model? Model { get; set; }
        public uint? Length { get; set; }
    }
}