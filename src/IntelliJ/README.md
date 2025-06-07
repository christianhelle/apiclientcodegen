# REST API Client Code Generator - IntelliJ Plugin

This IntelliJ plugin provides code generation capabilities for REST API clients from OpenAPI/Swagger specifications directly within JetBrains IDEs, including IntelliJ IDEA, Rider, WebStorm, and other JetBrains products.

## Features

### C# Code Generators
- **NSwag** - Generate C# client code using NSwag
- **Refitter** - Generate C# client code using Refitter (Refit-based)
- **OpenAPI Generator** - Generate C# client code using OpenAPI Generator
- **Microsoft Kiota** - Generate C# client code using Microsoft Kiota
- **Swagger Codegen CLI** - Generate C# client code using Swagger Codegen CLI
- **AutoREST** - Generate C# client code using AutoREST

### TypeScript Code Generators
- **Angular** - Generate Angular service clients
- **Aurelia** - Generate Aurelia HTTP clients
- **Axios** - Generate Axios-based clients
- **Fetch API** - Generate Fetch API clients
- **Inversify** - Generate Inversify-compatible clients
- **jQuery** - Generate jQuery-based clients
- **NestJS** - Generate NestJS clients
- **Node.js** - Generate Node.js clients
- **Redux Query** - Generate Redux Query clients
- **RxJS** - Generate RxJS-based clients

### Refitter Settings Support
- **Create Refitter Settings** - Generate `.refitter` configuration files
- Automatic detection and context menu integration for `.refitter` files

### Configuration
- **Namespace Configuration** - Set custom namespace for generated code
- **Output Directory** - Configure where generated files are saved
- **Global Settings** - IDE-wide configuration through Settings/Preferences

## Compatibility

- **JetBrains Rider** 2025.1.2+
- **IntelliJ IDEA** 2024.1+
- **WebStorm** 2024.1+
- **Other JetBrains IDEs** with corresponding version compatibility

## Installation

### From JetBrains Marketplace
1. Open your JetBrains IDE
2. Go to **File** → **Settings** (or **Preferences** on macOS)
3. Navigate to **Plugins**
4. Search for "REST API Client Code Generator"
5. Click **Install**
6. Restart your IDE

### Manual Installation
1. Download the plugin `.zip` file from the releases
2. Open your JetBrains IDE
3. Go to **File** → **Settings** → **Plugins**
4. Click the gear icon and select **Install Plugin from Disk**
5. Select the downloaded `.zip` file
6. Restart your IDE

## Usage

### Generate Code from OpenAPI Files

1. **Right-click** on any `.json`, `.yaml`, or `.yml` file containing OpenAPI/Swagger specification
2. Select **Generate API Client Code** from the context menu
3. Choose your desired generator (C# or TypeScript)
4. The generated code will be saved to your configured output directory

### Configure Settings

1. Go to **File** → **Settings** → **Tools** → **REST API Client Code Generator**
2. Set your preferred:
   - **Default Namespace** for generated C# code
   - **Output Directory** for generated files
3. Click **Apply** and **OK**

### Create Refitter Settings

1. **Right-click** in your project
2. Select **Generate API Client Code** → **Create Refitter Settings**
3. A `.refitter` configuration file will be created
4. You can then right-click on the `.refitter` file to generate code

## Prerequisites

The plugin requires the following tools to be installed on your system:

### For C# Generators
- **.NET SDK** 6.0 or later
- **rapicgen** .NET tool (installed automatically when first used)

### For TypeScript Generators
- **Node.js** 16.0 or later
- **npm** or **yarn**

### For Java-based Generators
- **Java** 11 or later (usually provided by your JetBrains IDE)

## Generated Code Structure

### C# Code
```
/Generated
├── Models/
│   ├── ApiModels.cs
│   └── ...
├── Services/
│   ├── ApiClient.cs
│   └── ...
└── Interfaces/
    ├── IApiClient.cs
    └── ...
```

### TypeScript Code
```
/Generated
├── models/
│   ├── api-models.ts
│   └── ...
├── services/
│   ├── api-client.ts
│   └── ...
└── types/
    ├── api-types.ts
    └── ...
```

## Configuration Files

### Global Settings
Plugin settings are stored in your IDE's configuration and include:
- Default namespace for C# code generation
- Default output directory
- Generator-specific preferences

### Refitter Settings (`.refitter`)
```json
{
  "openApiSpecUrl": "https://api.example.com/swagger.json",
  "namespace": "MyApp.ApiClient",
  "outputFolder": "./Generated",
  "clientName": "ExampleApiClient"
}
```

## Troubleshooting

### Common Issues

**"rapicgen tool not found"**
- Ensure .NET SDK is installed
- The plugin will attempt to install rapicgen automatically
- Manual installation: `dotnet tool install -g rapicgen`

**"Generated code not appearing"**
- Check the output directory configuration
- Ensure you have write permissions to the output directory
- Refresh your project tree (**F5** or right-click → **Synchronize**)

**"Generator failed"**
- Check the IDE's Event Log for detailed error messages
- Ensure your OpenAPI specification is valid
- Verify all prerequisites are installed

### Support

For issues, feature requests, or contributions:
- GitHub Issues: [https://github.com/christianhelle/apiclientcodegen/issues](https://github.com/christianhelle/apiclientcodegen/issues)
- Documentation: [https://github.com/christianhelle/apiclientcodegen](https://github.com/christianhelle/apiclientcodegen)

## Building from Source

### Prerequisites
- JDK 17 or later
- Gradle 8.5+ (included via wrapper)

### Build Commands
```bash
# Build the plugin
./gradlew build

# Run tests
./gradlew test

# Build plugin distribution
./gradlew buildPlugin

# Run plugin verification
./gradlew verifyPlugin
```

### Development
```bash
# Run plugin in development mode
./gradlew runIde

# Run tests in development mode
./gradlew test --continuous
```

## License

This project is licensed under the MIT License - see the [LICENSE](../../LICENSE) file for details.

## Related Projects

- **VSCode Extension**: [../VSCode](../VSCode) - Visual Studio Code extension with the same functionality
- **Visual Studio Extension**: [../VSIX](../VSIX) - Visual Studio extension for Windows
- **CLI Tool**: [../CLI](../CLI) - Command-line interface for the code generators
