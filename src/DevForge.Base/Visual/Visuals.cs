using System.Collections.Generic;
using System.IO;
using System.Text;
using DevForge.Lib.Hex;

namespace DevForge.Lib.Visual
{
    public static class Visuals
    {
        public static IEnumerable<HexBit> ReadHexFile(string file)
        {
            var enc = Encoding.UTF8;
            using (var reader = new StreamReader(file, enc))
            {
                foreach (var line in XxdFile.ReadHexLines(reader))
                {
                    var addr = line.GetAddr() ?? 0;
                    var bytes = line.GetRaw() ?? new byte[0];
                    for (var i = 0; i < bytes.Length; i++)
                        yield return new HexBit(addr, bytes, i);
                }
            }
        }
    }
}