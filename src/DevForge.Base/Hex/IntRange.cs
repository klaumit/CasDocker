
// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Hex
{
    public sealed class IntRange
    {
        public uint? Off { get; set; }
        public uint? Len { get; set; }
        public uint? Next => Off + Len;

        public string Desc
            => string.Format("{0:X8} + {1:X8} --> {2:X8}", Off, Len, Next);

        public override string ToString()
        {
            return Desc;
        }
    }
}