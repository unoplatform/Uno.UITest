#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

echo "Lising iOS simulators"
xcrun simctl list devices --json

/Applications/Xcode.app/Contents/Developer/Applications/Simulator.app/Contents/MacOS/Simulator &

dotnet build \
	-c Release \
	-f net8.0-ios \
	-p:RunAOTCompilation=false \
	-p:MtouchUseLlvm=false \
	-p:UseInterpreter=false \
	-p:TargetFrameworks=net8.0-ios \
	/p:IsUiAutomationMappingEnabled=True \
	$UNO_UITEST_IOS_PROJECT \
	/bl:$UNO_UITEST_SCREENSHOT_PATH/logs/ios-build.binlog

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

dotnet test -c Release \
	$UNO_UITEST_PROJECT \
	--logger "nunit;LogFileName=$UNO_TEST_RESULTS_FILE" \
	|| true
