using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using static Vanara.PInvoke.Kernel32;

// ReSharper disable UseStringInterpolation

namespace MemForge.Lib
{
	public static class MemReader
	{
		private const uint MEM_STATE_MEM_COMMIT = 0x1000;

		public static IEnumerable<MemGot> ReadAll(uint pid)
		{
			var proc = Process.GetProcessById((int)pid);
			var pName = proc.ProcessName.Replace(' ', '_');
			var pac = (uint)(ProcessAccess.PROCESS_VM_READ | ProcessAccess.PROCESS_QUERY_INFORMATION);
			using (var hProc = OpenProcess(pac, false, pid))
			{
				if (hProc.IsInvalid)
					throw new InvalidOperationException(string.Format("Failed to open process #{0}!", pid));

				var address = IntPtr.Zero;
				var mbiType = typeof(MEMORY_BASIC_INFORMATION);
				var mbiSize = Marshal.SizeOf(mbiType);
				var mbiPtr = Marshal.AllocHGlobal(mbiSize);
				try
				{
					while (VirtualQueryEx(hProc, address, mbiPtr, mbiSize) != 0)
					{
						var mbi = (MEMORY_BASIC_INFORMATION)Marshal.PtrToStructure(mbiPtr, mbiType);

						var isCommitted = mbi.State == MEM_STATE_MEM_COMMIT;
						var protect = mbi.Protect;
						var isReadable = (protect & (uint)MEM_PROTECTION.PAGE_READONLY) != 0
						                 || (protect & (uint)MEM_PROTECTION.PAGE_READWRITE) != 0
						                 || (protect & (uint)MEM_PROTECTION.PAGE_EXECUTE_READ) != 0
						                 || (protect & (uint)MEM_PROTECTION.PAGE_EXECUTE_READWRITE) != 0;
						var notGuarded = (protect & (uint)MEM_PROTECTION.PAGE_GUARD) == 0;

						if (isCommitted && isReadable && notGuarded)
						{
							var regSize = (int)mbi.RegionSize;
							var regBuffer = Marshal.AllocHGlobal(regSize);

							try
							{
								var ok = ReadProcessMemory(hProc, mbi.BaseAddress, regBuffer,
									regSize, out var bytesRead);

								if (ok && bytesRead.Value > 0)
								{
									var manBuffer = new byte[(int)bytesRead.Value];
									Marshal.Copy(regBuffer, manBuffer, 0, manBuffer.Length);
									yield return new MemGot(pName, mbi.BaseAddress, manBuffer);
								}
							}
							finally
							{
								Marshal.FreeHGlobal(regBuffer);
							}
						}

						var next = mbi.BaseAddress.ToInt64() + (long)mbi.RegionSize;
						if (next <= address.ToInt64()) break;
						address = new IntPtr(next);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(mbiPtr);
				}
			}
		}

		public static void ReadAndOpen(uint pid)
		{
			var bName = string.Format("proc_{0}_dmp", pid);
			var fName = bName + ".bin";
			using (var outPut = File.Create(fName))
			{
				foreach (var item in ReadAll(pid))
				{
					var array = item.Buffer.SwapEndian(true);
					outPut.Write(array, 0, item.Buffer.Length);
				}
				outPut.Flush();
			}
			Process.Start(fName);

			var xName = bName + ".xxd";
			Printer.PrintXxd(fName, xName);
			Process.Start(xName);
		}
	}
}