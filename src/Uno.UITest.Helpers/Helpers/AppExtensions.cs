using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uno.UITest.Helpers.Queries;
using static Uno.UITest.Helpers.Configuration;
using static Uno.UITest.Helpers.Queries.Helpers;

namespace Uno.UITest.Helpers
{
	public static partial class AppExtensions
	{
		public static QueryEx CreateQuery(this IApp app, Func<QueryEx, QueryEx> query)
		{
			return app.CreateQuery().Transform(query);
		}

		public static QueryEx CreateQuery(this IApp app)
		{
			app.Initialize();
		
			return QueryExFactory.BlankQuery();
		}

		public static T Initialize<T>(this T app) where T : IApp
		{
			App = app;
			return app;
		}

		public static void SwipeLeftToRightInstant(this IApp app)
		{
			app.Initialize().SwipeLeftToRight(swipeSpeed: InstantSwipeSpeed);
		}

		public static void SwipeRightToLeftInstant(this IApp app)
		{
			app.Initialize().SwipeRightToLeft(swipeSpeed: InstantSwipeSpeed);
		}

		public static void PopulateField(this IApp app, Func<QueryEx, QueryEx> toField, string text)
		{
			app.Initialize().CreateQuery().Transform(toField).Find().EnterTextAndDismiss(text);
		}

		public static IAppRect GetScreenDimensions(this IApp app)
		{
			return app.Initialize().CreateQuery().Descendant(0).Results().FirstOrDefault()?.Rect;
		}

		public static T Step<T>(this T app, string title) where T : IApp
		{
			app.Initialize();

			Uno.UITest.Helpers.Queries.Helpers.Step(title);
			return app;
		}

		public static T Wait<T>(this T app, TimeSpan waitTime) where T : IApp
		{
			app.Initialize();

			Uno.UITest.Helpers.Queries.Helpers.Wait(waitTime);
			return app;
		}

		public static T Wait<T>(this T app, int seconds) where T : IApp
		{
			app.Initialize();

			Uno.UITest.Helpers.Queries.Helpers.Wait(seconds);
			return app;
		}

		public static T Wait<T>(this T app, float seconds) where T : IApp
		{
			app.Initialize();

			Uno.UITest.Helpers.Queries.Helpers.Wait(seconds);
			return app;
		}

		public static T Loop<T>(this T app, Action<int> step, Action<int> toNext, int stepCount) where T : IApp
		{
			app.Initialize();

			for (var stepIndex = 0; stepIndex < stepCount; stepIndex++)
			{
				step(stepIndex);
				toNext(stepIndex);
			}

			return app;
		}
	}
}
