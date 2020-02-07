Write-Host "`r`nTesting AutoRest Code Generation`r`n"
dotnet run -- autorest ../Swagger.json

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run -- nswag ../Swagger.json

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run -- swagger ../Swagger.json

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run -- openapi ../Swagger.json

Write-Host "`r`n"