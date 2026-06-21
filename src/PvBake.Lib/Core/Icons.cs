using System.IO;
using System.Text;
using PvBake.Lib.Models;
using PvBake.Lib.Tools;

// ReSharper disable UseCollectionExpression
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvBake.Lib.Core
{
    public static class Icons
    {
        internal static Icon ReadX86Icon(string file)
        {
            var info = new FileInfo(file);
            if (info.Length is < 83 or > 173)
                return null;
            using var stream = File.OpenRead(file);
            return LoadX86Icon(stream);
        }

        internal static bool SaveX86Icon(Icon a, Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryWriter(stream, enc);
            b.Write(a.Width);
            b.Write(a.Height);
            var rowLen = a.GetRowBytes();
            b.Write(FromPixelArray(a.Pixels, a.Width, a.Height, rowLen));
            return true;
        }

        private static byte[] FromPixelArray(bool[] pixels, int width, int height, int rowBytes)
        {
            var data = new byte[rowBytes * height];
            for (var y = 0; y < height; y++)
            {
                var rowStart = y * rowBytes;
                for (var x = 0; x < width; x++)
                    if (pixels.GetPixel(x, y, width))
                        data[rowStart + (x >> 3)] |= (byte)(0x80 >> (x & 7));
            }
            return data;
        }

        internal static bool GetPixel(this Icon a, int x, int y) => a.Pixels.GetPixel(x, y, a.Width);
        internal static bool GetPixel(this bool[] a, int x, int y, int width) => a[y * width + x];

        private static bool[] ToPixelArray(byte[] data, int offset, int width, int height, int rowBytes)
        {
            var pixels = new bool[width * height];
            for (var y = 0; y < height; y++)
            {
                var rowStart = offset + y * rowBytes;
                for (var x = 0; x < width; x++)
                {
                    var val = data[rowStart + (x >> 3)];
                    var bit = (val >> (7 - (x & 7))) & 1;
                    pixels[y * width + x] = bit != 0;
                }
            }
            return pixels;
        }

        internal static Icon LoadX86Icon(Stream stream)
        {
            var enc = Encoding.ASCII;
            using var b = new BinaryReader(stream, enc);
            if (b.GetSafeUInt16() is not { } width)
                return null;
            if (b.GetSafeUInt16() is not { } height)
                return null;
            var o = new Icon { Width = width, Height = height };
            var dataLen = o.Length - 4;
            if (b.GetSafeBytes((int)dataLen) is not { } bytes)
                return null;
            var rowLen = o.GetRowBytes();
            o.Pixels = ToPixelArray(bytes, 0, width, height, rowLen);
            return o;
        }
    }
}