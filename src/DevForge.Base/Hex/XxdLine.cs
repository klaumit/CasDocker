using System.Globalization;
using DevForge.Lib.Tools;

// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Hex
{
    public sealed class XxdLine
    {
        public string Addr { get; set; }
        public string Raw { get; set; }
        public string Txt { get; set; }

        public override string ToString()
        {
            return string.Format("{0:X8}: {1}  {2}",
                GetAddr(), GetRaw().ToHexString(true, 2), Txt);
        }

        public uint? GetAddr()
        {
            return uint.Parse(Addr, NumberStyles.HexNumber);
        }

        public byte[] GetRaw()
        {
            return TextExt.FromHexString(Raw.Replace(" ", "").Trim());
        }
    }
}