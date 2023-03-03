.PHONY: restore-packages debug release

all: debug release package publish
	
restore-packages: 
	dotnet restore ./Mac.sln

debug: restore-packages
	dotnet build ./Mac.sln

release: restore-packages
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool build --configuration:Release $(shell pwd)/Mac.sln

package:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack $(shell pwd)/VSMac/ApiClientCodeGen.VSMac/bin/Release/net6.0/ApiClientCodeGen.VSMac.dll -d:$(shell pwd)

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build $(shell pwd)

test:
	dotnet test ./Core/ApiClientCodeGen.Core.Tests/ApiClientCodeGen.Core.Tests.csproj -f net6.0
	dotnet test ./Core/ApiClientCodeGen.Core.IntegrationTests/ApiClientCodeGen.Core.IntegrationTests.csproj -f net6.0
