using System;
using System.IO;
using System.Runtime.InteropServices;
using static Vanara.PInvoke.Kernel32;

namespace MemForge.Lib
{
	public static class MemReader
	{
		private const uint MEM_STATE_MEM_COMMIT = 0x1000;

		public static void Read(uint pid)
		{
			// var proc = Process.GetProcessById((int)pid);
			var pac = (uint)(ProcessAccess.PROCESS_VM_READ | ProcessAccess.PROCESS_QUERY_INFORMATION);
			using (SafeHPROCESS hProc = OpenProcess(pac, false, pid))
			{
				if (hProc.IsInvalid)
					throw new InvalidOperationException("Failed to open process!");

				IntPtr address = IntPtr.Zero;
				using (var output = new MemoryStream())
				{
					var mbiType = typeof(MEMORY_BASIC_INFORMATION);
					var mbiSize = Marshal.SizeOf(mbiType);
					var mbiPtr = Marshal.AllocHGlobal(mbiSize);
					try
					{
						while (VirtualQueryEx(hProc, address, mbiPtr, mbiSize) != 0)
						{
							var mbi = (MEMORY_BASIC_INFORMATION)Marshal.PtrToStructure(mbiPtr, mbiType);

							bool isCommitted = mbi.State == MEM_STATE_MEM_COMMIT;
							uint protect = (uint)mbi.Protect;
							bool isReadable = (protect & (uint)MEM_PROTECTION.PAGE_READONLY) != 0
											|| (protect & (uint)MEM_PROTECTION.PAGE_READWRITE) != 0
											|| (protect & (uint)MEM_PROTECTION.PAGE_EXECUTE_READ) != 0
											|| (protect & (uint)MEM_PROTECTION.PAGE_EXECUTE_READWRITE) != 0;
							bool notGuarded = (protect & (uint)MEM_PROTECTION.PAGE_GUARD) == 0;

							if (isCommitted && isReadable && notGuarded)
							{
								int regSize = (int)mbi.RegionSize;
								IntPtr regBuffer = Marshal.AllocHGlobal(regSize);

								try
								{
									bool ok = ReadProcessMemory(hProc, mbi.BaseAddress, regBuffer,
																 regSize, out var bytesRead);

									if (ok && bytesRead.Value > 0)
									{
										var manBuffer = new byte[(int)bytesRead.Value];
										Marshal.Copy(regBuffer, manBuffer, 0, manBuffer.Length);
										output.Write(manBuffer, 0, manBuffer.Length);

										// TODO

										var debug = (string.Format("Read 0x{0:X} bytes at 0x{1:X}",
											mbi.RegionSize, mbi.BaseAddress.ToInt64()));
										Console.WriteLine(debug);
									}
								}
								finally
								{
									Marshal.FreeHGlobal(regBuffer);
								}
							}

							long next = mbi.BaseAddress.ToInt64() + (long)mbi.RegionSize;
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
		}
	}
}