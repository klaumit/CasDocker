using System;

namespace MemForge.Lib
{
    public enum ByteOrder
    {
        BigEndian = 0,
        LittleEndian
    }

    public static class Ends
    {
        public static byte[] SwapEndian(this byte[] data, bool inPlace = false)
        {
            if (data == null || data.Length % 4 != 0)
                throw new ArgumentException("Array not a multiple of 4!");

            var result = inPlace ? data : new byte[data.Length];

            for (var i = 0; i < data.Length; i += 4)
            {
                var b0 = data[i];
                var b1 = data[i + 1];
                var b2 = data[i + 2];
                var b3 = data[i + 3];

                result[i] = b3;
                result[i + 1] = b2;
                result[i + 2] = b1;
                result[i + 3] = b0;
            }
            return result;
        }

        public static ushort ToUInt16(byte[] data, int idx, bool bigEndian)
        {
            return bigEndian
                ? (ushort)((data[idx] << 8) | data[idx + 1])
                : (ushort)(data[idx] | (data[idx + 1] << 8));
        }

        public static byte[] GetBytes(ushort val, bool bigEndian)
        {
            return bigEndian
                ? new[] { (byte)(val >> 8), (byte)(val & 0xFF) }
                : new[] { (byte)(val & 0xFF), (byte)(val >> 8) };
        }

        public static uint ToUInt32(byte[] data, int idx, bool bigEndian)
        {
            return bigEndian
                ? (uint)((data[idx] << 24) | (data[idx + 1] << 16) | (data[idx + 2] << 8) | data[idx + 3])
                : (uint)(data[idx] | (data[idx + 1] << 8) | (data[idx + 2] << 16) | (data[idx + 3] << 24));
        }

        public static byte[] FromUInt32(uint val, bool bigEndian)
        {
            return bigEndian
                ? new[] { (byte)(val >> 24), (byte)(val >> 16), (byte)(val >> 8), (byte)(val & 0xFF) }
                : new[] { (byte)(val & 0xFF), (byte)(val >> 8), (byte)(val >> 16), (byte)(val >> 24) };
        }
    }
}