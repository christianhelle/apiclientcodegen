param (
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]
    $sonar
)

.\build.ps1 --target Clean

dotnet tool update dotnet-sonarscanner --tool-path .sonar\scanner

.\.sonar\scanner\dotnet-sonarscanner begin `
    /k:"christianhelle_apiclientcodegen" `
    /o:"christianhelle-github" `
    /d:sonar.login=$sonar `
    /d:sonar.host.url="https://sonarcloud.io" `
    /d:sonar.cs.vstest.reportsPaths=TestResults/**/*.trx `
    /d:sonar.cs.vscoveragexml.reportsPaths=TestResults/**/*.coverage `
    /d:sonar.cs.opencover.reportsPaths=**/coverage.cobertura.xml

.\build.ps1

.\.sonar\scanner\dotnet-sonarscanner end /d:sonar.login=$sonar