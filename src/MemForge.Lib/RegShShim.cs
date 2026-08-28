using System;
using Vanara.PInvoke;
using System.Text;
using DevForge.Lib.Modern;
using MR = MemForge.Lib.MemReader;
using SP = Vanara.PInvoke.Kernel32.SafeHPROCESS;

namespace MemForge.Lib
{
    public sealed class RegShShim : RegShim
    {
        public RegShShim(SP proc, IntPtr baseAddr)
            : base(proc, baseAddr)
        {
        }
    }
}