namespace DevForge.Lib.Messages.Impl
{
	public sealed class Quit : BaseTxt
    {
        public Quit(string text) : base(text)
        {
            Kind = MsgKind.Quit;
        }

        public Quit(Message msg) : base(msg)
        {
            Kind = MsgKind.Quit;
        }
    }
}