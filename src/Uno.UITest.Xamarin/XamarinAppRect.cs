using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin
{
	internal class XamarinAppRect : IAppRect
	{
		private AppRect _source;

		public AppRect Source => _source;

		public XamarinAppRect(AppRect rect) => this._source = rect;

		public float Width => _source.Width;
		public float Height => _source.Height;
		public float X => _source.X;
		public float Y => _source.Y;
		public float CenterX => _source.CenterX;
		public float CenterY => _source.CenterY;

		float IAppRect.Right => _source.X + _source.Width;

		float IAppRect.Bottom => _source.Y + _source.Height;
	}
}
