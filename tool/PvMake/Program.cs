using System.Threading.Tasks;
using CommandLine;
using PvMake.Core;

namespace PvMake
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var parser = Parser.Default;
            await parser.ParseArguments<Options>(args).WithParsedAsync(async o =>
            {
                if (o.PreProcess)
                {
                    await PreProc.Run(o);
                    return;
                }
                if (o.Clean)
                {
                    await Cleaner.Run(o);
                }
            });
        }
    }
}