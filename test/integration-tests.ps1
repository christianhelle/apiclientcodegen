param (
    [Parameter(Mandatory=$false)]
    [bool]
    $Parallel = $false
)

. .\utilities.ps1
# Install-DotNetRuntimes
Install-Rapicgen

Measure-Command { RunTests -Method "rapicgen" -Parallel $Parallel }
Write-Host "`r`n"