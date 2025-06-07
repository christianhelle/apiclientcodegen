# REST API Client Code Generator for JetBrains IDEs

A plugin for generating C# and TypeScript REST API clients from OpenAPI/Swagger specifications directly in JetBrains IDEs (IntelliJ IDEA, Rider, WebStorm, etc.).

## Features

This plugin provides context menu integration for generating REST API client code from OpenAPI/Swagger specification files (JSON/YAML).

### C# Generators

- **NSwag (v14.4.0)**: Generates a C# REST API Client using NSwag
- **Refitter (v1.5.5)**: Generates a C# REST API Client interface for Refit using Refitter
- **OpenAPI Generator (v7.13.0)**: Generates a C# REST API Client using OpenAPI Generator
- **Microsoft Kiota (v1.26.1)**: Generates a C# REST API Client using Microsoft Kiota
- **Swagger Codegen CLI (v3.0.34)**: Generates a C# REST API Client using Swagger Codegen CLI
- **AutoREST (v3.0.0-beta)**: Generates a C# REST API Client using AutoREST

### TypeScript Generators

- **Angular**: Generates a TypeScript REST API Client for Angular
- **Aurelia**: Generates a TypeScript REST API Client for Aurelia
- **Axios**: Generates a TypeScript REST API Client for Axios
- **Fetch**: Generates a TypeScript REST API Client for Fetch
- **Inversify**: Generates a TypeScript REST API Client for Inversify
- **jQuery**: Generates a TypeScript REST API Client for jQuery
- **NestJS**: Generates a TypeScript REST API Client for NestJS
- **Node**: Generates a TypeScript REST API Client for Node
- **Redux Query**: Generates a TypeScript REST API Client for Redux Query
- **RxJS**: Generates a TypeScript REST API Client for RxJS

### Refitter Settings Support

- **Generate Refitter Output**: When right-clicking on a `.refitter` settings file, you can generate C# Refit interfaces using Refitter with custom configuration. This allows for advanced Refitter configurations including custom settings for authentication, serialization, and other Refitter-specific options.

## Installation

### From JetBrains Marketplace (Recommended)

1. Open your JetBrains IDE (IntelliJ IDEA, Rider, WebStorm, etc.)
2. Go to **File** → **Settings** → **Plugins**
3. Click on **Marketplace** tab
4. Search for "REST API Client Code Generator"
5. Click **Install**
6. Restart your IDE

### From Plugin File

1. Download the plugin `.zip` file from the [releases page](https://github.com/christianhelle/apiclientcodegen/releases)
2. Open your JetBrains IDE (IntelliJ IDEA, Rider, WebStorm, etc.)
3. Go to **File** → **Settings** → **Plugins**
4. Click on **⚙️** icon → **Install Plugin from Disk...**
5. Select the downloaded `.zip` file
6. Restart your IDE

## Usage

### For OpenAPI/Swagger Specifications

1. Right-click on a Swagger/OpenAPI specification file (JSON or YAML) in the Project Explorer
2. Select **REST API Client Generator** in the context menu
3. Choose your desired language (C# or TypeScript)
4. Select one of the available generators for that language
5. The generated code will be saved in the configured output directory and opened in the editor

### For Refitter Settings Files

1. Create a `.refitter` settings file with your Refitter configuration
2. Right-click on the `.refitter` file in the Project Explorer
3. Select **Generate Refitter Output** from the context menu
4. The generated C# Refit interfaces will be created according to your settings file configuration

## Requirements

### For C# code generation

- .NET SDK 6.0 or higher
- Java Runtime Environment (for OpenAPI Generator and Swagger Codegen CLI)
- NPM (for AutoREST and NSwag)

### For TypeScript code generation

- Node.js and NPM
- OpenAPI Generator

The plugin uses the `rapicgen` .NET tool to generate the code. If not already installed, you will be prompted to install it when first attempting to generate code.

## Configuration

The plugin includes configurable settings available at **File** → **Settings** → **Tools** → **REST API Client Code Generator**:

- **Default namespace**: Default namespace to use in generated C# code (default: "GeneratedCode")
- **Output directory**: Output directory relative to project root (default: same directory as the specification file)

## Dependencies

The plugin piggy backs on top of well known Open API client code generators like **AutoRest**, **NSwag**, **OpenAPI Generator**, **Microsoft Kiota**, **Refitter**, and **Swagger Codegen CLI**. These tools require [NPM](https://www.npmjs.com/get-npm) and the [Java Runtime Environment](https://java.com/en/download/manual.jsp) to be installed on the developers machine.

The **Swagger Codegen CLI** and **OpenAPI Generator** are distributed as JAR files and are downloaded on demand but requires the Java SDK to be installed on the machine. **AutoRest** is installed on-demand via [NPM](https://www.npmjs.com/get-npm) as a global tool and uses the latest available version. **Microsoft Kiota** is installed on-demand as a .NET Tool and requires .NET 7.0.

The **NSwag** code generator produces code that depends on the [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.3) NuGet package

The **Refitter** code generator produces code that depends on the [Refit](https://www.nuget.org/packages/Refit/8.0.0) NuGet package

The **OpenAPI Generator** code generator produces code that depends on the following NuGet packages:
- [RestSharp](https://www.nuget.org/packages/RestSharp/112.0.0)
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/2.0.1)
- [Polly](https://www.nuget.org/packages/Polly/8.5.2)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.3)

The **Microsoft Kiota** code generator produces code that depends on the following NuGet packages:
- [Microsoft.Kiota.Abstractions](https://www.nuget.org/packages/Microsoft.Kiota.Abstractions)
- [Microsoft.Kiota.Http.HttpClientLibrary](https://www.nuget.org/packages/Microsoft.Kiota.Http.HttpClientLibrary)
- [Microsoft.Kiota.Serialization.Form](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Form)
- [Microsoft.Kiota.Serialization.Text](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Text)
- [Microsoft.Kiota.Serialization.Json](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Json)
- [Microsoft.Kiota.Serialization.Multipart](https://www.nuget.org/packages/Microsoft.Kiota.Serialization.Multipart)
- [Microsoft.Kiota.Authentication.Azure](https://www.nuget.org/packages/Microsoft.Kiota.Authentication.Azure)
- [Azure.Identity](https://www.nuget.org/packages/Azure.Identity)

The **Swagger Codegen CLI** code generator produces code that depends on the [RestSharp](https://www.nuget.org/packages/RestSharp/105.1.0) and [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/1.9.0) NuGet packages

The **AutoRest** code generator produces code that depends on the [Microsoft.Rest.ClientRuntime](https://www.nuget.org/packages/Microsoft.Rest.ClientRuntime/2.3.24) and [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.3) NuGet packages

## Development

### Building the Plugin

To build the plugin from source:

```bash
./gradlew build
```

### Running the Plugin in Development Mode

```bash
./gradlew runIde
```

### Building the Plugin Distribution

```bash
./gradlew buildPlugin
```

The built plugin will be available in `build/distributions/`

## Related

- This plugin is the IntelliJ/JetBrains Rider equivalent of the [REST API Client Code Generator](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator2022) extension for Visual Studio.
- VS Code version: [REST API Client Code Generator for VS Code](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.rest-api-client-code-generator)

## License

This project is licensed under the MIT License - see the [LICENSE](../../LICENSE) file for details.

## Support

If you find this useful and feel a bit generous then feel free to [buy me a coffee ☕](https://www.buymeacoffee.com/christianhelle)