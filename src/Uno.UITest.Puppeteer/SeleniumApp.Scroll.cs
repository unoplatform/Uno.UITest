using System;
using System.IO;
using System.Threading.Tasks;

namespace Uno.UITest.Selenium
{
	public partial class SeleniumApp : IApp
	{
		void IApp.ScrollDown(string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.ScrollDown(Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollDownTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollUp(Func<IAppQuery, IAppQuery> query, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.ScrollUp(string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
		void IApp.ScrollUpTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotSupportedException();
	}
}
