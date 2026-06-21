using System;
using System.Collections.Generic;
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

        public static IEnumerable<byte[]> SaveAsBmp(this IFile obj)
        {
            if (obj is Icon icon)
            {
                using var stream = new MemoryStream();
                if (icon.SaveAsBmp(stream))
                    yield return stream.ToArray();
            }
            if (obj is AddIn addIn)
            {
                var menu = addIn.GetMenuIcon();
                if (menu != null) yield return menu;
                var list = addIn.GetListIcon();
                if (list != null) yield return list;
            }
        }

        private static byte[] GetIcon(this byte[] payload, uint? offset, int head = AddIns.FixedHeadSize)
        {
            var startPos = (offset ?? 0) - head;
            using var input = new MemoryStream(payload);
            if (startPos >= input.Length)
                return null;
            input.Position = startPos;
            var icon = Icons.LoadX86Icon(input);
            if (icon == null)
                return null;
            using var output = new MemoryStream();
            if (icon.SaveAsBmp(output))
                return output.ToArray();
            return null;
        }

        public static byte[] GetMenuIcon(this AddIn addIn)
        {
            var bmp = addIn.Payload.GetIcon(addIn.OffsMenuIcon);
            return bmp;
        }

        public static byte[] GetListIcon(this AddIn addIn)
        {
            var bmp = addIn.Payload.GetIcon(addIn.OffsListIcon);
            return bmp;
        }
    }
}