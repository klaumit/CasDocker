using System;
using System.Collections.Generic;
using System.Text;

namespace DevForge.Lib.Ponder
{
    internal static class MemMapGen
    {
        public static IEnumerable<string> PrintHexDump(long address, byte[] data, int len = 16)
        {
            for (var rowStart = 0; rowStart < data.Length; rowStart += len)
            {
                var rowLen = Math.Min(len, data.Length - rowStart);
                var rowAddress = address + rowStart;
                var line = new StringBuilder();
                line.Append(rowAddress.ToString("X8")).Append(": ");
                for (var i = 0; i < len; i += 2)
                {
                    line.Append(i < rowLen ? data[rowStart + i].ToString("x2") : "  ");
                    line.Append(i + 1 < rowLen
                        ? data[rowStart + i + 1].ToString("x2")
                        : i + 1 < len
                            ? "  "
                            : "");
                    line.Append(' ');
                }
                line.Append(' ');
                for (var i = 0; i < rowLen; ++i)
                {
                    var b = data[rowStart + i];
                    line.Append(b >= 0x20 && b <= 0x7E ? (char)b : '.');
                }
                yield return line.ToString();
            }
        }
    }
}