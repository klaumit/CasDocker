using System.Text;

namespace PvRanger
{
    public static class TextExt
    {
        static TextExt()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static Encoding Utf => Encoding.UTF8;

        public static Encoding Win => Encoding.GetEncoding("Windows-1252");
    }
}