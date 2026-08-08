namespace DevForge.Lib.Messages.Impl
{
	public sealed class Jump : BaseTxt
    {
        public Jump(string text) : base(text)
        {
            Kind = MsgKind.JumpFar;
        }

        public Jump(Message msg) : base(msg)
        {
            Kind = MsgKind.JumpFar;
        }
    }
}