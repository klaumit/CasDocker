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
	public enum RegShName
	{
		None = 0,
		
		R0,
		R1,
		R2,
		R3,
		R4,
		R5,
		R6,
		R7,
		R8,
		R9,
		R10,
		R11,
		R12,
		R13,
		R14,
		R15,
		GBR,
		MACH,
		MACL,
		PR,
		PC,
		SR,
		R0b,
		R1b,
		R2b,
		R3b,
		R4b,
		R5b,
		R6b,
		R7b,
		VBR,
		SPC,
		SSR
	}
}