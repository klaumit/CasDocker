using System.Globalization;

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

        public void Unpack(out byte kind, out ushort code, out ushort stat, out uint ptr)
        {
            var gmParts = Text.Split('|');
            kind = byte.Parse(gmParts[0], NumberStyles.HexNumber);
            code = ushort.Parse(gmParts[1], NumberStyles.HexNumber);
            stat = ushort.Parse(gmParts[2], NumberStyles.HexNumber);
            ptr = uint.Parse(gmParts[3], NumberStyles.HexNumber);
        }
    }
}