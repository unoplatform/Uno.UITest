﻿using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Sample.Shared.Tests
{
	public sealed partial class DoubleTap_Tests_01 : Page
	{
		public DoubleTap_Tests_01()
		{
			this.InitializeComponent();
		}

		private void OnTouchTargetDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
		{
			Result.Text = "Double tapped!";
		}
	}
}
