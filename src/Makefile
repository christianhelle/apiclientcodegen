.PHONY: restore-packages debug release

all: debug release package publish
	
restore-packages: 
	msbuild /t:"Restore" Mac.sln

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" Mac.sln

release: restore-packages
	msbuild /p:Configuration="Release" /t:"Rebuild" Mac.sln

package:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup help pack
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack ./ApiClientCodeGen.VSMac/bin/Release/net48/ApiClientCodeGen.VSMac.dll

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build .
