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
    ./dotnet-install.ps1 -Version 6.0.100
    ./dotnet-install.ps1 -Version 7.0.100
    ./dotnet-install.ps1 -Version 8.0.100
    ./dotnet-install.ps1 -Version 9.0.100-preview.1.24101.2
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
        [ValidateSet("All", "NSwag", "OpenApiGenerator", "SwaggerCodegen", "AutoRest-V2", "AutoRest-V3", "Kiota", "Refitter")]
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
        $tools = @("AutoRest-V2", "NSwag", "Kiota", "Refitter", "SwaggerCodegen", "OpenApiGenerator")
    } else {
        $tools = @("AutoRest-V3", "NSwag", "Kiota", "Refitter", "SwaggerCodegen", "OpenApiGenerator")
    }  

    if ($Parallel) {
        $argumentsList = @()
        if ($ToolName -eq "All") {
            $tools | ForEach-Object {
                $argumentsList += "build ./GeneratedCode/$_/NetStandard20/NetStandard20.csproj"
                $argumentsList += "build ./GeneratedCode/$_/NetStandard21/NetStandard21.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net6/Net6.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net7/Net7.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net8/Net8.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net9/Net9.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net48/Net48.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net481/Net481.csproj"
                $argumentsList += "build ./GeneratedCode/$_/Net472/Net472.csproj"

                if ($_ -notcontains "AutoRest-V3") {
                    $argumentsList += "build ./GeneratedCode/$_/Net462/Net462.csproj"
                }
            }
        } else {
            $argumentsList = @(
                "build ./GeneratedCode/$ToolName/NetStandard20/NetStandard20.csproj",
                "build ./GeneratedCode/$ToolName/NetStandard21/NetStandard21.csproj",
                "build ./GeneratedCode/$ToolName/Net6/Net6.csproj",
                "build ./GeneratedCode/$ToolName/Net7/Net7.csproj",
                "build ./GeneratedCode/$ToolName/Net8/Net8.csproj",
                "build ./GeneratedCode/$ToolName/Net9/Net9.csproj",
                "build ./GeneratedCode/$ToolName/Net48/Net48.csproj",
                "build ./GeneratedCode/$ToolName/Net481/Net481.csproj",
                "build ./GeneratedCode/$ToolName/Net472/Net472.csproj"
            )

            if ($ToolName -notcontains "AutoRest-V3") {
                $argumentsList += "build ./GeneratedCode/$ToolName/Net462/Net462.csproj"
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
                dotnet build ./GeneratedCode/$_/NetStandard21/NetStandard21.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net6/Net6.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net7/Net7.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net8/Net8.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net9/Net9.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net48/Net48.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net481/Net481.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$_/Net472/Net472.csproj; ThrowOnNativeFailure

                if ($_ -notcontains "AutoRest-V3") {
                    dotnet build ./GeneratedCode/$_/Net462/Net462.csproj; ThrowOnNativeFailure
                    dotnet build ./GeneratedCode/$_/Net452/Net452.csproj; ThrowOnNativeFailure
                }
            }
        } else {
            Write-Host "`r`nBuilding $ToolName`r`n"
            dotnet build ./GeneratedCode/$ToolName/NetStandard20/NetStandard20.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/NetStandard21/NetStandard21.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net6/Net6.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net7/Net7.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net8/Net8.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net9/Net9.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net48/Net48.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net481/Net481.csproj; ThrowOnNativeFailure
            dotnet build ./GeneratedCode/$ToolName/Net472/Net472.csproj; ThrowOnNativeFailure

            if ($ToolName -notcontains "AutoRest-V3") {
                dotnet build ./GeneratedCode/$ToolName/Net462/Net462.csproj; ThrowOnNativeFailure
                dotnet build ./GeneratedCode/$ToolName/Net452/Net452.csproj; ThrowOnNativeFailure
            }
        }
    }
}

function Generate-Code {
    
    param (
        [Parameter(Mandatory=$true)]
        [ValidateSet("NSwag", "OpenApiGenerator", "SwaggerCodegen", "AutoRest-V2", "AutoRest-V3", "Kiota", "Refitter")]
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
            $command = "csharp swagger"
        }
        "OpenApiGenerator" { 
            $command = "csharp openapi -v V7140"
        }
        "AutoRest-V2" {
            $command = "csharp autorest"
        } 
        "AutoRest-V3" {
            $command = "csharp autorest"
        }
        "NSwag" {
            $command = "csharp nswag"
        }
        "Kiota" {
            $command = "csharp kiota"
        }
        "Refitter" {
            $command = "csharp refitter"
        }
    }

    if ($Version -eq "V3" -and $ToolName -eq "AutoRest-V2") {
        $ToolName = "AutoRest-V3"
    }

    $project = "../src/CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj"
    $arguments = "$command ./Swagger.$Format GeneratedCode ./GeneratedCode/$ToolName/Output.cs --no-logging"

    switch ($Method) {
        "dotnet-run" {
            $process = Start-Process "dotnet" -Args "run --project $project --configuration Release -- $arguments" -Wait -NoNewWindow -PassThru
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

    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net7/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net6/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net8/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net9/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net48/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net481/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net472/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/Net462/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/NetStandard20/Output.cs" -Force
    Copy-Item "GeneratedCode/$ToolName/Output.cs" "./GeneratedCode/$ToolName/NetStandard21/Output.cs" -Force
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
    "AutoRest-V2", "NSwag", "SwaggerCodegen", "OpenApiGenerator", "Kiota", "Refitter" | ForEach-Object {
        switch ($_) {
            "SwaggerCodegen" {
                $command = "csharp swagger"
            }
            "OpenApiGenerator" { 
                $command = "csharp openapi -v V7070"
            }
            Default {
                $command = $_.ToLower()
            }
        }

        $project = "../src/CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj"
        $arguments = "$command ./Swagger.$Format GeneratedCode ./GeneratedCode/$_/Output.cs --no-logging"

        switch ($Method) {
            "dotnet-run" {
                $processes += Start-Process "dotnet" -Args "run --project $project --configuration Release -- $arguments" -NoNewWindow -PassThru
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

    "AutoRest-V2", "NSwag", "SwaggerCodegen", "OpenApiGenerator", "Kiota", "Refitter" | ForEach-Object {
        if (Test-Path "GeneratedCode/$_/Output.cs" -PathType Leaf) {
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net7/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net6/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net8/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net9/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net48/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net481/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net472/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/Net462/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/NetStandard20/Output.cs" -Force
            Copy-Item "GeneratedCode/$_/Output.cs" "./GeneratedCode/$_/NetStandard21/Output.cs" -Force
            Remove-Item "GeneratedCode/$_/Output.cs" -Force
        } else {            
            throw "$_ code generation failed"
        }
    }
}

function Generate-CodeThenBuild {
    
    param (
        [Parameter(Mandatory=$false)]
        [ValidateSet("All", "NSwag", "OpenApiGenerator", "SwaggerCodegen", "AutoRest-V2", "AutoRest-V3", "Kiota", "Refitter")]
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
                $tools = @("NSwag", "Kiota", "Refitter", "OpenApiGenerator", "SwaggerCodegen", "AutoRest-V2")
            } else {
                $tools = @("NSwag", "Kiota", "Refitter", "OpenApiGenerator", "SwaggerCodegen", "AutoRest-V3")
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
        }
    }

    Remove-Item Swagger.* -Force
    Remove-Item ./**/*Output.cs -Force
}
