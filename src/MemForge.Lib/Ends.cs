using System;

namespace MemForge.Lib
{
    public static class Ends
    {
        public static byte[] SwapEndian(this byte[] data, bool inPlace = false)
        {
            if (data == null || data.Length % 4 != 0)
                throw new ArgumentException("Array not a multiple of 4!");

            byte[] result = inPlace ? data : new byte[data.Length];

            for (int i = 0; i < data.Length; i += 4)
            {
                byte b0 = data[i];
                byte b1 = data[i + 1];
                byte b2 = data[i + 2];
                byte b3 = data[i + 3];

                result[i] = b3;
                result[i + 1] = b2;
                result[i + 2] = b1;
                result[i + 3] = b0;
            }
            return result;
        }
    }
}