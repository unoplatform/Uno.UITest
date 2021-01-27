#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

export UNO_UITEST_PLATFORM=iOS
export UNO_UITEST_IOSBUNDLE_PATH=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.iOS/bin/iPhoneSimulator/Release/Sample.app
export UNO_UITEST_SCREENSHOT_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots/ios
export UNO_UITEST_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
export UNO_UITEST_BINARY=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/bin/Release/net47/Sample.UITests.dll
export UNO_UITEST_IOS_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.iOS/Sample.iOS.csproj
export UNO_UITEST_NUNIT_VERSION=3.10.0
export UNO_UITEST_NUGET_URL=https://dist.nuget.org/win-x86-commandline/v5.7.0/nuget.exe

cd $BUILD_SOURCESDIRECTORY/build
scripts/ios-uitest.sh
