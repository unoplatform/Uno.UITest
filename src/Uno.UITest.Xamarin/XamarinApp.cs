using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uno.UITest.Xamarin.Extensions;

namespace Uno.UITest.Xamarin
{
	internal class XamarinApp : IApp
	{
		global::Xamarin.UITest.IApp _source;

		public XamarinApp(global::Xamarin.UITest.IApp xamarinApp)
		{
			_source = xamarinApp;
		}

		public IDevice Device => new XamarinDevice(_source.Device);

		public Task Back()
			=> Task.Run(() => _source.Back());

		public Task ClearText()
			=> Task.Run(() => _source.ClearText());

		public Task ClearText(string marked)
			=> Task.Run(() => _source.ClearText(marked));

		public Task ClearText(Func<IAppQuery, IAppQuery> query)
			=> Task.Run(() => _source.ClearText(q => query(q.AsGenericAppQuery()).ToXamarinQuery()));

		public Task ClearText(Func<IAppQuery, IAppWebQuery> query)
			=> throw new NotImplementedException();

		public Task DismissKeyboard()
			=> Task.Run(() => _source.DismissKeyboard());

		public Task DoubleTap(Func<IAppQuery, IAppQuery> query)
			=> Task.Run(() => _source.DoubleTap(q => query(q.AsGenericAppQuery()).ToXamarinQuery()));

		public Task DoubleTap(string marked)
			=> Task.Run(() => _source.DoubleTap(marked));

		public Task DoubleTapCoordinates(float x, float y)
			=> throw new NotImplementedException();
		public Task DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to)
			=> Task.Run(() => _source.DragAndDrop(
				q => from(q.AsGenericAppQuery()).ToXamarinQuery(),
				q => from(q.AsGenericAppQuery()).ToXamarinQuery()
			));

		public Task DragAndDrop(string from, string to)
			=> Task.Run(() => _source.DragAndDrop(from, to));

		public Task DragCoordinates(float fromX, float fromY, float toX, float toY)
			=> Task.Run(() => _source.DragCoordinates(fromX, fromY, toX, toY));

		public Task EnterText(string marked, string text)
			=> Task.Run(() => _source.EnterText(marked, text));

		public Task EnterText(string text)
			=> Task.Run(() => _source.EnterText(text));

		public Task EnterText(Func<IAppQuery, IAppWebQuery> query, string text)
			=> Task.Run(() => _source.EnterText(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery(),
				text
			));

		public Task EnterText(Func<IAppQuery, IAppQuery> query, string text)
			=> Task.Run(() => _source.EnterText(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery(),
				text
			));

		public Task<IAppResult[]> Flash(string marked)
			=> Task.Run(() => _source.Flash(marked).Select(r => r.ToGenericAppResult()).ToArray());

		public Task<IAppResult[]> Flash(Func<IAppQuery, IAppQuery> query = null)
			=> Task.Run(() =>
				_source.Flash(
					query != null
						? q => query(q.AsGenericAppQuery()).ToXamarinQuery()
						: (Func<global::Xamarin.UITest.Queries.AppQuery, global::Xamarin.UITest.Queries.AppQuery>)null
					)
					.Select(r => r.ToGenericAppResult())
					.ToArray()
				);

		public Task<object> Invoke(string methodName, object[] arguments)
			=> Task.Run(() => _source.Invoke(methodName, arguments));

		public Task<object> Invoke(string methodName, object argument = null)
			=> Task.Run(() => _source.Invoke(methodName, argument));

		public Task PinchToZoomIn(string marked, TimeSpan? duration = null)
			=> Task.Run(() => _source.PinchToZoomIn(marked, duration));

