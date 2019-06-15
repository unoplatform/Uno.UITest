using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest.Helpers.Queries.Uno
{
	public static class EnumerableExtensions
	{
		public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue)
		{
			using (var e = source.GetEnumerator())
			{
				if (e.MoveNext())
				{
					Console.WriteLine($"Got {e.Current}");
					return e.Current;
				}
				else
				{
					Console.WriteLine($"Returning default {defaultValue}");
					return defaultValue;
				}
			}
		}
	}
}
