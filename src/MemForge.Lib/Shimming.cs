using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vanara.PInvoke;
using System.IO;
using System.Runtime.InteropServices;
using DevForge.Lib.Tools;
using K = Vanara.PInvoke.Kernel32;
using P = Vanara.PInvoke.Kernel32.SafeHPROCESS;

namespace MemForge.Lib
{
    public static class Shimming
    {
        public static byte[] ReadBytes(this P proc, IntPtr bAddr, int offset, int size)
        {
            var buf = new byte[size];
            var addr = IntPtr.Add(bAddr, offset);
            var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            try
            {
                var buff = handle.AddrOfPinnedObject();
                SizeT rs;
                K.ReadProcessMemory(proc, addr, buff, size, out rs);
            }
            finally
            {
                handle.Free();
            }
            return buf;
        }

        public static void WriteBytes(this P proc, IntPtr bAddr, int offset, byte[] buf)
        {
            var addr = IntPtr.Add(bAddr, offset);
            var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            try
            {
                var buff = handle.AddrOfPinnedObject();
                var size = buf.Length;
                SizeT ws;
                K.WriteProcessMemory(proc, addr, buff, size, out ws);
            }
            finally
            {
                handle.Free();
            }
        }
    }
}