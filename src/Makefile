.PHONY: restore-packages debug release

all: debug release
	
restore-packages: 
	nuget restore Mac.sln

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" Mac.sln

release: restore-packages
	msbuild /p:Configuration="Release" /t:"Rebuild" Mac.sln

package: release
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack ./ApiClientCodeGen.VSMac/bin/Release/net471/ApiClientCodeGen.VSMac.dll

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build .
