using Newtonsoft.Json;

namespace sss
{
    public sealed class PvInfo
    {
        public string App { get; set; }
        public PvCpu Cpu { get; set; }
        public PvComm Comm { get; set; }
        public PvArea Area { get; set; }
        [JsonConverter(typeof(PvVerConv))] public PvVer Ver { get; set; }
        public PvChip Chip { get; set; }
        public ushort Mem { get; set; }
    }
}