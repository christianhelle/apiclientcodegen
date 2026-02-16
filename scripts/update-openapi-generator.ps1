<#
.SYNOPSIS
    Updates OpenAPI Generator version across the entire codebase.

.DESCRIPTION
    This script automates the process of upgrading OpenAPI Generator to a new version.
    It downloads the JAR to compute SHA1/MD5 hashes, then updates all source files,
    documentation, IDE extensions, and tests.

.PARAMETER NewVersion
    The new OpenAPI Generator version (e.g., "7.21.0").

.PARAMETER OldVersion
    The current/old version to replace. If not specified, it is auto-detected from
    OpenApiSupportedVersionExtensions.Latest in the source code.

.PARAMETER SHA1
    Optional. SHA1 hash of the JAR file. If not provided, the JAR is downloaded to compute it.

.PARAMETER MD5
    Optional. MD5 hash of the JAR file. If not provided, the JAR is downloaded to compute it.

.PARAMETER SkipDownload
    Skip downloading the JAR (requires SHA1 and MD5 to be provided).

.PARAMETER SkipBuild
    Skip building and testing after making changes.

.PARAMETER SkipCommit
    Skip committing changes to git.

.EXAMPLE
    .\scripts\update-openapi-generator.ps1 -NewVersion "7.21.0"

.EXAMPLE
    .\scripts\update-openapi-generator.ps1 -NewVersion "7.21.0" -SHA1 "abc123" -MD5 "def456" -SkipDownload
