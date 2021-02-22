using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Uno.UITest.Selenium
{
	public partial class SeleniumApp : IApp
	{
		void IApp.ScrollDown(string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia)
			=> ((IApp)this).ScrollDown(q => q.Marked(withinMarked), strategy, swipePercentage, swipeSpeed, withInertia);

		void IApp.ScrollDown(Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia)
		{
			if (withinQuery != null)
			{
				var element = GetSingleElement(withinQuery);
				_driver.ExecuteScript("arguments[0].scrollBy(0, 250)", element);
			}
			else
			{
				_driver.ExecuteScript("window.scrollBy(0, 250)");
			}
		}

		void IApp.ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> ScrollTo(
					toQuery: toQuery,
					strategy: strategy,
					swipePercentage: swipePercentage,
					swipeSpeed: swipeSpeed,
					withInertia: withInertia,
					timeout: timeout
				);


		void IApp.ScrollDownTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> ScrollTo(
					toQuery: q => q.Marked(toMarked),
					strategy: strategy,
					swipePercentage: swipePercentage,
					swipeSpeed: swipeSpeed,
					withInertia: withInertia,
					timeout: timeout
				);

		void IApp.ScrollTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> ScrollTo(
					toQuery: q => q.Marked(toMarked),
					strategy: strategy,
					swipePercentage: swipePercentage,
					swipeSpeed: swipeSpeed,
					withInertia: withInertia,
					timeout: timeout
				);

		void IApp.ScrollUp(Func<IAppQuery, IAppQuery> query, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia)
		{
			if(query != null)
			{
				var element = GetSingleElement(query);
				_driver.ExecuteScript("arguments[0].scrollBy(0, -250)", element);
			}
			else
			{
				_driver.ExecuteScript("window.scrollBy(0, -250)");
			}
		}

		void IApp.ScrollUp(string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia)
			=> ((IApp)this).ScrollUp(q => q.Marked(withinMarked), strategy, swipePercentage, swipeSpeed, withInertia);

		void IApp.ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> ScrollTo(
					toQuery: toQuery,
					strategy: strategy,
					swipePercentage: swipePercentage,
					swipeSpeed: swipeSpeed,
					withInertia: withInertia,
					timeout: timeout
				);

		void IApp.ScrollUpTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> ScrollTo(
					toQuery: q => q.Marked(toMarked),
					strategy: strategy,
					swipePercentage: swipePercentage,
					swipeSpeed: swipeSpeed,
					withInertia: withInertia,
					timeout: timeout
				);

		private void ScrollTo(
			Func<IAppQuery, IAppQuery> toQuery,
			ScrollStrategy strategy = ScrollStrategy.Auto,
			double swipePercentage = 0.67,
			int swipeSpeed = 500,
			bool withInertia = true,
			TimeSpan? timeout = null)
			=> PerformActions(a => a.MoveToElement(GetSingleElement(toQuery)));

		void IApp.ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> throw new NotSupportedException();
		void IApp.ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> throw new NotSupportedException();

		void IApp.ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> throw new NotSupportedException();
		void IApp.ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout)
			=> throw new NotSupportedException();

		
	}
}
