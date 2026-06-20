using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PvBake.Lib.Tools;

namespace PvBake.Lib.Encodings
{
    public static class Specials
    {
        private static readonly Dictionary<byte, char> PvRus;

        static Specials()
        {
            var pvRusJson = typeof(Specials).GetEmbeddedText("pvRus.json");
            var pvRusDict = JsonTool.FromJson<Dictionary<string, string>>(pvRusJson);
            PvRus = pvRusDict.ToDictionary(
                k => byte.Parse(k.Key, NumberStyles.HexNumber),
                v => v.Value.Single()
            );
        }

        public static string TryAsPvRus(byte[] bytes, int index, int count, out int err)
        {
            var bld = new StringBuilder();
            var error = 0;
            for (var i = index; i < (index + count); i++)
            {
                var bit = bytes[i];
                if (PvRus.TryGetValue(bit, out var val))
                    bld.Append(val);
                else
                    error++;
            }
            var res = bld.ToString();
            err = error;
            return res;
        }
    }
}