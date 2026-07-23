using DevForge.Lib.High;

namespace DevForge.Lib.Messages.Impl
{
	public sealed class Hello : BaseTxt
    {
        public Hello(string text) : base(text)
        {
            Kind = MsgKind.Hello;
        }

        public Hello(Message msg) : base(msg)
        {
            Kind = MsgKind.Hello;
        }

        public PvInfo AsInfo()
        {
            return Parsers.Parse(this);
        }
    }
}