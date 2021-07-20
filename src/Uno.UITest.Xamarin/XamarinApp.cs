using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uno.UITest.Xamarin.Extensions;
using Xamarin.UITest;

namespace Uno.UITest.Xamarin
{
	public class XamarinApp : IApp
	{
		global::Xamarin.UITest.IApp _source;
		global::Xamarin.UITest.iOS.iOSApp _iOSApp;
		global::Xamarin.UITest.Android.AndroidApp _androidApp;

		public XamarinApp(global::Xamarin.UITest.iOS.iOSApp iOSApp)
		{
			_source = iOSApp;
			_iOSApp = iOSApp;
		}

		public XamarinApp(global::Xamarin.UITest.Android.AndroidApp androidApp)
		{
			_source = androidApp;
			_androidApp = androidApp;
		}

		public IDevice Device => new XamarinDevice(_source.Device);

		public global::Xamarin.UITest.IApp Source { get => _source; set => _source = value; }

		public void Back()
			=> _source.Back();

		public void ClearText()
			=> _source.ClearText();

		public void ClearText(string marked)
			=> _source.ClearText(marked);

		public void ClearText(Func<IAppQuery, IAppQuery> query)
			=> _source.ClearText(q => query(q.AsGenericAppQuery()).ToXamarinQuery());

		public void ClearText(Func<IAppQuery, IAppWebQuery> query)
			=> throw new NotImplementedException();

		public void DismissKeyboard()
			=> _source.DismissKeyboard();

		public void DoubleTap(Func<IAppQuery, IAppQuery> query)
			=> _source.DoubleTap(q => query(q.AsGenericAppQuery()).ToXamarinQuery());

		public void DoubleTap(string marked)
			=> _source.DoubleTap(marked);

		public void DoubleTapCoordinates(float x, float y)
			=> _source.DoubleTapCoordinates(x, y);

		public void DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to)
			=> _source.DragAndDrop(
				q => from(q.AsGenericAppQuery()).ToXamarinQuery(),
				q => from(q.AsGenericAppQuery()).ToXamarinQuery()
			);

		public void DragAndDrop(string from, string to)
			=> _source.DragAndDrop(from, to);

		public void DragCoordinates(float fromX, float fromY, float toX, float toY)
			=> _source.DragCoordinates(fromX, fromY, toX, toY);

		public void EnterText(string marked, string text)
			=> _source.EnterText(marked, text);

		public void EnterText(string text)
			=> _source.EnterText(text);

		public void EnterText(Func<IAppQuery, IAppWebQuery> query, string text)
			=> _source.EnterText(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery(),
				text
			);

		public void EnterText(Func<IAppQuery, IAppQuery> query, string text)
			=> _source.EnterText(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery(),
				text
			);

		public IAppResult[] Flash(string marked)
			=> _source.Flash(marked).Select(r => r.ToGenericAppResult()).ToArray();

		public IAppResult[] Flash(Func<IAppQuery, IAppQuery> query = null)
			=> _source.Flash(
					query != null
						? q => query(q.AsGenericAppQuery()).ToXamarinQuery()
						: (Func<global::Xamarin.UITest.Queries.AppQuery, global::Xamarin.UITest.Queries.AppQuery>)null
					)
					.Select(r => r.ToGenericAppResult())
					.ToArray();

		public object Invoke(string methodName, object[] arguments)
			=> _source.Invoke(methodName, arguments);

		public object Invoke(string methodName, object argument = null)
			=> _source.Invoke(methodName, argument);

		public void PinchToZoomIn(string marked, TimeSpan? duration = null)
			=> _source.PinchToZoomIn(marked, duration);

