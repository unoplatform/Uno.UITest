/*
Xamarin SDK

The MIT License (MIT)

Copyright (c) .NET Foundation Contributors

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Uno.UITest.Xamarin.Extensions;
using Xamarin.UITest.Desktop;

using XUQ = global::Xamarin.UITest.Queries;

namespace Uno.UITest.macOS
{

	public class MacOSApp : IApp
	{
		string _backButtonIdentifier = "NSBackButton";
		static CocoaApp _cocoaApp;
		public MacOSApp(CocoaApp app)
		{
			_cocoaApp = app;
		}

		public IDevice Device
		{
			get
			{
				return null;
			}
		}

		public void Back()
		{
			Tap(_backButtonIdentifier);
		}

		public void ClearText()
		{
			_cocoaApp.ClearText();
		}

		public void ClearText(string marked)
		{
			var textField = _cocoaApp.QueryById(marked).FirstOrDefault((arg) => arg.Class.Contains("SearchField") || arg.Class.Contains("TextField"));
			ClearText(textField.Rect.CenterX, textField.Rect.CenterY);
		}

		public void ClearText(Func<IAppQuery, IAppWebQuery> query)
		{

		}

		public void ClearText(Func<IAppQuery, IAppQuery> query)
		{
			var queryStr = query(new XUQ.AppQuery(XUQ.QueryPlatform.iOS).AsGenericAppQuery()).ToString();

			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if(isMarked)
			{
				var markedWord = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWord[0].Trim() == "*";
				var marked = markedWord[1].Replace("'", "");
				ClearText(marked);
			}
		}

		public void DismissKeyboard()
		{

		}

		public void DoubleTap(string marked)
		{

		}

		public void DoubleTap(Func<IAppQuery, IAppQuery> query)
		{

		}

		public void DoubleTapCoordinates(float x, float y)
		{

		}

		public void DragAndDrop(string from, string to)
		{

		}

		public void DragAndDrop(Func<IAppQuery, IAppQuery> from, Func<IAppQuery, IAppQuery> to)
		{

		}

		public void DragCoordinates(float fromX, float fromY, float toX, float toY)
		{

		}

		public void EnterText(string text)
		{
			var all = _cocoaApp.Query();
			var staticText = _cocoaApp.QueryByType("StaticText");
			var textFields = _cocoaApp.QueryByType("Textfield");
			var textField = staticText.Union(textFields).FirstOrDefault();
			EnterText(text, textField.Rect.CenterX, textField.Rect.CenterY);
		}

		public void EnterText(Func<IAppQuery, IAppWebQuery> query, string text)
		{

		}

		public void EnterText(string marked, string text)
		{
			var textField = _cocoaApp.QueryById(marked).FirstOrDefault((arg) => arg.Class.Contains("SearchField") || arg.Class.Contains("TextField"));
			EnterText(text, textField.Rect.CenterX, textField.Rect.CenterY);
		}

		public void EnterText(Func<IAppQuery, IAppQuery> query, string text)
		{
			string markedWord = string.Empty;
			int indexMarked = 0;

			if(ExtractInfo(query, out markedWord, out indexMarked))
				EnterText(markedWord, indexMarked, text);
		}

		public IAppResult[] Flash(string marked)
		{
			var resulr = new List<IAppResult>();
			return resulr.ToArray();
		}

		public IAppResult[] Flash(Func<IAppQuery, IAppQuery> query = null)
		{
			var resulr = new List<IAppResult>();
			return resulr.ToArray();
		}

		public object Invoke(string methodName, object[] arguments)
		{
			return null;
		}

		public object Invoke(string methodName, object argument = null)
		{
			return null;
		}

		public void PinchToZoomIn(string marked, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomIn(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomInCoordinates(float x, float y, TimeSpan? duration)
		{

		}

		public void PinchToZoomOut(string marked, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomOut(Func<IAppQuery, IAppQuery> query, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration)
		{

		}

		public void PressEnter()
		{
			_cocoaApp.SendKey(13, KeyModifier.None);
		}

		public void PressVolumeDown()
		{

		}

		public void PressVolumeUp()
		{

		}

		public IAppWebResult[] Query(Func<IAppQuery, IAppWebQuery> query)
		{
			var resulr = new List<IAppWebResult>();
			return resulr.ToArray();
		}

		public string[] Query(Func<IAppQuery, IInvokeJSAppQuery> query)
		{
			return new List<string>().ToArray();
		}

		public IAppResult[] Query(string marked)
		{
			var results = new List<IAppResult>();
			var allResults = _cocoaApp.Query();
			var allResultsById = _cocoaApp.QueryById(marked);
			foreach(var result in allResultsById)
				results.Add(result.ToUITestResult().ToGenericAppResult());
			var allResultsByText = _cocoaApp.QueryByText(marked);
			foreach(var result in allResultsByText)
				results.Add(result.ToUITestResult().ToGenericAppResult());
			return results.ToArray();
		}

		public IAppResult[] Query(Func<IAppQuery, IAppQuery> query = null)
		{
			var queryStr = query(new XUQ.AppQuery(XUQ.QueryPlatform.iOS).AsGenericAppQuery()).ToString();
			var results = new List<IAppResult>();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			var isText = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\btext\b");
			if(isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Remove(markedWords[1].Length - 1).Trim();
				return Query(markedWord);
			}
			if(isText)
			{
				var textWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\btext\b:'");
				var isAll = textWords[0].Trim() == "*";
				var textWord = textWords[1].Remove(textWords[1].Length - 1).Trim();
				return Query(textWord);
			}
			else if(queryStr.Contains("* index:0"))
			{
				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0];
				results.Add(result.ToUITestResult().ToGenericAppResult());
			}
			else if(queryStr.Contains("* index:7"))
			{
				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0].Children[0].Children[1];
				results.Add(result.ToUITestResult().ToGenericAppResult());
			}
			else if(queryStr.Contains("button"))
			{
				var allREsults = _cocoaApp.QueryByType("button");
				foreach(var item in allREsults)
				{
					results.Add(item.ToUITestResult().ToGenericAppResult());
				}

			}

			return results.ToArray();
		}

		public T[] Query<T>(Func<IAppQuery, IAppTypedSelector<T>> query)
		{

			var results = new List<T>();
			var queryStr = query(new XUQ.AppQuery(XUQ.QueryPlatform.iOS).AsGenericAppQuery()).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if(isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				var ss = Query(markedWord);

			}
			else if(queryStr.Contains("* index:0"))
			{

				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0];
				//	results.Add(result.ToUITestResult());
			}
			else if(queryStr.Contains("* index:7"))
			{
				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0].Children[0].Children[1];
				//	results.Add(result.ToUITestResult());
			}

			return results.ToArray();
		}

		public void Repl()
		{

		}

		public FileInfo Screenshot(string title)
		{
			return null;
		}

		public void ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollDown(Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollDownTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollDownTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollUp(Func<IAppQuery, IAppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUpTo(Func<IAppQuery, IAppQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUpTo(Func<IAppQuery, IAppWebQuery> toQuery, Func<IAppQuery, IAppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void SetOrientationLandscape()
		{

		}

		public void SetOrientationPortrait()
		{

		}

		public void SetSliderValue(Func<IAppQuery, IAppQuery> query, double value)
		{

		}

		public void SetSliderValue(string marked, double value)
		{

		}

		public void SwipeLeft()
		{

		}

		public void SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeLeftToRight(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeLeftToRight(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{
			throw new NotImplementedException();
		}

		public void SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeRight()
		{

		}

		public void SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeRightToLeft(Func<IAppQuery, IAppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{
			throw new NotImplementedException();
		}

		public void SwipeRightToLeft(Func<IAppQuery, IAppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void Tap(Func<IAppQuery, IAppWebQuery> query)
		{

		}

		public void Tap(string marked)
		{
			Tap(marked, 0);
		}

		public void Tap(Func<IAppQuery, IAppQuery> query)
		{
			string markedWord = string.Empty;
			int indexMarked = 0;

			if(ExtractInfo(query, out markedWord, out indexMarked))
				Tap(markedWord, indexMarked);
		}

		public void TapCoordinates(float x, float y)
		{

		}

		public void TouchAndHold(string marked)
		{
			TouchAndHold(marked, 0);
		}

		void TouchAndHold(string marked, int index)
		{
			var safeIndex = Math.Max(index, 0);
			var queryById = _cocoaApp.QueryById(marked.Trim())[safeIndex];
			TouchAndHoldCoordinates(queryById.Rect.CenterX, queryById.Rect.CenterY);

		}

		public void TouchAndHold(Func<IAppQuery, IAppQuery> query)
		{
			string markedWord = string.Empty;
			int indexMarked = 0;

			if(ExtractInfo(query, out markedWord, out indexMarked))
				TouchAndHold(markedWord, indexMarked);
		}

		public void TouchAndHoldCoordinates(float x, float y)
		{
			_cocoaApp.RightClick(x, y);
			Thread.Sleep(1000);
		}

		public void WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{

		}

		public IAppWebResult[] WaitForElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var resulr = new List<IAppWebResult>();
			return resulr.ToArray();
		}

		public IAppResult[] WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var results = new List<IAppResult>();

			var queryById = _cocoaApp.QueryById(marked);
			foreach(var res in queryById)
			{
				results.Add(res.ToUITestResult().ToGenericAppResult());
			}
			Stopwatch s = new Stopwatch();
			s.Start();
			bool foundElement = false;
			while(s.Elapsed < timeout && !foundElement)
			{
				var allResultsById = _cocoaApp.QueryById(marked);
				foreach(var res in queryById)
				{
					results.Add(res.ToUITestResult().ToGenericAppResult());
				}
				foundElement = results.Count > 0;
				System.Diagnostics.Debug.WriteLine(foundElement);
			}
			s.Stop();

			return results.ToArray();
		}

		public IAppResult[] WaitForElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var queryStr = query(new XUQ.AppQuery(XUQ.QueryPlatform.iOS).AsGenericAppQuery()).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if(isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "").Trim();
				return WaitForElement(markedWord, timeoutMessage, timeout, retryFrequency, postTimeout);
			}
			return new List<IAppResult>().ToArray();
		}

		public void WaitForNoElement(Func<IAppQuery, IAppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{


		}

		public void WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			Stopwatch s = new Stopwatch();
			s.Start();
			bool noElement = false;
			while(s.Elapsed < timeout && !noElement)
			{
				var allResultsById = _cocoaApp.QueryById(marked);
				noElement = allResultsById.Length == 0;
				System.Diagnostics.Debug.WriteLine(noElement);
			}
			s.Stop();
			if(s.Elapsed < timeout && !noElement)
				throw (new Exception(timeoutMessage));

		}

		public void WaitForNoElement(Func<IAppQuery, IAppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var queryStr = query(new XUQ.AppQuery(XUQ.QueryPlatform.iOS).AsGenericAppQuery()).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if(isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "").Trim();
				WaitForNoElement(markedWord, timeoutMessage, timeout, retryFrequency, postTimeout);
			}
		}

		static bool ExtractInfo(Func<IAppQuery, IAppQuery> query, out string markedWord, out int indexMarked)
		{
			indexMarked = 0;
			markedWord = string.Empty;
			var isSuccess = false;

			var queryStr = query(new XUQ.AppQuery(XUQ.QueryPlatform.iOS).AsGenericAppQuery()).ToString();
			var isIndex = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bindex\b");
			if(isIndex)
			{
				var indexWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bindex\b:");
				var indexWord = indexWords[1];
				int.TryParse(indexWord, out indexMarked);
				queryStr = indexWords[0].Trim();
			}
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if(isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				markedWord = markedWords[1].Replace("'", "").Trim();
				isSuccess = true;

			}
			var isText = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\btext\b");
			if(isText)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\btext\b:'");
				var isAll = markedWords[0].Trim() == "*";
				markedWord = markedWords[1].Replace("'", "").Trim();
				isSuccess = true;
			}
			if(!isSuccess)
			{
				if(queryStr == "button")
				{
					isSuccess = true;
				}
			}
			return isSuccess;
		}

		void Tap(string marked, int index)
		{
			var safeIndex = Math.Max(index, 0);
			var all = _cocoaApp.Query();
			var centerPoint = new PointF();
			if(!string.IsNullOrEmpty(marked))
				centerPoint = _cocoaApp.QueryById(marked.Trim())[safeIndex].Rect.Center;
			else
				centerPoint = _cocoaApp.QueryByType("Button")[safeIndex].Rect.Center;
			_cocoaApp.Click(centerPoint.X, centerPoint.Y);
			Thread.Sleep(1000);
		}

		static void EnterText(string marked, int index, string text)
		{
			AppResult textField = null;
			var safeIndex = Math.Max(index, 0);
			var textFields = _cocoaApp.QueryById(marked).Where((arg) => arg.Class.Contains("SearchField") || arg.Class.Contains("TextField"));
			if(textFields.Count() > 0)
			{
				textField = textFields.ElementAt(safeIndex);
			}
			else
			{
				var markedField = _cocoaApp.QueryById(marked);
				if(markedField.Length > 0)
				{
					textField = markedField[0];
				}
				else
				{

					var allTextFields = _cocoaApp.QueryByType("TextField");
					textField = allTextFields[0];
				}
			}

			if(textField != null)
				EnterText(text, textField.Rect.CenterX, textField.Rect.CenterY);

		}

		static void EnterText(string text, float x, float y)
		{
			_cocoaApp.Click(x, y);
			_cocoaApp.Click(x, y);
			Thread.Sleep(500);
			_cocoaApp.EnterText(text);
			Thread.Sleep(500);
		}

		static void ClearText(float x, float y)
		{
			_cocoaApp.Click(x, y);
			_cocoaApp.Click(x, y);
			Thread.Sleep(500);
			_cocoaApp.ClearText();
			Thread.Sleep(500);
		}

		public void Dispose()
		{
		}
	}
}
