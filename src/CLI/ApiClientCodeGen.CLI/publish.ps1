Write-Host "`r`nBuilding for Win-x64`r`n"
dotnet publish -c Release -r win-x64 --self-contained true

Write-Host "`r`nBuilding for OSX-x64`r`n"
dotnet publish -c Release -r osx-x64 --self-contained true

Write-Host "`r`nBuilding for Linux-x64`r`n"
dotnet publish -c Release -r linux-x64 --self-contained true

Write-Host "`r`n"