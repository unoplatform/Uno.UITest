#!/usr/bin/env bash
set -euo pipefail
IFS=$'\n\t'

echo "Lising iOS simulators"
xcrun simctl list devices --json

/Applications/Xcode.app/Contents/Developer/Applications/Simulator.app/Contents/MacOS/Simulator &

msbuild /r /p:Configuration=Release $UNO_UITEST_PROJECT
msbuild /r /p:Configuration=Release /p:Platform=iPhoneSimulator /p:IsUiAutomationMappingEnabled=True $UNO_UITEST_IOS_PROJECT 

wget $UNO_UITEST_NUGET_URL
mono nuget.exe install NUnit.ConsoleRunner -Version $UNO_UITEST_NUNIT_VERSION

mkdir -p $UNO_UITEST_SCREENSHOT_PATH

mono $BUILD_SOURCESDIRECTORY/build/NUnit.ConsoleRunner.$UNO_UITEST_NUNIT_VERSION/tools/nunit3-console.exe \
   --inprocess --agents=1 --workers=1 \
   $UNO_UITEST_BINARY || true
