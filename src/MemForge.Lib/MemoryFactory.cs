using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Linq;
using DevForge.Lib.API;
using System.Text;
using System.Threading;
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