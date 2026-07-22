namespace MemForge.Lib
{
    public static class ByteExt
    {
        public static int IndexOf(this byte[] haystack, byte[] pattern, int startIndex = 0)
        {
            if (pattern == null || pattern.Length == 0)
                return -1;
            if (haystack == null || haystack.Length < pattern.Length)
                return -1;

            var end = haystack.Length - pattern.Length;
            for (var i = startIndex; i <= end; i++)
            {
                var match = true;
                for (var j = 0; j < pattern.Length; j++)
                {
                    if (haystack[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return i;
            }
            return -1;
        }
    }
}