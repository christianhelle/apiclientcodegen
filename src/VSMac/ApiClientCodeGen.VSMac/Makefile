.PHONY: restore-packages debug release

all: debug release
	
restore-packages: 
	nuget restore ApiClientCodeGen.VSMac.csproj

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" ApiClientCodeGen.VSMac.csproj

release: restore-packages
	msbuild /p:Configuration="Release" /t:"Rebuild" ApiClientCodeGen.VSMac.csproj
	mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe setup pack bin/Release/net472/ApiClientCodeGen.VSMac.dll