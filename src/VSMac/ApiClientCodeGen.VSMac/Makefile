.PHONY: restore-packages debug release

all: debug release
	
restore-packages: 
	nuget restore ApiClientCodeGen.VSMac.sln

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" ApiClientCodeGen.VSMac.sln

release: restore-packages
	msbuild /p:Configuration="Release" /t:"Rebuild" ApiClientCodeGen.VSMac.sln
	mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe setup pack bin/Release/net471/ApiClientCodeGen.VSMac.dll