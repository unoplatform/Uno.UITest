#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

msbuild /r /p:Configuration=Release $UNO_UITEST_PROJECT
dotnet build /r /p:Configuration=Release $UNO_UITEST_WASM_PROJECT /p:IsUiAutomationMappingEnabled=True

# Start the server
dotnet run --project $UNO_UITEST_WASM_PROJECT -c Release --no-build &

npm i chromedriver@$UNO_UITEST_PUPETTEER_VERSION
npm i puppeteer@$UNO_UITEST_CHROMEDRIVER_VERSION
wget $UNO_UITEST_NUGET_URL
mono nuget.exe install NUnit.ConsoleRunner -Version $UNO_UITEST_NUNIT_VERSION

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

mono $BUILD_SOURCESDIRECTORY/build/NUnit.ConsoleRunner.$UNO_UITEST_NUNIT_VERSION/tools/nunit3-console.exe \
  --trace=Verbose --inprocess --agents=1 --workers=1 \
  $UNO_UITEST_BINARY || true
