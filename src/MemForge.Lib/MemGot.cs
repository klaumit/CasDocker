using System;

namespace MemForge.Lib
{
    public struct MemGot
    {
        public MemGot(string name, IntPtr address, byte[] buffer)
        {
            Name = name;
            Address = address;
            Buffer = buffer;
        }

        public string Name { get; set; }
        public IntPtr Address { get; set; }
        public byte[] Buffer { get; set; }
    }
}