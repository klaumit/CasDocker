using System.Collections.Generic;
using Newtonsoft.Json;

namespace PvBake.Lib.Models
{
    public sealed class Dump : IFile
    {
        public int Length { get; set; }
        public Bios Bios { get; set; }
        [JsonIgnore]
        public SortedDictionary<int, AddIn> AddIns { get; set; }
        public SortedDictionary<int, Blob> Blobs { get; set; }
    }
}