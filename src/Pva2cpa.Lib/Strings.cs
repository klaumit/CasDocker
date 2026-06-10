namespace Pva2cpa.Lib
{
    public static class Strings
    {
        public static bool IsNullOrWhiteSpace(string folder)
        {
#if NETFRAMEWORK
            return string.IsNullOrEmpty(folder);
#else
            return string.IsNullOrWhiteSpace(folder);
#endif
        }
    }
}