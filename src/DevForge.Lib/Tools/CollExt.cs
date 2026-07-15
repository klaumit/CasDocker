using System;
using System.Collections.Generic;

namespace DevForge.Lib.Tools
{
	public static class CollExt
	{
		public static Dictionary<K, V> ToDict<T, K, V>(
			this IEnumerable<T> items,
			Func<T, K> keyFunc, Func<T, V> valFunc)
		{
			var dict = new Dictionary<K, V>();
			foreach (var item in items)
			{
				var key = keyFunc(item);
				var val = valFunc(item);
				dict[key] = val;
			}
			return dict;
		}
	}
}