#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

cd $BUILD_SOURCESDIRECTORY/build

# This block allows to override the Android SDK
# disabled until hosted agents move to macOS 11
#
export ANDROID_HOME=$BUILD_SOURCESDIRECTORY/build/android-sdk
export ANDROID_SDK_ROOT=$BUILD_SOURCESDIRECTORY/build/android-sdk
export CMDLINETOOLS=commandlinetools-mac-8512546_latest.zip

if [[ ! -d $ANDROID_HOME ]];
then
	mkdir -p $ANDROID_HOME
	wget https://dl.google.com/android/repository/$CMDLINETOOLS
	unzip $CMDLINETOOLS -d $ANDROID_HOME/cmdline-tools
	rm $CMDLINETOOLS
	mv $ANDROID_SDK_ROOT/cmdline-tools/cmdline-tools $ANDROID_SDK_ROOT/cmdline-tools/latest
fi

# Install AVD files
echo "y" | $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager --install "system-images;android-$UNO_UITEST_ANDROID_API_LEVEL;google_apis_playstore;x86_64"
echo "y" | $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'platform-tools'  | tr '\r' '\n' | uniq
echo "y" | $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'platforms;android-34' | tr '\r' '\n' | uniq
echo "y" | $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'build-tools;34.0.0' | tr '\r' '\n' | uniq
echo "y" | $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'tools'| tr '\r' '\n' | uniq

# Create emulator
echo "no" | $ANDROID_HOME/cmdline-tools/latest/bin/avdmanager create avd -n xamarin_android_emulator --abi "x86_64" -k "system-images;android-$UNO_UITEST_ANDROID_API_LEVEL;google_apis_playstore;x86_64" --force

echo $ANDROID_HOME/emulator/emulator -list-avds

echo "Starting emulator"

# Kickstart the ADB server
$ANDROID_HOME/platform-tools/adb devices

# Start emulator in background
nohup $ANDROID_HOME/emulator/emulator -avd xamarin_android_emulator -no-snapshot -no-window -qemu > /dev/null 2>&1 &

# build the sample, while the emulator is starting
dotnet build -c Release -f net8.0-android -p:TargetFrameworks=net8.0-android $UNO_UITEST_ANDROID_PROJECT

# Wait for the emulator to finish booting
$ANDROID_HOME/platform-tools/adb wait-for-device shell 'while [[ -z $(getprop sys.boot_completed | tr -d '\r') ]]; do sleep 1; done; input keyevent 82'

$ANDROID_HOME/platform-tools/adb devices

echo "Emulator started"

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

dotnet test -c Release \
	$UNO_UITEST_PROJECT \
	--logger "nunit;LogFileName=$UNO_TEST_RESULTS_FILE" \
	|| true
