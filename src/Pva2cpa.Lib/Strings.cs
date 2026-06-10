namespace Pva2cpa.Lib
{
    public static class Strings
    {
        public static bool IsNullOrWhiteSpace(string folder)
        {
#if NET20
            return string.IsNullOrEmpty(folder);
#else
            return string.IsNullOrWhiteSpace(folder);
#endif
        }
    }
}