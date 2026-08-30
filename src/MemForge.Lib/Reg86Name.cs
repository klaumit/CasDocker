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

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeObjectCreationWhenTypeEvident

namespace MemForge.Lib
{
	public enum Reg86Name
	{
		None = 0,

		AX,
		BX,
		CX,
		DX,
		SI,
		DI,
		DS,
		ES,
		SS,
		SP,
		BP,
		CS,
		IP
	}
}