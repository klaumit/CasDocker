namespace DevForge.Lib.Messages.Impl
{
    public sealed class Hello : Message
    {
        public Hello(string text)
        {
            Kind = MsgKind.Hello;
            Payload = text.AsBytes();
        }
    }
}