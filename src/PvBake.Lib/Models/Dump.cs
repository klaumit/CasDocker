using System.Collections.Generic;

namespace PvBake.Lib.Models
{
    public sealed class Dump : IFile
    {
        public int Length { get; set; }
        public Bios Bios { get; set; }
        public List<AddIn> AddIns { get; set; }
    }
}