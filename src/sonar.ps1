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
    /d:sonar.token=$sonar `
    /d:sonar.host.url="https://sonarcloud.io" `
    /d:sonar.cs.vstest.reportsPaths=TestResults/*.trx `
    /d:sonar.cs.vscoveragexml.reportsPaths=TestResults/*/*.xml `
    /d:sonar.cpd.exclusions="**/Installer/OpenApiGeneratorVersions.cs,**/Settings/OpenApiGeneratorSettings.cs,**/*.vsct"

# Build
.\build.ps1 --target VSIX

# Test (coverage is written as XML that SonarCloud can import directly)
$testProjects = @(
    "CLI/ApiClientCodeGen.CLI.Tests\ApiClientCodeGen.CLI.Tests.csproj",
    "Core/ApiClientCodeGen.Core.Tests\ApiClientCodeGen.Core.Tests.csproj",
    "Core/ApiClientCodeGen.Core.IntegrationTests\ApiClientCodeGen.Core.IntegrationTests.csproj"
)
foreach ($testProject in $testProjects) {
    dotnet test $testProject --collect "Code Coverage;Format=xml" --logger trx --results-directory TestResults
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}

# Publish results to SonarCloud
.\.tools\scanner\dotnet-sonarscanner end /d:sonar.token=$sonar