using System;
using System.Runtime.InteropServices;

namespace DevForge.Lib.Modern
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

        private const string KernName = "kernel32.dll";

        public const uint GENERIC_READ_WRITE = 0xC0000000;
        public const int OPEN_EXISTING = 3;
        public const int FILE_FLAG_OVERLAPPED = 0x40000000;

        [DllImport(KernName, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
            IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport(KernName, SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);
    }
}