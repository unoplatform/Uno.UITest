#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

dotnet build -c Release -f net8.0-browserwasm -p:TargetFrameworks=net8.0-browserwasm $UNO_UITEST_WASM_PROJECT /bl:$UNO_UITEST_SCREENSHOT_PATH/logs/wasm-build.binlog

# Start the server
dotnet run --project $UNO_UITEST_WASM_PROJECT -f net8.0-browserwasm -c Release --no-build &

npm i chromedriver@$UNO_UITEST_CHROMEDRIVER_VERSION
npm i puppeteer@$UNO_UITEST_PUPETTEER_VERSION
wget $UNO_UITEST_NUGET_URL
mono nuget.exe install NUnit.ConsoleRunner -Version $UNO_UITEST_NUNIT_VERSION

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

dotnet test -c Release \
	$UNO_UITEST_PROJECT \
	--logger "nunit;LogFileName=$UNO_TEST_RESULTS_FILE"
