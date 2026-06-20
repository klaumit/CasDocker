using System.Text;
using ByteSizeLib;

namespace PvBake.Lib.Tools
{
    public static class TextExt
    {
        static TextExt()
        {
#if NETFRAMEWORK
#else
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }

        public static Encoding Utf => Encoding.UTF8;

        public static Encoding Win => Encoding.GetEncoding("Windows-1252");

        public static string ToByteSize(int bytes)
        {
            var obj = ByteSize.FromBytes(bytes);
            var txt = obj.ToString();
            if (txt == " b")
                txt = "0 b";
            return txt;
        }

        public static void AddZeroMark(ref string val)
        {
            const char end = (char)0x00;
            val += end;
        }
    }
}