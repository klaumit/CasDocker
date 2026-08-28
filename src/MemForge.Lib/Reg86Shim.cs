using System;
using Vanara.PInvoke;

namespace MemForge.Lib
{
    public sealed class Reg86Shim : RegShim
    {
        public Reg86Shim(Kernel32.SafeHPROCESS proc, IntPtr baseAddr)
            : base(proc, baseAddr)
        {
        }
    }
}