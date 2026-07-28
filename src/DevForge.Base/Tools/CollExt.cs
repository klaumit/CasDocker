using System;
using System.Collections.Generic;

namespace DevForge.Lib.Tools
{
	public static class CollExt
	{
		public static SortedDictionary<K, V> ToDict<T, K, V>(
			this IEnumerable<T> items,
			Func<T, K> keyFunc, Func<T, V> valFunc,
			Func<K, bool> accept = null)
		{
			var dict = new SortedDictionary<K, V>();
			foreach (var item in items)
			{
				var key = keyFunc(item);
				if (accept != null && !accept(key))
					continue;
				var val = valFunc(item);
				dict[key] = val;
			}
			return dict;
		}
	}
}