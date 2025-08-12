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
		if (Test-Path $javaExe -and Test-Path $javacExe) {
			Write-Host "Using JDK: $p" -ForegroundColor Green
			$env:JAVA_HOME = $p
			$env:PATH = "$p/bin;" + $env:PATH
			return
		}
	}
	Write-Warning "No Java 21 JDK with javac found. Install JDK 21 or place it at src/java/jdk-21.0.3+9-jre-windows (must be full JDK, not only JRE)."
}

Use-Jdk21

if (-not (Get-Command java -ErrorAction SilentlyContinue)) {
	Write-Error "Java executable not found after attempting to configure JDK 21. Aborting build."
	exit 1
}

./gradlew buildPlugin
