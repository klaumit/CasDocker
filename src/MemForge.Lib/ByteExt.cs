using System.Linq;
using System.Collections.Generic;

namespace MemForge.Lib
{
    public static class ByteExt
    {
        public static IEnumerable<KeyValuePair<int, int>> FirstIndicesOf(
            this byte[] haystack, params byte[][] patterns)
        {
            var i = 0;
            foreach (var pattern in patterns)
            {
                var idx = IndicesOf(haystack, pattern).Take(1).ToArray();
                if (idx.Length == 1)
                {
                    yield return new KeyValuePair<int, int>(i++, idx[0]);
                    continue;
                }
                break;
            }
        }

        public static IEnumerable<int> IndicesOf(this byte[] haystack, byte[] pattern)
        {
            var start = 0;
            int idx;
            while ((idx = haystack.IndexOf(pattern, start)) >= 0)
            {
                yield return idx;
                start = idx + pattern.Length;
            }
        }

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