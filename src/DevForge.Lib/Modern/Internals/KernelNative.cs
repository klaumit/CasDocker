using System;
using System.Runtime.InteropServices;

namespace DevForge.Lib.Modern.Internals
{
    internal static class KernelNative
    {
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