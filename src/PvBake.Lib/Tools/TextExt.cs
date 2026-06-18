using System.Text;

namespace PvRanger
{
    public static class TextExt
    {
        static TextExt()
        {
#if NETFRAMEWORK
#else
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }

        public static Encoding Utf => Encoding.UTF8;

        public static Encoding Win => Encoding.GetEncoding("Windows-1252");
    }
}