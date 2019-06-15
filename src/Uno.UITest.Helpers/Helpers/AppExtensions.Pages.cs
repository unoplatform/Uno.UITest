using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Uno.UITest.Helpers.Configuration;
using static Uno.UITest.Helpers.Queries.Helpers;
using static Uno.UITest.Helpers.BackdoorInvocationHelper;
using static Uno.UITest.Helpers.PlatformHelpers;

namespace Uno.UITest.Helpers
{
	public static partial class AppExtensions
	{
		private static readonly Dictionary<Type, AppPage> _pages = new Dictionary<Type, AppPage>();

		public static AppPage GetCurrentAppPage(this IApp app)
		{
			return FindAppPageInstanceByName(app.GetTopActivityName());
		}

		public static T GetCurrentAppPage<T>(this IApp app) where T : AppPage
		{
			return app.GetCurrentAppPage() as T;
		}

		/// <summary>
		/// Gets the instance of the <see cref="AppPage"/> of the given type
		/// </summary>
		/// <typeparam name="T">The type of the page</typeparam>
		/// <param name="app">The application</param>
		/// <returns>The page instance for the given type</returns>
		public static T GetPage<T>(this IApp app) where T : AppPage, new()
		{
			app.Initialize();
			
			var type = typeof(T);

			if (_pages.ContainsKey(type))
			{
				return _pages[type] as T;
			}

			var page = new T();
			RegisterPage(page);

			return page;
		}

		public static IApp RegisterPage<T>(this IApp app) where T : AppPage, new()
		{
			var page = new T();
			RegisterPage(page);

			return app;
		}

		internal static void RegisterPage(AppPage appPage)
		{
			_pages[appPage.GetType()] = appPage;
		}