		public Task PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null)
			=> Task.Run(() => _source.PinchToZoomIn(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				, duration
			));

		public Task PinchToZoomInCoordinates(float x, float y, TimeSpan? duration)
			=> Task.Run(() => _source.PinchToZoomInCoordinates(x, y, duration));

		public Task PinchToZoomOut(string marked, TimeSpan? duration = null)
			=> Task.Run(() => _source.PinchToZoomOut(marked, duration));

		public Task PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = null)
			=> Task.Run(() => _source.PinchToZoomOut(
				q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				, duration
			));

		public Task PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration)
			=> Task.Run(() => _source.PinchToZoomOutCoordinates(x, y, duration));

		public Task PressEnter()
			=> Task.Run(() => _source.PressEnter());

		public Task PressVolumeDown()
			=> Task.Run(() => _source.PressVolumeDown());

		public Task PressVolumeUp()
			=> Task.Run(() => _source.PressVolumeUp());

		public Task<IAppResult[]> Query(Func<IAppQuery, IAppQuery> query = null)
			=> Task.Run(() =>
				_source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
				.Select(r => r.ToGenericAppResult())
				.ToArray()
			);

		public Task<string[]> Query(Func<IAppQuery, IInvokeJSAppQuery> query)
			=> Task.Run(() =>
				_source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
			);

		public Task<IAppResult[]> Query(string marked)
			=> Task.Run(() => _source
				.Query(marked)
				.Select(r => r.ToGenericAppResult())
				.ToArray()
			);

		public Task<IAppWebResult[]> Query(Func<IAppQuery, IAppWebQuery> query)
			=> Task.Run(() =>
				_source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
				.Select(r => r.ToGenericAppWebResult())
				.ToArray()
			);

		public Task<T[]> Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query)
			=> Task.Run(() =>
				_source.Query(
					q => query(q.AsGenericAppQuery()).ToXamarinAppTypedSelector()
				)
			);

		public Task Repl()
			=> Task.Run(() => _source.Repl());

		public Task<FileInfo> Screenshot(string title)
			=> Task.Run(() => _source.Screenshot(title));

		public Task SetOrientationLandscape()
			=> Task.Run(() => _source.SetOrientationLandscape());

		public Task SetOrientationPortrait()
			=> Task.Run(() => _source.SetOrientationPortrait());

		public Task SetSliderValue(string marked, double value)
			=> Task.Run(() => _source.SetSliderValue(marked, value));

		public Task SetSliderValue(Func<IAppQuery, IAppQuery> query, double value)
			=> Task.Run(() =>
				_source.SetSliderValue(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, value
				)
			);

		public Task Tap(string marked)
			=> Task.Run(() => _source.Tap(marked));

		public Task Tap(Func<IAppQuery, IAppWebQuery> query)
			=> Task.Run(() =>
				_source.Tap(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
			);

		public Task Tap(Func<IAppQuery, IAppQuery> query)
			=> Task.Run(() =>
				_source.Tap(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
			);

		public Task TapCoordinates(float x, float y)
			=> Task.Run(() => _source.TapCoordinates(x, y));

		public Task TouchAndHold(Func<IAppQuery, IAppQuery> query)
			=> Task.Run(() =>
				_source.TouchAndHold(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
				)
			);

		public Task TouchAndHold(string marked)
			=> Task.Run(() => _source.TouchAndHold(marked));

		public Task TouchAndHoldCoordinates(float x, float y)
			=> Task.Run(() => _source.TouchAndHoldCoordinates(x, y));

		public Task WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> Task.Run(() => _source.WaitFor(predicate, timeoutMessage, timeout, retryFrequency, postTimeout));

		public Task<IAppWebResult[]> WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null) =>
			throw new NotImplementedException();

		public Task<IAppResult[]> WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> Task.Run(() =>
				_source.WaitForElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout)
				.Select(r => r.ToGenericAppResult())
				.ToArray()
			);

		public Task<IAppResult[]> WaitForElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> Task.Run(() =>
				_source.WaitForElement(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, timeoutMessage
					, timeout
					, retryFrequency
					, postTimeout
				)
				.Select(r => r.ToGenericAppResult())
				.ToArray()
			);

		public Task WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> Task.Run(() =>
				_source.WaitForNoElement(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, timeoutMessage
					, timeout
					, retryFrequency
					, postTimeout
				)
			);

		public Task WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> Task.Run(() =>
				_source.WaitForNoElement(marked, timeoutMessage, timeout, retryFrequency, postTimeout)
			);

		public Task WaitForNoElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = null, TimeSpan? retryFrequency = null, TimeSpan? postTimeout = null)
			=> Task.Run(() =>
				_source.WaitForNoElement(
					q => query(q.AsGenericAppQuery()).ToXamarinQuery()
					, timeoutMessage
					, timeout
					, retryFrequency
					, postTimeout
				)
			);



		public Task ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task ScrollDown(Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();
		public Task ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollUp(Func<IAppQuery, IAppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();
		public Task ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = null) =>
			throw new NotImplementedException();

		public Task SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

		public Task SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true) =>
			throw new NotImplementedException();

	}
}
