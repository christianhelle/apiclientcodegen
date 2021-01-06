[![Build status](https://ci.appveyor.com/api/projects/status/gb2doe3tgwjt47cn/branch/master?svg=true)](https://ci.appveyor.com/project/christianhelle/apiclientcodegen/branch/master)
![CLI Tool](https://github.com/christianhelle/apiclientcodegen/workflows/CLI%20Tool/badge.svg)
![VSIX](https://github.com/christianhelle/apiclientcodegen/workflows/VSIX/badge.svg)
![VS Mac](https://github.com/christianhelle/apiclientcodegen/workflows/VS%20Mac/badge.svg)
![Smoke Tests](https://github.com/christianhelle/apiclientcodegen/workflows/Smoke%20Tests/badge.svg)
[![Azure Pipelines](https://christianhelle.visualstudio.com/API%20Client%20Code%20Generator/_apis/build/status/CI%20Build)](https://dev.azure.com/christianhelle/API%20Client%20Code%20Generator/_build?definitionId=32)

[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=christianhelle_apiclientcodegen&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=christianhelle_apiclientcodegen)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=christianhelle_apiclientcodegen&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=christianhelle_apiclientcodegen)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=christianhelle_apiclientcodegen&metric=security_rating)](https://sonarcloud.io/dashboard?id=christianhelle_apiclientcodegen)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=christianhelle_apiclientcodegen&metric=bugs)](https://sonarcloud.io/dashboard?id=christianhelle_apiclientcodegen)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=christianhelle_apiclientcodegen&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=christianhelle_apiclientcodegen)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=christianhelle_apiclientcodegen&metric=alert_status)](https://sonarcloud.io/dashboard?id=christianhelle_apiclientcodegen)

[![Version](https://vsmarketplacebadge.apphb.com/version/ChristianResmaHelle.APIClientCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator) 
[![Installs](https://vsmarketplacebadge.apphb.com/downloads-short/ChristianResmaHelle.APIClientCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator) 
[![Rating](https://vsmarketplacebadge.apphb.com/rating-star/ChristianResmaHelle.APIClientCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator)
[![Join the chat at https://gitter.im/apiclientcodegen/community](https://badges.gitter.im/apiclientcodegen/community.svg)](https://gitter.im/apiclientcodegen/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![NuGet](https://img.shields.io/nuget/v/rapicgen.svg?style=flat-square)](http://www.nuget.org/packages/rapicgen)
[![NuGet](https://img.shields.io/nuget/dt/rapicgen.svg?style=flat-square)](http://www.nuget.org/packages/rapicgen)

# REST API Client Code Generator
A collection of Visual Studio C# custom tool code generators for Swagger / OpenAPI specification files

#### Download

Download the **[latest version for Windows](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator)** from the Visual Studio Marketplace or the **[latest version for Mac (.mpack)](https://github.com/christianhelle/apiclientcodegen/releases/latest)** from the Release page. Follow **[these instructions](#visual-studio-for-mac-1)** for update convenience on Visual Studio for Mac

## Features

- Supports Visual Studio 2017, 2019, and [Visual Studio for Mac](#visual-studio-for-mac-1)
- Add New REST API Client to a project from an OpenAPI specification URL (e.g https://petstore.swagger.io/v2/swagger.json) using [AutoRest](https://github.com/Azure/autorest), [NSwag](https://github.com/RicoSuter/NSwag), [Swagger Codegen](https://github.com/swagger-api/swagger-codegen), or [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator)
- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the OpenAPI specification JSON or YAML file
- Generate code using an [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio) specification file by including it in the project and using the **Generate with NSwag** context menu


### Custom Tools

- ***AutoRestCodeGenerator*** - Generates a single file C# REST API Client using **AutoRest**. 
The resulting file is the equivalent of using the AutoRest CLI tool with:
` --csharp --input-file=[swaggerFile] --output-file=[outputFile] --namespace=[namespace] --add-credentials`

- ***NSwagCodeGenerator*** - Generates a single file C# REST API Client using the [NSwag.CodeGeneration.CSharp](https://github.com/RSuter/NSwag/wiki/SwaggerToCSharpClientGenerator) [nuget package](https://www.nuget.org/packages/NSwag.CodeGeneration.CSharp/) v13.7.4

- ***SwaggerCodeGenerator*** - Generates a single file C# REST API Client using **Swagger Codegen CLI v3.0.14**.
The output file is the result of merging all the files generated using the Swagger Codegen CLI tool with:
` generate -l csharp --input-spec [swaggerFile] --output [output] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`

- ***OpenApiCodeGenerator*** - Generates a single file C# REST API Client using **OpenAPI Generator v5.0.0**.
The output file is the result of merging all the files generated using the OpenAPI Generator tool with:
` generate -g csharp --input-spec [swaggerFile] --output [output] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`


### Dependencies

The custom tool code generators piggy back on top of well known Open API client code generators like **AutoRest**, **NSwag**, **OpenAPI Generator**, and **Swagger Codegen CLI**. These tools require [NPM](https://www.npmjs.com/get-npm) and the [Java Runtime Environment](https://java.com/en/download/manual.jsp) to be installed on the developers machine. Alternative Java SDK implementations such as the [OpenJDK](https://adoptopenjdk.net) works fine with this extension. By default, the path to **java.exe** is read from the **JAVA_HOME** environment variable, but is also configurable in the Settings screen

The **Swagger Codegen CLI** and **OpenAPI Generator** are distributed as JAR files and are downloaded on demand but requires the Java SDK to be installed on the machine. **AutoRest** is installed on-demand via [NPM](https://www.npmjs.com/get-npm) as a global tool and uses the latest available version. This means that using these custom tools have an initial delay upon first time use. 

**NSwagStudio** is stand alone UI tool for editing a **.nswag** specification file for generating code. This tool is optional to install and official installation instructions are available on the [NSwag Wiki on Github](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio). If **NSwagStudio** is not installed on the machine then the Visual Studio Extension will install the **NSwag CLI** via [NPM](https://www.npmjs.com/get-npm) as a global tool using the latest available version. 

The **OpenAPI Generator** and **Swagger Codegen CLI** code generators produces code that depends on the [RestSharp](https://www.nuget.org/packages/RestSharp/105.1.0) and [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/1.2.0) NuGet packages

The **AutoRest** code generator produces code that depends on the [Microsoft.Rest.ClientRuntime](https://www.nuget.org/packages/Microsoft.Rest.ClientRuntime/2.3.21) and [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/12.0.3) NuGet packages

The **NSwag** code generator produces code that depends on the [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/12.0.3) NuGet package

This Visual Studio Extension will automatically add the required NuGet packages that the generated code depends on


## Screenshots

![Add - API Client from OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/add-new-menu.png)

![Enter URL to OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/add-new-dialog.png)

![Solution Explorer Context Menus](https://github.com/christianhelle/apiclientcodegen/raw/master/images/solution-explorer-context-menu.jpg)

![NSwag Studio Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/nswagstudio-context-menu.jpg)


### Settings

This extension will by default make some assumptions on the installation paths for **Java**, **NSwag** and **NPM** but also provides option pages for configuring this. The **Swagger Codegen CLI** and the **OpenAPI Generator** JAR files are by default downloaded to the user TEMP folder but it is also possible to specify to use existing JAR files

![Options - General](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-general.png)

Supports customising how AutoRest generates code based on the C# generator settings that the AutoRest CLI tool provides

![Options - AutoRest](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-autorest.png)

Supports customising how NSwag generates code using the properties exposed by the NSwag NuGet package

![Options - NSwag](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-nswag.png)

Supports customising how the **.nswag** file is generated using a subset of the options available in NSwag Studio

![Options - NSwag Studio](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-nswagstudio.png)


### Visual Studio for Mac

![Add - API Client from OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-add-new-menu.png)

![Enter URL to OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-add-new-dialog.png)

![Solution Explorer Context Menus](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-generate-code.png)

![NSwag Studio Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-nswag-studio.png)


## Installation

The Visual Studio extension can be installed directly from Visual Studio 2017 or 2019 via the **Extensions Dialog Box**. The process is best described in the official Microsoft documentation for [Managing extensions for Visual Studio](https://docs.microsoft.com/en-us/visualstudio/ide/finding-and-using-visual-studio-extensions?view=vs-2019)

### Visual Studio for Mac

This installation process for **Visual Studio for Mac** is currently a bit troublesome as the MonoDevelop Addin Repository is currently not accepting new users so I can't really register and setup my extension.

There are 2 ways of installing my extension on Visual Studio for Mac: Adding a custom extension repository or Installing the **.mpack** file directly from the Extensions Manager

### Adding a new extension repository

Here's what you need to do:

- Open the Visual Studio for Mac **Extension Manager**
- You can do this from the menu **Visual Studio** -> **Extentions**

![Open Extensions Dialog Box](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-menu.png)

- Select the **Gallery** Tab
- Expand the Repository drop down box and select **Manage Repositories**

![Manage Repositories](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-manage-repositories.png)

- Press on the **Add** button to add a new custom extension repository

![Manage Repositories](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-repositories.png)

- Register an online repository at **https://christianhelle.com/vsmac/main.mrep**
- Click **OK**

![Add Repository](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-add-repository.png)

- Now my extension repository is added to the list
- Make sure that this is enabled (indicated by a check box)

![Add Repository Dialog](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-added-repository.png)

- You should now be able to see my extensions from the **Gallery** tab

![Added Repository](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-gallery.png)

- By adding my extension repository you will be able to conveniently update my extension using the Visual Studio for Mac Extension Manager

![Add Repository](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-updates.png)

![Add Repository](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-update-install.png)

![Add Repository](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-update-install-progress.png)

### Installing the **.mpack** file directly

Here's what you need to do:
- Download the latest **.mpack file** from the [Latest Github Release](https://github.com/christianhelle/apiclientcodegen/releases/latest)
- Now from within Visual Studio for Mac you need to launch the **Extensions Dialog Box**. You can do this from the menu **Visual Studio** -> **Extentions**

![Open Extensions Dialog Box](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-menu.png)

- Click on the **Install from File** button

![Manually install .mpack file](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-install.png)

- Browse to the .mpack file and select it. You will be prompted with a confirmation dialog

![Confirm .mpack file install](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-install-confirm.png)

- Click **Install** and restart Visual Studio for Mac
- To Verify that the Add-in was installed you can re-open the Extensions Dialog Box, select the **Installed** tab and expand the **IDE Extensions**. You should be able to see the **REST API Client Code Generator**.

- Uninstalling the Add-in is done in this same dialog box by clicking the **Uninstall** button

![Manually uninstall Add-in](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vsmac-extensions-uninstall.png)


## Cross Platform Command Line Tool
All custom tools mentioned above are also implemented in a cross platform command line application

#### Requirements
- .NET Core 2.1 runtime
- Java Runtime Environment
- NPM

### Installation
The tool can be installed as a .NET Core global tool that you can call from the shell / command line
```
dotnet tool install --global rapicgen
```
or by following the instructions [here](https://www.nuget.org/packages/rapicgen) to install a specific version of tool

### Usage
Since the tool is published as a .NET Core Tool, it can be launched from anywhere using any command line interface by calling **rapicgen**.
The help information is displayed when not specifying any arguments to **rapicgen**

```
Usage: run [options] [command]

Options:
  -v|--verbose  Show verbose output
  -?|-h|--help  Show help information

Commands:
  autorest      Generate Swagger / Open API client using AutoRest
  nswag         Generate Swagger / Open API client using NSwag
  openapi       Generate Swagger / Open API client using OpenAPI Generator
  swagger       Generate Swagger / Open API client using Swagger Codegen CLI

Run 'run [command] --help' for more information about a command.
```

Some help information is also provided per command and can be launched by 

```
$ rapicgen [command name] -?
```

For example:

```
$ rapicgen autorest -?
```

will output this:

```
Generate Swagger / Open API client using AutoRest

Usage: run autorest [options] <swaggerFile> <namespace> <outputFile>

Arguments:
  swaggerFile   Path to the Swagger / Open API specification file
  namespace     Default namespace to in the generated code
  outputFile    Output filename to write the generated code to. Default is the swaggerFile .cs

Options:
  -?|-h|--help  Show help information
```

## Usage Examples:

Let's say we have a OpenAPI Specifications document called **Swagger.json**

For starters, we can use the Swagger Petstore spec. Here's an example powershell script for downloading it

```
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json
```

In case you don't have the CLI tool installed you can install it by

```
dotnet tool install --global rapicgen
```

Here's an example of how to generate code using **AutoRest**

```
rapicgen autorest Swagger.json GeneratedCode ./AutoRestOutput.cs
```

Here's an example of how to generate code using **NSwag**

```
rapicgen nswag Swagger.json GeneratedCode ./NSwagOutput.cs
```

Here's an example of how to generate code using **Swagger Codegen CLI**

```
rapicgen swagger Swagger.json GeneratedCode ./SwaggerOutput.cs
```

And last but but not the least, here's an example of how to generate code using **OpenAPI Generator**

```
rapicgen openapi Swagger.json GeneratedCode ./OpenApiOutput.cs
```

#

For tips and tricks on software development, check out [my blog](https://christian-helle.blogspot.com)

If you find this useful and feel a bit generous then feel free to [buy me a coffee](https://www.buymeacoffee.com/christianhelle) :)