		internal static AppPage FindAppPageInstanceByName(string pageName)
		{
			return _pages.Values.First(page => page.PageName.Equals(pageName, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>
		/// Get the name of the activity that is currently on top of the navigation stack
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="backdoorMethodName">[Optionnal]The name of the backdoor method that will be invoked to obtain the top activity's name. 
		/// Defaults to <see cref="Constants.DefaultBackdoorMethodName"/> if no value is explicitely provided</param>
		/// <returns>The name of the activity that is currently on top of the navigation stack in the application</returns>
		public static string GetTopActivityName(this IApp app, string backdoorMethodName = Constants.DefaultBackdoorMethodName)
		{
			backdoorMethodName = FormatBackdoorMethodName(Configuration.NameOfTopActivityBackdoorMethod ?? backdoorMethodName);

			return On(
				// iOS requires an argument to be passed to match the signature, even if it is not used
				iOS: () => app.Initialize().Invoke(backdoorMethodName, "")?.ToString(), 
				Android: () => app.Initialize().Invoke(backdoorMethodName)?.ToString()
			);
		}

		/// <summary>
		/// Indicates if the activity with the given name is the one that is currently displayed on top of the navigation stack in the app
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="activityName">The name of the activity</param>
		/// <returns>True if the given activity is the one that is currently on top in the application</returns>
		public static bool IsActivity(this IApp app, string activityName)
		{
			return app.GetTopActivityName() == activityName;
		}

		/// <summary>
		/// Indicates if the activity with the given name is the one that is currently displayed on top of the navigation stack in the app
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="activityName">The name of the activity</param>
		/// <param name="comparisonType">Indicates how the provided name will be compared to the name of the top activity</param>
		/// <returns>True if the given activity is the one that is currently on top in the application</returns>
		public static bool IsActivity(this IApp app, string activityName, StringComparison comparisonType)
		{
			return activityName != null && activityName.Equals(app.GetTopActivityName(), comparisonType);
		}

		/// <summary>
		/// Indicates if the activity that is currently displayed on top of the navigation stack in the app matches the given predicate
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="activityPredicate">The predicate that will be used to compare the name of the activity that is currently on top</param>
		/// <returns>True if the activity is currently on top in the application matches the given predicate</returns>
		public static bool IsActivity(this IApp app, Func<string, bool> activityPredicate)
		{
			return activityPredicate(app.GetTopActivityName());
		}

		/// <summary>
		/// Wait for a page with a name that matches he given predicate to appear on top of the navigation stack in the app
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="activityPredicate">The predicate used to check if an activity corresponds to the one that is awaited, based on the activity's name</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <param name="timeout">[Optionnal] An explicit timeout for the wait operation.</param>
		/// <param name="waitHandle">[Optionnal] Alows the current test to await a given element once the page is shown</param>
		public static void WaitForPage(this IApp app, Func<string, bool> activityPredicate, bool? createStepWhenPageShown = null, TimeSpan? timeout = null, Action waitHandle = null)
		{
			string activityName = null;
			app.WaitFor(
				() => app.IsActivity(name =>
				{
					activityName = name;
					return activityPredicate(name);
				}), 
				timeout: timeout
			);

			app.AfterPageShown(createStepWhenPageShown, activityName, waitHandle);
		}

		/// <summary>
		/// Wait for a page with the given name to appear on top of the navigation stack in the app
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="activityName">The name of the activity that is awaited</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <param name="timeout">[Optionnal]An explicit timeout for the wait operation.</param>
		/// <param name="waitHandle">[Optionnal] Alows the current test to await a given element once the page is shown</param>
		public static void WaitForPage(this IApp app, string activityName, bool? createStepWhenPageShown = null, TimeSpan? timeout = null, Action waitHandle = null)
		{
			app.WaitFor(() => app.IsActivity(activityName), timeout: timeout);

			app.AfterPageShown(createStepWhenPageShown, activityName, waitHandle);
		}

		/// <summary>
		/// Wait for a page with the given name to appear on top of the navigation stack in the app
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="activityName">The name of the activity that is awaited</param>
		/// <param name="comparisonType">Indicates how the provided name will be compared to the name of the top activity</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <param name="timeout">[Optionnal]An explicit timeout for the wait operation.</param>
		/// <param name="waitHandle">[Optionnal] Alows the current test to await a given element once the page is shown</param>
		public static void WaitForPage(
			this IApp app, 
			string activityName, 
			StringComparison comparisonType, 
			bool? createStepWhenPageShown = null, 
			TimeSpan ? timeout = null,
			Action waitHandle = null)
		{
			app.WaitFor(() => app.IsActivity(activityName), timeout: timeout);

			app.AfterPageShown(createStepWhenPageShown, activityName, waitHandle);
		}

		/// <summary>
		/// Wait for a page of the given type to appear on top of the navigation stack in the app
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <param name="timeout">[Optionnal]An explicit timeout for the wait operation.</param>
		public static T WaitForPage<T>(this IApp app, bool? createStepWhenPageShown = null, TimeSpan ? timeout = null) where T : AppPage, new()
		{
			var page = app.GetPage<T>();
			app.WaitFor(() => app.IsActivity(page.PageName), timeout: timeout);

			app.AfterPageShown(page, createStepWhenPageShown);

			return page;
		}

		internal static void AfterPageShown<T>(this IApp app, T page, bool? createStepWhenPageShown) where T : AppPage
		{
			app.AfterPageShown(createStepWhenPageShown, page.PageName, () => page.PageQueryWaitHandle());
		}

		private static void AfterPageShown(this IApp app, bool? takeScreenshotWhenPageShown, string pageName, Action waitHandle = null)
		{
			if (Configuration.IdleWaitTimeAfterPageShown.HasValue)
			{
				app.Wait(Configuration.IdleWaitTimeAfterPageShown.Value);
			}

			// The test can await a certain element once the page is shown
			waitHandle?.Invoke();

			if (takeScreenshotWhenPageShown ?? Configuration.AutoCreateStepOnNewPage && !string.IsNullOrEmpty(pageName))
			{
				var stepTitle = Configuration.NewPageStepTitleFormat != null
					? string.Format(Configuration.NewPageStepTitleFormat, pageName)
					: pageName;

				app.Step(stepTitle);
			}
		}

		/// <summary>
		/// Executes a 'Back' operation (see <see cref="IApp.Back"/>) and then waits for a page of the given instance to show up
		/// </summary>
		/// <param name="app">The application</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <param name="timeout">[Optionnal]An explicit timeout for the wait operation.</param>
		/// <returns>The awaited page</returns>
		public static T BackToPage<T>(this IApp app, bool? createStepWhenPageShown = null, TimeSpan? timeout = null) where T : AppPage, new()
		{
			app.Back();
			return app.WaitForPage<T>(createStepWhenPageShown, timeout);
		}
	}
}
