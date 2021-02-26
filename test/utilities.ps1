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

function Prepare-SwaggerPetstore {

    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("V2", "V3")]
        [string]
        $Version,

        [Parameter(Mandatory=$true)]
        [ValidateSet("json", "yaml")]
        [string]
        $Format,

        [Parameter(Mandatory=$false)]
        [switch]
        $Download
    )

    if ($Download) {
        Write-Host "`r`nDownload Swagger Petstore $Version spec ($Format)`r`n"

        if ($Version -eq "V2") {
            Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.$Format -OutFile Swagger.$Format
        }

        if ($Version -eq "V3") {
            Invoke-WebRequest -Uri https://petstore3.swagger.io/api/v3/openapi.$Format -OutFile Swagger.$Format
        }
    } else {
        Copy-Item ./OpenAPI/$Version/Swagger.$Format ./Swagger.$Format
    }
}

function Build-GeneratedCode {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("All", "AutoRest-V2", "AutoRest-V3", "NSwag", "SwaggerCodegen", "OpenApiGenerator")]
        [string]
        $ToolName,

        [Parameter(Mandatory=$true)]
        [ValidateSet("V2", "V3")]
        [string]
        $Version,
        
        [Parameter(Mandatory=$false)]
        [bool]
        $Parallel = $true
    )

    if ($Version -eq "V2") {
        $tools = @("AutoRest-V2", "NSwag", "SwaggerCodegen", "OpenApiGenerator")
    } else {
        $tools = @("AutoRest-V3", "NSwag", "SwaggerCodegen", "OpenApiGenerator")
    }  

    if ($Parallel) {
        $argumentsList = @()
        if ($ToolName -eq "All") {
            $tools | ForEach-Object {
                $argumentsList += "build ./GeneratedCode/$_/NetStandard20/NetStandard20.csproj"
                $argumentsList += "build ./GeneratedCode/$_/NetCore21/NetCore21.csproj"
                $argumentsList += "build ./GeneratedCode/$_/NetCore31/NetCore31.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net5/Net5.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net48/Net48.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net472/Net472.csproj"

                if ($_ -notcontains "AutoRest-V3") {
                    $argumentsList += "build ./GeneratedCode/$_/Net462/Net462.csproj"
                    $argumentsList += "build ./GeneratedCode/$_/Net452/Net452.csproj"
                }
            }
        } else {
            $argumentsList = @(
                "build ./GeneratedCode/$ToolName/NetStandard20/NetStandard20.csproj",
                "build ./GeneratedCode/$ToolName/NetCore21/NetCore21.csproj",
                "build ./GeneratedCode/$ToolName/NetCore31/NetCore31.csproj",
                "build ./GeneratedCode/$ToolName/Net5/Net5.csproj",
                "build ./GeneratedCode/$ToolName/Net48/Net48.csproj",
                "build ./GeneratedCode/$ToolName/Net472/Net472.csproj"
            )

            if ($_ -notcontains "AutoRest-V3") {
                $argumentsList += "build ./GeneratedCode/$_/Net462/Net462.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net452/Net452.csproj"
            }
        }
        
        $processes = ($argumentsList | ForEach-Object {
            Start-Process "dotnet" -Args $PSItem -NoNewWindow -PassThru
        })
        $processes | Wait-Process
        $processes | ForEach-Object {
            if ($_.ExitCode -ne 0) {
                throw "Build Failed!"
            }
        }
    } else {
        if ($ToolName -eq "All") {
            $tools | ForEach-Object {
                Write-Host "`r`nBuilding $_`r`n"
                dotnet build ./GeneratedCode/$_/NetStandard20/NetStandard20.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/NetCore21/NetCore21.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/NetCore31/NetCore31.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net5/Net5.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net48/Net48.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net472/Net472.csproj; ThrowOnNativeFailure

                if ($_ -notcontains "AutoRest-V3") {
                    dotnet build ./GeneratedCode/$_/Net462/Net462.csproj; ThrowOnNativeFailure
                    dotnet build ./GeneratedCode/$_/Net452/Net452.csproj; ThrowOnNativeFailure
                }
            }
        } else {
            Write-Host "`r`nBuilding $ToolName`r`n"
            dotnet build ./GeneratedCode/$ToolName/NetStandard20/NetStandard20.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/NetCore21/NetCore21.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/NetCore31/NetCore31.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net5/Net5.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net48/Net48.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net472/Net472.csproj; ThrowOnNativeFailure

            if ($_ -notcontains "AutoRest-V3") {
                dotnet build ./GeneratedCode/$ToolName/Net462/Net462.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$ToolName/Net452/Net452.csproj; ThrowOnNativeFailure
            }
        }
    }
}

