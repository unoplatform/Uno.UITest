using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace Uno.UITest.Selenium
{
	public partial class SeleniumApp : IApp
	{
		private readonly RemoteWebDriver _driver;
		private string _screenShotPath;

		public SeleniumApp(ChromeAppConfigurator config)
		{
			var options = new ChromeOptions();

			if(config.InternalHeadless)
			{
				options.AddArgument("headless");
			}

			options.AddArgument($"window-size={config.InternalWindowWidth}x{config.InternalWindowHeight}"); 

			_driver = new ChromeDriver(config.ChromeDriverPath, options);
			_driver.Url = config.SiteUri.OriginalString;
			_screenShotPath = config.InternalScreenShotsPath;
		}

		void PerformActions(Action<Actions> action)
		{
			var actions = new OpenQA.Selenium.Interactions.Actions(_driver);
			action(actions);
			actions.Perform();
		}

		void IDisposable.Dispose()
		{
			_driver.Close();
		}

		IDevice IApp.Device => new SeleniumDevice(this);

		void IApp.Back()
			=> _driver.Navigate().Back();

		void IApp.ClearText()
			=> throw new NotImplementedException();

		void IApp.ClearText(string marked)
			=> throw new NotImplementedException();

		void IApp.ClearText(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		void IApp.ClearText(Func<IAppQuery, IAppWebQuery> query) => throw new NotImplementedException();

		void IApp.DismissKeyboard() => throw new NotImplementedException();

		void IApp.DoubleTap(Func<IAppQuery, IAppQuery> query)
		{
			var q = query(new SeleniumAppQuery(this));
		}

		void IApp.DoubleTap(string marked) => throw new NotImplementedException();
		void IApp.DoubleTapCoordinates(float x, float y) => throw new NotImplementedException();
		void IApp.DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to) => throw new NotImplementedException();
		void IApp.DragAndDrop(string from, string to) => throw new NotImplementedException();
		void IApp.DragCoordinates(float fromX, float fromY, float toX, float toY) => throw new NotImplementedException();
		void IApp.EnterText(string marked, string text) => throw new NotImplementedException();
		void IApp.EnterText(string text) => throw new NotImplementedException();
		void IApp.EnterText(Func<IAppQuery, IAppWebQuery> query, string text) => throw new NotImplementedException();
		void IApp.EnterText(Func<IAppQuery, IAppQuery> query, string text) => throw new NotImplementedException();
		IAppResult[] IApp.Flash(string marked) => throw new NotImplementedException();
		IAppResult[] IApp.Flash(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		object IApp.Invoke(string methodName, object[] arguments) => throw new NotImplementedException();
		object IApp.Invoke(string methodName, object argument) => throw new NotImplementedException();
		void IApp.PinchToZoomIn(string marked, TimeSpan? duration) => throw new NotImplementedException();
		void IApp.PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration) => throw new NotImplementedException();
		void IApp.PinchToZoomInCoordinates(float x, float y, TimeSpan? duration) => throw new NotImplementedException();
		void IApp.PinchToZoomOut(string marked, TimeSpan? duration) => throw new NotImplementedException();
		void IApp.PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration) => throw new NotImplementedException();
		void IApp.PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration) => throw new NotImplementedException();
		void IApp.PressEnter() => throw new NotImplementedException();
		void IApp.PressVolumeDown() => throw new NotImplementedException();
		void IApp.PressVolumeUp() => throw new NotImplementedException();
		IAppResult[] IApp.Query(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		string[] IApp.Query(Func<IAppQuery, IInvokeJSAppQuery> query) => throw new NotImplementedException();
		IAppResult[] IApp.Query(string marked) => throw new NotImplementedException();
		IAppWebResult[] IApp.Query(Func<IAppQuery, IAppWebQuery> query) => throw new NotImplementedException();

		T[] IApp.Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query)
		{
			var q = query(new SeleniumAppQuery(this));

			return new[] { (T)Convert.ChangeType(EvaluateTypeSelector<T>(q as SeleniumAppTypedSelector<T>), typeof(T), CultureInfo.InvariantCulture) };
		}

		void IApp.Repl() => throw new NotImplementedException();

		FileInfo IApp.Screenshot(string title)
		{
			var shot = _driver.GetScreenshot();
			var fileName = Path.Combine(_screenShotPath, title + ".png");
			shot.SaveAsFile(fileName);

			return new FileInfo(fileName);
		}

		void IApp.SetOrientationLandscape() => throw new NotImplementedException();
		void IApp.SetOrientationPortrait() => throw new NotImplementedException();
		void IApp.SetSliderValue(string marked, double value) => throw new NotImplementedException();
		void IApp.SetSliderValue(Func<IAppQuery, IAppQuery> query, double value) => throw new NotImplementedException();
		void IApp.SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeLeftToRight(string marked, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeLeftToRight(double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeRightToLeft(double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.SwipeRightToLeft(string marked, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		void IApp.Tap(string marked) => throw new NotImplementedException();
		void IApp.Tap(Func<IAppQuery, IAppWebQuery> query) => throw new NotImplementedException();

		void IApp.Tap(Func<IAppQuery, IAppQuery> query)
		{
			var q = query(new SeleniumAppQuery(this));
			var result = Evaluate(q as SeleniumAppQuery);

			if(result is IEnumerable<IWebElement> elements)
			{
				var count = elements.Count();

				if(count == 0)
				{
					throw new InvalidOperationException("The query returned no results");
				}
				else if(count > 1)
				{
					var itemsString = string.Join(", ", elements.Select(e => e.GetAttribute("id")));
					throw new InvalidOperationException($"The query returned too many results ({itemsString})");
				}
				else
				{
					PerformActions(a => a.Click(elements.First()));
				}
			}
		}

		void IApp.TapCoordinates(float x, float y) => throw new NotImplementedException();
		void IApp.TouchAndHold(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		void IApp.TouchAndHold(string marked) => throw new NotImplementedException();
		void IApp.TouchAndHoldCoordinates(float x, float y) => throw new NotImplementedException();
		void IApp.WaitFor(Func<bool> predicate, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotImplementedException();
		IAppWebResult[] IApp.WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotImplementedException();
		IAppResult[] IApp.WaitForElement(string marked, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotImplementedException();

		IAppResult[] IApp.WaitForElement(
			Func<IAppQuery, IAppQuery> query,
			string timeoutMessage,
			TimeSpan? timeout,
			TimeSpan? retryFrequency,
			TimeSpan? postTimeout)
		{
			var sw = Stopwatch.StartNew();
			timeout = timeout ?? TimeSpan.MaxValue;
			retryFrequency = retryFrequency ?? TimeSpan.FromMilliseconds(500);
			timeoutMessage = timeoutMessage ?? "Timed out waiting for element...";

			while(sw.Elapsed < timeout)
			{
				var q = query(new SeleniumAppQuery(this));

				var result = Evaluate(q as SeleniumAppQuery);

				if(result is IEnumerable<IWebElement> elements)
				{
					if(elements.Count() != 0)
					{
						return ToAppResults(elements);
					}
				}

				Thread.Sleep(retryFrequency.Value);
			}

			throw new TimeoutException(timeoutMessage);
		}

		void IApp.WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
			=> throw new NotImplementedException();

		void IApp.WaitForNoElement(string marked, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
			=> throw new NotImplementedException();

		void IApp.WaitForNoElement(
			Func<IAppQuery, IAppWebQuery> query,
			string timeoutMessage,
			TimeSpan? timeout,
			TimeSpan? retryFrequency,
			TimeSpan? postTimeout) => throw new NotImplementedException();

		private IAppResult[] ToAppResults(IEnumerable<IWebElement> elements)
		{
			return elements.Select(e => new SeleniumAppResult(e)).ToArray();
		}

		private object Evaluate(SeleniumAppQuery q)
		{
			object currentItem = null;

			foreach(var item in q.QueryItems)
			{
				switch(item)
				{
					case SeleniumAppQuery.SearchQueryItem query:
						if(currentItem == null)
						{
							currentItem = _driver.FindElements(By.XPath(query.Query));
						}
						else if(currentItem is IEnumerable<IWebElement> items)
						{
							if(items.Count() == 1)
							{
								currentItem = items.First().FindElements(By.XPath(query.Query));
							}
							else
							{
								throw new InvalidOperationException($"Unable to execute a search on multiple items");
							}
						}
						else
						{
							throw new InvalidOperationException($"Unable to execute a search on {currentItem?.GetType()}");
						}
						break;
				}
			}

			return currentItem;
		}

		private object EvaluateTypeSelector<T>(SeleniumAppTypedSelector<T> selector)
		{
			if(selector.Invocations.Count() > 1)
			{
				throw new NotSupportedException($"Multiple invocations are not supporte in IAppTypedSelector");
			}

			var invocation = selector.Invocations.First();
			var evaluationResult = Evaluate(selector.Parent as SeleniumAppQuery);

			if(evaluationResult is IEnumerable<IWebElement> elements)
			{
				var element = elements.First();

				var xamlHandle = element is IWebElement we ? we.GetAttribute("xamlhandle") : "";

				var args = string.Join(", ", invocation.Args.Select(a => $"\'{a}\'"));

				var script = $"return {invocation.MethodName}({xamlHandle}, {args});";
				return _driver.ExecuteScript(script);
			}

			throw new InvalidOperationException($"Unable to invoke {invocation.MethodName} {evaluationResult}");
		}
	}
}
