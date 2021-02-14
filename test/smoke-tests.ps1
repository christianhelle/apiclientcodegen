param (
    [Parameter(Mandatory=$false)]
    [bool]
    $Parallel = $false
)

. .\utilities.ps1
# Install-DotNetRuntimes

Measure-Command { RunTests -Method "dotnet-run" -Parallel $Parallel }
Write-Host "`r`n"