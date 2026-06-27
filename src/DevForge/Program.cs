using System;
using System.IO.Ports;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            using (ICommDevice dev1 = new PocketDevice(new Legacy()))
            using (ICommDevice dev2 = new PocketDevice(new Modern()))
            {
                dev1.Start();
                dev2.Start();

                Console.WriteLine("Waiting...");
                Console.ReadLine();
            }
        }
    }
}