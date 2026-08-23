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
			foreach (var item in MR.ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000080 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x01000000 &&
					item.Info.RegionSize == 0x0005F000)
				{
					Console.WriteLine(" x86 | " + item);
					;
				}
			}
		}

		public static void ReadRegSh(uint pid)
		{
			foreach (var item in MR.ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000080 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x01000000 &&
					item.Info.RegionSize == 0x001E9000)
				{
					Console.WriteLine(" sh3 | " + item);
					;
				}
			}
		}
	}
}