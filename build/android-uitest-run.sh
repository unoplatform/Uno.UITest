#!/usr/bin/env bash
export UNO_UITEST_SCREENSHOT_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots/android
export UNO_UITEST_PLATFORM=Android
export UNO_UITEST_ANDROIDAPK_PATH=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.Droid/bin/Release/uno.platform.uitestsample.apk
export UNO_UITEST_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
export UNO_UITEST_ANDROID_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.Droid/Sample.Droid.csproj
export UNO_UITEST_BINARY=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/bin/Release/net47/Sample.UITests.dll
export UNO_UITEST_NUNIT_VERSION=3.10.0
export UNO_UITEST_ANDROID_API_LEVEL=28
export UNO_UITEST_NUGET_URL=https://dist.nuget.org/win-x86-commandline/v5.7.0/nuget.exe

cd $BUILD_SOURCESDIRECTORY/build
scripts/android-uitest.sh
