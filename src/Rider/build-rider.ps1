# Build JetBrains Rider plugin (cross-platform)
param(
    [string]$GradleCmd = "./gradlew"
)

Write-Host "Building JetBrains Rider plugin..."

Push-Location $PSScriptRoot
if (-not (Test-Path "$PSScriptRoot/gradlew")) {
    & gradle wrapper
}
& $GradleCmd buildPlugin
Pop-Location

Write-Host "Build complete. Plugin artifact is in src/Rider/build/distributions/"
