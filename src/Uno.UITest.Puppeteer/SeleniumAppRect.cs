using System.Globalization;
using OpenQA.Selenium;

namespace Uno.UITest.Selenium
{
	internal class SeleniumAppRect : IAppRect
	{
		private IWebElement _source;

		public SeleniumAppRect(IWebElement source)
			=> _source = source;

		float IAppRect.Width => _source.Size.Width;
		float IAppRect.Height => _source.Size.Height;
		float IAppRect.X => _source.Location.X;
		float IAppRect.Y => _source.Location.Y;
		float IAppRect.CenterX => _source.Location.X + _source.Size.Width / 2;
		float IAppRect.CenterY => _source.Location.Y + _source.Size.Height / 2;
	}
}
