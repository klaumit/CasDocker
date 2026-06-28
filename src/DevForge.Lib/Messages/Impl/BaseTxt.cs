namespace DevForge.Lib.Messages.Impl
{
    public abstract class BaseTxt : Message
    {
        protected BaseTxt(string text)
        {
            Payload = (Text = text).AsBytes();
        }

        protected BaseTxt(Message msg)
        {
            Text = (Payload = msg.Payload).AsString();
            Length = (ushort)Payload.Length;
            Checksum = msg.Checksum;
        }

        public string Text { get; set; }
    }
}