using System.Linq;
using System.Text;

// ReSharper disable ArrangeAccessorOwnerBody

namespace PvMake.Lib
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

        public static Encoding Utf
        {
            get { return Encoding.UTF8; }
        }

        public static Encoding Win
        {
            get { return Encoding.GetEncoding(1252); }
        }

        public static string TrimOrNull(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }

        public static string[] Split(char sep, string text)
        {
            return text.Split(sep)
                .Select(t => t.TrimOrNull())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToArray();
        }
    }
}