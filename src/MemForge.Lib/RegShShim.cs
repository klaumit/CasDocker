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
	public sealed class RegShShim : RegShim<RegShName>
	{
		private static readonly Dictionary<int, RegShName> Offsets = new Dictionary<int, RegShName>
		{
			[0] = RegShName.R0, [8] = RegShName.R1, [16] = RegShName.R2,
			[24] = RegShName.R3, [32] = RegShName.R4, [40] = RegShName.R5,
			[48] = RegShName.R6, [56] = RegShName.R7, [64] = RegShName.R8,
			[72] = RegShName.R9, [80] = RegShName.R10, [88] = RegShName.R11,
			[96] = RegShName.R12, [104] = RegShName.R13, [112] = RegShName.R14,
			[120] = RegShName.R15, [128] = RegShName.SR, [136] = RegShName.GBR,
			[144] = RegShName.VBR, [152] = RegShName.SSR, [160] = RegShName.SPC,
			[168] = RegShName.R0b, [176] = RegShName.R1b, [184] = RegShName.R2b,
			[192] = RegShName.R3b, [200] = RegShName.R4b, [208] = RegShName.R5b,
			[216] = RegShName.R6b, [224] = RegShName.R7b, [232] = RegShName.MACH,
			[240] = RegShName.MACL, [248] = RegShName.PR, [256] = RegShName.PC
		};

		public RegShShim(SP proc, IntPtr baseAddr) : base(proc, baseAddr)
		{
		}

		public override IDictionary<RegShName, string> ReadRegs()
		{
			var region = S.ReadBytes(Proc, BaseAddr, 0, 270);
			var res = S.ReadUInt32(region, Offsets);

			var hex = Hexer.Tools.TextExt.ToHex(region);
			var debug = hex + "\n" + JsonExt.ToJson(res, false);
			Console.WriteLine(debug);

			return res;
		}
	}
}