#>
param(
    [Parameter(Mandatory = $true)]
    [string]$NewVersion,

    [string]$OldVersion,

    [string]$SHA1,
    [string]$MD5,

    [switch]$SkipDownload,
    [switch]$SkipBuild,
    [switch]$SkipCommit
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot

# Auto-detect old version from source if not provided
if (-not $OldVersion) {
    $versionFile = Join-Path $repoRoot "src\Core\ApiClientCodeGen.Core\Options\OpenApiGenerator\OpenApiSupportedVersion.cs"
    $content = Get-Content $versionFile -Raw
    if ($content -match 'Latest => OpenApiSupportedVersion\.V(\d)(\d{2})(\d);') {
        $major = $Matches[1]
        $minor = [int]$Matches[2]
        $patch = $Matches[3]
        $OldVersion = "$major.$minor.$patch"
        Write-Host "Auto-detected current version: $OldVersion" -ForegroundColor Cyan
    }
    else {
        Write-Error "Could not auto-detect current version. Please provide -OldVersion."
        exit 1
    }
}

# Parse version components for enum naming
# Version format: major.minor.patch (e.g., 7.20.0)
# Enum naming: V + major + minor(2 digits) + patch (e.g., V7200)
$newParts = $NewVersion.Split('.')
$newMajor = $newParts[0]
$newMinor = $newParts[1]
$newPatch = $newParts[2]
$newEnumName = "V$newMajor$($newMinor.PadLeft(2,'0'))$newPatch"
$newEnumValue = [int]"$newMajor$($newMinor.PadLeft(2,'0'))$newPatch"

$oldParts = $OldVersion.Split('.')
$oldMajor = $oldParts[0]
$oldMinor = $oldParts[1]
$oldPatch = $oldParts[2]
$oldEnumName = "V$oldMajor$($oldMinor.PadLeft(2,'0'))$oldPatch"

Write-Host ""
Write-Host "=== OpenAPI Generator Version Update ===" -ForegroundColor Green
Write-Host "  Old: $OldVersion ($oldEnumName)" -ForegroundColor Yellow
Write-Host "  New: $NewVersion ($newEnumName = $newEnumValue)" -ForegroundColor Yellow
Write-Host ""

# Step 1: Get hashes
if (-not $SkipDownload -and (-not $SHA1 -or -not $MD5)) {
    $jarUrl = "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/$NewVersion/openapi-generator-cli-$NewVersion.jar"
    $tempFile = Join-Path $env:TEMP "openapi-generator-cli-$NewVersion.jar"

    Write-Host "Downloading JAR from $jarUrl..." -ForegroundColor Cyan
    Invoke-WebRequest -Uri $jarUrl -OutFile $tempFile
    Write-Host "  Downloaded: $((Get-Item $tempFile).Length) bytes" -ForegroundColor Gray

    $SHA1 = (Get-FileHash -Path $tempFile -Algorithm SHA1).Hash.ToLower()
    $MD5 = (Get-FileHash -Path $tempFile -Algorithm MD5).Hash.ToLower()

    Remove-Item $tempFile -ErrorAction SilentlyContinue
}

if (-not $SHA1 -or -not $MD5) {
    Write-Error "SHA1 and MD5 hashes are required. Provide them via parameters or allow download."
    exit 1
}

Write-Host "  SHA1: $SHA1" -ForegroundColor Gray
Write-Host "  MD5:  $MD5" -ForegroundColor Gray
Write-Host ""

# Helper for simple version string replacement in a file
function Update-VersionInFile {
    param([string]$RelativePath)
    $fullPath = Join-Path $repoRoot $RelativePath
    if (-not (Test-Path $fullPath)) {
        Write-Host "  SKIP (not found): $RelativePath" -ForegroundColor Yellow
        return
    }
    $content = Get-Content $fullPath -Raw
    $content = $content.Replace($OldVersion, $NewVersion)
    Set-Content $fullPath $content -NoNewline
    Write-Host "  Updated: $RelativePath" -ForegroundColor Green
}

# Step 2: Update Core - OpenApiGeneratorVersions.cs
Write-Host "--- Core: Version Registry ---" -ForegroundColor Magenta
$versionsFile = Join-Path $repoRoot "src\Core\ApiClientCodeGen.Core\Installer\OpenApiGeneratorVersions.cs"
$versionsContent = Get-Content $versionsFile -Raw

$newVersionEntry = "        new(`r`n            ""$NewVersion"",`r`n            `$""{DownloadUrlPrefix}/$NewVersion/openapi-generator-cli-$NewVersion.jar"",`r`n            ""$SHA1"",`r`n            ""$MD5""`r`n        ),`r`n"
$oldFirstEntry = "        new(`r`n            ""$OldVersion"""
$versionsContent = $versionsContent.Replace($oldFirstEntry, "$newVersionEntry$oldFirstEntry")
Set-Content $versionsFile $versionsContent -NoNewline
Write-Host "  Updated: OpenApiGeneratorVersions.cs" -ForegroundColor Green

# Step 3: Update Core - OpenApiSupportedVersion.cs
Write-Host "--- Core: Supported Version Enum ---" -ForegroundColor Magenta
$enumFile = Join-Path $repoRoot "src\Core\ApiClientCodeGen.Core\Options\OpenApiGenerator\OpenApiSupportedVersion.cs"
$enumContent = Get-Content $enumFile -Raw

$enumContent = $enumContent.Replace(
    "maps to <see cref=""$oldEnumName""/>",
    "maps to <see cref=""$newEnumName""/>"
)

$enumContent = $enumContent.Replace(
    "    [Description(""$OldVersion"")]`r`n    $oldEnumName = ",
    "    [Description(""$NewVersion"")]`r`n    $newEnumName = $newEnumValue,`r`n    [Description(""$OldVersion"")]`r`n    $oldEnumName = "
)

$enumContent = $enumContent.Replace(
    "Latest => OpenApiSupportedVersion.$oldEnumName;",
    "Latest => OpenApiSupportedVersion.$newEnumName;"
)

Set-Content $enumFile $enumContent -NoNewline
Write-Host "  Updated: OpenApiSupportedVersion.cs" -ForegroundColor Green

# Step 4: Update Resource.resx and Resource.Designer.cs
Write-Host "--- Core: Resources ---" -ForegroundColor Magenta

$resxFile = Join-Path $repoRoot "src\Core\ApiClientCodeGen.Core\Resource.resx"
$resxContent = Get-Content $resxFile -Raw
$resxContent = $resxContent.Replace($OldVersion, $NewVersion)
$resxContent = $resxContent -replace '(<data name="OpenApiGenerator_MD5" xml:space="preserve">\s*<value>)[^<]+(</value>)', "`${1}$MD5`${2}"
$resxContent = $resxContent -replace '(<data name="OpenApiGenerator_SHA1" xml:space="preserve">\s*<value>)[^<]+(</value>)', "`${1}$SHA1`${2}"
Set-Content $resxFile $resxContent -NoNewline
Write-Host "  Updated: Resource.resx" -ForegroundColor Green

$designerFile = Join-Path $repoRoot "src\Core\ApiClientCodeGen.Core\Resource.Designer.cs"
$designerContent = Get-Content $designerFile -Raw
$designerContent = $designerContent.Replace($OldVersion, $NewVersion)
# Update hash comments in designer (format: "Looks up a localized string similar to <hash>.")
# Match the MD5 hash (32 hex chars) in the OpenApiGenerator_MD5 context
$designerContent = $designerContent -replace '(Looks up a localized string similar to )[0-9a-f]{32}(\.)', "`${1}$MD5`${2}"
# Match the SHA1 hash (40 hex chars) - but only for OpenApiGenerator, not Legacy
$designerContent = $designerContent -replace '(Looks up a localized string similar to )[0-9a-f]{40}(\.)', "`${1}$SHA1`${2}"
Set-Content $designerFile $designerContent -NoNewline
Write-Host "  Updated: Resource.Designer.cs" -ForegroundColor Green

# Step 5: Update CLI Program.cs
Write-Host "--- CLI ---" -ForegroundColor Magenta
Update-VersionInFile "src\CLI\ApiClientCodeGen.CLI\Program.cs"

# Step 6: Update Tests
Write-Host "--- Tests ---" -ForegroundColor Magenta
$testFile = Join-Path $repoRoot "src\Core\ApiClientCodeGen.Core.Tests\Options\OpenApiGenerator\OpenApiVersionExtensionsTests.cs"
$testContent = Get-Content $testFile -Raw

# IsLatest: old latest becomes false, add new as true
$testContent = $testContent.Replace(
    "[InlineData(OpenApiSupportedVersion.$oldEnumName, true)]   // Latest version",
    "[InlineData(OpenApiSupportedVersion.$newEnumName, true)]   // Latest version`r`n    [InlineData(OpenApiSupportedVersion.$oldEnumName, false)]  // Not latest version"
)

# IsOlderThanLatest: old "equal to latest" becomes new, add old as "older"
$testContent = $testContent.Replace(
    "[InlineData(OpenApiSupportedVersion.$oldEnumName, false)]  // Equal to latest version",
    "[InlineData(OpenApiSupportedVersion.$newEnumName, false)]  // Equal to latest version"
)
# Add old version as older-than-latest (insert before the first existing "Older than latest" line)
if (-not $testContent.Contains("$oldEnumName, true)]   // Older than latest version")) {
    # Find the first "Older than latest version" line and insert old version before it
    $olderPattern = "(\[InlineData\(OpenApiSupportedVersion\.)"
    $olderLines = $testContent -split "`r?`n" | Where-Object { $_ -match "Older than latest version" }
    if ($olderLines.Count -gt 0) {
        $firstOlderLine = $olderLines[0].Trim()
        $testContent = $testContent.Replace(
            "    $firstOlderLine",
            "    [InlineData(OpenApiSupportedVersion.$oldEnumName, true)]   // Older than latest version`r`n    $firstOlderLine"
        )
    }
}

# ResolveVersion: Latest maps to new, add new entry
$testContent = $testContent.Replace(
    "[InlineData(OpenApiSupportedVersion.Latest, OpenApiSupportedVersion.$oldEnumName)]",
    "[InlineData(OpenApiSupportedVersion.Latest, OpenApiSupportedVersion.$newEnumName)]"
)
# Add new version resolve entry before old
if (-not $testContent.Contains("$newEnumName, OpenApiSupportedVersion.$newEnumName")) {
    $testContent = $testContent.Replace(
        "[InlineData(OpenApiSupportedVersion.$oldEnumName, OpenApiSupportedVersion.$oldEnumName)]",
        "[InlineData(OpenApiSupportedVersion.$newEnumName, OpenApiSupportedVersion.$newEnumName)]`r`n    [InlineData(OpenApiSupportedVersion.$oldEnumName, OpenApiSupportedVersion.$oldEnumName)]"
    )
}

Set-Content $testFile $testContent -NoNewline
Write-Host "  Updated: OpenApiVersionExtensionsTests.cs" -ForegroundColor Green

# Step 7: Update Documentation
Write-Host "--- Documentation ---" -ForegroundColor Magenta
$docFiles = @(
    "README.md",
    "docs\CLI.md",
    "docs\Marketplace.md",
    "docs\Marketplace2022.md",
    "docs\VisualStudioForMac.md",
    "docs\website\cli.html",
    "docs\website\features.html",
    "docs\website\index.html",
    "java\README.md"
)
foreach ($f in $docFiles) {
    Update-VersionInFile $f
}

# Step 8: Update IDE Extensions
Write-Host "--- IDE Extensions ---" -ForegroundColor Magenta
$extFiles = @(
    "src\VSCode\package.json",
    "src\VSIX\ApiClientCodeGen.VSIX.Dev17\VSCommandTable.vsct",
    "src\VSIX\ApiClientCodeGen.VSIX\VSCommandTable.vsct",
    "src\VSIX\ApiClientCodeGen.VSIX.Extensibility\.vsextension\string-resources.json",
    "src\VSMac\ApiClientCodeGen.VSMac\Properties\Manifest.addin.xml",
    "src\IntelliJ\src\main\resources\META-INF\plugin.xml"
)
foreach ($f in $extFiles) {
    Update-VersionInFile $f
}

# Step 9: Build and Test
if (-not $SkipBuild) {
    Write-Host ""
    Write-Host "--- Building ---" -ForegroundColor Magenta
    Push-Location (Join-Path $repoRoot "src")
    try {
        dotnet restore Rapicgen.slnx
        if ($LASTEXITCODE -ne 0) { Write-Error "Restore failed!"; exit 1 }

        dotnet build Rapicgen.slnx
        if ($LASTEXITCODE -ne 0) { Write-Error "Build failed!"; exit 1 }

        Write-Host "--- Running Tests ---" -ForegroundColor Magenta
        dotnet test Core\ApiClientCodeGen.Core.Tests\ApiClientCodeGen.Core.Tests.csproj --no-build -f net8.0 --filter "FullyQualifiedName~OpenApiVersionExtensionsTests"
        if ($LASTEXITCODE -ne 0) { Write-Error "Tests failed!"; exit 1 }
    }
    finally {
        Pop-Location
    }
}

# Step 10: Commit
if (-not $SkipCommit) {
    Write-Host ""
    Write-Host "--- Committing ---" -ForegroundColor Magenta
    Push-Location $repoRoot
    try {
        git add src/Core/ApiClientCodeGen.Core/Installer/OpenApiGeneratorVersions.cs `
              src/Core/ApiClientCodeGen.Core/Options/OpenApiGenerator/OpenApiSupportedVersion.cs `
              src/Core/ApiClientCodeGen.Core/Resource.resx `
              src/Core/ApiClientCodeGen.Core/Resource.Designer.cs
        git commit -m "Add OpenAPI Generator v$NewVersion version entry and hashes`n`nCo-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"

        git add src/CLI/ApiClientCodeGen.CLI/Program.cs `
              src/Core/ApiClientCodeGen.Core.Tests/Options/OpenApiGenerator/OpenApiVersionExtensionsTests.cs
        git commit -m "Update CLI description and tests for OpenAPI Generator v$NewVersion`n`nCo-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"

        git add README.md docs/ java/README.md
        git commit -m "Update documentation for OpenAPI Generator v$NewVersion`n`nCo-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"

        git add src/VSCode/ src/VSIX/ src/VSMac/ src/IntelliJ/
        git commit -m "Update IDE extensions for OpenAPI Generator v$NewVersion`n`nCo-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
    }
    finally {
        Pop-Location
    }
}

Write-Host ""
Write-Host "=== Done! OpenAPI Generator updated to v$NewVersion ===" -ForegroundColor Green
Write-Host ""
Write-Host "Version Details:" -ForegroundColor Cyan
Write-Host "  Version: $NewVersion"
Write-Host "  SHA1:    $SHA1"
Write-Host "  MD5:     $MD5"
Write-Host ""
