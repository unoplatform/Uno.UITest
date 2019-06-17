#!/bin/bash

msbuild /r /p:Configuration=Release /uno.uitest/src/Sample/Sample.UITests/Sample.UITests.csproj
msbuild /r /p:Configuration=Release /uno.uitest/src/Sample/Sample.Wasm/Sample.Wasm.csproj

cd /uno.uitest/src/Sample/Sample.Wasm/bin/Release/netstandard2.0/dist
python server.py &

cd /uno.uitest/build

npm i chromedriver@74.0.0
npm i puppeteer@1.13.0
mono build/nuget install

export UNO_UITEST_TARGETURI=http://localhost:8000
export UNO_UITEST_DRIVERPATH_CHROME=/uno.uitest/build/node_modules/chromedriver/lib/chromedriver/chromedriver
export UNO_UITEST_CHROME_BINARY_PATH=/uno.uitest/build/node_modules/puppeteer/.local-chromium/linux-637110/chrome-linux
cp /uno.uitest/build/node_modules/chromedriver/lib/chromedriver/chromedriver /uno.uitest/build/node_modules/puppeteer/.local-chromium/linux-637110/chrome-linux/

mono NUnit.ConsoleRunner.3.10.0/tools/nunit3-console.exe /uno.uitest/src/Sample/Sample.UITests/bin/Release/net47/Sample.UITests.dll
