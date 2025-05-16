# PowerShell script for testing the VS Code extension

$ErrorActionPreference = "Stop"

function Write-Step {
    param([string]$Message)
    Write-Host "===> $Message" -ForegroundColor Cyan
}

# Set up a test folder with sample OpenAPI files
$testFolder = Join-Path (Split-Path $PSScriptRoot -Parent) "test\VSCode-Test"
$samplesFolder = Join-Path $testFolder "samples"

# Create test directories if they don't exist
if (!(Test-Path $testFolder)) {
    Write-Step "Creating test folder at $testFolder"
    New-Item -Path $testFolder -ItemType Directory | Out-Null
}

if (!(Test-Path $samplesFolder)) {
    Write-Step "Creating samples folder at $samplesFolder"
    New-Item -Path $samplesFolder -ItemType Directory | Out-Null
}

# Copy sample OpenAPI files to the test folder
$swaggerJsonSource = Join-Path $PSScriptRoot ".." "Swagger.json"
$swaggerYamlSource = Join-Path $PSScriptRoot ".." "Swagger.yaml"

if (Test-Path $swaggerJsonSource) {
    Write-Step "Copying Swagger.json to samples folder"
    Copy-Item -Path $swaggerJsonSource -Destination (Join-Path $samplesFolder "Swagger.json") -Force
} else {
    Write-Host "Warning: Swagger.json not found at $swaggerJsonSource" -ForegroundColor Yellow
}

if (Test-Path $swaggerYamlSource) {
    Write-Step "Copying Swagger.yaml to samples folder"
    Copy-Item -Path $swaggerYamlSource -Destination (Join-Path $samplesFolder "Swagger.yaml") -Force
} else {
    Write-Host "Warning: Swagger.yaml not found at $swaggerYamlSource" -ForegroundColor Yellow
}

# If no sample files found, download the Petstore OpenAPI specs (both JSON and YAML)
if (!(Test-Path (Join-Path $samplesFolder "*.json")) -and !(Test-Path (Join-Path $samplesFolder "*.yaml"))) {
    Write-Step "No sample files found. Downloading the Petstore OpenAPI specs..."
    $petstoreJsonUrl = "https://petstore3.swagger.io/api/v3/openapi.json"
    $petstoreJsonOutput = Join-Path $samplesFolder "petstore.json"
    $petstoreYamlUrl = "https://petstore3.swagger.io/api/v3/openapi.yaml"
    $petstoreYamlOutput = Join-Path $samplesFolder "petstore.yaml"
    
    # Download JSON version
    Write-Host "Downloading JSON version of Petstore OpenAPI spec..." -ForegroundColor Cyan
    try {
        Invoke-WebRequest -Uri $petstoreJsonUrl -OutFile $petstoreJsonOutput
        Write-Host "Downloaded Petstore OpenAPI JSON spec to $petstoreJsonOutput" -ForegroundColor Green
    } catch {
        Write-Host "Error downloading Petstore OpenAPI JSON spec: $_" -ForegroundColor Red
    }
    
    # Download YAML version
    Write-Host "Downloading YAML version of Petstore OpenAPI spec..." -ForegroundColor Cyan
    try {
        Invoke-WebRequest -Uri $petstoreYamlUrl -OutFile $petstoreYamlOutput
        Write-Host "Downloaded Petstore OpenAPI YAML spec to $petstoreYamlOutput" -ForegroundColor Green
    } catch {
        Write-Host "Error downloading Petstore OpenAPI YAML spec: $_" -ForegroundColor Red
    }
    
    # Check if any downloads succeeded
    if (!(Test-Path $petstoreJsonOutput) -and !(Test-Path $petstoreYamlOutput)) {
        Write-Host "Failed to download any OpenAPI specs. Please manually add sample OpenAPI spec files to $samplesFolder" -ForegroundColor Yellow
    }
}

# Build the extension if it's not already built
$vsixPath = Join-Path $PSScriptRoot "VSCode" "*.vsix"
if (!(Test-Path $vsixPath)) {
    Write-Step "VSIX file not found. Building the extension..."
    $buildScript = Join-Path $PSScriptRoot "build-vscode.ps1"
    if (Test-Path $buildScript) {
        & $buildScript
    } else {
        Write-Host "Error: build-vscode.ps1 not found at $buildScript" -ForegroundColor Red
        exit 1
    }
}

$vsixFile = Get-ChildItem -Path (Join-Path $PSScriptRoot "VSCode") -Filter "*.vsix" | Sort-Object LastWriteTime | Select-Object -Last 1

if ($vsixFile) {
    Write-Step "Using VSIX file: $($vsixFile.FullName)"
    
    # Launch VS Code with the test folder and the extension
    Write-Step "Launching VS Code with the test folder..."
    
    $command = "code --extensionDevelopmentPath=`"$(Join-Path $PSScriptRoot 'VSCode')`" `"$testFolder`""
    Write-Host "Executing: $command" -ForegroundColor Gray
    
    Invoke-Expression $command
      Write-Host ""
    Write-Host "VS Code has been launched with the extension in development mode." -ForegroundColor Green
    Write-Host "Use the following steps to test the extension:" -ForegroundColor Green
    Write-Host "1. Open a sample OpenAPI file from the 'samples' folder" -ForegroundColor Green
    Write-Host "   - JSON sample: petstore.json" -ForegroundColor Green
    Write-Host "   - YAML sample: petstore.yaml" -ForegroundColor Green
    Write-Host "2. Right-click on the file in the Explorer view" -ForegroundColor Green
    Write-Host "3. Select 'REST API Client Code Generator' from the context menu" -ForegroundColor Green
    Write-Host "4. Choose one of the code generators" -ForegroundColor Green
} else {
    Write-Host "Error: No VSIX file found after building. The build process may have failed." -ForegroundColor Red
    exit 1
}
