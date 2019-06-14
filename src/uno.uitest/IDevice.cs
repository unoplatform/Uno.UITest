using System;

namespace Uno.UITest
{
	public interface IDevice
	{
		Uri DeviceUri { get; }

		string DeviceIdentifier { get; }

		void SetLocation(double latitude, double longitude);
	}
}
