using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sample
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();

			TestControls.Add(new TestControl("ComboBox 1", "Sample.Shared.Tests.ComboBox_Tests"));
			TestControls.Add(new TestControl("CheckBox 1", "Sample.Shared.Tests.CheckBox_Tests"));
		}

		public ObservableCollection<TestControl> TestControls { get; } = new ObservableCollection<TestControl>();

		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			if(sender is HyperlinkButton button && button.DataContext is TestControl tc)
			{
				var type = Type.GetType(tc.Type);

				var instance = Activator.CreateInstance(type) as FrameworkElement;

				if(instance != null)
				{
					testHost.Content = instance;
				}
			}
		}
	}

	[Bindable]
	public class TestControl
	{
		public TestControl(string name, string type)
		{
			Name = name;
			Type = type;
		}

		public string Name { get; }
		public string Type { get; }
	}
}
