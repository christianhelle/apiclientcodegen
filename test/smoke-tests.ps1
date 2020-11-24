Write-Host "`r`nDownload Swagger Petstore spec`r`n"
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json

Write-Host "`r`nTesting AutoRest Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- autorest ./Swagger.json GeneratedCode ./GeneratedCode/AutoRest/AutoRestOutput.cs
dotnet build ./GeneratedCode/AutoRest/Project.csproj

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/NSwagOutput.cs
dotnet build ./GeneratedCode/NSwag/Project.csproj

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/SwaggerOutput.cs
dotnet build ./GeneratedCode/SwaggerCodegen/Project.csproj

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/OpenApiOutput.cs
dotnet build ./GeneratedCode/OpenApiGenerator/Project.csproj

Remove-Item Swagger.json

Write-Host "`r`n"