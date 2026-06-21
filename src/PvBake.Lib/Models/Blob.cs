using Newtonsoft.Json;

namespace PvBake.Lib.Models
{
    public sealed class Blob
    {
        public uint? Length { get; set; }
        [JsonIgnore] public byte[] Data { get; set; }
    }
}