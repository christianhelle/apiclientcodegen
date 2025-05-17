# Build script for the REST API Client Code Generator Rider extension

param (
    [switch]$Release = $false
)

$ErrorActionPreference = "Stop"

# Function to log messages with timestamp
function Write-Log {
    param (
        [string]$Message
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Host "[$timestamp] $Message"
}

# Set the build configuration
$configuration = if ($Release) { "Release" } else { "Debug" }
Write-Log "Building with configuration: $configuration"

# Get the script directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$solutionPath = Join-Path $scriptDir "Rider.sln"
$projectPath = Join-Path $scriptDir "ApiClientCodeGen.Rider/ApiClientCodeGen.Rider.csproj"
$outputDir = Join-Path $scriptDir "bin/$configuration"

# Ensure output directory exists
if (-Not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
}

try {
    # Restore packages
    Write-Log "Restoring NuGet packages..."
    dotnet restore $solutionPath
    if ($LASTEXITCODE -ne 0) {
        throw "Package restore failed with exit code $LASTEXITCODE"
    }

    # Build the solution
    Write-Log "Building solution..."
    dotnet build $solutionPath -c $configuration -o $outputDir
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed with exit code $LASTEXITCODE"
    }

    # Package as zip for JetBrains Marketplace
    Write-Log "Creating Rider plugin package..."
    $version = (Select-Xml -Path (Join-Path $scriptDir "ApiClientCodeGen.Rider/META-INF/plugin.xml") -XPath "/idea-plugin/version").Node.InnerText
    $pluginName = "REST.API.Client.Code.Generator-$version"
    $packagePath = Join-Path $outputDir "$pluginName.zip"
    
    $tempDir = Join-Path $outputDir "plugin-temp"
    if (Test-Path $tempDir) {
        Remove-Item -Path $tempDir -Recurse -Force
    }
    New-Item -ItemType Directory -Path $tempDir -Force | Out-Null
    
    # Copy files to temp directory
    Copy-Item -Path (Join-Path $outputDir "*.dll") -Destination $tempDir
    Copy-Item -Path (Join-Path $scriptDir "ApiClientCodeGen.Rider/META-INF") -Destination $tempDir -Recurse
    
    # Create zip file
    if (Test-Path $packagePath) {
        Remove-Item -Path $packagePath -Force
    }
    Compress-Archive -Path "$tempDir/*" -DestinationPath $packagePath
    
    # Cleanup temp directory
    Remove-Item -Path $tempDir -Recurse -Force
    
    Write-Log "Build completed successfully."
    Write-Log "Plugin package created at: $packagePath"
} 
catch {
    Write-Log "Build failed: $_"
    exit 1
}
