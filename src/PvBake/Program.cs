using System;
using CommandLine;
using PvMake.Core;

namespace PvMake
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var parser = Parser.Default;
            parser.ParseArguments<Options>(args).WithParsed(o =>
            {
                try
                {
                    RunAll(o);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(" [ERROR] {0}", ex.Message);
                }
            });
        }

        private static void RunAll(Options o)
        {
            if (o.Clean)
            {
                Cleaner.Run(o);
            }
        }
    }
}