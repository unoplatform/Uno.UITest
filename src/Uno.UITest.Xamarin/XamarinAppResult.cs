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
			set => _source.Id = value;
		}

		string IAppResult.Description
		{
			get => _source.Description;
			set => _source.Description = value;
		}
		IAppRect IAppResult.Rect
		{
			get => new XamarinAppRect(_source.Rect);
			set => _source.Rect = value is XamarinAppRect xr ? xr.Source : throw new InvalidOperationException();
		}

		string IAppResult.Label
		{
			get => _source.Label;
			set => _source.Label = value;
		}

		string IAppResult.Text
		{
			get => _source.Text;
			set => _source.Text = value;
		}

		string IAppResult.Class
		{
			get => _source.Class;
			set => _source.Class = value;
		}

		bool IAppResult.Enabled
		{
			get => _source.Enabled;
			set => _source.Enabled = value;
		}
	}
}
