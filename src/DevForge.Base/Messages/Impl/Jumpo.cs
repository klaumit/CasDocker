namespace DevForge.Lib.Messages.Impl
{
	public sealed class Jumpo : BaseTxt
    {
        public Jumpo(string text) : base(text)
        {
            Kind = MsgKind.JumpOS;
        }

        public Jumpo(Message msg) : base(msg)
        {
            Kind = MsgKind.JumpOS;
        }
    }
}