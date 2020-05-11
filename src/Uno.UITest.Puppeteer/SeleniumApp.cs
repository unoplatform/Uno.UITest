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
using static System.Math;

namespace Uno.UITest.Selenium
{
	public partial class SeleniumApp : IApp
	{
		private readonly RemoteWebDriver _driver;
		private readonly string _screenShotPath;
		private readonly TimeSpan DefaultRetry = TimeSpan.FromMilliseconds(500);
		private readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);

		public SeleniumApp(RemoteWebDriver driver, string screenShotPath)
		{
			_driver = driver;
			_screenShotPath = screenShotPath;
		}

		public static SeleniumDriverManager SelectedBrowser { get; set; }

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
			=> PerformActions(a =>
				a.KeyDown(Keys.Control)
				.SendKeys("a")
				.KeyUp(Keys.Control)
				.SendKeys(Keys.Delete));

		void IApp.ClearText(string marked)
			=> (this as IApp).ClearText(q => q.Marked(marked));

		void IApp.ClearText(Func<IAppQuery, IAppQuery> query)
		{
			var element = GetSingleElement(query);
			PerformActions(a =>
				a.KeyDown(element, Keys.Control)
				.SendKeys(element, "a"+Keys.Delete)
				.KeyUp(element, Keys.Control));
		}

		void IApp.ClearText(Func<IAppQuery, IAppWebQuery> query) => throw new NotSupportedException();

		void IApp.DismissKeyboard() => Console.WriteLine("DismissKeybord has no effect on this platform");

