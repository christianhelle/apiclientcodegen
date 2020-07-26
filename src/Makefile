.PHONY: restore-packages debug release

all: debug release
	
restore-packages: 
	nuget restore Mac.sln

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" Mac.sln

release: restore-packages
	msbuild /p:Configuration="Release" /t:"Rebuild" Mac.sln
	mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe setup pack ApiClientCodeGen.VSMac/bin/Release/net471/ApiClientCodeGen.VSMac.dll