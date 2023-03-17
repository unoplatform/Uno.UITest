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

## Getting started with Uno.UITest

Visit [our documentation](https://aka.platform.uno/uno-uitests) on how to get started with Uno.UITest.