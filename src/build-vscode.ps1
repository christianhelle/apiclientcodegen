# Build script for VS Code Extension

Write-Host "Building VS Code Extension..." -ForegroundColor Cyan

# Navigate to the VSCode directory
$vscodePath = Join-Path $PSScriptRoot "VSCode"
if (!(Test-Path $vscodePath)) {
    Write-Host "VSCode directory not found at $vscodePath" -ForegroundColor Red
    exit 1
}

Set-Location -Path $vscodePath

# Check if npm is installed
if (!(Get-Command npm -ErrorAction SilentlyContinue)) {
    Write-Host "npm is not installed. Please install Node.js and npm." -ForegroundColor Red
    exit 1
}

# Check if vsce is installed
if (!(Get-Command npx -ErrorAction SilentlyContinue)) {
    Write-Host "npx is not available. Please install Node.js and npm." -ForegroundColor Red
    exit 1
}

try {
    # Install dependencies
    Write-Host "Installing npm packages..." -ForegroundColor Yellow
    npm install
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to install npm packages. Exiting." -ForegroundColor Red
        exit 1
    }

# Build the extension
Write-Host "Building extension..." -ForegroundColor Yellow
npm run compile

# Bundle the extension with webpack
Write-Host "Bundling extension..." -ForegroundColor Yellow
npm run package

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to bundle extension. Exiting." -ForegroundColor Red
    exit 1
}

# Package as VSIX
Write-Host "Creating VSIX package..." -ForegroundColor Yellow
npm run vsix

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to create VSIX package. Exiting." -ForegroundColor Red
    exit 1
}

$vsixFiles = Get-ChildItem -Path "*.vsix" -ErrorAction SilentlyContinue
if ($vsixFiles.Count -gt 0) {
    Write-Host "Build complete. VSIX package is available at:" -ForegroundColor Green
    foreach ($file in $vsixFiles) {
        Write-Host "  - $($file.FullName)" -ForegroundColor Green
    }
} else {
    Write-Host "Build completed but no VSIX file was found." -ForegroundColor Yellow
}

# Return to the original directory
Set-Location -Path $PSScriptRoot

} catch {
    Write-Host "An error occurred during the build process:" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}
