using System;
using System.IO;
using System.Linq;

// ReSharper disable UseCollectionExpression

namespace DevForge.Lib.Tools
{
    public static class Utils
    {
        /*
        private const BF flags = BF.Instance | BF.NonPublic;

        private static readonly PropertyInfo cpProp
            = typeof(StreamReader).GetProperty("CharPos_Prop", flags);
        */

        public static string ReadLineWithLen(this StreamReader r, out int l)
        {
            var txt = r.ReadLine();
            if (string.IsNullOrWhiteSpace(txt))
                l = 0;
            else
                l = txt.Length + 2;
            return txt;
        }

        public static string GetDateStr(this DateTime date)
        {
            var dts = date.ToString("u");
            return dts.Split(new[] { ' ' }, 2).First();
        }

        public static string GetTimeStr(this DateTime date)
        {
            var dts = date.ToString("u").TrimEnd('Z');
            return dts.Split(new[] { ' ' }, 2).Last();
        }

        public static string GetEnumStr<T>(this T val)
        {
            var txt = (val + "").TrimStart('_');
            return txt;
        }

        public static string GetVerStr(this Version val)
        {
            var txt = (val + "");
            return txt;
        }
    }
}