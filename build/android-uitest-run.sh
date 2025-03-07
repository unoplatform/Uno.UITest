#!/usr/bin/env bash

## Adjust those variables for your project
export UNO_UITEST_SCREENSHOT_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots/android
export UNO_UITEST_ANDROIDAPK_PATH=$BUILD_SOURCESDIRECTORY/src/Sample/Sample/bin/Release/net8.0-android/uno.platform.uitestsample-Signed.apk
export UNO_UITEST_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
export UNO_UITEST_ANDROID_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample/Sample.csproj
export UNO_UITEST_ANDROID_API_LEVEL=34
export UNO_TEST_RESULTS_FILE=$BUILD_ARTIFACTSTAGINGDIRECTORY/test-results/uitest-android.xml
export UNO_UITEST_PLATFORM=Android

cd $BUILD_SOURCESDIRECTORY/build
scripts/android-uitest.sh
