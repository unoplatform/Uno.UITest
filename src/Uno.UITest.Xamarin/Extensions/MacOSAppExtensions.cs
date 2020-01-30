namespace Uno.UITest.macOS
{
	public static class MacOSAppExtensions
	{
		public static global::Xamarin.UITest.Queries.AppResult ToUITestResult(this global::Xamarin.UITest.Desktop.AppResult result)
		{
			return new global::Xamarin.UITest.Queries.AppResult
			{
				Id = result.Id ?? result.TestId,
				Label = result.Label,
				Text = result.Text ?? result.Value,
				Enabled = result.Enabled,
				Class = result.Class,
				Rect = new global::Xamarin.UITest.Queries.AppRect
				{
					X = result.Rect.X,
					Y = result.Rect.Y,
					Width = result.Rect.Width,
					Height = result.Rect.Height,
					CenterX = result.Rect.CenterX,
					CenterY = result.Rect.CenterY
				}
			};
		}
	}
}
