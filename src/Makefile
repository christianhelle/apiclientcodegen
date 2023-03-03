.PHONY: restore-packages debug release

all: debug release package publish
	
restore-packages: 
	dotnet restore ./Mac.sln

debug: restore-packages
	dotnet build ./VSMac/ApiClientCodeGen.VSMac/ApiClientCodeGen.VSMac.csproj

release: restore-packages
	dotnet build -c Release ./VSMac/ApiClientCodeGen.VSMac/ApiClientCodeGen.VSMac.csproj

package:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack $(shell pwd)/VSMac/ApiClientCodeGen.VSMac/bin/Release/net7.0/ApiClientCodeGen.VSMac.dll -d:$(shell pwd)

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build $(shell pwd)

test:
	dotnet test ./Core/ApiClientCodeGen.Core.Tests/ApiClientCodeGen.Core.Tests.csproj
	dotnet test ./Core/ApiClientCodeGen.Core.IntegrationTests/ApiClientCodeGen.Core.IntegrationTests.csproj
