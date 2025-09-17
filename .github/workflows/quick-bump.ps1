#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Quick version bump script for common scenarios.

.DESCRIPTION
    Simple wrapper around bump-version.ps1 for common version bumping scenarios.
    This script provides shortcuts for the most frequently used operations.

.EXAMPLE
    .\quick-bump.ps1
    Increments minor version (1.32 -> 1.33)

.EXAMPLE
    .\quick-bump.ps1 major
    Increments major version (1.32 -> 2.0)

.EXAMPLE
    .\quick-bump.ps1 1.45
    Sets version to exactly 1.45
#>

param(
    [string]$VersionOrType = "minor"
)

# Check if bump-version.ps1 exists
if (-not (Test-Path "bump-version.ps1")) {
    Write-Error "bump-version.ps1 not found in current directory. Make sure you're in the .github/workflows directory."
    exit 1
}

# Determine what kind of parameter we received
if ($VersionOrType -match "^\d+\.\d+$") {
    # It's a version number
    Write-Host "Setting version to $VersionOrType..." -ForegroundColor Green
    & .\bump-version.ps1 -NewVersion $VersionOrType
} elseif ($VersionOrType -eq "major") {
    # Major version increment
    Write-Host "Incrementing major version..." -ForegroundColor Green
    & .\bump-version.ps1 -IncrementType major
} elseif ($VersionOrType -eq "minor") {
    # Minor version increment (default)
    Write-Host "Incrementing minor version..." -ForegroundColor Green
    & .\bump-version.ps1 -IncrementType minor
} else {
    Write-Error "Invalid parameter: $VersionOrType"
    Write-Host "Usage:" -ForegroundColor Yellow
    Write-Host "  .\quick-bump.ps1          # Increment minor version" -ForegroundColor White
    Write-Host "  .\quick-bump.ps1 major    # Increment major version" -ForegroundColor White
    Write-Host "  .\quick-bump.ps1 1.45     # Set specific version" -ForegroundColor White
    exit 1
}