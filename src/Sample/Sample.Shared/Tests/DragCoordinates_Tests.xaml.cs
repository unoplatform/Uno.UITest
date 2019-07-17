using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests
{
	public sealed partial class DragCoordinates_Tests : UserControl
	{
		public DragCoordinates_Tests()
		{
			this.InitializeComponent();

			Point startPos = new Point();
			bool pressed = false;

			myBorder.PointerPressed += (s, e) => {
				Console.WriteLine("Pointer pressed");
				startPos = e.GetCurrentPoint(myBorder).Position;
				pressed = true;
				myBorder.CapturePointer(e.Pointer);
			};

			myBorder.PointerMoved += (s, e) => {
				if(pressed)
				{
					Canvas.SetTop(myBorder, e.GetCurrentPoint(rootCanvas).Position.Y - startPos.Y);
					Canvas.SetLeft(myBorder, e.GetCurrentPoint(rootCanvas).Position.X - startPos.X);
				}
			};

			myBorder.PointerCanceled += (s, e) => {
				Console.WriteLine("Pointer cancelled");
			};

			myBorder.PointerReleased += (s, e) =>
			{
				Console.WriteLine("Pointer released");
				myBorder.ReleasePointerCapture(e.Pointer);
				pressed = false;
			};

			rootCanvas.PointerPressed += (s, e) => {
				Console.WriteLine($"Root pointer pressed {e.GetCurrentPoint(rootCanvas).Position}");
			};
			rootCanvas.PointerMoved += (s, e) => {
				Console.WriteLine($"Root pointer moved {e.GetCurrentPoint(rootCanvas).Position}");
			};
			rootCanvas.PointerReleased += (s, e) =>
			{
				Console.WriteLine($"Root pointer released {e.GetCurrentPoint(rootCanvas).Position}");
			};
		}
	}
}
