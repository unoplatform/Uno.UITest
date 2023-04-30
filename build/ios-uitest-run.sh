#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

echo "Listing iOS simulators"
xcrun simctl list devices --json

export UNO_UITEST_SIMULATOR_VERSION="com.apple.CoreSimulator.SimRuntime.iOS-16-2"
export UNO_UITEST_SIMULATOR_NAME="iPad Pro (12.9-inch) (5th generation)"

##
## Pre-install the application to avoid https://github.com/microsoft/appcenter/issues/2389
##
export UITEST_IOSDEVICE_ID=`xcrun simctl list -j | jq -r --arg sim "$UNO_UITEST_SIMULATOR_VERSION" --arg name "$UNO_UITEST_SIMULATOR_NAME" '.devices[$sim] | .[] | select(.name==$name) | .udid'`

echo "Starting simulator: $UITEST_IOSDEVICE_ID ($UNO_UITEST_SIMULATOR_VERSION / $UNO_UITEST_SIMULATOR_NAME)"
xcrun simctl boot "$UITEST_IOSDEVICE_ID" || true

echo "Shutdown simulator: $UITEST_IOSDEVICE_ID ($UNO_UITEST_SIMULATOR_VERSION / $UNO_UITEST_SIMULATOR_NAME)"
xcrun simctl shutdown "$UITEST_IOSDEVICE_ID" || true

echo "Disable keyboard connection to the simulator"
# Xamarin.UITest needs this for keyboard interactions

#overwrite the existing value with false
#OR if the plist doesn't have that value add it in
/usr/libexec/PlistBuddy -c "Set :DevicePreferences:$UITEST_IOSDEVICE_ID:ConnectHardwareKeyboard 
false" ~/Library/Preferences/com.apple.iphonesimulator.plist || true

echo "searching for plist"
find ~ -name com.apple.iphonesimulator.plist
