using System;

namespace Uno.UITest.Puppeteer
{
	internal class SeleniumDevice : IDevice
	{
		private SeleniumApp puppeteerApp;

		public SeleniumDevice(SeleniumApp puppeteerApp) => this.puppeteerApp = puppeteerApp;

		Uri IDevice.DeviceUri => throw new NotImplementedException();

		string IDevice.DeviceIdentifier => throw new NotImplementedException();

		void IDevice.SetLocation(double latitude, double longitude) => throw new NotImplementedException();
	}
}
