using System;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin
{
	internal class XamarinAppResult : IAppResult
	{
		private AppResult _source;

		public XamarinAppResult(AppResult result) => this._source = result;

		string IAppResult.Id
		{
			get => _source.Id;
		}

		string IAppResult.Description
		{
			get => _source.Description;
		}
		IAppRect IAppResult.Rect
		{
			get => new XamarinAppRect(_source.Rect);
		}

		string IAppResult.Label
		{
			get => _source.Label;
		}

		string IAppResult.Text
		{
			get => _source.Text;
		}

		string IAppResult.Class
		{
			get => _source.Class;
		}

		bool IAppResult.Enabled
		{
			get => _source.Enabled;
		}
	}
}
