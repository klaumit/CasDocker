using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using K = Vanara.PInvoke.Kernel32;
using PA = Vanara.PInvoke.Kernel32.ProcessAccess;
using Vanara.PInvoke;

// ReSharper disable UseStringInterpolation

namespace MemForge.Lib
{
	public static class MemReader
	{
		private const uint MEM_STATE_MEM_COMMIT = 0x1000;
		private const uint MEM_STATE_MEM_FREE = 0x10000;
		private const uint MEM_STATE_MEM_RESERVE = 0x2000;

		private const uint MEM_TYPE_MEM_IMAGE = 0x1000000;
		private const uint MEM_TYPE_MEM_MAPPED = 0x40000;
		private const uint MEM_TYPE_MEM_PRIVATE = 0x20000;

		public static K.SafeHPROCESS OpenProc(uint pid, out string pName, bool rw)
		{
			var proc = Process.GetProcessById((int)pid);
			pName = proc.ProcessName.Replace(' ', '_');
			var acc = PA.PROCESS_VM_READ | PA.PROCESS_QUERY_INFORMATION;
			if (rw) acc |= PA.PROCESS_VM_WRITE | PA.PROCESS_VM_OPERATION;
			var pac = (uint)acc;
			var hProc = K.OpenProcess(pac, false, pid);
			if (hProc.IsInvalid)
				throw new InvalidOperationException(string.Format("Failed to open process #{0}!", pid));
			return hProc;
		}

		public static IEnumerable<MemGot> ReadAll(uint pid)
		{
            string pName;
			using (var hProc = OpenProc(pid, out pName, false))
			{
				var address = IntPtr.Zero;
				var mbiType = typeof(K.MEMORY_BASIC_INFORMATION);
				var mbiSize = Marshal.SizeOf(mbiType);
				var mbiPtr = Marshal.AllocHGlobal(mbiSize);
				try
				{
					while (K.VirtualQueryEx(hProc, address, mbiPtr, mbiSize) != 0)
					{
						var mbi = (K.MEMORY_BASIC_INFORMATION)Marshal.PtrToStructure(mbiPtr, mbiType);

						var isCommitted = mbi.State == MEM_STATE_MEM_COMMIT;
						var protect = mbi.Protect;
                        var isReadable = (protect & (uint)K.MEM_PROTECTION.PAGE_READONLY) != 0
                                         || (protect & (uint)K.MEM_PROTECTION.PAGE_WRITECOPY) != 0
										 || (protect & (uint)K.MEM_PROTECTION.PAGE_READWRITE) != 0
                                         || (protect & (uint)K.MEM_PROTECTION.PAGE_EXECUTE_READ) != 0
                                         || (protect & (uint)K.MEM_PROTECTION.PAGE_EXECUTE_WRITECOPY) != 0
                                         || (protect & (uint)K.MEM_PROTECTION.PAGE_EXECUTE_READWRITE) != 0;
                        var notGuarded = (protect & (uint)K.MEM_PROTECTION.PAGE_GUARD) == 0;

						if (isCommitted && isReadable && notGuarded)
						{
							var regSize = (int)mbi.RegionSize;
							var regBuffer = Marshal.AllocHGlobal(regSize);

							try
							{
                                SizeT bytesRead;
								var ok = K.ReadProcessMemory(hProc, mbi.BaseAddress, regBuffer,
									regSize, out bytesRead);

								if (ok && bytesRead.Value > 0)
								{
									var manBuffer = new byte[(int)bytesRead.Value];
									Marshal.Copy(regBuffer, manBuffer, 0, manBuffer.Length);
									yield return new MemGot(pName, mbi, manBuffer);
								}
							}
							finally
							{
								Marshal.FreeHGlobal(regBuffer);
							}
						}

						var next = mbi.BaseAddress.ToInt64() + mbi.RegionSize;
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

		public static void WriteFullDump(uint pid, string wName)
		{
			var bName = string.Format("proc_{0}_dmp", pid);
			var fName = bName + ".bin";
			using (var outPut = File.Create(fName))
			{
				foreach (var item in ReadAll(pid))
				{
					var debug = Encoding.ASCII.GetBytes(item.Info.ToStr() + "\r\n");
					outPut.Write(debug, 0, debug.Length);
					byte[] array;
					if (wName.Contains("SIM3022"))
						array = item.Buffer;
					else
						array = item.Buffer.SwapEndian(true);
					outPut.Write(array, 0, item.Buffer.Length);
				}
				outPut.Flush();
			}
			Process.Start(fName);

			// var xName = bName + ".xxd";
			// Printer.PrintXxd(fName, xName);
			// Process.Start(xName);
		}

		public static string ToStr(this K.MEMORY_BASIC_INFORMATION mbi)
		{
			var bld = new StringBuilder();
			bld.Append("[MBI]");
			bld.AppendFormat(" BaseAddress={0:X8}", mbi.BaseAddress.ToInt32());
			bld.AppendFormat(" AllocationBase={0:X8}", mbi.AllocationBase.ToInt32());
			bld.AppendFormat(" AllocationProtect={0:X8}", mbi.AllocationProtect);
			bld.AppendFormat(" RegionSize={0:X8}", (uint)mbi.RegionSize.Value);
			bld.AppendFormat(" State={0:X8}", mbi.State);
			bld.AppendFormat(" Protect={0:X8}", mbi.Protect);
			bld.AppendFormat(" Type={0:X8}", mbi.Type);
			bld.Append(" ");
			return bld.ToString();
		}

		public static string ToStr(this MemGot mg)
		{
			var bld = new StringBuilder();
			bld.Append("[MG]");
			bld.AppendFormat(" Name={0}", mg.Name);
			bld.AppendFormat(" Size={0:X8}", mg.Buffer.Length);
			bld.AppendFormat(" {0} ", ToStr(mg.Info));
			return bld.ToString();
		}
	}
}