using System;
using System.Collections.Generic;
using System.Text;
using Uno.UITest.Helpers.Queries;
using static Uno.UITest.Helpers.Configuration;

namespace Uno.UITest.Helpers
{
	public static partial class AppExtensions
	{
		private static QueryEx _findScrollContext = null;

		/// <summary>
		/// Creates a query that targets any element marked with the give value
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="marked">The mark of the element that is searched for</param>
		/// <returns>A query that targets any element marked with the give value</returns>
		public static QueryEx Marked(this IApp app, string marked)
		{
			return QueryExFactory.BlankQuery().Marked(marked);
		}

		/// <summary>
		/// Successively execute the given set of queries until one of them yields one or more results, in which case this query is returned. 
		/// If none of the given queries yield any result, 
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="candidates">The set of candidate queries, each of which will be executed sequentialy until one yields one or more results</param>
		/// <returns>The first query which yielded one or more results</returns>
		public static QueryEx TryAcquire(this IApp app, params Func<QueryEx, QueryEx>[] candidates)
		{
			return QueryExFactory.BlankQuery().TryAcquire(candidates);
		}

		/// <summary>
		/// Attempts to find an element that is marked with the given value, scrolling UP if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="marked">The mark of the element that is searched for</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwards(this IApp app, string marked, TimeSpan? timeout = null)
		{
			return app.FindUpwards(q => q.Marked(marked), timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling UP if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query transform that indicates the element that is searched for"/></param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwards(this IApp app, Func<QueryEx, QueryEx> to, TimeSpan? timeout = null)
		{
			return app.FindUpwards(QueryExFactory.BlankQuery().Transform(to), timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling UP if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query that indicates the element that is searched for"/></param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwards(this IApp app, QueryEx to, TimeSpan? timeout = null)
		{
			return app.FindInternal(to, direction: FindScrollDirection.Up, timeOut: timeout);
		}

		/// <summary>
		/// Attempts to find an element that is marked with the given value, scrolling DOWN if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="marked">The mark of the element that is searched for</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx Find(this IApp app, string marked, TimeSpan? timeout = null)
		{
			return app.Find(q => q.Marked(marked), timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling DOWN if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query transform that indicates the element that is searched for"/></param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx Find(this IApp app, Func<QueryEx, QueryEx> to, TimeSpan? timeout = null)
		{
			return app.Find(QueryExFactory.BlankQuery().Transform(to), timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling DOWN if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query that indicates the element that is searched for"/></param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx Find(this IApp app, QueryEx to, TimeSpan? timeout = null)
		{
			return app.FindInternal(to, timeOut: timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling down on 
		/// the element matching the <see cref="within"/> query if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query that indicates the element that is searched for within <see cref="within"/></param>
		/// <param name="within">The query that indicates the element where any scrolling required to reach the element will occur</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindWithin(this IApp app, QueryEx to, QueryEx within, TimeSpan? timeout = null)
		{
			return app.FindInternal(to, within, timeOut: timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling down on 
		/// the element matching the <see cref="within"/> query if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query transform that indicates the element that is searched for within <see cref="within"/></param>
		/// <param name="within">The query that indicates the element where any scrolling required to reach the element will occur</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindWithin(this IApp app, Func<QueryEx, QueryEx> to, QueryEx within, TimeSpan? timeout = null)
		{
			var target = QueryExFactory.BlankQuery().Transform(to);
			return app.FindInternal(target, within, timeOut: timeout);
		}

		public static QueryEx FindWithin(this IApp app, string marked, QueryEx within, TimeSpan? timeout = null)
		{
			return app.FindInternal(QueryExFactory.BlankQuery().Marked(marked), within, timeOut: timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling down on 
		/// the element matching the <see cref="within"/> query if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query transform that indicates the element that is searched for within <see cref="within"/></param>
		/// <param name="within">The query transform that indicates the element where any scrolling required to reach the element will occur</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindWithin(this IApp app, Func<QueryEx, QueryEx> to, Func<QueryEx, QueryEx> within, TimeSpan? timeout = null)
		{
			return app.FindWithin(to, within(QueryExFactory.BlankQuery()), timeout: timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling up on 
		/// the element matching the <see cref="within"/> query if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query that indicates the element that is searched for within <see cref="within"/></param>
		/// <param name="within">The query that indicates the element where any scrolling required to reach the element will occur</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwardsWithin(this IApp app, Func<QueryEx, QueryEx> to, QueryEx within, TimeSpan? timeout = null)
		{
			return app.FindUpwardsWithin(QueryExFactory.BlankQuery().Transform(to), within, timeout: timeout);
		}

		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling up on 
		/// the element matching the <see cref="within"/> query if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query transform that indicates the element that is searched for within <see cref="within"/></param>
		/// <param name="within">The query that indicates the element where any scrolling required to reach the element will occur</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwardsWithin(this IApp app, QueryEx to, QueryEx within, TimeSpan? timeout = null)
		{
			return app.FindInternal(to, within, direction: FindScrollDirection.Up, timeOut: timeout);
		}

		/// <summary>
		/// Attempts to find an element marked with the given value, scrolling up on 
		/// the element matching the <see cref="within"/> query if necessary
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="marked">The mark of the element that is searched for within <see cref="within"/></param>
		/// <param name="within">The query that indicates the element where any scrolling required to reach the element will occur</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwardsWithin(this IApp app, string marked, QueryEx within, TimeSpan? timeout = null)
		{
			return app.FindUpwardsWithin(q => q.Marked(marked), within);
		}

		internal static QueryEx FindInternal(this IApp app, QueryEx target, QueryEx within = null, FindScrollDirection direction = FindScrollDirection.DownThenUp, TimeSpan? timeOut = null)
		{
			// TODO: Add Visibility check here ?

			if (AttemptToFindTargetBeforeScrolling && target.HasResults())
			{
				return target;
			}

			Action executeScrollDownTo;
			Action executeScrollUpTo;

			if (within == null)
			{
				executeScrollDownTo = () =>
				{
					app.ScrollDownTo(
						toQuery: target,
						strategy: DefaultScrollStrategy,
						swipeSpeed: InstantSwipeSpeed,
						withInertia: false,
						timeout: timeOut
					);
				};

				executeScrollUpTo = () =>
				{
					app.ScrollUpTo(
						toQuery: target,
						strategy: DefaultScrollStrategy,
						swipeSpeed: InstantSwipeSpeed,
						withInertia: false,
						timeout: timeOut
					);
				};
			}
			else
			{
				executeScrollDownTo = () =>
				{
					app.ScrollDownTo(
						toQuery: target,
						withinQuery: within,
						strategy: DefaultScrollStrategy,
						swipeSpeed: InstantSwipeSpeed,
						withInertia: false,
						timeout: timeOut
					);
				};

				executeScrollUpTo = () =>
				{
					app.ScrollUpTo(
						toQuery: target,
						withinQuery: within,
						strategy: DefaultScrollStrategy,
						swipeSpeed: InstantSwipeSpeed,
						withInertia: false,
						timeout: timeOut
					);
				};
			}

			// First step. Ignore misses if search is two-step process
			switch (direction)
			{
				case FindScrollDirection.Down:
					executeScrollDownTo();
					return target;
				case FindScrollDirection.DownThenUp:
					try
					{
						executeScrollDownTo();
					}
					catch (Exception) { }
					break;
				case FindScrollDirection.Up:
					executeScrollUpTo();
					return target;
				case FindScrollDirection.UpThenDown:
					try
					{
						executeScrollUpTo();
					}
					catch (Exception) { }
					break;
			}

			// Second step (if the search is a two step process)
			switch (direction)
			{
				case FindScrollDirection.DownThenUp:
					executeScrollUpTo();
					break;
				case FindScrollDirection.UpThenDown:
					executeScrollDownTo();
					break;
			}

			return target;
		}


		/// <summary>
		/// Attempts to find an element matching the <see cref="to"/> query, scrolling down on 
		/// the element matching the current scroll context (which can be set using <see cref="WithScrollContext"/>)
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="to">The query that indicates the element that is searched for within current scroll context</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindWithinContext(this IApp app, Func<QueryEx, QueryEx> to)
		{
			if (_findScrollContext == null)
			{
				throw new ArgumentException();
			}

			return app.FindWithin(to, _findScrollContext);
		}

		/// <summary>
		/// Set the current scroll context (used in operations such as <see cref="FindWithinContext"/>), and returns 
		/// a disposable handle which will restore the previous context when disposed
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="scrollContext">The scroll context to set</param>
		/// <returns></returns>
		public static IDisposable WithScrollContext(IApp app, QueryEx scrollContext)
		{
			return new ScrollContextTracker(scrollContext);
		}

		private class ScrollContextTracker : IDisposable
		{
			private readonly QueryEx _previousContext;

			public ScrollContextTracker(QueryEx currentContext)
			{
				_previousContext = _findScrollContext;
				_findScrollContext = currentContext;
			}

			public void Dispose()
			{
				_findScrollContext = _previousContext;
			}
		}
	}
}
