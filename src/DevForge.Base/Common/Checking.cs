namespace DevForge.Lib.Common
{
    public static class Checking
    {
        public static void UpdateCrc(ref ushort crc, byte[] data)
        {
            foreach (var b in data)
            {
                crc ^= (ushort)(b << 8);

                for (var i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
        }
    }
}