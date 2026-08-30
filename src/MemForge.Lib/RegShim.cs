using System;
using System.Collections.Generic;
using System.Text;
using DevForge.Lib.Modern;
using Vanara.PInvoke;
using MR = MemForge.Lib.MemReader;
using SP = Vanara.PInvoke.Kernel32.SafeHPROCESS;
using System.IO;
using System.Runtime.InteropServices;
using K = Vanara.PInvoke.Kernel32;
using S = MemForge.Lib.Shimming;
using DevForge.Lib.Tools;

namespace MemForge.Lib
{
	public abstract class RegShim<T> : IDisposable
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
			if (Proc != null)
				Proc.Dispose();
		}

		public abstract IDictionary<T, string> ReadRegs();
	}
}