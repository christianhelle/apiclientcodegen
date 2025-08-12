Param()
$ErrorActionPreference = 'Stop'

Write-Host "Building IntelliJ plugin" -ForegroundColor Cyan

# Attempt to locate a Java 21+ installation (needed by IntelliJ Platform 2025.1)
function Get-JavaMajorVersion([string]$javaHome) {
	try {
		$javaExe = Join-Path $javaHome 'bin/java.exe'
		if (-not (Test-Path $javaExe)) { return $null }
		$line = & $javaExe -version 2>&1 | Select-Object -First 1
		if ($line -match 'version "([0-9]+)') { return [int]$matches[1] }
	} catch { }
	return $null
}

function Use-Jdk21 {
	$candidatePaths = @()
	$candidatePaths += (Resolve-Path -ErrorAction SilentlyContinue "$PSScriptRoot/../java" | ForEach-Object { (Get-ChildItem $_.Path -Directory -ErrorAction SilentlyContinue) }) | ForEach-Object { $_.FullName }
	if ($env:JAVA_HOME) { $candidatePaths += $env:JAVA_HOME }
	if ($env:JDK_HOME) { $candidatePaths += $env:JDK_HOME }
	$candidatePaths += (Get-ChildItem "$Env:ProgramFiles/Java" -ErrorAction SilentlyContinue | Select-Object -ExpandProperty FullName)

	foreach ($p in ($candidatePaths | Where-Object { $_ } | Get-Unique)) {
		$javaExe = Join-Path $p 'bin/java.exe'
		$javacExe = Join-Path $p 'bin/javac.exe'
		if (Test-Path $javaExe -and Test-Path $javacExe) {
			$major = Get-JavaMajorVersion $p
			if ($major -ge 21) {
				Write-Host "Using JDK ${major}: $p" -ForegroundColor Green
				$env:JAVA_HOME = $p
				$env:PATH = "$p/bin;" + $env:PATH
				return $true
			}
		}
	}
	return $false
}

function Get-Jdk21 {
	Write-Warning "Attempting to download Temurin JDK 21 (HotSpot) ..."
	$downloadDir = Join-Path $PSScriptRoot '..' | Join-Path -ChildPath 'java'
	if (-not (Test-Path $downloadDir)) { New-Item -ItemType Directory -Force -Path $downloadDir | Out-Null }
	$zipPath = Join-Path $downloadDir 'jdk21.zip'
	$extractPath = Join-Path $downloadDir 'jdk21-extracted'
	try {
		$jdkUrl = 'https://github.com/adoptium/temurin21-binaries/releases/download/jdk-21.0.3%2B9/OpenJDK21U-jdk_x64_windows_hotspot_21.0.3_9.zip'
		Write-Host "Downloading $jdkUrl" -ForegroundColor Cyan
		Invoke-WebRequest -Uri $jdkUrl -OutFile $zipPath -UseBasicParsing
		Write-Host "Extracting JDK..." -ForegroundColor Cyan
		Add-Type -AssemblyName System.IO.Compression.FileSystem
		if (Test-Path $extractPath) { Remove-Item -Recurse -Force $extractPath }
		New-Item -ItemType Directory -Force -Path $extractPath | Out-Null
		[System.IO.Compression.ZipFile]::ExtractToDirectory($zipPath, $extractPath)
		$folder = Get-ChildItem $extractPath | Where-Object { $_.PSIsContainer -and (Test-Path (Join-Path $_.FullName 'bin/javac.exe')) } | Select-Object -First 1
		if ($folder) {
			$env:JAVA_HOME = $folder.FullName
			$env:PATH = "$($env:JAVA_HOME)/bin;" + $env:PATH
			Write-Host "Using downloaded JDK: $($env:JAVA_HOME)" -ForegroundColor Green
		}
	}
	catch {
		Write-Warning "Automatic JDK download failed: $($_.Exception.Message)"
	}
}

if (-not (Use-Jdk21)) { Get-Jdk21 }
if (-not (Use-Jdk21)) { Write-Error "JDK 21 not available. Aborting."; exit 1 }

# Ensure Gradle sees the JDK
$env:ORG_GRADLE_PROJECT_orgGradleJavaHome = $env:JAVA_HOME

if (-not (Get-Command java -ErrorAction SilentlyContinue)) {
	Write-Error "Java executable not found after attempting to configure JDK 21. Aborting build."
	exit 1
}

./gradlew buildPlugin
