using System;
using DevForge.Lib.API;
using DevForge.Lib.Common;
using DevForge.Lib.Modern;
using DevForge.Lib.Legacy;
using DevForge.Lib.Messages.Impl;
using System.Threading;

#pragma warning disable CA1859
// ReSharper disable ConvertToUsingDeclaration

namespace DevForge
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // using (ICommDevice dev1 = new PocketDevice(new LegacyFactory()))
            using (ICommDevice dev2 = new PocketDevice(new ModernFactory()))
            {
                // dev1.Start();
                dev2.Start();

                Console.WriteLine("Waiting...");
                Console.ReadLine();

                var quit = new Quit("Noyb");
                // dev1.Send(quit);
                dev2.Send(quit);

                Console.WriteLine("Sending...");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Done.");
        }
    }
}