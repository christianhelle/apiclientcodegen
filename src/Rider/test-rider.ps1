# Test JetBrains Rider plugin (manual launch)
param(
    [string]$RiderPath = $null,
    [string]$RiderVersion = "2025.1"
)

# Try to find Rider executable if not specified
if (-not $RiderPath) {
    if ($IsWindows) {
        $RiderPath = Get-ChildItem "C:/Program Files/JetBrains/JetBrains Rider $RiderVersion*/bin/rider64.exe" -ErrorAction SilentlyContinue | Select-Object -First 1 | ForEach-Object { $_.FullName }
    } elseif ($IsMacOS) {
        $RiderPath = "/Applications/JetBrains Rider.app/Contents/MacOS/rider"
    } else {
        $RiderPath = "/usr/share/jetbrains/rider/bin/rider.sh"
        if (-not (Test-Path $RiderPath)) {
            $RiderPath = "/opt/jetbrains/rider/bin/rider.sh"
        }
    }
}

if (-not (Test-Path $RiderPath)) {
    Write-Error "Could not find Rider executable. Please specify with -RiderPath."
    exit 1
}

Write-Host "Launching Rider: $RiderPath"
& $RiderPath
