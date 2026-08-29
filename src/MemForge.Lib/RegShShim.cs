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
    public sealed class RegShShim : RegShim
    {
        public RegShShim(SP proc, IntPtr baseAddr)
            : base(proc, baseAddr)
        {
        }

		public override void TestIt()
		{
            var region = S.ReadBytes(Proc, BaseAddr, 0, 32);
            var hex = Hexer.Tools.TextExt.ToHex(region);

            
            // 0=R0|8=R1|16=R2|24=R3|32=R4|40=R5|48=R6|56=R7|64=R8|72=R9|80=R10|88=R11|96=R12|104=R13|112=R14|120=R15|128=SR|136=GBR|144=VBR|152=SSR|160=SPC|168=R0b|176=R1b|184=R2b|192=R3b|200=R4b|208=R5b|216=R6b|224=R7b|232=MACH|240=MACL|248=PR|256=PC


            
            



            throw new NotImplementedException(hex);
		}
	}
}