using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PvBake.Lib.Tools;

namespace PvBake.Lib.Encodings
{
    public static class Specials
    {
        private static readonly Dictionary<byte, char> PvRusF;
        private static readonly Dictionary<char, byte> PvRusT;

        static Specials()
        {
            var pvRusJson = typeof(Specials).GetEmbeddedText("pvRus.json");
            var pvRusDict = JsonTool.FromJson<Dictionary<string, string>>(pvRusJson);
            PvRusF = pvRusDict.ToDictionary(
                k => byte.Parse(k.Key, NumberStyles.HexNumber),
                v => v.Value.Single()
            );
            PvRusT = pvRusDict.ToDictionary(
                k => k.Value.Single(),
                v => byte.Parse(v.Key, NumberStyles.HexNumber)
            );
        }

        public static byte[] TryAsPvRus(this string text, int index, int count, out int err)
        {
            var bld = new List<byte>();
            var error = 0;
            for (var i = index; i < (index + count); i++)
            {
                var bit = text[i];
                if (PvRusT.TryGetValue(bit, out var val))
                    bld.Add(val);
                else
                    error++;
            }
            var res = bld.Count == 0 ? null : bld.ToArray();
            err = error;
            return res;
        }

        public static string TryAsPvRus(this byte[] bytes, int index, int count, out int err)
        {
            var bld = new StringBuilder();
            var error = 0;
            for (var i = index; i < (index + count); i++)
            {
                var bit = bytes[i];
                if (PvRusF.TryGetValue(bit, out var val))
                    bld.Append(val);
                else
                    error++;
            }
            var res = bld.Length == 0 ? null : bld.ToString();
            err = error;
            return res;
        }
    }
}