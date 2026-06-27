using System;
using System.Runtime.InteropServices;

namespace DevForge.Lib.Modern.Internals
{
    internal static class EnumDevNative
    {
        private const string DllName = "EnumDev.dll";

        [DllImport(DllName, EntryPoint = "_PVEnumUsbA@12", CallingConvention = CallingConvention.StdCall)]
        public static extern int PVEnumUsbA(int deviceIndex,
            [MarshalAs(UnmanagedType.LPArray)] byte[] outDevicePath,
            int bufferMaxLen);

        [DllImport(DllName, EntryPoint = "_PVReadUsb@16", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PVReadUsb(IntPtr deviceHandle,
            byte[] outBuffer, uint length, out uint bytesRead);

        [DllImport(DllName, EntryPoint = "_PVWriteUsb@16", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PVWriteUsb(IntPtr deviceHandle,
            byte[] inBuffer, uint length, out uint bytesWritten);
    }
}