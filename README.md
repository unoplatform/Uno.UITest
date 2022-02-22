# Uno.UITest for Uno Platform

Welcome to the **Uno.UITest** repository, a framework which enables **unified UI Testing** of [Uno Platform apps](https://github.com/unoplatform/uno), using [NUnit 3.x](https://github.com/nunit/nunit).

This library provides a set of APIs to interact with an app, and assess its behavior using device simulators and browsers. The API set is based on [Xamarin.UITest](https://docs.microsoft.com/en-us/appcenter/test-cloud/uitest/), which makes the migration and patterns very familiar.

The testing is available through :
- Selenium for WebAssembly apps, using Chrome
- [Xamarin.UITest](https://docs.microsoft.com/en-us/appcenter/test-cloud/uitest/) and [AppCenter](https://appcenter.ms/apps) for iOS and Android apps.

The following target platforms are not yet supported:
- SkiaSharp backends (GTK, WPF and Tizen )
- Xamarin.macOS
- Windows

## Build status

| Target | Branch | Status | Recommended Nuget packages version |
| ------ | ------ | ------ | ------ |
| development | master |[![Build Status](https://dev.azure.com/uno-platform/Uno%20Platform/_apis/build/status/Uno%20Platform/Uno.UITest?branchName=master)](https://dev.azure.com/uno-platform/Uno%20Platform/_build/latest?definitionId=58&branchName=master) | [![NuGet](https://img.shields.io/nuget/v/Uno.UITest.svg)](https://www.nuget.org/packages/Uno.UITest/) |

## How to use Uno.UITest with an Uno Platform app

- Make sure [Chrome is installed](https://www.google.com/chrome)
- In Visual Studio for Windows, [create an application](https://platform.uno/docs/articles/getting-started-tutorial-1.html) using the Uno Platform templates
- Add the following code to each `.csproj` files (iOS, Android and WebAssembly), at the end before the closing `</project>` tag:
	```xml
	<PropertyGroup Condition="'$(Configuration)'=='Debug' or '$(IsUiAutomationMappingEnabled)'=='True'">
		<IsUiAutomationMappingEnabled>True</IsUiAutomationMappingEnabled>
		<DefineConstants>$(DefineConstants);USE_UITESTS</DefineConstants>
	</PropertyGroup>
	```
- In the iOS project, add a reference to the `Xamarin.TestCloud.Agent` nuget package (0.21.8 or later)
- In the `OnLaunched` method of `App.xaml.cs`, add the following at the beginning:
	```csharp
	#if __IOS__ && USE_UITESTS
		// Launches Xamarin Test Cloud Agent
		Xamarin.Calabash.Start();
	#endif
	```
- Install the Uno Platform `dotnet new` templates:

	```sh
	dotnet new -i Uno.ProjectTemplates.Dotnet
	```
- Navigate to your `.sln` folder using a command line:
    - Create a folder named `YourAppName\YourAppName.UITests`
    - Then run :
	```
    cd YourAppName.UITests
	dotnet new unoapp-uitest
	```
    The new project will be added automatically to your solution. If you get "No templates found matching: 'unoapp-uitest'." error, please see [dotnet new templates for Uno Platform](https://platform.uno/docs/articles/get-started-dotnet-new.html) article.
- In the new UI Tests project, edit the `Constants.cs` file with values that match your project 
- In your application, add the following XAML:

	```XAML
	<StackPanel>
		<CheckBox AutomationProperties.AutomationId="cb1" AutomationProperties.AccessibilityView="Raw" Content="Test 1"/>
	</StackPanel>
	```
   > Note that `AutomationProperties.AccessibilityView="Raw"` is only required for `ContentControl` based controls to allow for `cb1` to be selectable instead of the text `Test 1`. 

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

This sample is provided through the [`Sample.UITests` project](https://github.com/unoplatform/Uno.UITest/tree/master/src/Sample/Sample.UITests) in this repository.

### Running the tests for WebAssembly
- To test in Chrome, first deploy the WebAssemly app using `Ctrl+F5`, take note of the Url of the app
- Update the `Constants.WebAssemblyDefaultUri` property in `Constants.cs`
- Change the `Constants.CurrentPlatform` to `Platform.Browser`
- [Launch a test](https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2019) by right clicking on the test in the Test Explorer, or in the test code itself.
- A Chrome browser window will open in automated mode, and the test will execute.


### Running the tests for Android
- Build and deploy the app on a simulator
- Update the `Constants.AndroidAppName` property in `Constants.cs` to the value set in your app manifest
- Change the `Constants.CurrentPlatform` to `Platform.Android`
- [Launch a test](https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2019) by right clicking on the test in the Test Explorer, or in the test code itself.
- The application will start on the emulator, and the test will execute

### Running the tests for iOS
> testing for iOS is only available through Visual Studio for Mac, where the simulators can run.

- Open your solution in Visual Studio for mac
- Build and deploy the app on an iOS simulator
- Update the `Constants.iOSAppName` property in `Constants.cs` to the value specified in your `info.plist` file
- Change the `Constants.CurrentPlatform` to `Platform.iOS`
- [Launch a test](https://docs.microsoft.com/en-us/visualstudio/mac/testing?view=vsmac-2019)
- The application will start on the emulator, and the test will execute

This sample is provided through the [`Sample.UITests` project](https://github.com/unoplatform/Uno.UITest/tree/master/src/Sample/Sample.UITests) in this repository.

### Validating the currently running environment

```csharp
if(AppInitializer.GetLocalPlatform() == Platform.Android)
{
    Assert.Ignore();
}
```

## UI Testing in a CI environment

One of the design goal of the `Uno.UITest` library is to enable UI Testing in Pull Request builds, so that the UI testing is not an afterthought, and is part of the development flow.

You can find some scripts examples to enable such testing, using [Azure Devops hosted agents](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops):
- [Android UI Testing in a Simulator](https://github.com/unoplatform/Uno.UITest/blob/master/build/android-uitest-run.sh) using Linux
- [WebAssembly UI Testing](https://github.com/unoplatform/Uno.UITest/blob/master/build/wasm-uitest-run.sh) using Linux
- [iOS UI Testing in an simulator](https://github.com/unoplatform/Uno.UITest/blob/master/build/ios-uitest-run.sh) using macOS
