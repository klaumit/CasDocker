using System;
using System.Collections.Generic;
using System.Linq;

namespace PvMake.Lib
{
    public static class CollExt
    {
        public static SortedDictionary<string, SortedSet<string>> Copy(
            this IDictionary<string, SortedSet<string>> raw, List<string> allowed,
            List<KeyValuePair<string, Func<string, bool>>> actions)
        {
            var dict = new SortedDictionary<string, SortedSet<string>>();
            foreach (var key in allowed)
            {
                SortedSet<string> set;
                if (!raw.TryGetValue(key, out set))
                    continue;
                dict.Add(key, set);
            }
            foreach (var item in actions)
            {
                var key = item.Key;
                var func = item.Value;
                SortedSet<string> set;
                if (!raw.TryGetValue(key, out set))
                    continue;
                dict.Add(key, new SortedSet<string>(set.Where(s => func(s))));
            }
            return dict;
        }
    }
}