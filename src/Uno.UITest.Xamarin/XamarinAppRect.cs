using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin
{
	internal class XamarinAppRect : IAppRect
	{
		private AppRect _source;

		public AppRect Source => _source;

		public XamarinAppRect(AppRect rect) => this._source = rect;

		public float Width { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public float Height { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public float X { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public float Y { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public float CenterX { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public float CenterY { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
	}
}
