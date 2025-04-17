#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

cd $BUILD_SOURCESDIRECTORY/build

export ANDROID_HOME=$BUILD_SOURCESDIRECTORY/build/android-sdk
export ANDROID_SDK_ROOT=$BUILD_SOURCESDIRECTORY/build/android-sdk
export LATEST_CMDLINE_TOOLS_PATH=$ANDROID_SDK_ROOT/cmdline-tools/latest
export CMDLINETOOLS=commandlinetools-mac-8512546_latest.zip
mkdir -p $ANDROID_HOME

if [ -d $LATEST_CMDLINE_TOOLS_PATH ];
then
	rm -rf $LATEST_CMDLINE_TOOLS_PATH
fi

wget https://dl.google.com/android/repository/$CMDLINETOOLS
unzip -o $CMDLINETOOLS -d $ANDROID_HOME/cmdline-tools
rm $CMDLINETOOLS
mv $ANDROID_SDK_ROOT/cmdline-tools/cmdline-tools $LATEST_CMDLINE_TOOLS_PATH


AVD_NAME=xamarin_android_emulator
AVD_CONFIG_FILE=~/.android/avd/$AVD_NAME.avd/config.ini
EMU_UPDATE_FILE=~/.android/emu-update-last-check.ini
SDK_MGR_TOOLS_FLAG=.sdk_toolkit_installed

install_android_sdk() {
	SIMULATOR_APILEVEL=$1

	if [[ ! -f $SDK_MGR_TOOLS_FLAG ]];
	then
		touch $SDK_MGR_TOOLS_FLAG 

		echo "y" | $LATEST_CMDLINE_TOOLS_PATH/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'tools'| tr '\r' '\n' | uniq
		echo "y" | $LATEST_CMDLINE_TOOLS_PATH/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'platform-tools'  | tr '\r' '\n' | uniq
		echo "y" | $LATEST_CMDLINE_TOOLS_PATH/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'build-tools;35.0.0' | tr '\r' '\n' | uniq
		echo "y" | $LATEST_CMDLINE_TOOLS_PATH/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install 'extras;android;m2repository' | tr '\r' '\n' | uniq
	fi
	
	echo "y" | $LATEST_CMDLINE_TOOLS_PATH/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install "platforms;android-$SIMULATOR_APILEVEL" | tr '\r' '\n' | uniq
	echo "y" | $LATEST_CMDLINE_TOOLS_PATH/bin/sdkmanager --sdk_root=${ANDROID_HOME} --install "system-images;android-$SIMULATOR_APILEVEL;google_apis_playstore;x86_64" | tr '\r' '\n' | uniq
}

if [[ ! -f $EMU_UPDATE_FILE ]];
then
	touch $EMU_UPDATE_FILE
fi

if [[ ! -f $AVD_CONFIG_FILE ]];
then
	# Install AVD files
	install_android_sdk $UNO_UITEST_ANDROID_API_LEVEL

	if [[ -f $ANDROID_HOME/platform-tools/platform-tools/adb ]]
	then
		# It appears that the platform-tools 29.0.6 are extracting into an incorrect path
		mv $ANDROID_HOME/platform-tools/platform-tools/* $ANDROID_HOME/platform-tools
	fi

	# Create emulator
	echo "no" | $LATEST_CMDLINE_TOOLS_PATH/bin/avdmanager create avd -n "$AVD_NAME" --abi "x86_64" -k "system-images;android-$UNO_UITEST_ANDROID_API_LEVEL;google_apis_playstore;x86_64" --sdcard 128M --force

	# based on https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops&tabs=yaml#hardware
	# >> Agents that run macOS images are provisioned on Mac pros with a 3 core CPU, 14 GB of RAM, and 14 GB of SSD disk space.
	echo "hw.cpu.ncore=3" >> $AVD_CONFIG_FILE

	# Bump the heap size as the tests are stressing the application
	echo "vm.heapSize=256M" >> $AVD_CONFIG_FILE

	$ANDROID_HOME/emulator/emulator -list-avds

	echo "Checking for hardware acceleration"
	$ANDROID_HOME/emulator/emulator -accel-check

	echo "Starting emulator"

	# kickstart ADB
	$ANDROID_HOME/platform-tools/adb devices

	# Start emulator in background
	nohup $ANDROID_HOME/emulator/emulator -avd "$AVD_NAME" -skin 1280x800 -no-window -gpu swiftshader_indirect -no-snapshot -noaudio -no-boot-anim > $UNO_UITEST_SCREENSHOT_PATH/android-emulator-log.txt 2>&1 &

	# Wait for the emulator to finish booting
	source $BUILD_SOURCESDIRECTORY/build/scripts/android-uitest-wait-systemui.sh 500

else
	# Restart the emulator to avoid running first-time tasks
	$ANDROID_HOME/platform-tools/adb reboot

	# Wait for the emulator to finish booting
	source $BUILD_SOURCESDIRECTORY/build/scripts/android-uitest-wait-systemui.sh 500
fi

wget $UNO_UITEST_NUGET_URL
mono nuget.exe install NUnit.ConsoleRunner -Version $UNO_UITEST_NUNIT_VERSION

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

mono $BUILD_SOURCESDIRECTORY/build/NUnit.ConsoleRunner.$UNO_UITEST_NUNIT_VERSION/tools/nunit3-console.exe $UNO_UITEST_BINARY || true
