using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DevForge.Lib.Hex;

// ReSharper disable ConvertToUsingDeclaration

namespace DevForge.Lib.Comb
{
    public static class Searcher
    {
        public static IEnumerable<TxtMatch> FindNeedle(string path, string needle)
        {
            var reader = File.OpenText(path);
            var lines = XxdFile.ReadHexLines(reader);
            return lines.FindNeedle(needle);
        }

        public static IEnumerable<TxtMatch> FindNeedle(this IEnumerable<XxdLine> lines, string needle)
        {
            var needleHex = ToHex(needle);

            string prevAddrText = null;
            var prevHex = "";

            foreach (var line in lines)
            {
                var curAddrText = line.Addr;
                var curHex = line.Raw.Replace(" ", "");

                var combined = prevHex + curHex;

                var searchFrom = 0;
                int idx;
                const StringComparison oi = StringComparison.OrdinalIgnoreCase;
                while ((idx = combined.IndexOf(needleHex, searchFrom, oi)) >= 0)
                {
                    string matchAddrText;
                    int matchOffset;

                    if (idx < prevHex.Length)
                    {
                        matchAddrText = prevAddrText;
                        matchOffset = idx / 2;
                    }
                    else
                    {
                        matchAddrText = curAddrText;
                        matchOffset = (idx - prevHex.Length) / 2;
                    }

                    var lineAddr = Convert.ToUInt32(matchAddrText, 16);
                    var absolute = (uint)(lineAddr + matchOffset);

                    yield return new TxtMatch
                    {
                        Address = matchAddrText,
                        Offset = matchOffset,
                        Absolute = absolute
                    };

                    searchFrom = idx + 1;
                }

                prevHex = curHex;
                prevAddrText = curAddrText;
            }
        }

        private static string ToHex(string text)
        {
            var bld = new StringBuilder();
            foreach (var c in text)
                bld.Append(((byte)c).ToString("x2"));
            return bld.ToString();
        }
    }
}