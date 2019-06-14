using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	internal class XamarinAppWebRect : IAppWebRect
	{
		private AppWebRect _source;

		public XamarinAppWebRect(AppWebRect rect) => this._source = rect;

		public AppWebRect Source => _source;

		float IAppWebRect.Width { get => _source.Width; set => _source.Width = value; }
		float IAppWebRect.Height { get => _source.Height; set => _source.Height = value; }
		float IAppWebRect.CenterX { get => _source.CenterX; set => _source.CenterX = value; }
		float IAppWebRect.CenterY { get => _source.CenterY; set => _source.CenterY = value; }
		float IAppWebRect.Top { get => _source.Top; set => _source.Top = value; }
		float IAppWebRect.Bottom { get => _source.Bottom; set => _source.Bottom = value; }
		float IAppWebRect.Left { get => _source.Left; set => _source.Left = value; }
		float IAppWebRect.Right { get => _source.Right; set => _source.Right = value; }
		float IAppWebRect.X { get => _source.X; set => _source.X = value; }
		float IAppWebRect.Y { get => _source.Y; set => _source.Y = value; }
	}
}
