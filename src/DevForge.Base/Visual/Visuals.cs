using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using DevForge.Lib.Hex;

namespace DevForge.Lib.Visual
{
    public static class Visuals
    {
        internal static void Fill<T>(T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
        
        private static double Log2(double x)
        {
            return Math.Log(x, 2);
        }

        internal static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static Color ClassifyChunk(int[] counts, int chunkLen)
        {
            if (chunkLen == 0)
                return Color.White;

            if (counts[0x00] == chunkLen) return Color.FromArgb(15, 15, 15);
            if (counts[0xFF] == chunkLen) return Color.FromArgb(235, 235, 235);

            return EntropyToColor(ShannonEntropy(counts, chunkLen), chunkLen);
        }

        private static double ShannonEntropy(int[] counts, int total)
        {
            if (total <= 1) return 0.0;

            var len = (double)total;
            var entropy = 0.0;
            foreach (var c in counts)
            {
                if (c == 0) continue;
                var p = c / len;
                entropy -= p * Math.Log(p, 2);
            }
            return entropy;
        }

        public static Bitmap ScaleTo(this Image source, int width, int height)
        {
            var result = new Bitmap(width, height);
            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(source, 0, 0, width, height);
            }
            return result;
        }

		private static Color EntropyToColor(double entropyBits, int sampleCount)
        {
            var maxEntropy = Log2(Math.Max(1, Math.Min(sampleCount, 256)));
            var t = maxEntropy > 0 ? Clamp(entropyBits / maxEntropy, 0.0, 1.0) : 0.0;

            if (t < 0.33)
                return Lerp(Color.RoyalBlue, Color.LimeGreen, t / 0.33);

            if (t < 0.66)
                return Lerp(Color.LimeGreen, Color.Gold, (t - 0.33) / 0.33);

            return Lerp(Color.Gold, Color.Crimson, (t - 0.66) / 0.34);
        }

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

        private static Color Lerp(Color a, Color b, double t)
        {
            t = Clamp(t, 0.0, 1.0);
            var r = (int)(a.R + (b.R - a.R) * t);
            var g = (int)(a.G + (b.G - a.G) * t);
            var bl = (int)(a.B + (b.B - a.B) * t);
            return Color.FromArgb(r, g, bl);
        }
    }
}