using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyFramework.App.Helpers
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			if (enumeration == null) return;
			foreach (T item in enumeration)
			{
				action(item);
			}
		}

		public static async Task ForEachAsync<T>(this IEnumerable<T> enumeration, Func<T, Task> action)
		{
			if (enumeration == null) return;
			foreach (T item in enumeration)
			{
				await action(item);
			}
		}
	}
}