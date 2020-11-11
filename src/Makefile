.PHONY: restore-packages debug release

all: debug release package publish
	
restore-packages: 
	msbuild /t:"Restore" Mac.sln

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" Mac.sln

release: restore-packages
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool build --configuration:Release Mac.sln

package:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack ./ApiClientCodeGen.VSMac/bin/Release/**/ApiClientCodeGen.VSMac.dll

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build .
