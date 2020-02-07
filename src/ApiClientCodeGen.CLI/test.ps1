Write-Host "`r`nTesting AutoRest Code Generation`r`n"
dotnet run -- autorest ../Swagger.json GeneratedCode ../AutoRestOutput.cs

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run -- nswag ../Swagger.json GeneratedCode ../NSwagOutput.cs

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run -- swagger ../Swagger.json GeneratedCode ../SwaggerOutput.cs

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run -- openapi ../Swagger.json GeneratedCode ../OpenApiOutput.cs

Write-Host "`r`n"