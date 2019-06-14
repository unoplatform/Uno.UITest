using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest
{
	public interface IApp
	{
		IDevice Device { get; }

		Task Back();

		Task ClearText();

		Task ClearText(string marked);

		Task ClearText(Func<IAppQuery, IAppQuery> query);

		Task ClearText(Func<IAppQuery, IAppWebQuery> query);
		Task DismissKeyboard();
		Task DoubleTap(Func<IAppQuery, IAppQuery> query);
		Task DoubleTap(string marked);
		Task DoubleTapCoordinates(float x, float y);
		Task DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to);
		Task DragAndDrop(string from, string to);
		Task DragCoordinates(float fromX, float fromY, float toX, float toY);
		Task EnterText(string marked, string text);
		Task EnterText(string text);
		Task EnterText(Func<IAppQuery, IAppWebQuery> query, string text);
		Task EnterText(Func<IAppQuery, IAppQuery> query, string text);
		Task<IAppResult[]> Flash(string marked);
		Task<IAppResult[]> Flash(Func<IAppQuery, IAppQuery> query = null);
		Task<object> Invoke(string methodName, object[] arguments);
		Task<object> Invoke(string methodName, object argument = null);
		Task PinchToZoomIn(string marked, TimeSpan? duration = null);
		Task PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null);
		Task PinchToZoomInCoordinates(float x, float y, TimeSpan? duration);
		Task PinchToZoomOut(string marked, TimeSpan? duration = null);
		Task PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null);
		Task PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration);
		Task PressEnter();
		Task PressVolumeDown();
		Task PressVolumeUp();
		Task<IAppResult[]> Query(Func<IAppQuery, IAppQuery> query = null);
		Task<string[]> Query(Func<IAppQuery, IInvokeJSAppQuery> query);
		Task<IAppResult[]> Query(string marked);
		Task<IAppWebResult[]> Query(Func<IAppQuery, IAppWebQuery> query);
		Task<T[]> Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query);
		Task Repl();
		Task<FileInfo> Screenshot(string title);
		Task ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task ScrollDown(Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollUp(Func<IAppQuery, IAppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		Task SetOrientationLandscape();
		Task SetOrientationPortrait();
		Task SetSliderValue(string marked, double value);
		Task SetSliderValue(Func<IAppQuery, IAppQuery> query, double value);
		Task SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		Task Tap(string marked);
		Task Tap(Func<IAppQuery, IAppWebQuery> query);
		Task Tap(Func<IAppQuery, IAppQuery> query);
		Task TapCoordinates(float x, float y);
		Task TouchAndHold(Func<IAppQuery, IAppQuery> query);
		Task TouchAndHold(string marked);
		Task TouchAndHoldCoordinates(float x, float y);
		Task WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		Task<IAppWebResult[]> WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		Task<IAppResult[]> WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		Task<IAppResult[]> WaitForElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		Task WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		Task WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		Task WaitForNoElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
	}
}
