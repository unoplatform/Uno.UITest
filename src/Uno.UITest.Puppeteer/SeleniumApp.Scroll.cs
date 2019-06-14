using System;
using System.IO;
using System.Threading.Tasks;

namespace Uno.UITest.Puppeteer
{
	internal partial class SeleniumApp : IApp
	{
		Task IApp.ScrollDown(string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.ScrollDown(Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollDownTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollUp(Func<IAppQuery, IAppQuery> query, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.ScrollUp(string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
		Task IApp.ScrollUpTo(string toMarked, string withinMarked, ScrollStrategy strategy, double swipePercentage, int swipeSpeed, bool withInertia, TimeSpan? timeout) => throw new NotImplementedException();
	}
}
