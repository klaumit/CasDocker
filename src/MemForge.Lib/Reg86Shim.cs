using System;
using System.Collections.Generic;
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

// ReSharper disable ArrangeObjectCreationWhenTypeEvident

namespace MemForge.Lib
{
	public sealed class Reg86Shim : RegShim<Reg86Name>
	{
		private static readonly Dictionary<int, Reg86Name> Offsets = new Dictionary<int, Reg86Name>
		{
			[0] = Reg86Name.AX, [2] = Reg86Name.CX, [4] = Reg86Name.DX,
			[6] = Reg86Name.BX, [8] = Reg86Name.SP, [10] = Reg86Name.BP,
			[12] = Reg86Name.SI, [14] = Reg86Name.DI, [16] = Reg86Name.ES,
			[18] = Reg86Name.CS, [20] = Reg86Name.SS, [22] = Reg86Name.DS,
			[24] = Reg86Name.IP
		};

		public Reg86Shim(SP proc, IntPtr baseAddr) : base(proc, baseAddr)
		{
		}

		public override IDictionary<Reg86Name, string> ReadRegs()
		{
			var region = S.ReadBytes(Proc, BaseAddr, 0, 26);
			var res = S.ReadUInt16(region, Offsets);
			return res;
		}
	}
}