using System;
using DevForge.Lib.API;
using MemForge.Lib;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
	public sealed class MemoryPort : ICommPort
	{
		private readonly MemShim shim;

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
			var buffer = new byte[256];
			var got = shim.Read(buffer, 0, buffer.Length);
			Array.Resize(ref buffer, got);

			throw new NotImplementedException();
		}

		public bool WriteBytes(byte[] buffer)
		{
			var isOk = shim.Write(buffer, 0, buffer.Length);
			return isOk;
		}
	}
}