using System;
using Vanara.PInvoke;
using System.Text;
using DevForge.Lib.Modern;
using MR = MemForge.Lib.MemReader;
using SP = Vanara.PInvoke.Kernel32.SafeHPROCESS;
using System.IO;
using System.Runtime.InteropServices;
using K = Vanara.PInvoke.Kernel32;
using S = MemForge.Lib.Shimming;
using DevForge.Lib.Tools;

namespace MemForge.Lib
{
    public sealed class Reg86Shim : RegShim
    {
        public Reg86Shim(SP proc, IntPtr baseAddr)
            : base(proc, baseAddr)
        {
        }

		public override void TestIt()
		{
            var region = S.ReadBytes(Proc, BaseAddr, 0, 32);
            var hex = Hexer.Tools.TextExt.ToHex(region);





            throw new NotImplementedException(hex);
		}
	}
}