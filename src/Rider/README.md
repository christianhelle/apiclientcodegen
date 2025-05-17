# REST API Client Code Generator for JetBrains Rider

A JetBrains Rider extension for generating REST API clients from OpenAPI/Swagger specifications using various code generators.

## Features

- Generate C# API clients from OpenAPI/Swagger specifications (JSON or YAML files)
- Support for multiple code generators:
  - [NSwag](https://github.com/RicoSuter/NSwag)
  - [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator)
  - [Swagger Codegen](https://github.com/swagger-api/swagger-codegen)
  - [Refitter](https://github.com/christianhelle/refitter)
  - [Microsoft Kiota](https://github.com/microsoft/kiota)
  - [AutoRest](https://github.com/Azure/autorest)
- Context menu integration for JSON and YAML files in the solution explorer
- Automatic NuGet package dependency installation for generated code

## Requirements

- JetBrains Rider 2023.1 or later
- .NET SDK 6.0 or later
- Java Runtime Environment (for OpenAPI Generator and Swagger Codegen CLI)
- NPM (for AutoREST and NSwag)

## Usage

1. Right-click on an OpenAPI/Swagger specification file (JSON or YAML) in the solution explorer
2. Select "REST API Client Code Generator" from the context menu
3. Choose one of the available code generators:
   - NSwag
   - Refitter
   - OpenAPI Generator
   - Microsoft Kiota
   - Swagger Codegen CLI
   - AutoREST
4. The extension will execute the [Rapicgen](https://www.nuget.org/packages/rapicgen) .NET tool to generate the code
5. Generated code file will be saved next to the specification file and opened in the editor

## Dependencies

The code generators rely on various tools that may need to be installed on your system:
- The Swagger Codegen CLI and OpenAPI Generator are distributed as JAR files and require the Java SDK
- AutoREST and NSwag are installed via NPM as global tools
- Microsoft Kiota is installed as a .NET Tool and requires .NET 7.0

The extension will attempt to install the Rapicgen .NET tool if it's not already installed.

## Related

This extension is the JetBrains Rider equivalent of:
- [REST API Client Code Generator](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator2022) extension for Visual Studio
- [REST API Client Code Generator](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.rest-api-client-code-generator) extension for Visual Studio Code

For more information, see the [main project repository](https://github.com/christianhelle/apiclientcodegen).
