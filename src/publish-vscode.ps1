# PowerShell script for publishing the VS Code extension

param(
    [Parameter(Mandatory=$false)]
    [string]$PersonalAccessToken
)

$ErrorActionPreference = "Stop"

function Write-Step {
    param([string]$Message)
    Write-Host "===> $Message" -ForegroundColor Cyan
}

# Validate required tools
Write-Step "Validating required tools"

if (!(Get-Command npm -ErrorAction SilentlyContinue)) {
    Write-Host "Error: npm is not installed. Please install Node.js and npm." -ForegroundColor Red
    exit 1
}

if (!(Get-Command npx -ErrorAction SilentlyContinue)) {
    Write-Host "Error: npx is not installed. Please install Node.js and npm." -ForegroundColor Red
    exit 1
}

# Navigate to the VSCode directory
$vscodePath = Join-Path $PSScriptRoot "VSCode"
if (!(Test-Path $vscodePath)) {
    Write-Host "Error: VSCode directory not found at $vscodePath" -ForegroundColor Red
    exit 1
}

Set-Location -Path $vscodePath

try {
    # Build the extension
    Write-Step "Building the extension"
    npm install
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to install dependencies"
    }

    npm run compile
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to compile the extension"
    }

    npm run package
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to package the extension"
    }

    # Create the VSIX file
    Write-Step "Creating VSIX package"
    npm run vsix
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to create the VSIX package"
    }

    $vsixFile = Get-ChildItem -Path "*.vsix" | Sort-Object LastWriteTime | Select-Object -Last 1
    if (!$vsixFile) {
        throw "No VSIX file was created"
    }

    Write-Host "Created VSIX package: $($vsixFile.FullName)" -ForegroundColor Green

    # Publish to VS Code Marketplace if a token was provided
    if ($PersonalAccessToken) {
        Write-Step "Publishing to VS Code Marketplace"
        npx vsce publish -p $PersonalAccessToken
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to publish the extension"
        }

        Write-Host "Successfully published extension to VS Code Marketplace!" -ForegroundColor Green
    }
    else {
        Write-Host "No Personal Access Token provided. Skipping publishing to VS Code Marketplace." -ForegroundColor Yellow
        Write-Host "To publish, run this script with the -PersonalAccessToken parameter." -ForegroundColor Yellow
        Write-Host "Example: .\publish-vscode.ps1 -PersonalAccessToken 'your-token-here'" -ForegroundColor Yellow
    }
}
catch {
    Write-Host "Error: $_" -ForegroundColor Red
    exit 1
}
finally {
    Set-Location -Path $PSScriptRoot
}
