using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using DevForge.Lib.Hex;
using DevForge.Lib.Tools;
using V = DevForge.Lib.Visual.Visuals;

// ReSharper disable ConvertToUsingDeclaration

namespace DevForge.Lib.Visual
{
    public static class Renders
    {
        public static void Render(string file, string name, out string bs)
        {
            name = Path.GetFileNameWithoutExtension(name);
            var dest = $"{name}.png";
            if (File.Exists(dest))
            {
                bs = null;
                return;
            }

            var xxd = new XxdFile(file);
            xxd.ReadLines();
            var fileLen = xxd.Stats.Info.Pos;
            const double xxdFactor = 4.25;
            var realLen = fileLen / xxdFactor;
            bs = TextExt.ToByteSize(realLen);

            const int minBytesPerPixel = 256;
            const int minDim = 64;
            const int maxDim = 2048;
            var targetPixels = Math.Max(1.0, realLen / minBytesPerPixel);
            var side = (int)V.Clamp((int)Math.Ceiling(Math.Sqrt(targetPixels)), minDim, maxDim);

            var pixelCount = side * side;
            var bytesProPix = Math.Max(1, (int)Math.Ceiling(realLen / pixelCount));

            var buffer = new byte[pixelCount * 4];
            V.Fill(buffer, (byte)255);

            var counts = new int[256];
            var chunkLen = 0;
            var x = 0;
            var y = 0;

            foreach (var item in V.ReadHexFile(file))
            {
                var raw = item.Raw;
                var idx = item.Idx;

                counts[raw[idx]]++;
                chunkLen++;

                if (chunkLen != bytesProPix) continue;

                var color = V.ClassifyChunk(counts, chunkLen);
                var offset = (y * side + x) * 4;
                buffer[offset] = color.B;
                buffer[offset + 1] = color.G;
                buffer[offset + 2] = color.R;
                buffer[offset + 3] = 255;

                Array.Clear(counts, 0, counts.Length);
                chunkLen = 0;
                x++;
                if (x >= side)
                {
                    y++;
                    x = 0;
                }
                if (y >= side)
                    break;
            }

            using (var bitmap = new Bitmap(side, side, PixelFormat.Format32bppArgb))
            {
                var rect = new Rectangle(0, 0, side, side);
                var bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                try
                {
                    var rowBytes = side * 4;
                    if (bmpData.Stride == rowBytes)
                    {
                        Marshal.Copy(buffer, 0, bmpData.Scan0, buffer.Length);
                    }
                    else
                    {
                        for (var row = 0; row < side; row++)
                        {
                            var dstRow = IntPtr.Add(bmpData.Scan0, row * bmpData.Stride);
                            Marshal.Copy(buffer, row * rowBytes, dstRow, rowBytes);
                        }
                    }
                }
                finally
                {
                    bitmap.UnlockBits(bmpData);
                }

                bitmap.Save(dest);
            }
        }
    }
}