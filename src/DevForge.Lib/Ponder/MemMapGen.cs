using System;
using System.Collections.Generic;
using System.Text;
using DevForge.Lib.High;

// ReSharper disable UseArrayEmptyMethod

namespace DevForge.Lib.Ponder
{
    public static class MemMapGen
    {
        public static IEnumerable<string> PrintHex(this PvBuff call, PvCpu cpu)
        {
            switch (cpu)
            {
                case PvCpu.X86: return call.Print86Hex();
                case PvCpu.SH3: return call.PrintSHHex();
                default: return new string[0];
            }
        }

        public static uint GetAddress(this PvBuff call, PvCpu cpu)
        {
            switch (cpu)
            {
                case PvCpu.X86: return call.Get86Address();
                case PvCpu.SH3: return call.GetSHAddress();
                default: return 0;
            }
        }

        internal static IEnumerable<string> PrintHexDump(long address, byte[] data, int len = 16)
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