using System.Diagnostics;
using System.Text;
using static MemForge.Lib.MemReader;

namespace MemForge.Lib
{
	public static class MemAbstract
	{
		public static void FindInSim(uint pid)
		{
			foreach (var item in ReadAll(pid))
			{
				if (item.Info.AllocationProtect == 0x00000004 &&
					item.Info.State == 0x00001000 &&
					item.Info.Protect == 0x00000004 &&
					item.Info.Type == 0x00020000 &&
					item.Info.RegionSize == 0x00085000)
				{
					var array = item.Buffer.SwapEndian(true);
					var text = Encoding.UTF8.GetString(array);
					if (text.Contains("###MEMORY_MARKER_START7###"))
					{
						var debug = Encoding.ASCII.GetBytes(item.Info.ToStr() + "\r\n");
						Debugger.Break();
						// outPut.Write(debug, 0, debug.Length);
						// outPut.Write(array, 0, item.Buffer.Length);

						break;
					}
				}
			}
		}
	}
}