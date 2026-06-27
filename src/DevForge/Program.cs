using System;
using DevForge.Lib.API;
using DevForge.Lib.Legacy;
using DevForge.Lib.Modern;

// ReSharper disable ConvertToUsingDeclaration

namespace DevForge
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (ICommDevice dev1 = new LegacyDevice())
            using (ICommDevice dev2 = new ModernDevice())
            {
                dev1.Start();
                dev2.Start();

                Console.WriteLine("Waiting...");
                Console.ReadLine();
            }
        }
    }
}