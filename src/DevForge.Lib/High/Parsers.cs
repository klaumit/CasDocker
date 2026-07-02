using System.Collections.Generic;
using PvMake.Lib;

namespace sss
{
    public static class Parsers
    {
        public static Dictionary<string, string> ParseDict(string text)
        {
            var dict = new Dictionary<string, string>();
            foreach (var item in text.Split(';'))
            {
                var tmp = item.Split(new[] { '=' }, 2);
                if (tmp.Length != 2)
                    continue;
                var key = tmp[0].TrimOrNull();
                var val = tmp[1].TrimOrNull();
                if (key == null || val == null)
                    continue;
                dict[key] = val;
            }
            return dict;
        }
    }
}