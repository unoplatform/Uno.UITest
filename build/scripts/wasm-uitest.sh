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

cd $UNO_UITEST_SCREENSHOT_PATH

dotnet test -c Release \
	$UNO_UITEST_PROJECT \
	--logger "nunit;LogFileName=$UNO_TEST_RESULTS_FILE" || true

# terminate the bg task
kill %1

## fail if $UNO_TEST_RESULTS_FILE does not exist
if [ ! -f $UNO_TEST_RESULTS_FILE ]; then
	echo "No test results file found at $UNO_TEST_RESULTS_FILE"
	exit 1
fi
