Param()
$ErrorActionPreference = 'Stop'

Write-Host "Building IntelliJ plugin" -ForegroundColor Cyan

# Attempt to locate a Java 21+ installation (needed by IntelliJ Platform 2025.1)
function Use-Jdk21 {
	$candidatePaths = @()
	# Bundled repo path (put full JDK here if only JRE exists)
	$candidatePaths += (Resolve-Path -ErrorAction SilentlyContinue "$PSScriptRoot/../java/jdk-21.0.3+9-jre-windows" | ForEach-Object { $_.Path })
	# Common environment variables
	if ($env:JAVA_HOME) { $candidatePaths += $env:JAVA_HOME }
	if ($env:JDK_HOME) { $candidatePaths += $env:JDK_HOME }
	# Java installations directory (JetBrains toolboxes etc.)
	$candidatePaths += (Get-ChildItem "$Env:ProgramFiles/Java" -ErrorAction SilentlyContinue | Where-Object { $_.Name -match '21' } | Select-Object -ExpandProperty FullName)

	foreach ($p in $candidatePaths | Where-Object { $_ } | Get-Unique) {
		$javaExe = Join-Path $p 'bin/java.exe'
		$javacExe = Join-Path $p 'bin/javac.exe'
		if ((Test-Path $javaExe) -and (Test-Path $javacExe)) {
			Write-Host "Using JDK: $p" -ForegroundColor Green
			$env:JAVA_HOME = $p
			$env:PATH = "$p/bin;" + $env:PATH
			return $true
		}
	}
	return $false
}

if (-not (Use-Jdk21)) {
	Write-Warning "Attempting to download Temurin JDK 21 (HotSpot) ..."
	$downloadDir = Join-Path $PSScriptRoot '..' | Join-Path -ChildPath 'java'
	if (-not (Test-Path $downloadDir)) { New-Item -ItemType Directory -Force -Path $downloadDir | Out-Null }
	$zipPath = Join-Path $downloadDir 'jdk21.zip'
	$extractPath = Join-Path $downloadDir 'jdk-21'
	if (-not (Test-Path $extractPath)) {
		try {
			$jdkUrl = 'https://github.com/adoptium/temurin21-binaries/releases/download/jdk-21.0.3%2B9/OpenJDK21U-jdk_x64_windows_hotspot_21.0.3_9.zip'
			Write-Host "Downloading $jdkUrl" -ForegroundColor Cyan
			Invoke-WebRequest -Uri $jdkUrl -OutFile $zipPath -UseBasicParsing
			Write-Host "Extracting JDK..." -ForegroundColor Cyan
			Add-Type -AssemblyName System.IO.Compression.FileSystem
			[System.IO.Compression.ZipFile]::ExtractToDirectory($zipPath, $extractPath)
			# Find extracted folder (it may contain a single top folder)
			$folder = Get-ChildItem $extractPath | Where-Object { $_.PSIsContainer } | Select-Object -First 1
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
	if (-not (Get-Command javac -ErrorAction SilentlyContinue)) {
		Write-Error "JDK 21 still not available. Install manually and rerun."
		exit 1
	}
}

if (-not (Get-Command java -ErrorAction SilentlyContinue)) {
	Write-Error "Java executable not found after attempting to configure JDK 21. Aborting build."
	exit 1
}

./gradlew buildPlugin
