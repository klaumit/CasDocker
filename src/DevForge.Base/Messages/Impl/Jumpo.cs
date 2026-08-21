using System.Globalization;

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

        public void Unpack(out byte kind, out ushort code, out ushort stat, out byte val, out uint ptr)
        {
            var joParts = Text.Split('|');
            kind = byte.Parse(joParts[0], NumberStyles.HexNumber);
            code = ushort.Parse(joParts[1], NumberStyles.HexNumber);
            stat = ushort.Parse(joParts[2], NumberStyles.HexNumber);
            val = byte.Parse(joParts[3], NumberStyles.HexNumber);
            ptr = uint.Parse(joParts[4], NumberStyles.HexNumber);
        }
    }
}