function Generate-Code {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("AutoRest-V2", "AutoRest-V3", "NSwag", "SwaggerCodegen", "OpenApiGenerator")]
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

        [Parameter(Mandatory=$true)]
        [ValidateSet("V2", "V3")]
        [string]
        $Version
    )

    switch ($ToolName) {
        "SwaggerCodegen" {
            $command = "swagger"
        }
        "OpenApiGenerator" { 
            $command = "openapi"
        }
        "AutoRest-V2" {
            $command = "autorest"
        } 
        "AutoRest-V3" {
            $command = "autorest"
        }
        "NSwag" {
            $command = "nswag"
        }
    }

    if ($Version -eq "V3" -and $ToolName -eq "AutoRest-V2") {
        $ToolName = "AutoRest-V3"
    }

    $project = "../src/CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj"
    $arguments = "--verbose $command ./Swagger.$Format GeneratedCode ./GeneratedCode/$ToolName/Output.cs --no-logging"

    switch ($Method) {
        "dotnet-run" {
            $process = Start-Process "dotnet" -Args "run --project $project -- $arguments" -Wait -NoNewWindow -PassThru
            Break
        }
        "rapicgen" {
            $process = Start-Process "rapicgen" -Args $arguments -Wait -NoNewWindow -PassThru
            Break
        }
    }

    Write-Host "`r`n$ToolName - Code Generation Completed`r`n"

    if ($process.ExitCode -ne 0) {
        throw "$_ exited with status code $($process.ExitCode)"
    }

    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net5/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net48/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net472/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net462/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net452/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/NetCore21/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/NetCore31/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/NetStandard20/Output.cs" -Force
    Remove-Item "GeneratedCode/$ToolName/Output.cs" -Force
}

function Generate-CodeParallel {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("json", "yaml")]
        [string]
        $Format,

        [Parameter(Mandatory=$true)]
        [ValidateSet("dotnet-run", "rapicgen")]
        [string]
        $Method
    )

    $processes = @()
    "AutoRest-V2", "NSwag", "SwaggerCodegen", "OpenApiGenerator" | ForEach-Object {
        switch ($_) {
            "SwaggerCodegen" {
                $command = "swagger"
            }
            "OpenApiGenerator" { 
                $command = "openapi"
            }
            Default {
                $command = $_.ToLower()
            }
        }

        $project = "../src/CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj"
        $arguments = "--verbose $command ./Swagger.$Format GeneratedCode ./GeneratedCode/$_/Output.cs --no-logging"

        switch ($Method) {
            "dotnet-run" {
                $processes += Start-Process "dotnet" -Args "run --project $project -- $arguments" -NoNewWindow -PassThru
                Break
            }
            "rapicgen" {
                $processes += Start-Process "rapicgen" -Args $arguments -NoNewWindow -PassThru
                Break
            }
        }
    }

    foreach ($process in $processes) {
        $process.WaitForExit()
    }

    "AutoRest-V2", "NSwag", "SwaggerCodegen", "OpenApiGenerator" | ForEach-Object {
        if (Test-Path "GeneratedCode/$_/Output.cs" -PathType Leaf) {
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net5/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net48/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net472/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net462/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net452/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/NetCore21/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/NetCore31/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/NetStandard20/Output.cs" -Force
            Remove-Item "GeneratedCode/$_/Output.cs" -Force
        } else {            
            throw "$_ code generation failed"
        }
    }
}

function Generate-CodeThenBuild {
    
    param (
        [Parameter(Mandatory=$false)]
        [ValidateSet("All", "AutoRest-V2", "AutoRest-V3", "NSwag", "SwaggerCodegen", "OpenApiGenerator")]
        [string]
        $ToolName = "All",

        [Parameter(Mandatory=$true)]
        [ValidateSet("V2", "V3")]
        [string]
        $Version,

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

    if ($ToolName -eq "All") {
        if ($Parallel) {
            Generate-CodeParallel -Format $Format -Method $Method
            Build-GeneratedCode -ToolName $ToolName
        } else {
            if ($Version -eq "V2") {
                $tools = @("AutoRest-V2", "NSwag", "SwaggerCodegen", "OpenApiGenerator")
            } else {
                $tools = @("AutoRest-V3", "NSwag", "SwaggerCodegen", "OpenApiGenerator")
            }  
            $tools | ForEach-Object {
                Generate-CodeThenBuild `
                    -ToolName $_ `
                    -Format $Format `
                    -Method $Method `
                    -Parallel $Parallel `
                    -Version $Version 
            }
        }
    } else {
        Write-Host "`r`n$ToolName - Generate Code then Build`r`n"
        Generate-Code -ToolName $ToolName -Format $Format -Method $Method -Version $Version
        Build-GeneratedCode -Version $Version -ToolName $ToolName
    }
}

function RunTests {

    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("dotnet-run", "rapicgen")]
        [string]
        $Method,
        
        [Parameter(Mandatory=$false)]
        [bool]
        $Parallel = $false
    )

    "V2", "V3" | ForEach-Object {
        $version = $_
        "json", "yaml" | ForEach-Object {
            $format = $_
            Remove-Item ./**/*Output.cs -Force
            Prepare-SwaggerPetstore -Version $version -Format $format
            Generate-CodeThenBuild -Version $version -Format $format -Method $Method -Parallel $Parallel
            Remove-Item Swagger.* -Force
            Remove-Item ./**/*Output.cs -Force
        }
    }
}