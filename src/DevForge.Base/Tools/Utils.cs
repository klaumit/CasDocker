using System;
using System.Linq;

// ReSharper disable UseCollectionExpression

namespace DevForge.Lib.Tools
{
    public static class Utils
    {
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