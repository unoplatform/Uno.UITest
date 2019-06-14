using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Uno.UITest
{
	public interface IApp
	{
		IDevice Device { get; }

		void Back();

		void ClearText();

		void ClearText(string marked);

		void ClearText(Func<IAppQuery, IAppQuery> query);

		void ClearText(Func<IAppQuery, IAppWebQuery> query);
		void DismissKeyboard();
		void DoubleTap(Func<IAppQuery, IAppQuery> query);
		void DoubleTap(string marked);
		void DoubleTapCoordinates(float x, float y);
		void DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to);
		void DragAndDrop(string from, string to);
		void DragCoordinates(float fromX, float fromY, float toX, float toY);
		void EnterText(string marked, string text);
		void EnterText(string text);
		void EnterText(Func<IAppQuery, IAppWebQuery> query, string text);
		void EnterText(Func<IAppQuery, IAppQuery> query, string text);
		IAppResult[] Flash(string marked);
		IAppResult[] Flash(Func<IAppQuery, IAppQuery> query = null);
		object Invoke(string methodName, object[] arguments);
		object Invoke(string methodName, object argument = null);
		void PinchToZoomIn(string marked, TimeSpan? duration = null);
		void PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null);
		void PinchToZoomInCoordinates(float x, float y, TimeSpan? duration);
		void PinchToZoomOut(string marked, TimeSpan? duration = null);
		void PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null);
		void PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration);
		void PressEnter();
		void PressVolumeDown();
		void PressVolumeUp();
		IAppResult[] Query(Func<IAppQuery, IAppQuery> query = null);
		string[] Query(Func<IAppQuery, IInvokeJSAppQuery> query);
		IAppResult[] Query(string marked);
		IAppWebResult[] Query(Func<IAppQuery, IAppWebQuery> query);
		T[] Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query);
		void Repl();
		FileInfo Screenshot(string title);
		void ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void ScrollDown(Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollUp(Func<IAppQuery, IAppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null);
		void SetOrientationLandscape();
		void SetOrientationPortrait();
		void SetSliderValue(string marked, double value);
		void SetSliderValue(Func<IAppQuery, IAppQuery> query, double value);
		void SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true);
		void Tap(string marked);
		void Tap(Func<IAppQuery, IAppWebQuery> query);
		void Tap(Func<IAppQuery, IAppQuery> query);
		void TapCoordinates(float x, float y);
		void TouchAndHold(Func<IAppQuery, IAppQuery> query);
		void TouchAndHold(string marked);
		void TouchAndHoldCoordinates(float x, float y);
		void WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		IAppWebResult[] WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		IAppResult[] WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		IAppResult[] WaitForElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		void WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		void WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
		void WaitForNoElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null);
	}
}
