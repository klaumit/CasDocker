using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Linq;
using MemForge.Lib;
using B = System.BitConverter;
using X = MemForge.Lib.ByteExt;
using DevForge.Lib.Modern;
using MR = MemForge.Lib.MemReader;

namespace WinFinder
{
	public static class Monitoring
	{
		public static void ReadReg86(uint pid)
		{
			byte[] pattern = {
				0x02, 0x00, 0x00, 0x01, 0x00, 0x50, 0x6F, 0x63,
				0x6B, 0x65, 0x74, 0x56, 0x69, 0x65, 0x77, 0x65,
				0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
			foreach (var item in MR.ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000080 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x01000000 &&
					item.Info.RegionSize == 0x0005F000)
				{
					var offset = item.Buffer.IndexOf(pattern);
					if (offset >= 0)
					{
						var real = offset - 55;

						// 0=AX|2=CX|4=DX|6=BX|8=SP|10=BP|12=SI|14=DI|16=ES|18=CS|20=SS|22=DS|24=IP
						Console.WriteLine(" x86 | " + real + " |" + item);
						;
					}
				}
			}
		}

		public static void ReadRegSh(uint pid)
		{
			byte[] pattern ={
				0x00, 0xE6, 0x78, 0x02, 0x8C, 0x08, 0x5A, 0x02, 0x8C,
				0xE1, 0x10, 0x00, 0x40, 0xE0, 0x00, 0x00, 0x00, 0x01,
				0x00, 0x00, 0x00, 0x19, 0x07, 0x00, 0x00, 0x85, 0xAA,
				0x58, 0x02, 0xB8, 0xF6, 0x12, 0x00, 0x00
			};
			foreach (var item in MR.ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000080 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x01000000 &&
					item.Info.RegionSize == 0x001E9000)
				{
					var offset = item.Buffer.IndexOf(pattern);
					if (offset >= 0)
					{
						var real = offset - 267;

						// 0=R0|8=R1|16=R2|24=R3|32=R4|40=R5|48=R6|56=R7|64=R8|72=R9|80=R10|88=R11|96=R12|104=R13|112=R14|120=R15|128=SR|136=GBR|144=VBR|152=SSR|160=SPC|168=R0b|176=R1b|184=R2b|192=R3b|200=R4b|208=R5b|216=R6b|224=R7b|232=MACH|240=MACL|248=PR|256=PC
						Console.WriteLine(" sh3 | " + real + " |" + item);
						;
					}
				}
			}
		}
	}
}