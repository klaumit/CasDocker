using System.Collections.Generic;
using DevForge.Lib.Hex;

namespace DevForge.UI.Core
{
	public interface IHexView
	{
		IEnumerable<XxdLine> GetLines(int offset, int count);
	}
}