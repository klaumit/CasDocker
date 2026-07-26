using DevForge.Lib.Tools;
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