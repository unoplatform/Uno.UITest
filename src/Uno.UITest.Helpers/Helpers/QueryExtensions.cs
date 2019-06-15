using System;
using System.Collections.Generic;
using System.Linq;
using Uno.UITest.Helpers.Queries;
using static Uno.UITest.Helpers.Queries.Helpers;

namespace Uno.UITest.Helpers
{
	public static partial class QueryExtensions
	{
		/// <summary>
		/// Indicates if the current query has any results
		/// </summary>
		/// <param name="query">The current query</param>
		/// <returns>True if the query has any results</returns>
		public static bool HasResults(this QueryEx query)
		{
			return query.Results().Length > 0;
		}

		/// <summary>
		/// Waits for the given amount of time before checking if the current query has any results
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="delayUntilCheck">The delay to wait until the check is made</param>
		/// <returns>True if the query has any results after waiting for the given amount of time</returns>
		public static bool HasResults(this QueryEx query, TimeSpan delayUntilCheck)
		{
			Wait(delayUntilCheck);
			return query.Results().Length > 0;
		}

		public static T WaitForPage<T>(this QueryEx query, bool? createStepWhenPageShown = null, TimeSpan? timeout = null) where T : AppPage, new()
		{
			return App.WaitForPage<T>(createStepWhenPageShown, timeout);
		}

		/// <summary>
		/// Apply a transform to the current query
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="transform">The transform that will be applied to the current query</param>
		/// <returns>A new query that incorporated the changes applied by the given transform</returns>
		public static QueryEx Transform(this QueryEx query, Func<QueryEx, QueryEx> transform)
		{
			return transform(new QueryEx(query));
		}

		/// <summary>
		/// Apply a transform (that processes <see cref="IAppQuery"/> elements) to the current query's inner definition
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="transform">The transform that will be applied to the current query's inner definition</param>
		/// <returns>A new query that incorporated the changes applied by the given transform</returns>
		public static QueryEx RawTransform(this QueryEx query, Func<IAppQuery, IAppQuery> transform)
		{
			return new QueryEx(q => transform(query.Unwrap(q)));
		}

		/// <summary>
		/// Executes the current query and returns the results
		/// </summary>
		/// <param name="query">The current query</param>
		/// <returns>The results of running the query in the current context, as a list of UI elements</returns>
		public static IAppResult[] Results(this QueryEx query)
		{
			return App.Query(query);
		}

		/// <summary>
		/// Executes the current query and returns the first result
		/// </summary>
		/// <param name="query">The current query</param>
		/// <returns>The first result of the current query</returns>
		public static IAppResult FirstResult(this QueryEx query)
		{
			return query.Results()?.FirstOrDefault();
		}

		/// <summary>
		/// Executes the current query
		/// </summary>
		/// <param name="query">The current query</param>
		public static void Run(this QueryEx query)
		{
			query.Results();
		}

		/// <summary>
		/// Waits until the element targeted by the given query appears
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="waitFor">The query that defines the element that is awaited</param>
		public static void WaitFor(this QueryEx query, Func<QueryEx, QueryEx> waitFor, TimeSpan? timeout = null)
		{
			App.WaitForElement(waitFor(QueryExFactory.BlankQuery()), timeout: timeout);
		}

		/// <summary>
		/// Waits until the element targeted by the given query appears, and then returns the query to this element
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="waitFor">The query that defines the element that is awaited</param>
		/// <returns>The query to the awaited element</returns>
		public static QueryEx WaitForAndAcquire(this QueryEx query, Func<QueryEx, QueryEx> waitFor)
		{
			var target = waitFor(QueryExFactory.BlankQuery());
			WaitUntilExists(target);
			
			return target;
		}

		public static QueryEx Step(this QueryEx query, string title)
		{
			Queries.Helpers.Step(title);
			return query;
		}
	}
}
