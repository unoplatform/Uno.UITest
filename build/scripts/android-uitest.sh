#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

# Install AVD files
echo "y" | $ANDROID_HOME/tools/bin/sdkmanager --install "system-images;android-$UNO_UITEST_ANDROID_API_LEVEL;google_apis_playstore;x86_64"

# Create emulator
echo "no" | $ANDROID_HOME/tools/bin/avdmanager create avd -n xamarin_android_emulator --abi "x86_64" -k "system-images;android-$UNO_UITEST_ANDROID_API_LEVEL;google_apis_playstore;x86_64" --force

echo $ANDROID_HOME/emulator/emulator -list-avds

echo "Starting emulator"

# Kickstart the ADB server
$ANDROID_HOME/platform-tools/adb devices

# Start emulator in background
nohup $ANDROID_HOME/emulator/emulator -avd xamarin_android_emulator -no-snapshot -no-window -qemu > /dev/null 2>&1 &

# build the sample, while the emulator is starting
msbuild /r /p:Configuration=Release $UNO_UITEST_PROJECT
msbuild /r /p:Configuration=Release /p:IsUiAutomationMappingEnabled=True $UNO_UITEST_ANDROID_PROJECT

# Wait for the emulator to finish booting
$ANDROID_HOME/platform-tools/adb wait-for-device shell 'while [[ -z $(getprop sys.boot_completed | tr -d '\r') ]]; do sleep 1; done; input keyevent 82'

$ANDROID_HOME/platform-tools/adb devices

echo "Emulator started"

wget $UNO_UITEST_NUGET_URL
mono nuget.exe install NUnit.ConsoleRunner -Version $UNO_UITEST_NUNIT_VERSION

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

mono $BUILD_SOURCESDIRECTORY/build/NUnit.ConsoleRunner.$UNO_UITEST_NUNIT_VERSION/tools/nunit3-console.exe $UNO_UITEST_BINARY || true
