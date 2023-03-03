.PHONY: restore-packages debug release

all: debug release package publish
	
restore-packages: 
	dotnet restore ./Mac.sln

debug: restore-packages
	dotnet build ./VSMac/ApiClientCodeGen.VSMac/ApiClientCodeGen.VSMac.csproj

release: restore-packages
	dotnet build -c Release ./VSMac/ApiClientCodeGen.VSMac/ApiClientCodeGen.VSMac.csproj

package:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack $PWD/VSMac/ApiClientCodeGen.VSMac/bin/Release/net6.0/ApiClientCodeGen.VSMac.dll -d:$PWD

publish:
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build $PWD

test:
	dotnet test ./Core/ApiClientCodeGen.Core.Tests/ApiClientCodeGen.Core.Tests.csproj -f net6.0
	dotnet test ./Core/ApiClientCodeGen.Core.IntegrationTests/ApiClientCodeGen.Core.IntegrationTests.csproj -f net6.0