		void IApp.DoubleTap(Func<IAppQuery, IAppQuery> query)
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
					PerformActions(a => a.DoubleClick(elements.First()));
				}
			}
		}

		void IApp.DoubleTap(string marked) => ((IApp)this).Tap(q => q.Marked(marked));

		void IApp.DoubleTapCoordinates(float x, float y)
		{
			PerformActions(a => a
				.MoveToElement(_driver.FindElementByTagName("body"), 0, 0)
				.MoveByOffset((int)x, (int)y)
				.DoubleClick());
		}

		void IApp.DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to) => throw new NotSupportedException();

		void IApp.DragAndDrop(string from, string to) => throw new NotSupportedException();

		void IApp.DragCoordinates(float fromX, float fromY, float toX, float toY)
		{
			PerformActions(a => a
				.MoveToElement(_driver.FindElementByTagName("body"), 0, 0)
				.MoveByOffset((int)fromX, (int)fromY)
				.ClickAndHold()
			);

			MoveByOffsetIncremental((int)(toX - fromX), (int)(toY - fromY));

			PerformActions(a => a
				.Release()
			);
		}

		/// <summary>
		/// Move mouse in increments, this is closer to user behaviour and ensures eg that PointerMoved is raised.
		/// </summary>
		private void MoveByOffsetIncremental(int offsetX, int offsetY)
		{
			const int consumptionIncr = 10; // Corresponds to moderate mouse speed
			var incrX = offsetX > 0 ? consumptionIncr : -consumptionIncr;
			var incrY = offsetY > 0 ? consumptionIncr : -consumptionIncr;

			var unconsumedX = offsetX;
			var unconsumedY = offsetY;
			do
			{
				int currentOffsetX, currentOffsetY;

				if (Abs(unconsumedX) > consumptionIncr)
				{
					currentOffsetX = incrX;
				}
				else
				{
					currentOffsetX = unconsumedX;
				}

				if (Abs(unconsumedY) > consumptionIncr)
				{
					currentOffsetY = incrY;
				}
				else
				{
					currentOffsetY = unconsumedY;
				}

				PerformActions(a => a.MoveByOffset(currentOffsetX, currentOffsetY));

				unconsumedX -= currentOffsetX;
				unconsumedY -= currentOffsetY;
			}
			while (unconsumedX != 0 || unconsumedY != 0);
		}

		void IApp.EnterText(string marked, string text)
			=> (this as IApp).EnterText(q => q.Marked(marked), text);

		void IApp.EnterText(string text)
			=> PerformActions(a => a.SendKeys(text));

		void IApp.EnterText(Func<IAppQuery, IAppWebQuery> query, string text)
			=> throw new NotSupportedException();

		void IApp.EnterText(Func<IAppQuery, IAppQuery> query, string text)
		{
			var element = GetSingleElement(query);
			PerformActions(a => a.SendKeys(element, text));
		}

		IAppResult[] IApp.Flash(string marked) => throw new NotSupportedException();

		IAppResult[] IApp.Flash(Func<IAppQuery, IAppQuery> query) => throw new NotSupportedException();

		object IApp.Invoke(string methodName, object[] arguments)
		{
			var args = string.Join(", ", arguments.Select(a => $"\'{a}\'"));
			var script = $"return {methodName}({args});";
			return _driver.ExecuteScript(script);
		}

		object IApp.Invoke(string methodName, object argument)
		{
			var script = $"return {methodName}(\'{argument}\');";
			return _driver.ExecuteScript(script);
		}

		void IApp.PinchToZoomIn(string marked, TimeSpan? duration) => throw new NotSupportedException();
		void IApp.PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration) => throw new NotSupportedException();
		void IApp.PinchToZoomInCoordinates(float x, float y, TimeSpan? duration) => throw new NotSupportedException();
		void IApp.PinchToZoomOut(string marked, TimeSpan? duration) => throw new NotSupportedException();
		void IApp.PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration) => throw new NotSupportedException();
		void IApp.PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration) => throw new NotSupportedException();
		void IApp.PressEnter()
			=> PerformActions(a => a.SendKeys(Keys.Return));

		void IApp.PressVolumeDown() => throw new NotSupportedException();
		void IApp.PressVolumeUp() => throw new NotSupportedException();
		IAppResult[] IApp.Query(Func<IAppQuery, IAppQuery> query)
		{
			var q = query(new SeleniumAppQuery(this));

			var result = Evaluate(q as SeleniumAppQuery);

			if(result is IEnumerable<IWebElement> elements)
			{
				return ToAppResults(elements);
			}

			return Array.Empty<IAppResult>();
		}
		string[] IApp.Query(Func<IAppQuery, IInvokeJSAppQuery> query) => throw new NotSupportedException();

		IAppResult[] IApp.Query(string marked)
			=> (this as IApp).Query(q => q.Marked(marked));

		IAppWebResult[] IApp.Query(Func<IAppQuery, IAppWebQuery> query) => throw new NotSupportedException();

		T[] IApp.Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query)
		{
			var q = query(new SeleniumAppQuery(this));

			return new[] { (T)Convert.ChangeType(EvaluateTypeSelector<T>(q as SeleniumAppTypedSelector<T>), typeof(T), CultureInfo.InvariantCulture) };
		}

		void IApp.Repl() => throw new NotSupportedException();

		FileInfo IApp.Screenshot(string title)
		{
			var shot = _driver.GetScreenshot();
			var fileName = Path.Combine(_screenShotPath, title + ".png");
			shot.SaveAsFile(fileName);

			return new FileInfo(fileName);
		}

		void IApp.SetOrientationLandscape() => Console.WriteLine($"SetOrientationLandscape is not supported by this platform");
		void IApp.SetOrientationPortrait() => Console.WriteLine($"SetOrientationPortrait is not supported by this platform");
		void IApp.SetSliderValue(string marked, double value) => throw new NotSupportedException();
		void IApp.SetSliderValue(Func<IAppQuery, IAppQuery> query, double value) => throw new NotSupportedException();
		void IApp.SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeLeftToRight(string marked, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeLeftToRight(double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeRightToLeft(double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.SwipeRightToLeft(string marked, double swipePercentage, int swipeSpeed, bool withInertia) => throw new NotSupportedException();
		void IApp.Tap(string marked) => ((IApp)this).Tap(q => q.Marked(marked));
		void IApp.Tap(Func<IAppQuery, IAppWebQuery> query) => throw new NotSupportedException();

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

		void IApp.TapCoordinates(float x, float y)
		{
			PerformActions(a => a
				.MoveToElement(_driver.FindElementByTagName("body"), 0, 0)
				.MoveByOffset((int)x, (int)y)
				.Click());
		}

		void IApp.TouchAndHold(Func<IAppQuery, IAppQuery> query) => throw new NotSupportedException();
		void IApp.TouchAndHold(string marked) => throw new NotSupportedException();
		void IApp.TouchAndHoldCoordinates(float x, float y) => throw new NotSupportedException();
		void IApp.WaitFor(Func<bool> predicate, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
		{
			var sw = Stopwatch.StartNew();
			timeout = timeout ?? DefaultTimeout;
			retryFrequency = retryFrequency ?? TimeSpan.FromMilliseconds(500);
			timeoutMessage = timeoutMessage ?? "Timed out waiting for element...";

			while(sw.Elapsed < timeout)
			{
				if(predicate())
				{
					return;
				}

				Thread.Sleep(retryFrequency.Value);
			}

			throw new TimeoutException(timeoutMessage);
		}

		IAppWebResult[] IApp.WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout) => throw new NotSupportedException();
		IAppResult[] IApp.WaitForElement(string marked, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
			=> (this as IApp).WaitForElement(
				query: q => q.Marked(marked),
				timeoutMessage: timeoutMessage,
				timeout: timeout,
				retryFrequency: retryFrequency,
				postTimeout: postTimeout);

		IAppResult[] IApp.WaitForElement(
			Func<IAppQuery, IAppQuery> query,
			string timeoutMessage,
			TimeSpan? timeout,
			TimeSpan? retryFrequency,
			TimeSpan? postTimeout)
		{
			var sw = Stopwatch.StartNew();
			timeout = timeout ?? DefaultTimeout;
			retryFrequency = retryFrequency ?? DefaultRetry;
			timeoutMessage = timeoutMessage ?? "Timed out waiting for element...";

			while(sw.Elapsed < timeout)
			{
				var results = (this as IApp).Query(query);

				if(results.Any())
				{
					return results;
				}

				Thread.Sleep(retryFrequency.Value);
			}

			throw new TimeoutException(timeoutMessage);
		}

		void IApp.WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency, TimeSpan? postTimeout)
		{
			var sw = Stopwatch.StartNew();
			timeout = timeout ?? DefaultTimeout;
			retryFrequency = retryFrequency ?? DefaultRetry;
			timeoutMessage = timeoutMessage ?? "Timed out waiting for element...";

			while(sw.Elapsed < timeout)
			{
				var results = (this as IApp).Query(query);

				if(!results.Any())
				{
					return;
				}

				Thread.Sleep(retryFrequency.Value);
			}

			throw new TimeoutException(timeoutMessage);
		}

		void IApp.WaitForNoElement(string marked, string timeoutMessage, TimeSpan? timeout, TimeSpan? retryFrequency,
			TimeSpan? postTimeout)
			=> (this as IApp).WaitForNoElement(
				query: q => q.Marked(marked),
				timeoutMessage: timeoutMessage,
				timeout: timeout,
				retryFrequency: retryFrequency,
				postTimeout: postTimeout);

		void IApp.WaitForNoElement(
			Func<IAppQuery, IAppWebQuery> query,
			string timeoutMessage,
			TimeSpan? timeout,
			TimeSpan? retryFrequency,
			TimeSpan? postTimeout) => throw new NotSupportedException();

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
							var result = items.SelectMany(i => i.FindElements(By.XPath("." + query.Query))).ToArray();

							currentItem = result;
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

				if(elements.FirstOrDefault() is IWebElement element)
				{

					var xamlHandle = element is IWebElement we ? we.GetAttribute("xamlhandle") : "";

					var args = string.Join(", ", invocation.Args.Select(a => $"\'{a}\'"));

					var script = $"return {invocation.MethodName}({xamlHandle}, {args});";
					return _driver.ExecuteScript(script);
				}
				else
				{
					return null;
				}
			}

			throw new InvalidOperationException($"Unable to invoke {invocation.MethodName} {evaluationResult}");
		}

		private IWebElement GetSingleElement(Func<IAppQuery, IAppQuery> query)
		{
			var elements = (this as IApp).Query(query);

			if(elements.Length == 1)
			{
				return (elements[0] as SeleniumAppResult).Source;
			}
			else if(elements.Length == 0)
			{
				throw new InvalidOperationException($"Unable to find element");
			}
			else if(elements.Length > 1)
			{
				throw new InvalidOperationException($"Too many elements matching");
			}

			throw new InvalidOperationException($"Invalid query results");
		}
	}
}
