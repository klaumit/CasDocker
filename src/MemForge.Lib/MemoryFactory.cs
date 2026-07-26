using System;
using System.Collections.Concurrent;
using DevForge.Lib.API;
using DevForge.Lib.Common;
using MemForge.Lib;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
	public sealed class MemoryFactory : BaseFactory
	{
		internal static readonly BlockingCollection<Tuple<MemShim, ByteOrder>> Queue 
			= new BlockingCollection<Tuple<MemShim, ByteOrder>>();

		public override ICommPort Create()
		{
			var it = Queue.Take();
			var port = new MemoryPort(it.Item1, it.Item2);
			return port;
		}
	}
}