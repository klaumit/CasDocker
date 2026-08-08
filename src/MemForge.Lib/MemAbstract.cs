using System;
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
			var litPattern = enc.GetBytes("###ML_19");
			var bigPattern = enc.GetBytes("###ML_A0").SwapEndian();
			foreach (var item in MR.ReadAll(pid))
			{
				if (
					(item.Info.AllocationProtect == 0x00000001 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x00020000 &&
					(item.Info.RegionSize == 0x0075C000 || 
					 item.Info.RegionSize == 0x00740000))
					||
					(item.Info.AllocationProtect == 0x00000004 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x00020000 &&
					item.Info.RegionSize == 0x00085000)
					)
				{
					var offsetB = item.Buffer.IndexOf(bigPattern);
					var offsetL = item.Buffer.IndexOf(litPattern);
					if (offsetB >= 0 || offsetL >= 0)
					{
						var offset = Math.Max(offsetB, offsetL);
						var address = IntPtr.Add(item.Info.BaseAddress, offset);
						string pName;
						var rwHandle = MR.OpenProc(pid, out pName, true);
						var shim = new MemShim(rwHandle, address);
						var order = offsetB == offset ? ByteOrder.BigEndian : ByteOrder.LittleEndian; 
						var it = Tuple.Create(shim, order);
						MemoryFactory.Queue.Add(it);
					}
				}
			}
		}
	}
}