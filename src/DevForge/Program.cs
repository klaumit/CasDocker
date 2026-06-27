using System;
using DevForge.Lib.API;
using DevForge.Lib.Common;
using DevForge.Lib.Legacy;

#pragma warning disable CA1859
// ReSharper disable ConvertToUsingDeclaration

namespace DevForge
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (ICommDevice dev = new PocketDevice(new LegacyFactory()))
            {
                dev.Start();

                Console.WriteLine("Waiting...");
                Console.ReadLine();
            }
        }
    }
}