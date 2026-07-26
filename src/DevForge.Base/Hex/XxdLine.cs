using DevForge.Lib.Tools;

// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Hex
{
    public sealed class XxdLine
    {
        public uint? Addr { get; set; }
        public byte[] Raw { get; set; }
        public string Txt { get; set; }

        public override string ToString()
        {
            return string.Format("{0:X8}: {1}  {2}",
                Addr, Raw.ToHexString(true, 2), Txt);
        }
    }
}