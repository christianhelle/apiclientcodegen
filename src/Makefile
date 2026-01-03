.PHONY: restore-packages debug release

all: release pack publish
	
restore-packages: 
	dotnet restore ./Mac.slnx

debug: restore-packages
	dotnet build ./VSMac/ApiClientCodeGen.VSMac/ApiClientCodeGen.VSMac.csproj

release: restore-packages
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool build --configuration:Release $(shell pwd)/Mac.slnx

pack: release
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack $(shell pwd)/VSMac/ApiClientCodeGen.VSMac/bin/Release/ApiClientCodeGen.VSMac.dll -d:$(shell pwd)

publish: pack
	/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup rep-build $(shell pwd)

test:
	dotnet test ./Core/ApiClientCodeGen.Core.Tests/ApiClientCodeGen.Core.Tests.csproj
	dotnet test ./Core/ApiClientCodeGen.Core.IntegrationTests/ApiClientCodeGen.Core.IntegrationTests.csproj
