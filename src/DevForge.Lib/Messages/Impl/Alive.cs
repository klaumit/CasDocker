namespace DevForge.Lib.Messages.Impl
{
    public sealed class Alive : BaseTxt
    {
        public Alive(string text) : base(text)
        {
            Kind = MsgKind.Alive;
        }

        public Alive(Message msg) : base(msg)
        {
            Kind = MsgKind.Alive;
        }
    }
}