using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest.Helpers
{
	public static class Configuration
	{
		/// <summary>
		/// The speed that will be used to make 'instant' swipe gestures
		/// </summary>
		public static int InstantSwipeSpeed = 10000;

		/// <summary>
		/// The name of the method that is invoked to provide the name of the activity which is 
		/// currently on top of the navigation stack in the application
		/// </summary>
		public static string NameOfTopActivityBackdoorMethod;

		/// <summary>
		/// If true, the 'Find' queries will only traget elements that are visible
		/// </summary>
		public static bool OnlyTargetVisibleElements = false;

		/// <summary>
		/// If true, a query will be made to find the target element prior to scrolling up or down to check if the target element exists on the visible screen
		/// </summary>
		public static bool AttemptToFindTargetBeforeScrolling = false;

		/// <summary>
		/// The default scrolling strategy to use when making gestures that result in an in-app scrolling
		/// </summary>
		public static ScrollStrategy DefaultScrollStrategy = ScrollStrategy.Gesture;

		/// <summary>
		/// Indicates if each new page transition should be registered as a "step" in the test
		/// </summary>
		public static bool AutoCreateStepOnNewPage = false;

		/// <summary>
		/// The format string for the title of a step created by a page transition
		/// </summary>
		public static string NewPageStepTitleFormat;

		/// <summary>
		/// The amount of time that will be awaited after a page transition is detected
		/// </summary>
		public static TimeSpan? IdleWaitTimeAfterPageShown;

		public static class Constants
		{
			/// <summary>
			/// The name of the UI automation ID property for visual elements, based on the current platform
			/// </summary>
			public static string UiAutomationIdPropertyName => Queries.Helpers.On(iOS: "AccessibilityLabel", Android: "ContentDescription");

			/// <summary>
			/// The default name for the method that provides the name of the activity which is 
			/// currently on top of the navigation stack in the application
			/// </summary>
			public const string DefaultBackdoorMethodName = "GetTopActivityName";
		}
	}
}
