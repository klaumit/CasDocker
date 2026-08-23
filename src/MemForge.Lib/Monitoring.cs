using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using MemForge.Lib;
using B = System.BitConverter;
using X = MemForge.Lib.ByteExt;
using System.Text;

namespace WinFinder
{
	public static class Monitoring
	{
		public static void ReadReg86(uint pid)
		{
			foreach (var mem in MemReader.ReadAll(pid))
			{
				var array = mem.Buffer;
			}
		}

		public static void ReadRegSh(uint pid)
		{
			foreach (var mem in MemReader.ReadAll(pid))
			{
				var array = mem.Buffer;
			}
		}
	}
}