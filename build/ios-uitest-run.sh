#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

## Adjust those variables for your project
export UNO_UITEST_IOSBUNDLE_PATH=$BUILD_SOURCESDIRECTORY/src/Sample/Sample/bin/Release/net8.0-ios/iossimulator-x64/Sample.app
export UNO_UITEST_SCREENSHOT_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots/ios
export UNO_UITEST_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
export UNO_UITEST_IOS_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample/Sample.csproj
export UNO_TEST_RESULTS_FILE=$BUILD_ARTIFACTSTAGINGDIRECTORY/test-results/uitest-ios.xml
export UNO_UITEST_PLATFORM=iOS

export UNO_UITEST_SIMULATOR_VERSION="com.apple.CoreSimulator.SimRuntime.iOS-18-1"
export UNO_UITEST_SIMULATOR_NAME="iPad Pro 13-inch (M4)"

cd $BUILD_SOURCESDIRECTORY/build
scripts/ios-uitest.sh
