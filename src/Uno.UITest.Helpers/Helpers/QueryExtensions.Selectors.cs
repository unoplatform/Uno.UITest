using System;
using System.Collections.Generic;
using Uno.UITest.Helpers.Queries;
using static Uno.UITest.Helpers.Queries.Helpers;

namespace Uno.UITest.Helpers
{
	public static partial class QueryExExtensions
	{
		/// <summary>
		/// Transform the current query so that it targets TextBlock elements
		/// </summary>
		/// <param name="query">The current query</param>
		/// <returns>The current, adjusted so that it now targets TextBlock elements</returns>
		public static QueryEx TextBlock(this QueryEx query)
		{
			return query.WithClass("TextBlock");
		}

		/// <summary>
		/// Transform the current query so that it targets Button elements
		/// </summary>
		/// <param name="query">The current query</param>
		/// <returns>The current, adjusted so that it now targets Button elements</returns>

		public static QueryEx Button(this QueryEx query)
		{
			return query.WithClass("Button");
		}

		/// <summary>
		/// Transform the current query so that it targets elements that can be scrolled (ex: ScrollViewer)
		/// </summary>
		/// <param name="query">The current query</param>
		/// <returns>The current, adjusted so that it now targets scrollable elements</returns>

		public static QueryEx Scrollable(this QueryEx query)
		{
			return query.WithClass("ScrollViewer");
		}

		/// <summary>
		/// Successively execute the given set of queries until one of them yields one or more results, in which case this query is returned. 
		/// If none of the given queries yield any result, 
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="candidates">The set of candidate queries, each of which will be executed sequentialy until one yields one or more results</param>
		/// <returns>The first query which yielded one or more results</returns>
		public static QueryEx TryAcquire(this QueryEx query, params Func<QueryEx, QueryEx>[] candidates)
		{
			var collectedExceptions = new List<Exception>(candidates.Length);
			foreach (var candidate in candidates)
			{
				try
				{
					var after = candidate(new QueryEx(query));
					if (after.HasResults())
					{
						return after;
					}
				}
				catch (Exception e)
				{
					collectedExceptions.Add(e);
				}
			}

			throw new AggregateException("No element found based on the given set of queries", collectedExceptions);
		}

		/// <summary>
		/// Attempts to scroll DOWN within the element targeted by the current query until an element 
		/// matching the given query transform is found (or if the duration of the operation reaches the timeout)
		/// </summary>
		/// <param name="within">The current query</param>
		/// <param name="to">The query that defines the target element to reach by scrolling down within the current element</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>The query that defines the target element</returns>
		public static QueryEx FindWithinCurrent(this QueryEx within, Func<QueryEx, QueryEx> to, TimeSpan? timeout = null)
		{
			return App.FindWithin(to, within, timeout);
		}

		/// <summary>
		/// Attempts to scroll DOWN within the element targeted by the current query until an element 
		/// matching the given query is found (or if the duration of the operation reaches the timeout)
		/// </summary>
		/// <param name="within">The current query</param>
		/// <param name="marked">The mark of the element that is searched for within <see cref="within"/></param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>The query that defines the target element</returns>
		public static QueryEx FindWithinCurrent(this QueryEx within, string marked, TimeSpan? timeout = null)
		{
			return App.FindWithin(marked, within, timeout);
		}

		/// <summary>
		/// Attempts to scroll DOWN within the element targeted by the current query until an element 
		/// matching the given query is found (or if the duration of the operation reaches the timeout)
		/// </summary>
		/// <param name="within">The current query</param>
		/// <param name="to">The query that defines the target element to reach by scrolling down within the current element</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>The query that defines the target element</returns>
		public static QueryEx FindWithinCurrent(this QueryEx within, QueryEx to, TimeSpan? timeout = null)
		{
			return App.FindWithin(to, within, timeout);
		}

		/// <summary>
		/// Attempts to scroll UP within the element targeted by the current query until an element 
		/// matching the given query trnaform is found (or if the duration of the operation reaches the timeout)
		/// </summary>
		/// <param name="within">The current query</param>
		/// <param name="to">The query that defines the target element to reach by scrolling up within the current element</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>The query that defines the target element</returns>
		public static QueryEx FindUpwardsWithinCurrent(this QueryEx within, Func<QueryEx, QueryEx> to, TimeSpan? timeout = null)
		{
			return App.FindUpwardsWithin(to, within, timeout);
		}

