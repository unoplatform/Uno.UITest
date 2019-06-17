#!/bin/bash

cd $BUILD_SOURCESDIRECTORY

msbuild /r /p:Configuration=Release $BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/Sample.UITests.csproj
msbuild /r /p:Configuration=Release $BUILD_SOURCESDIRECTORY/src/Sample/Sample.Wasm/Sample.Wasm.csproj

cd $BUILD_SOURCESDIRECTORY/build

npm i chromedriver@74.0.0
npm i puppeteer@1.13.0
mono nuget.exe install NUnit.ConsoleRunner -Version 3.10.0

export UNO_UITEST_TARGETURI=http://localhost:8000
export UNO_UITEST_DRIVERPATH_CHROME=$BUILD_SOURCESDIRECTORY/build/node_modules/chromedriver/lib/chromedriver
export UNO_UITEST_CHROME_BINARY_PATH=$BUILD_SOURCESDIRECTORY/build/node_modules/puppeteer/.local-chromium/linux-637110/chrome-linux
export UNO_UITEST_SCREENSHOTS_PATH=$BUILD_ARTIFACTSTAGINGDIRECTORY/screenshots

## The python server serves the current working directory, and may be changed by the nunit runner
bash -c "cd $BUILD_SOURCESDIRECTORY/src/Sample/Sample.Wasm/bin/Release/netstandard2.0/dist/; python server.py &"

mono $BUILD_SOURCESDIRECTORY/build/NUnit.ConsoleRunner.3.10.0/tools/nunit3-console.exe $BUILD_SOURCESDIRECTORY/src/Sample/Sample.UITests/bin/Release/net47/Sample.UITests.dll
