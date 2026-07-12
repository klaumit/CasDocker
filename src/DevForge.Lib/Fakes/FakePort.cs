using System;
using DevForge.Lib.API;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Fakes
{
	public sealed class FakePort : ICommPort
	{
		public void Dispose()
		{
			// TODO release managed resources here
		}

		public void Close()
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

		public bool IsOpen()
		{
			throw new NotImplementedException();
		}
	}
}