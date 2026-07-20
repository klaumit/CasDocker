using System;
using System.Diagnostics;
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
			var proc = Process.GetProcessById((int)pid);
			var pac = (uint)(ProcessAccess.PROCESS_VM_READ | ProcessAccess.PROCESS_QUERY_INFORMATION);
			using (var hProc = OpenProcess(pac, false, pid))
			{
				if (hProc.IsInvalid)
					throw new InvalidOperationException("Failed to open process!");

				var address = IntPtr.Zero;
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

							var isCommitted = mbi.State == MEM_STATE_MEM_COMMIT;
							var protect = (uint)mbi.Protect;
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
										output.Write(manBuffer, 0, manBuffer.Length);
										output.Flush();
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

					var fName = proc.ProcessName.Replace(' ', '_') + ".bin";
					using (var fs = File.Create(fName))
					{
						output.Position = 0L;
						output.CopyTo(fs);
						output.Flush();
					}
					Process.Start(fName);
				}
			}
		}
	}
}