		/// <summary>
		/// Attempts to scroll UP within the element targeted by the current query until an element 
		/// matching the given query is found (or if the duration of the operation reaches the timeout)
		/// </summary>
		/// <param name="within">The current query</param>
		/// <param name="to">The query that defines the target element to reach by scrolling up within the current element</param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>The query that defines the target element</returns>
		public static QueryEx FindUpwardsWithinCurrent(this QueryEx within, QueryEx to, TimeSpan? timeout = null)
		{
			return App.FindUpwardsWithin(to, within, timeout);
		}

		/// <summary>
		/// Attempts to scroll UP within the element targeted by the current query until an element 
		/// matching the given query is found (or if the duration of the operation reaches the timeout)
		/// </summary>
		/// <param name="within">The current query</param>
		/// <param name="marked">The mark of the element that is searched for within <see cref="within"/></param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout  will be used.</param>
		/// <returns>The query that defines the target element</returns>
		public static QueryEx FindUpwardsWithinCurrent(this QueryEx within, string marked, TimeSpan? timeout = null)
		{
			return App.FindUpwardsWithin(marked, within, timeout);
		}

		/// <summary>
		/// Attempts to find an element that mathes the current query, scrolling DOWN if necessary
		/// </summary>
		/// <param name="query">The current query </param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx Find(this QueryEx query, TimeSpan? timeout = null)
		{
			return App.Find(query, timeout);
		}

		/// <summary>
		/// Attempts to find an element that mathes the current query, scrolling UP if necessary
		/// </summary>
		/// <param name="query">The current query </param>
		/// <param name="timeout">[Optionnal] An explicit value for the timeout. If not specified, the default timeout will be used.</param>
		/// <returns>A query pointing to the resulting element if it was found</returns>
		public static QueryEx FindUpwards(this QueryEx query, TimeSpan? timeout = null)
		{
			return App.FindUpwards(query, timeout);
		}

		/// <summary>
		/// Filters the current query to only include elements that have the target property with the given value
		/// </summary>
		/// <param name="query">The current query </param>
		/// <param name="propertyName">The name of the targeted property</param>
		/// <param name="propertyValue">The value expected for the given property</param>
		/// <returns>The filtered query</returns>
		public static QueryEx Filter(this QueryEx query, string propertyName, string propertyValue)
		{
			return query.RawTransform(q => q.Property(propertyName, propertyValue));
		}

		/// <summary>
		/// Filters the current query to only include elements that have the target property with the given value
		/// </summary>
		/// <param name="query">The current query </param>
		/// <param name="propertyName">The name of the targeted property</param>
		/// <param name="propertyValue">The value expected for the given property</param>
		/// <returns>The filtered query</returns>
		public static QueryEx Filter(this QueryEx query, string propertyName, int propertyValue)
		{
			return query.RawTransform(q => q.Property(propertyName, propertyValue));
		}

		/// <summary>
		/// Filters the current query to only include elements that have the target property with the given value
		/// </summary>
		/// <param name="query">The current query </param>
		/// <param name="propertyName">The name of the targeted property</param>
		/// <param name="propertyValue">The value expected for the given property</param>
		/// <returns>The filtered query</returns>
		public static QueryEx Filter(this QueryEx query, string propertyName, bool propertyValue)
		{
			return query.RawTransform(q => q.Property(propertyName, propertyValue));
		}

		/// <summary>
		/// Apply a predicate that will filter the current query based on the given property
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="propertyName">The name of the propery that will be used as part of the filter predicate</param>
		/// <param name="filterPredicate">The filter to apply to the current query</param>
		/// <returns>The filtered query</returns>
		public static QueryEx Filter(this QueryEx query, string propertyName, Func<IPropertyAppQuery, IAppQuery> filterPredicate)
		{
			return query.RawTransform(q => filterPredicate(q.Property(propertyName)));
		}

		/// <summary>
		/// Apply a predicate that will filter the current query based on the "id" property
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="filterPredicate">The filter to apply to the current query</param>
		/// <returns>The filtered query</returns>
		public static QueryEx Filter(this QueryEx query, Func<IPropertyAppQuery, IAppQuery> filterPredicate)
		{
			return query.Filter(Configuration.Constants.UiAutomationIdPropertyName, filterPredicate);
		}
	}
}
