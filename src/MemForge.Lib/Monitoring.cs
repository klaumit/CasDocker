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
						Console.WriteLine(" x86 | " + item);
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
						Console.WriteLine(" sh3 | " + item);
						;
					}
				}
			}
		}
	}
}