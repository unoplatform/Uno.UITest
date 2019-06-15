using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Utils;

namespace Uno.UITest.Helpers
{
	public abstract class AppPage : IFluentInterface
	{
		/// <summary>
		/// Indicates if this page is currently displayed at the top of the app's navigation stack
		/// </summary>
		public virtual bool IsCurrentTopPage => App.IsActivity(PageName, StringComparison.OrdinalIgnoreCase);

		public IApp App => Queries.Helpers.App;
		public abstract string PageName { get; }

		/// <summary>
		/// Provides a way to declare waiting logic specific to the current page, which will be executed 
		/// </summary>
		public virtual void PageQueryWaitHandle()
		{
		}

		/// <summary>
		/// Waits until the current page instance stops being at the top of the navigation stack, and 
		/// processes the new page that is subsequently displayed
		/// </summary>
		/// <typeparam name="T">The type of the final page shown</typeparam>
		/// <param name="onNextPage">Used to process the newly shown page that replaces the current one at the top of the navigation stack.</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <returns>The instance of the final page shown</returns>
		public T WaitForPageChange<T>(Func<AppPage, T> onNextPage, bool? createStepWhenPageShown = null) where T : AppPage
		{
			var nextPage = WaitForPageChange(createStepWhenPageShown);
			return onNextPage(nextPage);
		}

		/// <summary>
		/// Waits until the current page instance stops being at the top of the navigation stack, and 
		/// processes the new page that is subsequently displayed
		/// </summary>
		/// <param name="onNextPage">Used to process the newly shown page that replaces the current one at the top of the navigation stack</param>
		/// <param name="createStepWhenPageShown">[Optionnal] An explicit way to indicate if a step is automatically created if and when the awaited page is shown</param>
		/// <returns>The instance of the new page shown</returns>
		public AppPage WaitForPageChange(Action<AppPage> onNextPage, bool? createStepWhenPageShown = null)
		{
			var nextPage = WaitForPageChange(createStepWhenPageShown);
			onNextPage(nextPage);

			return nextPage;
		}

		private AppPage WaitForPageChange(bool? createStepWhenPageShown)
		{
			App.WaitFor(() => !IsCurrentTopPage);

			var nextPage = App.GetCurrentAppPage();
			App.AfterPageShown(nextPage, createStepWhenPageShown);

			return nextPage;
		}
	}
}
