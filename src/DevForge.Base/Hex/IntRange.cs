using System;
using DevForge.Lib.Tools;

// ReSharper disable ArrangeAccessorOwnerBody
// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Hex
{
    public sealed class IntRange
    {
        public uint? Off { get; set; }
        public uint? Len { get; set; }

        public uint? Next { get { return Off + Len; } }

        public string Desc { get { return string.Format("{0:X8} + {1:X8} --> {2:X8}", Off, Len, Next); } }

        public string T
        {
            set
            {
                var tmp = value.Split(new[] { '-' }, 2);
                var off = tmp[0].Trim();
                var end = tmp[1].Trim('-', '>', ' ');
                var offH = TextExt.ParseHex(off, -1);
                var endH = TextExt.ParseHex(end, -1);
                if (offH == -1 || endH == -1)
                    throw new InvalidOperationException(value);
                Off = (uint)offH;
                Len = (uint)endH - Off;
            }
        }

        public override string ToString() { return Desc; }
    }
}