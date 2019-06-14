using System;
using Xamarin.UITest;

namespace Uno.UITest.Xamarin
{
	internal class XamarinDevice : IDevice
	{
		private global::Xamarin.UITest.IDevice _source;

		public XamarinDevice(global::Xamarin.UITest.IDevice source) => _source = source;

		Uri IDevice.DeviceUri => _source.DeviceUri;

		string IDevice.DeviceIdentifier => _source.DeviceIdentifier;

		void IDevice.SetLocation(double latitude, double longitude) => _source.SetLocation(latitude, longitude);
	}
}
