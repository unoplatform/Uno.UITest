# Uno.UITest for Uno Platform

Welcome to the **Uno.UITest** repository, a framework which enables **unified UI Testing** of [Uno Platform apps](https://github.com/unoplatform/uno), using [NUnit 3.x](https://github.com/nunit/nunit).

This library provides a set of APIs to interact with an app, and assess its behaviour using device simulators and browsers. The API set is based on [Xamarin.UITest](https://docs.microsoft.com/en-us/appcenter/test-cloud/uitest/), which makes the migration and patterns very familiar.

The testing is available through :
- Selenium for WebAssembly apps, using Chrome
- [Xamarin.UITest](https://docs.microsoft.com/en-us/appcenter/test-cloud/uitest/) and [AppCenter](https://appcenter.ms/apps) for iOS and Android apps.

## Build status

| Target | Branch | Status | Recommended Nuget packages version |
| ------ | ------ | ------ | ------ |
| development | master |[![Build Status](https://dev.azure.com/uno-platform/Uno%20Platform/_apis/build/status/Uno%20Platform/Uno.UITest?branchName=master)](https://dev.azure.com/uno-platform/Uno%20Platform/_build/latest?definitionId=58&branchName=master) | [![NuGet](https://img.shields.io/nuget/v/Uno.UITest.svg)](https://www.nuget.org/packages/Uno.UITest/) |

## How to use Uno.UITest with a WebAssembly app

- Make sure [Chrome is installed](https://www.google.com/chrome)
- In Visual Studio for Windows, [create an application](https://platform.uno/docs/articles/getting-started-tutorial-1.html) using the Uno Platform templates
- In the Wasm `Program.cs`, add the following line at the top of the `Main` function to enable automation support:
```
Uno.UI.FeatureConfiguration.UIElement.AssignDOMXamlName = true;
```
Note that if running on iOS or Android, setting a property is required:
```xml
<IsUiAutomationMappingEnabled>true</IsUiAutomationMappingEnabled>
```
- Create a .NET Standard 2.0 library, and replace its content with the following:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net47</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Uno.UITest" Version="1.0.0-dev.72" />
    <PackageReference Include="Uno.UITest.Helpers" Version="1.0.0-dev.72" />
    <PackageReference Include="NUnit" Version="3.12.0" />
    <PackageReference Include="NUnit3TestAdapter" Version="3.15.1" />
  </ItemGroup>

</Project>
```
- Add a file named `TestBase.cs` that will be used as a base test: 
```csharp
using NUnit.Framework;
using Uno.UITest;
using Uno.UITests.Helpers;

public class TestBase
{
	private IApp _app;

	static TestBase()
	{
		// Change this to your android app name
		AppInitializer.TestEnvironment.AndroidAppName = "com.example.myapp"; 

		// Change this to the URL of your WebAssembly app, found in launchsettings.json
		AppInitializer.TestEnvironment.WebAssemblyDefaultUri = "http://localhost:CHANGEME";

		// Change this to the bundle ID of your app
		AppInitializer.TestEnvironment.iOSAppName = "com.example.myapp";

		// Change this to the iOS device you want to test on
		AppInitializer.TestEnvironment.iOSDeviceNameOrId = "iPad Pro (12.9-inch) (3rd generation)";

		// The current platform to test.
		AppInitializer.TestEnvironment.CurrentPlatform = Uno.UITest.Helpers.Queries.Platform.Browser;

#if DEBUG
		// Show the running tests in a browser window
		AppInitializer.TestEnvironment.WebAssemblyHeadless = false;
#endif

		// Start the app only once, so the tests runs don't restart it
		// and gain some time for the tests.
		AppInitializer.ColdStartApp();
	}

	protected IApp App { get; set; }

	[SetUp]
	public void StartApp()
	{
		// Attach to the running application, for better performance
		App = AppInitializer.AttachToApp();
	}
}
```

- In your application, add the following XAML:

```XAML
<StackPanel>
	<CheckBox x:Uid="cb1" Content="Test 1"/>
</StackPanel>
```

- Then following test can be written:

```csharp
using NUnit.Framework;
using Uno.UITest.Helpers.Queries;
using System.Linq;
// Alias to simplify the creation of element queries
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;

public class CheckBox_Tests : TestBase
{
	[Test]
	public void CheckBox01()
	{
		Query checkBoxSelector = q => q.Marked("cb1");
		App.WaitForElement(checkBoxSelector);

		Query cb1 = q => q.Marked("cb1");
		App.WaitForElement(cb1);

		var value1 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
		Assert.IsFalse(value1);

		App.Tap(cb1);

		var value2 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
		Assert.IsTrue(value2);
	}
}
```
- To test in Chrome, first deploy the WebAssemly app using `Ctrl+F5`, take note of the Url of the app
- Update the `AppInitializer.TestEnvironment.WebAssemblyDefaultUri` property in `TestBase.cs`
- [Launch a test](https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2019) by right clicking on the test in the Test Explorer, or in the test code itself.
- A Chrome browser window will open in automated mode, and the test will execute.

Note that testing for iOS is only available through Visual Studio for Mac, where the simulators can run.

This sample is provided through the [`Sample.UITests` project](https://github.com/unoplatform/Uno.UITest/tree/master/src/Sample/Sample.UITests) in this repository.

## UI Testing in a CI environment

One of the design goal of the `Uno.UITest` library is to enable UI Testing in Pull Request builds, so that the UI testing is not an afterthought, and is part of the development flow.

You can find some scripts examples to enable such testing, using [Azure DevOps hosted agents](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops):
- [Android UI Testing in a Simulator](https://github.com/unoplatform/Uno.UITest/blob/master/build/android-uitest-run.sh) using Linux
- [WebAssembly UI Testing](https://github.com/unoplatform/Uno.UITest/blob/master/build/wasm-uitest-run.sh) using Linux
- [iOS UI Testing in a simulator](https://github.com/unoplatform/Uno.UITest/blob/master/build/ios-uitest-run.sh) using macOS
