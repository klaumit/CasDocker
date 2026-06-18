using CommandLine;
using PvBake.Lib.API;

// ReSharper disable ClassNeverInstantiated.Global

namespace PvBake.Core
{
	public class Options : IOptions
	{
		[Option('c', "clean", HelpText = "Clean the project.")]
		public bool Clean { get; set; }
	}
}