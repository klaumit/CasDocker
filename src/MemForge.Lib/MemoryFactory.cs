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
		internal static readonly BlockingCollection<MemShim> Queue = new BlockingCollection<MemShim>();

		public override ICommPort Create()
		{
			var shim = Queue.Take();
			var port = new MemoryPort(shim);
			return port;
		}
	}
}