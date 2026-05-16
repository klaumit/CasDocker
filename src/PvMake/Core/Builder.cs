using System;
using System.IO;
using System.Linq;
using PvMake.Lib;

namespace PvMake.Core
{
    public static class Builder
    {
        public static void Run(IOptions o)
        {
            Bases.LoadAndPrepareProject(o);

            Console.WriteLine("   TODO   ??!?  ");

            Console.WriteLine("Done.");
        }
    }
}