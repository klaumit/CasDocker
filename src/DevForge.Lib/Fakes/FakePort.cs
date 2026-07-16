using System.Collections.Generic;
using System.IO;
using DevForge.Lib.API;
using DevForge.Lib.Common;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Fakes
{
	public sealed class FakePort : BaseFactory, ICommPort
	{
		private readonly string _name;
		private MemoryStream _mem;

		public FakePort(string name = "Fake")
		{
			_name = name;
		}

		public override string Prefix { get { return _name; } }

		public override ICommPort Create()
		{
			return this;
		}

		public void Open()
		{
			if (_mem == null)
				_mem = new MemoryStream();
		}

		public void Close()
		{
			if (_mem != null)
				_mem.Dispose();
			_mem = null;
		}

		public void Dispose()
		{
			Close();
		}

		public bool WriteBytes(byte[] buffer)
		{
			var oldPos = _mem.Position;
			_mem.Write(buffer, 0, buffer.Length);
			_mem.Flush();
			_mem.Position = oldPos;
			return true;
		}

		public bool IsOpen()
		{
			return true;
		}

		public byte[] ReadBytes(int count)
		{
			var buffer = new byte[count];
			if (_mem != null)
				_mem.Read(buffer, 0, buffer.Length);
			return buffer;
		}

		public void Rewind(int pos = 0)
		{
			_mem.Position = pos;
		}

		public override string ToString()
		{
			return "FakePort(" + _name + ")";
		}
	}
}