using System;
using DevForge.Lib.API;
using DevForge.Lib.Common;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
	public sealed class MemoryFactory : BaseFactory
	{
		public override ICommPort Create()
		{
			throw new NotImplementedException();
		}
	}
}