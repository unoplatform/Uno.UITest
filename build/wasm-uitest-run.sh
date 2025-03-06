#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

## Adjust those variables for your project
export UNO_UITEST_SCREENSHOT_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots/wasm
export UNO_TEST_RESULTS_FILE=$BUILD_ARTIFACTSTAGINGDIRECTORY/test-results/uitest-wasm.xml
export UNO_UITEST_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
export UNO_UITEST_BINARY=$BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/bin/Release/net47/Sample.UITests.dll
export UNO_UITEST_WASM_PROJECT=$BUILD_SOURCESDIRECTORY/src/Sample/Sample/Sample.csproj

## Less commonly modified variables
export UNO_UITEST_TARGETURI=http://localhost:54490
export UNO_UITEST_PLATFORM=Browser
export UNO_UITEST_CHROME_CONTAINER_MODE=true
export UNO_UITEST_NUNIT_VERSION=3.10.0
export UNO_UITEST_NUGET_URL=https://dist.nuget.org/win-x86-commandline/v5.7.0/nuget.exe
export UNO_UITEST_PUPETTEER_VERSION=23.11.1
export UNO_UITEST_CHROMEDRIVER_VERSION=131.0.0
export UNO_UITEST_CHROME_BINARY_PATH=~/.cache/puppeteer/chrome/linux-131.0.6778.204/chrome-linux64/chrome
export UNO_UITEST_DRIVERPATH_CHROME=$BUILD_SOURCESDIRECTORY/build/node_modules/chromedriver/lib/chromedriver

cd $BUILD_SOURCESDIRECTORY/build
scripts/wasm-uitest.sh
