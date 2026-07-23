using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using DevForge.Lib.API;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
	public sealed class MemoryPort : ICommPort
	{
		public void Close()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public bool IsOpen()
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
			throw new NotImplementedException();
		}

		public byte[] ReadBytes(int count)
		{
			throw new NotImplementedException();
		}

		public bool WriteBytes(byte[] buffer)
		{
			throw new NotImplementedException();
		}
	}
}