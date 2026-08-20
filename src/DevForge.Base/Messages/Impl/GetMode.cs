namespace DevForge.Lib.Messages.Impl
{
	public sealed class GetMode : BaseTxt
    {
        public GetMode(string text) : base(text)
        {
            Kind = MsgKind.GetMode;
        }

        public GetMode(Message msg) : base(msg)
        {
            Kind = MsgKind.GetMode;
        }
    }
}