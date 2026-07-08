using DevForge.Lib.Tools;

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

        public byte AsNumber()
        {
            return (byte)TextExt.ParseHex(Text, 0);
        }
    }
}