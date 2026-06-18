using System;

namespace PvRanger
{
    public sealed class AddIn : IFile
    {
        public byte[] Sig { get; set; }
        public Model? Model { get; set; }
        public Version HeadVersion { get; set; }
        public ushort? Status { get; set; }
        public ushort? Mode { get; set; }
        public string Name { get; set; }
        public uint? Length { get; set; }
        public DateTime? AppCompiled { get; set; }
        public Version AppVersion { get; set; }
        public DateTime? LibCompiled { get; set; }
        public Version LibVersion { get; set; }
        public uint? MenuIcon { get; set; }
        public uint? ListIcon { get; set; }
        public string Comment { get; set; }
    }
}