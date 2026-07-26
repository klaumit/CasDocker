using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using System.Linq;
using System.Threading;
using DevForge.Lib.API;
using DevForge.Lib.Messages;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using F = System.IO.File;

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