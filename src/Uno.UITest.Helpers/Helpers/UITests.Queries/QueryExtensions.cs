using System;
using System.Runtime.CompilerServices;

namespace Uno.UITest.Helpers.Queries
{
	public static partial class QueryExtensions
	{
		private static IApp App
		{
			get => Helpers.App;
		}

		public static QueryEx Tap(this QueryEx query)
		{
			QueryExtensions.App.Tap(query);
			return query;
		}

		public static QueryEx WaitUntilExists(this QueryEx query)
		{
			Helpers.WaitUntilExists(new QueryEx[]
			{
				query
			});
			return query;
		}

		public static QueryEx Wait(this QueryEx query, int seconds)
			=> query.Wait(TimeSpan.FromSeconds((double)seconds));

		public static QueryEx Wait(this QueryEx query, float seconds)
			=> query.Wait(TimeSpan.FromSeconds((double)seconds));

		public static QueryEx Wait(this QueryEx query, TimeSpan delay)
		{
			Helpers.Wait(delay);
			return query;
		}

		public static void EnterText(this QueryEx query, string text)
			=> QueryExtensions.App.EnterText(query, text);

		public static void Invoke(this QueryEx query, string method, string val)
			=> QueryExtensions.App.Query<object>((IAppQuery x) => query.Unwrap(x).Invoke(method, val));

		public static QueryEx Flash(this QueryEx query)
		{
			QueryExtensions.App.Flash(query);
			return query;
		}

		public static QueryEx ScrollDownTo(this QueryEx query)
		{
			QueryExtensions.App.ScrollDownTo(query, null, 0, 0.67, 500, true, null);
			return query;
		}

		public static QueryEx ScrollUpTo(this QueryEx query)
		{
			QueryExtensions.App.ScrollUpTo(query, null, 0, 0.67, 500, true, null);
			return query;
		}

		public static QueryEx ShouldBeVisible(this QueryEx query, string message = null)
		{
			message = (message ?? string.Format("Query wasn't visible: {0}", query.Unwrap));

			if(QueryExtensions.App.Query(query).Length == 0)
			{
				throw new InvalidOperationException(message);
			}

			return query;
		}

		public static QueryEx ShouldNotBeVisible(this QueryEx query, string message = null)
		{
			message = (message ?? string.Format("Query was visible: {0}", query.Unwrap));

			if(QueryExtensions.App.Query(query).Length != 0)
			{
				throw new InvalidOperationException(message);
			}

			return query;
		}
	}
}
