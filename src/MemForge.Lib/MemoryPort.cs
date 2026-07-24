using System;
using System.IO;
using DevForge.Lib.API;
using MemForge.Lib;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
	public sealed class MemoryPort : ICommPort
	{
		private readonly MemShim _shim;
		private readonly bool _swap;
		private MemoryStream _tmp;

		public MemoryPort(MemShim shim, bool swap)
		{
			_shim = shim;
			_swap = swap;
		}

		private void CloseTmp()
		{
			if (_tmp == null)
				return;
			_tmp.Dispose();
			_tmp = null;
		}

		public void Close()
		{
			CloseTmp();
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
			var buffer = new byte[512];
			int got;
			if (_tmp != null && _tmp.Position < _tmp.Length)
			{
				got = _tmp.Read(buffer, 0, count);
			}
			else
			{
				CloseTmp();
				got = _shim.Read(buffer, 0, buffer.Length);
				if (_swap)
				{
					buffer.SwapEndian(true);
				}
				_tmp = new MemoryStream();
				_tmp.Write(buffer, 0, got);
				_tmp.Position = 0L;
				got = _tmp.Read(buffer, 0, count);
			}
			Array.Resize(ref buffer, got);
			return buffer;
		}

		public bool WriteBytes(byte[] buffer)
		{
			if (_swap)
			{
				buffer = buffer.SwapEndian();
			}
			var isOk = _shim.Write(buffer, 0, buffer.Length);
			return isOk;
		}
	}
}