# Uno.UITest

Welcome to the Uno.UITest repository, a framework that enables the UI Testing of Uno Platform apps.

The testing is available through :
- Selenium for WebAssembly apps
- Xamarin.UITest and AppCenter for Xamarin.iOS and Xamarin.Android apps.

## Test Sample

For the following XAML file:

```XAML
<StackPanel>
	<CheckBox x:Name="cb1" Content="Test 1"/>
	<CheckBox x:Name="cb2" Content="Test 2"/>
</StackPanel>
```

The following test can be written:

```csharp
[Test]
public void CheckBox01()
{
	Query checkBoxSelector = q => q.Text("CheckBox 1");
	App.WaitForElement(checkBoxSelector);
	App.Tap(checkBoxSelector);

	Query cb1 = q => q.Marked("cb1");
	Query cb2 = q => q.Marked("cb2");
	App.WaitForElement(cb1);
	App.WaitForElement(cb2);

	var value = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
	Assert.IsFalse(value);

	var value2 = App.Query(q => cb2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
	Assert.IsFalse(value2);

	App.Tap(cb1);

	var value3 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
	Assert.IsTrue(value3);

	App.Tap(cb2);

	var value4 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
	Assert.IsTrue(value4);
}
```

This sample is provided through the `Sample.UITests` project in this repository.
