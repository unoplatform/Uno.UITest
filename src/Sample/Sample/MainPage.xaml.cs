using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Data;

namespace Sample;

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
		TestControls.Add(new TestControl("RadioButton 01", "Sample.Shared.Tests.RadioButton_Tests_01"));
		TestControls.Add(new TestControl("TextBox 01", "Sample.Shared.Tests.TextBox_Tests_01"));
		TestControls.Add(new TestControl("TextBox 02", "Sample.Shared.Tests.TextBox_Tests_02"));
		TestControls.Add(new TestControl("DragCoordinates 01", "Sample.Shared.Tests.DragCoordinates_Tests"));
		TestControls.Add(new TestControl("DoubleTapped 01", "Sample.Shared.Tests.DoubleTap_Tests_01"));
		TestControls.Add(new TestControl("SetPropertyValue 01", "Sample.Shared.Tests.SetPropertyValue_Tests"));
		TestControls.Add(new TestControl("Element Selection 01", "Sample.Shared.Tests.Element_Selection_Tests_01"));
		TestControls.Add(new TestControl("Scroll 1", "Sample.Shared.Tests.Scroll_Tests"));
		TestControls.Add(new TestControl("AppDataMode 1", "Sample.Shared.Tests.AppDataMode_Tests"));
	}
	public ObservableCollection<TestControl> TestControls { get; } = new ObservableCollection<TestControl>();


	private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
	{
		if(sender is HyperlinkButton button && button.DataContext is TestControl tc)
		{
			if(Type.GetType(tc.Type) is { } type)
			{
				if(Activator.CreateInstance(type) is FrameworkElement instance)
				{
					testHost.Content = instance;
				}
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

