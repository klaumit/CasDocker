using System;
using System.Text;
using DevForge.Lib.Modern;
using Vanara.PInvoke;
using MR = MemForge.Lib.MemReader;
using SP = Vanara.PInvoke.Kernel32.SafeHPROCESS;

namespace MemForge.Lib
{
	public abstract class RegShim : IDisposable
	{
		public SP Proc { get; }
		public IntPtr BaseAddr { get; }

		public RegShim(SP proc, IntPtr baseAddr)
		{
			Proc = proc;
			BaseAddr = baseAddr;
		}

		public void Dispose()
		{
		}
	}

	public sealed class Reg86Shim : RegShim
	{
		public Reg86Shim(SP proc, IntPtr baseAddr)
			: base(proc, baseAddr)
		{
		}
	}

	public sealed class RegShShim : RegShim
	{
		public RegShShim(SP proc, IntPtr baseAddr)
			: base(proc, baseAddr)
		{
		}
	}
}