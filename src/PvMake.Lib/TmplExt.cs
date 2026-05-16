using System;
using SmartFormat;

namespace PvMake.Lib
{
    public static class TmplExt
    {
        public static void Replace()
        {
            // TODO

            var data = new { Library = "SmartFormat" };
            var some = Smart.Format("Composed with {Library}.", data);
            Console.WriteLine(some);

        }
    }
}