using OpenQA.Selenium;

namespace Uno.UITest.Selenium
{
	internal class SeleniumAppResult : IAppResult
	{
		private readonly IWebElement _source;

		public SeleniumAppResult(IWebElement e) => _source = e;

		public string Id
		{
			get => Source.GetAttribute("id");
		}

		public string Description
		{
			get => "";
		}

		public IAppRect Rect
		{
			get => new SeleniumAppRect(Source);
		}

		public string Label
		{
			get => "";
		}

		public string Text
		{
			get => Source.GetAttribute("innerText");
		}

		public string Class
		{
			get => Source.GetAttribute("xamltype");
		}

		public bool Enabled
		{
			get => Source.Enabled;
		}

		internal IWebElement Source => _source;
	}
}
