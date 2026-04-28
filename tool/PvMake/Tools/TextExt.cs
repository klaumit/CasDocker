using System.Text;

namespace PvMake.Tools
{
    public static class TextExt
    {
        public static Encoding Utf => Encoding.UTF8;

        public static string? TrimOrNull(this string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }
    }
}