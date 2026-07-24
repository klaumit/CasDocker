using System;
using DevForge.Lib.API;
using MemForge.Lib;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
	public sealed class MemoryPort : ICommPort
	{
		private readonly MemShim _shim;

		public MemoryPort(MemShim shim)
		{
			_shim = shim;
		}

		public void Close()
		{
			_shim.Dispose();
		}

		public void Dispose()
		{
			Close();
		}

		public bool IsOpen()
		{
			return true;
		}

		public void Open()
		{
			// NO-OP!
		}

		public byte[] ReadBytes(int count)
		{
			var buffer = new byte[256];
			var got = _shim.Read(buffer, 0, buffer.Length);
			Array.Resize(ref buffer, got);
			return buffer;
		}

		public bool WriteBytes(byte[] buffer)
		{
			var isOk = _shim.Write(buffer, 0, buffer.Length);
			return isOk;
		}
	}
}