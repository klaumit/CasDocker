using System;
using CommandLine;
using PvBake.Core;

namespace PvBake
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
            if (o.ExtractSim)
            {
                SimExtractor.Run(o);
            }
            if (o.DetectAll)
            {
                Detector.Run(o);
            }
        }
    }
}