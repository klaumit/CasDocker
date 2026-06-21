using System;
using System.IO;
using System.Text;
using PvBake.Lib.Models;

// ReSharper disable UseCollectionExpression
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class Bitmaps
    {
        public static bool SaveAsBmp(this Icon a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            int width = a.Width;
            int height = a.Height;
            var unpaddedRowBytes = (width + 7) / 8;
            var rowBytes = (unpaddedRowBytes + 3) & ~3;
            const int paletteSize = 2 * 4;
            var pixelDataSize = rowBytes * height;
            const int fileHeaderSize = 14;
            const int infoHeaderSize = 40;
            const int pixelDataOffset = fileHeaderSize + infoHeaderSize + paletteSize;
            var fileSize = pixelDataOffset + pixelDataSize;
            b.Write((byte)'B');
            b.Write((byte)'M');
            b.Write(fileSize);
            b.Write((short)0);
            b.Write((short)0);
            b.Write(pixelDataOffset);
            b.Write(infoHeaderSize);
            b.Write(width);
            b.Write(height);
            b.Write((short)1);
            b.Write((short)1);
            b.Write(0);
            b.Write(pixelDataSize);
            b.Write(3780);
            b.Write(3780);
            b.Write(0);
            b.Write(0);
            b.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            b.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0x00 });
            var row = new byte[rowBytes];
            for (var y = height - 1; y >= 0; y--)
            {
                Array.Clear(row, 0, row.Length);
                for (var x = 0; x < width; x++)
                    if (!a.GetPixel(x, y))
                        row[x >> 3] |= (byte)(0x80 >> (x & 7));
                b.Write(row);
            }
            return true;
        }

        public static byte[] SaveAsBmp(this IFile obj)
        {
            if (obj is Icon icon)
            {
                using var stream = new MemoryStream();
                if (icon.SaveAsBmp(stream))
                    return stream.ToArray();
            }
            return null;
        }
    }
}