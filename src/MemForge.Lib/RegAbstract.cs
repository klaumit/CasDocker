using System;
using System.Text;
using DevForge.Lib.Modern;
using MR = MemForge.Lib.MemReader;

namespace MemForge.Lib
{
	public static class RegAbstract
	{
		public static void FindInSim(uint pid)
		{
			var enc = Encoding.ASCII;

			// x86 | 02 00 00 01 00 50 6F 63 6B 65 74 56 69 65 77 65 72 00 00 00 00 00 00 00 00

			// var litPattern = enc.GetBytes("###ML_19");
			// var bigPattern = enc.GetBytes("###ML_A0").SwapEndian();

			foreach (var item in MR.ReadAll(pid))
			{
				;

				/*
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
				*/
			}
		}
	}
}