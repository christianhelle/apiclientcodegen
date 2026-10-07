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
    /d:sonar.cs.vstest.reportsPaths=TestResults/*.trx `
    /d:sonar.cs.vscoveragexml.reportsPaths=TestResults/*/*.xml

# Build
.\build.ps1 --target VSIX

# Test (coverage is written as XML that SonarCloud can import directly)
dotnet test CLI/ApiClientCodeGen.CLI.Tests\ApiClientCodeGen.CLI.Tests.csproj --collect "Code Coverage;Format=xml" --logger trx --results-directory TestResults
dotnet test Core/ApiClientCodeGen.Core.Tests\ApiClientCodeGen.Core.Tests.csproj --collect "Code Coverage;Format=xml" --logger trx --results-directory TestResults
dotnet test Core/ApiClientCodeGen.Core.IntegrationTests\ApiClientCodeGen.Core.IntegrationTests.csproj --collect "Code Coverage;Format=xml" --logger trx --results-directory TestResults

# Publish results to SonarCloud
.\.tools\scanner\dotnet-sonarscanner end /d:sonar.login=$sonar