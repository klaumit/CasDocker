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
                if (o.Clean)
                {
                    Cleaner.Run(o);
                }
                else if (o.Build)
                {
                    Builder.Run(o);
                }
                else if (o.Simulate)
                {
                    Simulator.Run(o);
                }
                else if (o.Upload)
                {
                    Uploader.Run(o);
                }
            });
        }
    }
}