using OpenQA.Selenium;

namespace Uno.UITest.Puppeteer
{
	internal class SeleniumAppResult : IAppResult
	{
		private IWebElement _source;

		public SeleniumAppResult(IWebElement e) => this._source = e;

		public string Id
		{
			get => _source.GetAttribute("id");
		}

		public string Description
		{
			get => "";
		}

		public IAppRect Rect
		{
			get => null;
		}

		public string Label
		{
			get => "";
		}

		public string Text
		{
			get => _source.GetAttribute("innerText");
		}

		public string Class
		{
			get => _source.GetAttribute("xamltype");
		}

		public bool Enabled
		{
			get => _source.Enabled;
		}
	}
}
