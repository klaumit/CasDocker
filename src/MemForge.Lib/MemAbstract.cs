using System;
using System.Diagnostics;
using System.Text;
using static MemForge.Lib.MemReader;

namespace MemForge.Lib
{
	public static class MemAbstract
	{
		public static void FindInSim(uint pid)
		{
			var enc = Encoding.ASCII;
			var pattern = enc.GetBytes("###MEMORY_MARKER_START17###" + '\0').SwapEndian();
			foreach (var item in ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000004 &&
				    item.Info.State == 0x00001000 &&
				    item.Info.Protect == 0x00000004 &&
				    item.Info.Type == 0x00020000 &&
				    item.Info.RegionSize == 0x00085000)
				{
					var offset = item.Buffer.IndexOf(pattern);
					if (offset >= 0)
					{
						var address = IntPtr.Add(item.Info.BaseAddress, offset);
						var rwHandle = OpenProc(pid, out _, true);
						var shim = new MemShim(rwHandle, address);

						var buffer = new byte[512];
						if (shim.Read(buffer, 0, buffer.Length) >= 1)
						{
							buffer.SwapEndian(true);
							var yyy = Encoding.ASCII.GetString(buffer);

							// TODO
							Debugger.Break();

							break;
						}
					}
				}
			}
		}
	}
}