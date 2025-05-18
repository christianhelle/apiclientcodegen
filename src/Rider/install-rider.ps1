# Install JetBrains Rider plugin (cross-platform)
param(
    [string]$PluginZip = $null,
    [string]$RiderVersion = "2025.1"
)

# Detect OS and set Rider plugins directory
if ($IsWindows) {
    $riderConfig = Join-Path $env:APPDATA "JetBrains/Rider$RiderVersion"
} elseif ($IsMacOS) {
    $riderConfig = "$HOME/Library/Application Support/JetBrains/Rider$RiderVersion"
} else {
    $riderConfig = "$HOME/.config/JetBrains/Rider$RiderVersion"
}
$pluginsDir = Join-Path $riderConfig "plugins"

# Find plugin zip if not specified
if (-not $PluginZip) {
    $distDir = Join-Path $PSScriptRoot "build/distributions"
    $PluginZip = Get-ChildItem -Path $distDir -Filter "*.zip" | Select-Object -First 1 | ForEach-Object { $_.FullName }
}

if (-not (Test-Path $PluginZip)) {
    Write-Error "Plugin zip not found. Build the plugin first."
    exit 1
}

Write-Host "Installing plugin: $PluginZip to $pluginsDir"
New-Item -ItemType Directory -Force -Path $pluginsDir | Out-Null
Copy-Item $PluginZip $pluginsDir -Force
Write-Host "Plugin installed. Restart Rider to activate."
