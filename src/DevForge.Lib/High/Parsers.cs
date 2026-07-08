using System.Collections.Generic;
using DevForge.Lib.Messages.Impl;
using DevForge.Lib.Tools;

namespace DevForge.Lib.High
{
    public static class Parsers
    {
        public static Dictionary<string, string> ParseArray(string text)
        {
            var dict = new Dictionary<string, string>();
            var i = 0;
            foreach (var tmp in text.Split('|'))
            {
                var key = $"{(++i):D}".TrimOrNull();
                var val = tmp.TrimOrNull();
                if (key == null || val == null)
                    continue;
                dict[key] = val;
            }
            return dict;
        }

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

        public static PvBuff Parse(Read read)
        {
            var text = read.Text;
            var dict = ParseArray(text);
            dict.TryGetValue("6", out var hV);
            var info = new PvBuff
            {
                Src = (ushort)TextExt.ParseHex(dict["1"], 0),
                Bank = (byte)TextExt.ParseHex(dict["2"], 0),
                Seg = (ushort)TextExt.ParseHex(dict["3"], 0),
                Off = (ushort)TextExt.ParseHex(dict["4"], 0),
                Size = (ushort)TextExt.ParseHex(dict["5"], 0),
                Hex = TextExt.FromHexString(hV)
            };
            return info;
        }
    }
}