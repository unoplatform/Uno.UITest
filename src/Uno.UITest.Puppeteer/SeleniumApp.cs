using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace Uno.UITest.Puppeteer
{
	internal partial class SeleniumApp : IApp
	{
		private readonly RemoteWebDriver _driver;
		private string _screenShotPath;

		public SeleniumApp(ChromeAppConfigurator chromeAppConfigurator)
		{
			_driver = new ChromeDriver(chromeAppConfigurator.ChromeDriverPath);
			_driver.Url = chromeAppConfigurator.SiteUri.OriginalString;
			_screenShotPath = chromeAppConfigurator.InternalScreenShotsPath;
		}

		void PerformActions(Action<Actions> action) {
			var actions = new OpenQA.Selenium.Interactions.Actions(_driver);
			action(actions);
			actions.Perform();
		}

		void IDisposable.Dispose()
		{
			_driver.Close();
		}

		private Task CreateTask(Action action)
		{
			action();
			return Task.FromResult(true);
		}

		private Task<T> CreateTask<T>(Func<T> func)
		{
			return Task.FromResult(func());
		}

		IDevice IApp.Device => new SeleniumDevice(this);

		Task IApp.Back()
			=> CreateTask(() => _driver.Navigate().Back());

		Task IApp.ClearText()
			=> throw new NotImplementedException();

		Task IApp.ClearText(string marked)
			=> throw new NotImplementedException();

		Task IApp.ClearText(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		Task IApp.ClearText(Func<IAppQuery, IAppWebQuery> query) => throw new NotImplementedException();

		Task IApp.DismissKeyboard() => throw new NotImplementedException();

		Task IApp.DoubleTap(Func<IAppQuery, IAppQuery> query)
			=> CreateTask(() => {

				var q = query(new SeleniumAppQuery(this));

			});

		Task IApp.DoubleTap(string marked) => throw new NotImplementedException();
		Task IApp.DoubleTapCoordinates(float x, float y) => throw new NotImplementedException();
		Task IApp.DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to) => throw new NotImplementedException();
		Task IApp.DragAndDrop(string from, string to) => throw new NotImplementedException();
		Task IApp.DragCoordinates(float fromX, float fromY, float toX, float toY) => throw new NotImplementedException();
		Task IApp.EnterText(string marked, string text) => throw new NotImplementedException();
		Task IApp.EnterText(string text) => throw new NotImplementedException();
		Task IApp.EnterText(Func<IAppQuery, IAppWebQuery> query, string text) => throw new NotImplementedException();
		Task IApp.EnterText(Func<IAppQuery, IAppQuery> query, string text) => throw new NotImplementedException();
		Task<IAppResult[]> IApp.Flash(string marked) => throw new NotImplementedException();
		Task<IAppResult[]> IApp.Flash(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		Task<object> IApp.Invoke(string methodName, object[] arguments) => throw new NotImplementedException();
		Task<object> IApp.Invoke(string methodName, object argument) => throw new NotImplementedException();
		Task IApp.PinchToZoomIn(string marked, TimeSpan? duration) => throw new NotImplementedException();
		Task IApp.PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration) => throw new NotImplementedException();
		Task IApp.PinchToZoomInCoordinates(float x, float y, TimeSpan? duration) => throw new NotImplementedException();
		Task IApp.PinchToZoomOut(string marked, TimeSpan? duration) => throw new NotImplementedException();
		Task IApp.PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration) => throw new NotImplementedException();
		Task IApp.PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration) => throw new NotImplementedException();
		Task IApp.PressEnter() => throw new NotImplementedException();
		Task IApp.PressVolumeDown() => throw new NotImplementedException();
		Task IApp.PressVolumeUp() => throw new NotImplementedException();
		Task<IAppResult[]> IApp.Query(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		Task<string[]> IApp.Query(Func<IAppQuery, IInvokeJSAppQuery> query) => throw new NotImplementedException();
		Task<IAppResult[]> IApp.Query(string marked) => throw new NotImplementedException();
		Task<IAppWebResult[]> IApp.Query(Func<IAppQuery, IAppWebQuery> query) => throw new NotImplementedException();
		Task<T[]> IApp.Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query) => throw new NotImplementedException();
		Task IApp.Repl() => throw new NotImplementedException();
		async Task<FileInfo> IApp.Screenshot(string title)
		{
			var shot = _driver.GetScreenshot();
			var fileName = Path.Combine(_screenShotPath, title + ".png");
			shot.SaveAsFile(fileName);

			return new FileInfo(fileName);
		}

		Task IApp.SetOrientationLandscape() => throw new NotImplementedException();
		Task IApp.SetOrientationPortrait() => throw new NotImplementedException();
		Task IApp.SetSliderValue(string marked, double value) => throw new NotImplementedException();
		Task IApp.SetSliderValue(Func<IAppQuery, IAppQuery> query, double value) => throw new NotImplementedException();
		Task IApp.SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeLeftToRight(string marked, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeLeftToRight(double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeRightToLeft(double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.SwipeRightToLeft(string marked, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotImplementedException();
		Task IApp.Tap(string marked) => throw new NotImplementedException();
		Task IApp.Tap(Func<IAppQuery, IAppWebQuery> query) => throw new NotImplementedException();

		Task IApp.Tap(Func<IAppQuery, IAppQuery> query) => CreateTask(() => {

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
		});

		Task IApp.TapCoordinates(float x, float y) => throw new NotImplementedException();
		Task IApp.TouchAndHold(Func<IAppQuery, IAppQuery> query) => throw new NotImplementedException();
		Task IApp.TouchAndHold(string marked) => throw new NotImplementedException();
		Task IApp.TouchAndHoldCoordinates(float x, float y) => throw new NotImplementedException();
		Task IApp.WaitFor(Func<bool> predicate, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotImplementedException();
		Task<IAppWebResult[]> IApp.WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotImplementedException();
		Task<IAppResult[]> IApp.WaitForElement(string marked, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotImplementedException();

		Task<IAppResult[]> IApp.WaitForElement(
			Func<IAppQuery, IAppQuery> query,
			string timeoutMessage,
			TimeSpan? timeout,
			TimeSpan? retryFrequency,
			TimeSpan? postTimeout) => CreateTask(() => {
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
			});

		Task IApp.WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
			=> throw new NotImplementedException();

		Task IApp.WaitForNoElement(string marked, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
			=> throw new NotImplementedException();

		Task IApp.WaitForNoElement(
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
	}
}
