using System;
using DevForge.Lib.Common;
using System.Collections.Generic;
using System.Linq;
using DevForge.Lib.API;

// ReSharper disable UseCollectionExpression

namespace DevForge
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