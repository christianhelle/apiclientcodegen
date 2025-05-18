# REST API Client Code Generator for JetBrains Rider

This is a JetBrains Rider extension that adds a context menu to generate C# REST API clients from OpenAPI/Swagger specifications using the Rapicgen .NET tool.

## Features
- Adds a "REST API Client Generator" context menu when right-clicking on JSON, YAML, or YML files in the Solution Explorer
- Submenus for each supported C# generator:
  - NSwag
  - Refitter
  - OpenAPI Generator
  - Microsoft Kiota
  - Swagger Codegen CLI
  - AutoREST
- Invokes the Rapicgen .NET tool with the selected generator and file

## Requirements
- .NET SDK 6.0 or higher
- Rapicgen .NET tool installed globally (`dotnet tool install --global rapicgen`)

## Building
Open the `Rider` folder in JetBrains Rider and build the solution.

## Installation
1. Build the plugin
2. Install the generated plugin zip/jar in JetBrains Rider via `Settings > Plugins > Install Plugin from Disk`

## Usage
1. Right-click on a JSON, YAML, or YML file in the Solution Explorer
2. Select `REST API Client Generator`
3. Choose a generator
4. The extension will run Rapicgen and generate the client code

---

This is the initial README for the Rider extension. The source code and plugin manifest will be added next.