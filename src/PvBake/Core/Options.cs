using CommandLine;
using PvBake.Lib.API;

// ReSharper disable ClassNeverInstantiated.Global

namespace PvBake.Core
{
	public class Options : IOptions
	{
		[Option('e', "extract", HelpText = "Extract the simulator.")]
		public bool ExtractSim { get; set; }

		[Option('i', "input", HelpText = "Set input directory.")]
		public string InputDir { get; set; }
	}
}