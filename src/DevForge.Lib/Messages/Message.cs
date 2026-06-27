namespace DevForge.Lib.Messages
{
    public sealed class Message
    {
        public byte[] Sync { get; set; } = { 0xAA, 0x55 };
        public MsgKind Kind { get; set; }
        public ushort Length { get; set; }
        public byte[] Payload { get; set; }
        public byte[] Checksum { get; set; }
    }
}