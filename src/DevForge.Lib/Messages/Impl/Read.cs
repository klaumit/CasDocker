namespace DevForge.Lib.Messages.Impl
{
    public sealed class Read : BaseTxt
    {
        public Read(string text) : base(text)
        {
            Kind = MsgKind.MemRead;
        }

        public Read(Message msg) : base(msg)
        {
            Kind = MsgKind.MemRead;
        }
    }
}