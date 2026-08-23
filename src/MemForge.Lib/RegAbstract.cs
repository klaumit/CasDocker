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

			// var litPattern = enc.GetBytes("###ML_19");
			// var bigPattern = enc.GetBytes("###ML_A0").SwapEndian();						
			
				;

				/*
				  
				var offsetL = item.Buffer.IndexOf(litPattern);
				if (offsetL >= 0)
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