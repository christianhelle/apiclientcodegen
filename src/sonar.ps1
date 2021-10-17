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
    /d:sonar.cs.vstest.reportsPaths=TestResults/**/*.trx `
    /d:sonar.cs.vscoveragexml.reportsPaths=TestResults/**/*.coveragexml

# Build and Run Tests
.\build.ps1 --target All

# Convert .coverage files to XML format
dotnet tool update dotnet-coverageconverter --tool-path .tools\coverage
.\.tools\coverage\dotnet-coverageconverter.exe --CoverageFilesFolder "TestResults"

# Publish results to SonarCloud
.\.tools\scanner\dotnet-sonarscanner end /d:sonar.login=$sonar