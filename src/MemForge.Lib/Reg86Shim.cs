using System;
using Vanara.PInvoke;
using System.Text;
using DevForge.Lib.Modern;
using MR = MemForge.Lib.MemReader;
using SP = Vanara.PInvoke.Kernel32.SafeHPROCESS;

namespace MemForge.Lib
{
    public sealed class Reg86Shim : RegShim
    {
        public Reg86Shim(SP proc, IntPtr baseAddr)
            : base(proc, baseAddr)
        {
        }
    }
}