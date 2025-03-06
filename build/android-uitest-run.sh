#!/usr/bin/env bash

## Adjust those variables for your project
export UNO_UITEST_SCREENSHOT_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots/android
export UNO_UITEST_ANDROIDAPK_PATH=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.Droid/bin/Release/uno.platform.uitestsample.apk
export UNO_UITEST_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
export UNO_UITEST_ANDROID_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample/Sample.csproj
export UNO_UITEST_ANDROID_API_LEVEL=28

cd $BUILD_SOURCESDIRECTORY/build
scripts/android-uitest.sh
