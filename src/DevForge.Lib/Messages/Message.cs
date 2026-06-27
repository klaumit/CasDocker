namespace DevForge.Lib.Messages
{
    public sealed class Message
    {
        public MsgKind Kind { get; set; }
        public ushort Length { get; set; }
        public byte[] Payload { get; set; }
        public ushort Checksum { get; set; }
    }
}