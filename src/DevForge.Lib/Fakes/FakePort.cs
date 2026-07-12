using System.IO;
using DevForge.Lib.API;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Fakes
{
	public sealed class FakePort : ICommPort
	{
		private readonly string _name;
		private MemoryStream _mem;

		public FakePort(string name)
		{
			_name = name;
		}

		public void Open()
		{
			_mem = new MemoryStream();
		}

		public void Close()
		{
			_mem?.Dispose();
			_mem = null;
		}

		public void Dispose()
		{
			Close();
		}

		public bool WriteBytes(byte[] buffer)
		{
			_mem.Write(buffer, 0, buffer.Length);
			_mem.Flush();
			return true;
		}

		public bool IsOpen()
		{
			return true;
		}

		public byte[] ReadBytes(int count)
		{
			var buffer = new byte[count];
			_ = _mem.Read(buffer, 0, buffer.Length);
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