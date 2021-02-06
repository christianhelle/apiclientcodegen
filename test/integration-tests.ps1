$ErrorActionPreference = "Stop"

Write-Host "`r`nInstall .NET Core Tool`r`n"
dotnet tool install --global rapicgen

Write-Host "`r`nDownload Swagger Petstore spec`r`n"
curl -sSL https://petstore.swagger.io/v2/swagger.json -o Swagger.json

Write-Host "`r`nTesting AutoRest Code Generation`r`n"
rapicgen autorest ./Swagger.json GeneratedCode ./GeneratedCode/AutoRest/AutoRestOutput.cs --no-logging
dotnet build ./GeneratedCode/AutoRest/Project.csproj

Write-Host "`r`nTesting NSwag Code Generation`r`n"
rapicgen nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/NSwagOutput.cs --no-logging
dotnet build ./GeneratedCode/NSwag/Project.csproj

Write-Host "`r`nTesting Swagger Code Generation`r`n"
rapicgen swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/SwaggerOutput.cs --no-logging
dotnet build ./GeneratedCode/SwaggerCodegen/Project.csproj

Write-Host "`r`nTesting Open API Code Generation`r`n"
rapicgen openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/OpenApiOutput.cs --no-logging
dotnet build ./GeneratedCode/OpenApiGenerator/Project.csproj

Remove-Item Swagger.json
dotnet tool uninstall --global rapicgen

Write-Host "`r`n"