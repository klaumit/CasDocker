using System;
using System.Collections.Generic;
using System.Linq;

namespace PvMake.Tools
{
    public static class CollExt
    {
        public static SortedDictionary<string, SortedSet<string>> Copy(
            this IDictionary<string, SortedSet<string>> raw, List<string> allowed,
            List<(string ext, Func<string, bool> func)> actions)
        {
            var dict = new SortedDictionary<string, SortedSet<string>>();
            foreach (var key in allowed)
            {
                if (!raw.TryGetValue(key, out var set))
                    continue;
                dict.Add(key, set);
            }
            foreach (var (key, func) in actions)
            {
                if (!raw.TryGetValue(key, out var set))
                    continue;
                dict.Add(key, new SortedSet<string>(set.Where(s => func(s))));
            }
            return dict;
        }
    }
}