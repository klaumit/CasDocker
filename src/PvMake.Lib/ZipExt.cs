using System.Data.SharpZipLib;
using System.IO;
using System.Text;

// ReSharper disable ConvertToUsingDeclaration

namespace PvMake.Lib
{
    public static class ZipExt
    {
        public static void Uncompress(string fileName, string destDir)
        {
            var enc = Encoding.UTF8;
            using (var input = File.OpenRead(fileName))
            using (var gzip = new GZipInputStream(input))
            using (var tar = TarArchive.CreateInputTarArchive(gzip, enc))
            {
                tar.ExtractContents(destDir, false);
            }
        }
    }
}