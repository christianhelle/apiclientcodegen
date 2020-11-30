$ErrorActionPreference = "Stop"

Remove-Item ./**/Output.cs

Write-Host "`r`nDownload Swagger Petstore V2 spec (JSON)`r`n"
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json

Write-Host "`r`nTesting AutoRest Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- autorest ./Swagger.json GeneratedCode ./GeneratedCode/AutoRest/Output.cs
dotnet build ./GeneratedCode/AutoRest/Project.csproj

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/Output.cs
dotnet build ./GeneratedCode/NSwag/Project.csproj

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs
dotnet build ./GeneratedCode/SwaggerCodegen/Project.csproj

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs
dotnet build ./GeneratedCode/OpenApiGenerator/Project.csproj

Remove-Item Swagger.json
Remove-Item ./**/*Output.cs

Write-Host "`r`nDownload Swagger Petstore V3 spec (JSON)`r`n"
Invoke-WebRequest -Uri https://petstore3.swagger.io/api/v3/openapi.json -OutFile Swagger.json

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/Output.cs
dotnet build ./GeneratedCode/NSwag/Project.csproj

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs
dotnet build ./GeneratedCode/SwaggerCodegen/Project.csproj

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs
dotnet build ./GeneratedCode/OpenApiGenerator/Project.csproj

Remove-Item Swagger.json
Remove-Item ./**/*Output.cs

Write-Host "`r`nDownload Swagger Petstore V2 spec (YAML)`r`n"
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.yaml -OutFile Swagger.json

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/Output.cs
dotnet build ./GeneratedCode/NSwag/Project.csproj

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs
dotnet build ./GeneratedCode/SwaggerCodegen/Project.csproj

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs
dotnet build ./GeneratedCode/OpenApiGenerator/Project.csproj

Remove-Item Swagger.json
Remove-Item ./**/*Output.cs

Write-Host "`r`nDownload Swagger Petstore V3 spec (YAML)`r`n"
Invoke-WebRequest -Uri https://petstore3.swagger.io/api/v3/openapi.yaml -OutFile Swagger.json

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/Output.cs
dotnet build ./GeneratedCode/NSwag/Project.csproj

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs
dotnet build ./GeneratedCode/SwaggerCodegen/Project.csproj

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs
dotnet build ./GeneratedCode/OpenApiGenerator/Project.csproj

Remove-Item Swagger.json
Remove-Item ./**/*Output.cs

Write-Host "`r`n"