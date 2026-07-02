namespace PvMake.Lib
{
    public static class TextExt
    {
        public static string TrimOrNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}