This VS Code extension is a cross-platform alternative to the Visual Studio extension for generating C# REST API clients from OpenAPI/Swagger specifications.

## Features

- Generate C# REST API clients from OpenAPI/Swagger JSON or YAML files
- Context menu integration on JSON and YAML files in the VS Code explorer
- Support for all code generators:
  - NSwag
  - Refitter
  - OpenAPI Generator
  - Microsoft Kiota
  - Swagger Codegen CLI
  - AutoREST
- Configuration options for namespace and output directory
- Cross-platform support (Windows, macOS, Linux)

## Screenshot

![REST API Client Code Generator VS Code Extension](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vscode-context-menu.png)

## Requirements

- .NET 6.0 SDK or higher
- Java Runtime Environment (for OpenAPI Generator and Swagger Codegen CLI)
- NPM (for AutoREST and NSwag)

The extension uses the `rapicgen` .NET tool to generate the code. If not already installed, you will be prompted to install it when first attempting to generate code.

## Usage

1. Right-click on a Swagger/OpenAPI specification file (JSON or YAML) in the VS Code explorer
2. Select "REST API Client Code Generator" in the context menu
3. Choose one of the available code generators
4. The generated C# code will be opened in the editor

## Extension Settings

This extension contributes the following settings:

* `restApiClientCodeGenerator.namespace`: Default namespace to use in generated code (default: "GeneratedCode")
* `restApiClientCodeGenerator.outputDirectory`: Output directory relative to workspace folder (default: same directory as the specification file)

## Installation

You can install this extension in several ways:

1. Download the VSIX from [GitHub Releases](https://github.com/christianhelle/apiclientcodegen/releases)
2. Install from VSIX in VS Code:
   - Go to Extensions view (Ctrl+Shift+X)
   - Click "..." menu in the top right corner
   - Select "Install from VSIX..."
   - Select the downloaded .vsix file
