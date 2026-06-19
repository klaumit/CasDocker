using CommandLine;
using PvBake.Lib.API;

// ReSharper disable ClassNeverInstantiated.Global

namespace PvBake.Core
{
	public class Options : IOptions
	{
		[Option('e', "extract", HelpText = "Extract the simulator.")]
		public bool ExtractSim { get; set; }

		[Option('d', "detect", HelpText = "Detect all binaries.")]
		public bool DetectAll { get; set; }

		[Option('i', "input", HelpText = "Set input directory.")]
		public string InputDir { get; set; }

		[Option('o', "output", HelpText = "Set output directory.")]
		public string OutputDir { get; set; }
	}
}