using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global

namespace PvMake.Core
{
    public class Options
    {
        [Option('p', "process", HelpText = "Process project.")]
        public bool PreProcess { get; set; }

        [Option('i', "input", HelpText = "Set input directory.")]
        public string? InputDir { get; set; }

        [Option('o', "output", HelpText = "Set output directory.")]
        public string? OutputDir { get; set; }
    }
}