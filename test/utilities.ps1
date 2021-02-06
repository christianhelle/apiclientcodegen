Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues["*:ErrorAction"]="Stop"

function ThrowOnNativeFailure {
    if (-not $?)
    {
        throw "Native Failure"
    }
}

function Install-DotNetRuntimes {
    Write-Host "`r`nUpdate/Install .NET Runtimes`r`n"
    curl -sSL https://dotnet.microsoft.com/download/dotnet-core/scripts/v1/dotnet-install.ps1 -o ./dotnet-install.ps1
    ./dotnet-install.ps1 -Version 2.1.811
    ./dotnet-install.ps1 -Version 3.1.404
    ./dotnet-install.ps1 -Version 5.0.100
}

function Install-Rapicgen {
    Write-Host "`r`nUpdate/Install .NET Core Tool`r`n"
    dotnet tool update --global rapicgen    
}

function Download-SwaggerPetstore {

    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("v2", "v3")]
        [string]
        $Version,

        [Parameter(Mandatory=$true)]
        [ValidateSet("json", "yaml")]
        [string]
        $Format
    )

    Write-Host "`r`nDownload Swagger Petstore $Version spec ($Format)`r`n"

    if ($Version -eq "v2") {
        Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.$Format -OutFile Swagger.$Format
    }

    if ($Version -eq "v3") {
        Invoke-WebRequest -Uri https://petstore3.swagger.io/api/v3/openapi.$Format -OutFile Swagger.$Format
    }
}

function Build-GeneratedCode {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("AutoRest", "NSwag", "SwaggerCodegen", "OpenApiGenerator")]
        [string]
        $ToolName,
        
        [Parameter(Mandatory=$false)]
        [bool]
        $Parallel = $false
    )

    if ($Parallel) {
        $argumentsList = @(
            "build ./GeneratedCode/$ToolName/NetCore21.csproj",
            "build ./GeneratedCode/$ToolName/NetCore31.csproj",
            "build ./GeneratedCode/$ToolName/Net5.csproj",
            "build ./GeneratedCode/$ToolName/Net472.csproj",
            "build ./GeneratedCode/$ToolName/NetStandard20.csproj"
        )
        
        $processes = ($argumentsList | ForEach-Object {
            Start-Process "dotnet" -Args $PSItem -NoNewWindow -PassThru
        })
        $processes | Wait-Process
    }
    else {
        dotnet build ./GeneratedCode/$ToolName/NetCore21.csproj; ThrowOnNativeFailure
        dotnet build ./GeneratedCode/$ToolName/NetCore31.csproj; ThrowOnNativeFailure
        dotnet build ./GeneratedCode/$ToolName/Net5.csproj; ThrowOnNativeFailure
        dotnet build ./GeneratedCode/$ToolName/Net472.csproj; ThrowOnNativeFailure
        dotnet build ./GeneratedCode/$ToolName/NetStandard20.csproj; ThrowOnNativeFailure
    }
}

function Generate-Code {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("AutoRest", "NSwag", "SwaggerCodegen", "OpenApiGenerator")]
        [string]
        $ToolName,

        [Parameter(Mandatory=$true)]
        [ValidateSet("json", "yaml")]
        [string]
        $Format,

        [Parameter(Mandatory=$true)]
        [ValidateSet("dotnet-run", "rapicgen")]
        [string]
        $Method
    )

    switch ($ToolName) {
        "SwaggerCodegen" {
            $command = "swagger"
        }
        "OpenApiGenerator" { 
            $command = "openapi"
        }
        Default {
            $command = $ToolName.ToLower()
        }
    }

    $project = "../src/CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj"
    $arguments = "$command ./Swagger.$Format GeneratedCode ./GeneratedCode/$ToolName/Output.cs --no-logging"

    switch ($Method) {
        "dotnet-run" {
            Start-Process "dotnet" -Args "run --project $project -- $arguments" -Wait -NoNewWindow
            Break
        }
        "rapicgen" {
            Start-Process "rapicgen" -Args $arguments -Wait -NoNewWindow
            Break
        }
    }
}

function Generate-CodeThenBuild {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("AutoRest", "NSwag", "SwaggerCodegen", "OpenApiGenerator")]
        [string]
        $ToolName,

        [Parameter(Mandatory=$true)]
        [ValidateSet("json", "yaml")]
        [string]
        $Format,

        [Parameter(Mandatory=$true)]
        [ValidateSet("dotnet-run", "rapicgen")]
        [string]
        $Method,
        
        [Parameter(Mandatory=$false)]
        [bool]
        $Parallel = $false
    )

    Write-Host "`r`n$ToolName - Generate Code then Build`r`n"
    Generate-Code -ToolName $ToolName -Format $Format -Method $Method
    Build-GeneratedCode -ToolName $ToolName -Parallel $Parallel
}