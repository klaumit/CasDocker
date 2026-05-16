using CommandLine;
using PvMake.Lib;

// ReSharper disable ClassNeverInstantiated.Global

namespace PvMake.Core
{
    public class Options : IOptions
	{
        [Option('c', "clean", HelpText = "Clean the project.")]
        public bool Clean { get; set; }

		[Option('b', "build", HelpText = "Build the project.")]
		public bool Build { get; set; }

		[Option('s', "simulate", HelpText = "Test the project.")]
		public bool Simulate { get; set; }

		[Option('u', "upload", HelpText = "Upload the project.")]
		public bool Upload { get; set; }

		[Option('i', "input", HelpText = "Set input directory.")]
		public string InputDir { get; set; }

		[Option('o', "output", HelpText = "Set output directory.")]
		public string OutputDir { get; set; }
	}
}