# Build script for VS Code Extension

Write-Host "Building VS Code Extension..." -ForegroundColor Cyan

# Navigate to the VSCode directory
Set-Location -Path (Join-Path $PSScriptRoot "src\VSCode")

# Check if npm is installed
if (!(Get-Command npm -ErrorAction SilentlyContinue)) {
    Write-Host "npm is not installed. Please install Node.js and npm." -ForegroundColor Red
    exit 1
}

# Install dependencies
Write-Host "Installing npm packages..." -ForegroundColor Yellow
npm install

# Build the extension
Write-Host "Building extension..." -ForegroundColor Yellow
npm run compile

# Package the extension
Write-Host "Packaging extension..." -ForegroundColor Yellow
npm run package

Write-Host "Build complete. VSIX package is available in the src\VSCode directory." -ForegroundColor Green

# Return to the original directory
Set-Location -Path $PSScriptRoot
