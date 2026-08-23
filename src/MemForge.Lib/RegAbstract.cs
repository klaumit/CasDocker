using System;
using System.Text;
using DevForge.Lib.Modern;
using Vanara.PInvoke;
using MR = MemForge.Lib.MemReader;
using SP = Vanara.PInvoke.Kernel32.SafeHPROCESS;

namespace MemForge.Lib
{
	public abstract class RegShim
	{
	}

	public sealed class Reg86Shim : RegShim
	{
		public Reg86Shim(SP rwHandle, IntPtr address)
		{
			RwHandle = rwHandle;
			Address = address;
		}

		public SP RwHandle { get; }
		public IntPtr Address { get; }
	}

	public sealed class RegShShim : RegShim
	{
		public RegShShim(SP rwHandle, IntPtr address)
		{
			RwHandle = rwHandle;
			Address = address;
		}

		public SP RwHandle { get; }
		public IntPtr Address { get; }
	}
}