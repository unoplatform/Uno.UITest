#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

echo "Lising iOS simulators"
xcrun simctl list devices --json

# check for the presence of idb, and install it if it's not present
export PATH=$PATH:~/.local/bin

if ! command -v idb &> /dev/null
then
	echo "Installing idb"
	brew install pipx
	# # https://github.com/microsoft/appcenter/issues/2605#issuecomment-1854414963
	brew tap facebook/fb
	brew install idb-companion
	pipx install fb-idb
else
	echo "Using idb from:" `command -v idb`
fi

# Wait while ios runtime 16.1 is not having simulators. The install process may 
# take a few seconds and "simctl list devices" may not return devices.
while true; do
	export UITEST_IOSDEVICE_ID=`xcrun simctl list -j | jq -r --arg sim "$UNO_UITEST_SIMULATOR_VERSION" --arg name "$UNO_UITEST_SIMULATOR_NAME" '.devices[$sim] | .[] | select(.name==$name) | .udid'`
	export UITEST_IOSDEVICE_DATA_PATH=`xcrun simctl list -j | jq -r --arg sim "$UNO_UITEST_SIMULATOR_VERSION" --arg name "$UNO_UITEST_SIMULATOR_NAME" '.devices[$sim] | .[] | select(.name==$name) | .dataPath'`

	if [ -n "$UITEST_IOSDEVICE_ID" ]; then
		break
	fi

	echo "Waiting for the simulator to be available"
	sleep 5
done

##
## Pre-install the application to avoid https://github.com/microsoft/appcenter/issues/2389
##
echo "Starting simulator: [$UITEST_IOSDEVICE_ID] ($UNO_UITEST_SIMULATOR_VERSION / $UNO_UITEST_SIMULATOR_NAME)"
xcrun simctl boot "$UITEST_IOSDEVICE_ID"

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

echo "Install app on simulator: $UITEST_IOSDEVICE_ID"
idb install --udid "$UITEST_IOSDEVICE_ID" "$UNO_UITEST_IOSBUNDLE_PATH"

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
