using System;
using System.IO;
using PvMake.Lib;
using W = PvMake.Lib.Writing;
using M = PvMake.Lib.Making;
using S = PvMake.Lib.Siming;
using B = PvMake.Core.Bases;

namespace PvMake.Core
{
    public static class Preparer
    {
        public static void Run(IOptions o)
        {
            B.LoadAndPrepareProject(o);

            Console.WriteLine("Done.");
        }
    }
}