using System;
using System.Diagnostics;
using System.Text;
using DevForge.Lib.Modern;
using MR = MemForge.Lib.MemReader;

namespace MemForge.Lib
{
	public static class MemAbstract
	{
		public static void FindInSim(uint pid)
		{
			var enc = Encoding.ASCII;
			var basePat = enc.GetBytes("###MEMORY_MARKER_START17###" + '\0');
			var bigPattern = basePat.SwapEndian();
			foreach (var item in MR.ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000004 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x00020000 &&
					item.Info.RegionSize == 0x00085000)
				{
					var offset = item.Buffer.IndexOf(bigPattern);
					if (offset >= 0)
					{
						var address = IntPtr.Add(item.Info.BaseAddress, offset);
						string pName;
						var rwHandle = MR.OpenProc(pid, out pName, true);
						var shim = new MemShim(rwHandle, address);
						var it = Tuple.Create(shim, true);
						MemoryFactory.Queue.Add(it);
					}
				}
			}
		}
	}
}