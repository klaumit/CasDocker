namespace DevForge.Lib.Messages.Impl
{
    public sealed class Quit : Message
    {
        public Quit(string text)
        {
            Kind = MsgKind.Quit;
            Payload = text.AsBytes();
        }
    }
}