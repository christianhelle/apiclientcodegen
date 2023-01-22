.PHONY: restore-packages debug release

all: debug release package publish
	
restore-packages: 
	dotnet restore ./Mac.sln

debug: restore-packages
	msbuild /p:Configuration="Debug" /t:"Rebuild" ./Mac.sln

release: restore-packages
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool build --configuration:Release ./Mac.sln

package:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack ./VSMac/ApiClientCodeGen.VSMac/bin/Release/**/ApiClientCodeGen.VSMac.dll

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build .

test:
	dotnet test ./Core/ApiClientCodeGen.Core.Tests/ApiClientCodeGen.Core.Tests.csproj -f net6.0
	dotnet test ./Core/ApiClientCodeGen.Core.IntegrationTests/ApiClientCodeGen.Core.IntegrationTests.csproj -f net6.0