		public void PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null)
			=> _source.PinchToZoomIn(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				, duration
			);

		public void PinchToZoomInCoordinates(float x, float y, TimeSpan? duration)
			=> _source.PinchToZoomInCoordinates(x, y, duration);

		public void PinchToZoomOut(string marked, TimeSpan? duration = null)
			=> _source.PinchToZoomOut(marked, duration);

		public void PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null)
			=> _source.PinchToZoomOut(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				, duration
			);

		public void PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration)
			=> _source.PinchToZoomOutCoordinates(x, y, duration);

		public void PressEnter()
			=> _source.PressEnter();

		public void PressVolumeDown()
			=> _source.PressVolumeDown();

		public void PressVolumeUp()
			=> _source.PressVolumeUp();

		public IAppResult[] Query(Func<IAppQuery, IAppQuery> query = null)
			=> _source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
				.Select(r => r.ToGenericAppResult())
				.ToArray();

		public string[] Query(Func<IAppQuery, IInvokeJSAppQuery> query)
			=> _source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				);

		public IAppResult[] Query(string marked)
			=> _source
				.Query(marked)
				.Select(r => r.ToGenericAppResult())
				.ToArray();

		public IAppWebResult[] Query(Func<IAppQuery, IAppWebQuery> query)
			=> _source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
				.Select(r => r.ToGenericAppWebResult())
				.ToArray();

		public T[] Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query)
			=> _source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinAppTypedSelector()
				)
				.Select(o => (T)Convert.ChangeType(o, typeof(T), CultureInfo.InvariantCulture))
				.ToArray();

		public void Repl()
			=> _source.Repl();

		public FileInfo Screenshot(string title)
			=> _source.Screenshot(title);

		public void SetOrientationLandscape()
			=> _source.SetOrientationLandscape();

		public void SetOrientationPortrait()
			=> _source.SetOrientationPortrait();

		public void SetSliderValue(string marked, double value)
			=> _source.SetSliderValue(marked, value);

		public void SetSliderValue(Func<IAppQuery, IAppQuery> query, double value)
			=> _source.SetSliderValue(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, value
				);

		public void Tap(string marked)
			=> _source.Tap(marked);

		public void Tap(Func<IAppQuery, IAppWebQuery> query)
			=> _source.Tap(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				);

		public void Tap(Func<IAppQuery, IAppQuery> query)
			=> _source.Tap(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				);

		public void TapCoordinates(float x, float y)
			=> _source.TapCoordinates(x, y);

		public void TouchAndHold(Func<IAppQuery, IAppQuery> query)
			=> _source.TouchAndHold(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				);

		public void TouchAndHold(string marked)
			=> _source.TouchAndHold(marked);

		public void TouchAndHoldCoordinates(float x, float y)
			=> _source.TouchAndHoldCoordinates(x, y);

		public void WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> _source.WaitFor(predicate, timeoutMessage, timeout, retryFrequency, postTimeout);

		public IAppWebResult[] WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) =>
			throw new NotImplementedException();

		public IAppResult[] WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> _source.WaitForElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout)
				.Select(r => r.ToGenericAppResult())
				.ToArray();

		public IAppResult[] WaitForElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> _source.WaitForElement(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, timeoutMessage
					, timeout
					, retryFrequency
					, postTimeout
				)
				.Select(r => r.ToGenericAppResult())
				.ToArray();

		public void WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> _source.WaitForNoElement(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, timeoutMessage
					, timeout
					, retryFrequency
					, postTimeout
				);

		public void WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> _source.WaitForNoElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout);

		public void WaitForNoElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> _source.WaitForNoElement(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, timeoutMessage
					, timeout
					, retryFrequency
					, postTimeout
				);

		public void ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.ScrollDown(withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia);

		public void ScrollDown(Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.ScrollDown(q => withinQuery?.Invoke(q.AsGenericAppQuery()).ToXamarinQuery(), strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia);

		public void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollDownTo(q => toQuery(q.AsGenericAppQuery()).ToXamarinQuery(), q => withinQuery?.Invoke(q.AsGenericAppQuery()).ToXamarinQuery(), strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollDownTo(q => toQuery(q.AsGenericAppQuery()).ToXamarinQuery(), q => withinQuery?.Invoke(q.AsGenericAppQuery()).ToXamarinQuery(), strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollDownTo(q => toQuery(q.AsGenericAppQuery()).ToXamarinQuery(), withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollDownTo(toMarked, withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollTo(toMarked, withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollUp(Func<IAppQuery, IAppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.ScrollUp(q => query(q.AsGenericAppQuery()).ToXamarinQuery(), strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia);

		public void ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.ScrollUp(withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia);

		public void ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollUpTo(q => toQuery(q.AsGenericAppQuery()).ToXamarinQuery(), q => withinQuery?.Invoke(q.AsGenericAppQuery()).ToXamarinQuery(), strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollUpTo(q => toQuery(q.AsGenericAppQuery()).ToXamarinQuery(), q => withinQuery?.Invoke(q.AsGenericAppQuery()).ToXamarinQuery(), strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollUpTo(q => toQuery(q.AsGenericAppQuery()).ToXamarinQuery(), withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null)
			=> _source.ScrollUpTo(toMarked, withinMarked, strategy.ToXamarinScrollStrategy(), swipePercentage, swipeSpeed, withInertia, timeout);

		public void SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeLeftToRight(q => query(q.AsGenericAppQuery()).ToXamarinQuery(), swipePercentage, swipeSpeed, withInertia);

		public void SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeLeftToRight(marked, swipePercentage, swipeSpeed, withInertia);

		public void SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeLeftToRight(swipePercentage, swipeSpeed, withInertia);

		public void SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeLeftToRight(q => query(q.AsGenericAppQuery()).ToXamarinQuery(), swipePercentage, swipeSpeed, withInertia);

		public void SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeRightToLeft(swipePercentage, swipeSpeed, withInertia);

		public void SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeRightToLeft(q => query(q.AsGenericAppQuery()).ToXamarinQuery(), swipePercentage, swipeSpeed, withInertia);

		public void SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeRightToLeft(q => query(q.AsGenericAppQuery()).ToXamarinQuery(), swipePercentage, swipeSpeed, withInertia);

		public void SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
			=> _source.SwipeRightToLeft(marked, swipePercentage, swipeSpeed, withInertia);

		public void Dispose() { }

		public IQueryable<ILogEntry> GetSystemLogs(DateTime? afterDate = null)
		{
			return Enumerable.Empty<ILogEntry>().AsQueryable();
		}
	}
}
