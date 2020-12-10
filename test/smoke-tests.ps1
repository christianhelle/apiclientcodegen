Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
function ThrowOnNativeFailure {
    if (-not $?)
    {
        throw 'Native Failure'
    }
}

Invoke-WebRequest -Uri https://dotnet.microsoft.com/download/dotnet-core/scripts/v1/dotnet-install.ps1 -OutFile ./dotnet-install.ps1
./dotnet-install.ps1 -Version 2.1.811
./dotnet-install.ps1 -Version 3.1.404
./dotnet-install.ps1 -Version 5.0.100

############################
## OpenAPI Spec v2 (JSON) ##
############################

Remove-Item ./**/Output.cs

Write-Host "`r`nDownload Swagger Petstore V2 spec (JSON)`r`n"
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json

Write-Host "`r`nTesting AutoRest Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- autorest ./Swagger.json GeneratedCode ./GeneratedCode/AutoRest/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetStandard20.csproj; ThrowOnNativeFailure

Remove-Item Swagger.json
Remove-Item ./**/*Output.cs

############################
## OpenAPI Spec v2 (YAML) ##
############################

Write-Host "`r`nDownload Swagger Petstore V2 spec (YAML)`r`n"
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.yaml -OutFile Swagger.yaml

Write-Host "`r`nTesting AutoRest Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- autorest ./Swagger.yaml GeneratedCode ./GeneratedCode/AutoRest/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/AutoRest/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.yaml GeneratedCode ./GeneratedCode/NSwag/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.yaml GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.yaml GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetStandard20.csproj; ThrowOnNativeFailure

Remove-Item Swagger.yaml
Remove-Item ./**/*Output.cs

############################
## OpenAPI Spec v3 (JSON) ##
############################

Write-Host "`r`nDownload Swagger Petstore V3 spec (JSON)`r`n"
Invoke-WebRequest -Uri https://petstore3.swagger.io/api/v3/openapi.json -OutFile Swagger.json

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.json GeneratedCode ./GeneratedCode/NSwag/Output.cs --no-logging; ThrowOnNativeFailure; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.json GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.json GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetStandard20.csproj; ThrowOnNativeFailure

Remove-Item Swagger.json
Remove-Item ./**/*Output.cs

############################
## OpenAPI Spec v3 (YAML) ##
############################

Write-Host "`r`nDownload Swagger Petstore V3 spec (YAML)`r`n"
Invoke-WebRequest -Uri https://petstore3.swagger.io/api/v3/openapi.yaml -OutFile Swagger.yaml

Write-Host "`r`nTesting NSwag Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- nswag ./Swagger.yaml GeneratedCode ./GeneratedCode/NSwag/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/NSwag/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Swagger Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- swagger ./Swagger.yaml GeneratedCode ./GeneratedCode/SwaggerCodegen/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/SwaggerCodegen/NetStandard20.csproj; ThrowOnNativeFailure

Write-Host "`r`nTesting Open API Code Generation`r`n"
dotnet run --project ../src/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- openapi ./Swagger.yaml GeneratedCode ./GeneratedCode/OpenApiGenerator/Output.cs --no-logging; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore21.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetCore31.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net5.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/Net472.csproj; ThrowOnNativeFailure
dotnet build ./GeneratedCode/OpenApiGenerator/NetStandard20.csproj; ThrowOnNativeFailure

Remove-Item Swagger.yaml
Remove-Item ./**/*Output.cs

Write-Host "`r`n"