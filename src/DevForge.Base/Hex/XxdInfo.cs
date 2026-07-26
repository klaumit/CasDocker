using System.Collections.Generic;

// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Common
{
    public sealed class XxdInfo
    {
        public long Pos { get; set; }
        public SortedDictionary<uint, IntRange> Ranges { get; set; }
    }
}