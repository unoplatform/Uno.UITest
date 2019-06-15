using System;
using Uno.UITest.Helpers.Queries;
using static Uno.UITest.Helpers.Queries.Helpers;

namespace Uno.UITest.Helpers
{
	public static partial class QueryExtensions
	{

		/// <summary>
		/// Simulates a touch and hold gesture on the element targeted by the current query
		/// </summary>
		/// <param name="query">The current query</param>
		public static QueryEx TouchAndHold(this QueryEx query)
		{
			App.TouchAndHold(query);
			return query;
		}

		/// <summary>
		/// Double taps on the element targeted by the current query
		/// </summary>
		/// <param name="query">The current query</param>
		public static QueryEx DoubleTap(this QueryEx query)
		{
			App.DoubleTap(query);
			return query;
		}

		/// <summary>
		/// Enter the given text into an element matching the current query, and then dismiss the keyboard
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="text">The text to enter</param>
		public static QueryEx EnterTextAndDismiss(this QueryEx query, string text)
		{
			query.EnterText(text);
			App.DismissKeyboard();

			return query;
		}

		/// <summary>
		/// Clear the textual content of an an element matching the current query
		/// </summary>
		/// <param name="query">The current query</param>
		public static QueryEx ClearText(this QueryEx query)
		{
			App.ClearText(query);
			return query;
		}

		/// <summary>
		/// Replace the text of an element matching the current query with the given text, and then dismiss the keyboard
		/// </summary>
		/// <param name="query">The current query</param>
		/// <param name="text">The text that will replace the one currently in the element</param>
		public static QueryEx ReplaceTextAndDismiss(this QueryEx query, string text)
		{
			query.ClearText();
			return query.EnterTextAndDismiss(text);
		}

		/// <summary>
		/// Tap the element targeted by the current query and wait for the subsequent page transition to display the given page instance
		/// </summary>
		/// <typeparam name="T">The type of page that is awaited</typeparam>
		/// <param name="query">The query indicating the element to tap</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <param name="timeout">[Optionnal]An explicit timeout for the wait operation.</param>
		/// <returns>The awaited page</returns>
		public static T TapAndWaitForPage<T>(this QueryEx query, bool? createStepWhenPageShown = null, TimeSpan? timeout = null) where T : AppPage, new()
		{
			return query.Tap().WaitForPage<T>(createStepWhenPageShown, timeout);
		}
	}
}
