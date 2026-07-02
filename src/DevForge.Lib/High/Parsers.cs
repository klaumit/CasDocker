using System.Collections.Generic;
using DevForge.Lib.Messages.Impl;
using DevForge.Lib.Tools;

namespace DevForge.Lib.High
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

        public static PvInfo Parse(Hello hello)
        {
            var text = hello.Text;
            var dict = ParseDict(text);
            var json = JsonExt.ToJson(dict);
            var info = JsonExt.ToObj<PvInfo>(json);
            return info;
        }
    }
}