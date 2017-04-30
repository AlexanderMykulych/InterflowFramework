using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.LinqExtension
{
	public static class LinqEnumerableExtension
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if(source == null) {
				return;
			}
			foreach (var item in source)
			{
				action(item);
			}
		}
	}
}
