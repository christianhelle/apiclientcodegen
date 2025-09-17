#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Bumps the VERSION environment variable in GitHub workflow files.

.DESCRIPTION
    This script scans all GitHub workflow YAML files in the .github/workflows directory
    and increments the VERSION environment variable. It supports both major and minor
    version increments while preserving the GitHub run number part.

.PARAMETER IncrementType
    The type of version increment: "major" or "minor". Default is "minor".
    - major: Increments the first number (e.g., 1.32 -> 2.0)
    - minor: Increments the second number (e.g., 1.32 -> 1.33)

.PARAMETER NewVersion
    Optionally specify the exact version to set (e.g., "2.0", "1.45").
    If provided, this overrides the IncrementType parameter.

.PARAMETER WorkflowPath
    Path to the .github/workflows directory. Default is ".github/workflows".

.PARAMETER DryRun
    If specified, shows what changes would be made without actually modifying files.

.EXAMPLE
    .\bump-version.ps1
    Increments the minor version (e.g., 1.32 -> 1.33)

.EXAMPLE
    .\bump-version.ps1 -IncrementType major
    Increments the major version (e.g., 1.32 -> 2.0)

.EXAMPLE
    .\bump-version.ps1 -NewVersion "2.5"
    Sets the version to exactly 2.5

.EXAMPLE
    .\bump-version.ps1 -DryRun
    Shows what changes would be made without modifying files
#>

param(
    [ValidateSet("major", "minor")]
    [string]$IncrementType = "minor",
    
    [string]$NewVersion = $null,
    
    [string]$WorkflowPath = ".github/workflows",
    
    [switch]$DryRun
)

# Ensure we're in the repository root
if (-not (Test-Path $WorkflowPath)) {
    Write-Error "Workflow path '$WorkflowPath' not found. Make sure you're running this script from the repository root."
    exit 1
}

# Function to increment version
function Get-IncrementedVersion {
    param(
        [string]$CurrentVersion,
        [string]$Type
    )
    
    if ($CurrentVersion -match "^(\d+)\.(\d+)") {
        $major = [int]$matches[1]
        $minor = [int]$matches[2]
        
        switch ($Type) {
            "major" { 
                return "$($major + 1).0" 
            }
            "minor" { 
                return "$major.$($minor + 1)" 
            }
        }
    }
    
    throw "Invalid version format: $CurrentVersion"
}

# Function to process a single workflow file
function Update-WorkflowFile {
    param(
        [string]$FilePath,
        [string]$OldVersion,
        [string]$NewVersionValue
    )
    
    $content = Get-Content -Path $FilePath -Raw
    $oldPattern = "VERSION: $OldVersion."
    $newPattern = "VERSION: $NewVersionValue."
    
    if ($content -match [regex]::Escape($oldPattern)) {
        $newContent = $content -replace [regex]::Escape($oldPattern), $newPattern
        
        if ($DryRun) {
            Write-Host "Would update: $FilePath" -ForegroundColor Yellow
            Write-Host "  Old: $oldPattern`${{ github.run_number }}" -ForegroundColor Red
            Write-Host "  New: $newPattern`${{ github.run_number }}" -ForegroundColor Green
        } else {
            Set-Content -Path $FilePath -Value $newContent -NoNewline
            Write-Host "Updated: $FilePath" -ForegroundColor Green
            Write-Host "  $oldPattern`${{ github.run_number }} -> $newPattern`${{ github.run_number }}" -ForegroundColor Cyan
        }
        return $true
    }
    
    return $false
}

# Main script execution
try {
    Write-Host "GitHub Workflows VERSION Bumper" -ForegroundColor Magenta
    Write-Host "=================================" -ForegroundColor Magenta
    Write-Host ""
    
    # Get all workflow files
    $workflowFiles = Get-ChildItem -Path $WorkflowPath -Filter "*.yml" -File
    
    if ($workflowFiles.Count -eq 0) {
        Write-Warning "No workflow files found in $WorkflowPath"
        exit 0
    }
    
    Write-Host "Found $($workflowFiles.Count) workflow files to check..." -ForegroundColor Blue
    Write-Host ""
    
    # Find files with VERSION environment variables
    $filesWithVersion = @()
    $currentVersion = $null
    
    foreach ($file in $workflowFiles) {
        $content = Get-Content -Path $file.FullName -Raw
        
        # Look for VERSION: x.y.${{ github.run_number }} pattern
        if ($content -match "VERSION:\s*(\d+\.\d+)\.\$\{\{\s*github\.run_number\s*\}\}") {
            $versionInFile = $matches[1]
            
            if ($null -eq $currentVersion) {
                $currentVersion = $versionInFile
            } elseif ($currentVersion -ne $versionInFile) {
                Write-Warning "Inconsistent versions found: $currentVersion vs $versionInFile in $($file.Name)"
            }
            
            $filesWithVersion += $file
        }
    }
    
    if ($filesWithVersion.Count -eq 0) {
        Write-Warning "No files found with VERSION environment variable matching the expected pattern."
        Write-Host "Looking for pattern: VERSION: x.y.`${{ github.run_number }}" -ForegroundColor Gray
        exit 0
    }
    
    if ($null -eq $currentVersion) {
        Write-Error "Could not determine current version from workflow files."
        exit 1
    }
    
    Write-Host "Current version: $currentVersion" -ForegroundColor Blue
    Write-Host "Files to update:" -ForegroundColor Blue
    foreach ($file in $filesWithVersion) {
        Write-Host "  - $($file.Name)" -ForegroundColor Gray
    }
    Write-Host ""
    
    # Determine new version
    if ($NewVersion) {
        if ($NewVersion -match "^\d+\.\d+$") {
            $targetVersion = $NewVersion
            Write-Host "Setting version to: $targetVersion (specified)" -ForegroundColor Green
        } else {
            Write-Error "Invalid version format: $NewVersion. Expected format: x.y (e.g., 1.33, 2.0)"
            exit 1
        }
    } else {
        $targetVersion = Get-IncrementedVersion -CurrentVersion $currentVersion -Type $IncrementType
        Write-Host "Incrementing $IncrementType version: $currentVersion -> $targetVersion" -ForegroundColor Green
    }
    
    if ($DryRun) {
        Write-Host ""
        Write-Host "DRY RUN - No files will be modified" -ForegroundColor Yellow
        Write-Host ""
    }
    
    # Update files
    $updatedCount = 0
    foreach ($file in $filesWithVersion) {
        if (Update-WorkflowFile -FilePath $file.FullName -OldVersion $currentVersion -NewVersionValue $targetVersion) {
            $updatedCount++
        }
    }
    
    Write-Host ""
    if ($DryRun) {
        Write-Host "Dry run completed. $updatedCount files would be updated." -ForegroundColor Yellow
    } else {
        Write-Host "Successfully updated $updatedCount workflow files." -ForegroundColor Green
        Write-Host ""
        Write-Host "Next steps:" -ForegroundColor Magenta
        Write-Host "1. Review the changes: git diff" -ForegroundColor White
        Write-Host "2. Commit the changes: git add . && git commit -m 'Bump version to $targetVersion'" -ForegroundColor White
        Write-Host "3. Push to repository: git push" -ForegroundColor White
    }
}
catch {
    Write-Error "An error occurred: $($_.Exception.Message)"
    exit 1
}