param (
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]
    $sonar
)

.\build.ps1 --target Clean

dotnet tool update dotnet-sonarscanner --tool-path .tools\scanner

.\.tools\scanner\dotnet-sonarscanner begin `
    /k:"christianhelle_apiclientcodegen" `
    /o:"christianhelle-github" `
    /d:sonar.login=$sonar `
    /d:sonar.host.url="https://sonarcloud.io" `
    /d:sonar.cs.vstest.reportsPaths=**/*.trx `
    /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml

# Build
.\build.ps1 --target VSIX

# Test
dotnet test CLI/ApiClientCodeGen.CLI.Tests\ApiClientCodeGen.CLI.Tests.csproj --collect "Code coverage"
dotnet test Core/ApiClientCodeGen.Core.Tests\ApiClientCodeGen.Core.Tests.csproj --collect "Code coverage"
dotnet test Core/ApiClientCodeGen.Core.IntegrationTests\ApiClientCodeGen.Core.IntegrationTests.csproj --collect "Code coverage"

# Convert .coverage files to XML format
dotnet tool update dotnet-coverageconverter --tool-path .tools\coverage
.\.tools\coverage\dotnet-coverageconverter.exe --CoverageFilesFolder . --ProcessAllFiles

# Publish results to SonarCloud
.\.tools\scanner\dotnet-sonarscanner end /d:sonar.login=$sonar