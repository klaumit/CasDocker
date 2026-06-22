using System;
using Newtonsoft.Json;

namespace PvBake.Lib.Models
{
    public sealed class Bios : IFile
    {
        [JsonIgnore]
        public byte[] Sig { get; set; }
        public Model? Model { get; set; }
        public uint? Length { get; set; }
        public DateTime? Compiled { get; set; }
        public Model? SwModel { get; set; }
        [JsonIgnore]
        public byte[] Payload { get; set; }
    }
}