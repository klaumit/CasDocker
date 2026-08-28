using System;
using Vanara.PInvoke;

namespace MemForge.Lib
{
    public sealed class RegShShim : RegShim
    {
        public RegShShim(Kernel32.SafeHPROCESS proc, IntPtr baseAddr)
            : base(proc, baseAddr)
        {
        }
    }
}