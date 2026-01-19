# OpenAPI Generator Usage Guide

This guide provides comprehensive documentation for using the OpenAPI Generator features of the REST API Client Code Generator Visual Studio extension.

## Table of Contents

- [Overview](#overview)
- [Getting Started](#getting-started)
- [Configuration Options](#configuration-options)
- [Configuration Files](#configuration-files)
- [Custom Tool Usage](#custom-tool-usage)
- [Visual Studio Settings](#visual-studio-settings)
- [Advanced Features](#advanced-features)
- [CLI Usage](#cli-usage)
- [Supported Versions](#supported-versions)
- [Generated Code Dependencies](#generated-code-dependencies)
- [Troubleshooting](#troubleshooting)

## Overview

The OpenAPI Generator integration in this Visual Studio extension uses the official [OpenAPI Generator](https://openapi-generator.tech/) tool to generate C# REST API clients from OpenAPI (formerly Swagger) specifications. The generator supports both OpenAPI v2 and v3 specifications in JSON or YAML format.

### Key Features

- **Multiple Output Modes**: Generate a single merged file or multiple files for SDK-style projects
- **Configuration File Support**: Use `.config.json` or `.config.yaml` files for advanced customization
- **Custom Templates**: Support for custom Mustache templates
- **Version Selection**: Choose from multiple OpenAPI Generator versions (7.7.0 - 7.18.0)
- **Framework Targeting**: Target .NET Standard 1.3-2.1, .NET Framework 4.7-4.8, or .NET 6.0-9.0
- **Auto-refresh**: Automatically regenerate code when the OpenAPI specification changes

## Getting Started

### Prerequisites

The OpenAPI Generator requires the following dependencies to be installed:

- **Java Runtime Environment (JRE)**: Version 8 or higher
  - The extension looks for `java.exe` using the `JAVA_HOME` environment variable
  - Alternative: Configure a custom Java path in Visual Studio settings
- **Visual Studio**: 2017, 2019, 2022, or 2026

### Basic Usage in Visual Studio

1. **Add a new API client to your project**:
   - Right-click on your project in Solution Explorer
   - Select **Add** → **New REST API Client**
   - Enter the URL or path to your OpenAPI specification
   - Select **OpenAPI Generator** as the code generator
   - Specify a namespace and filename

2. **Use the OpenApiCodeGenerator Custom Tool**:
   - Add your OpenAPI specification file (`.json` or `.yaml`) to your project
   - Right-click the file in Solution Explorer and select **Properties**
   - Set **Custom Tool** to `OpenApiCodeGenerator`
   - The generated code file will be created automatically

3. **Regenerate code automatically**:
   - Any time you modify the OpenAPI specification file and save it, the code will be regenerated automatically
   - Alternatively, right-click the specification file and select **Generate Code**

## Configuration Options

The OpenAPI Generator supports extensive configuration options that control how the code is generated.

### Available Options

#### Emit Default Value
- **Property**: `EmitDefaultValue`
- **Type**: Boolean (default: `true`)
- **Description**: Controls whether default values for members should be generated in the serialization stream. Setting this to `false` is only recommended for specific scenarios like interoperability or reducing data size.

#### Optional Method Arguments
- **Property**: `MethodArgument`
- **Type**: Boolean (default: `true`)
- **Description**: Enables C# optional method arguments (e.g., `void Square(int x = 10)`). Requires .NET Framework 4.0 or later.

#### Generate Property Changed
- **Property**: `GeneratePropertyChanged`
- **Type**: Boolean (default: `false`)
- **Description**: Generates `INotifyPropertyChanged` implementation for model classes, useful for data binding scenarios in WPF or other UI frameworks.

#### Use Collection
- **Property**: `UseCollection`
- **Type**: Boolean (default: `false`)
- **Description**: Deserializes array types to `Collection<T>` instead of `List<T>`.

#### Use DateTimeOffset
- **Property**: `UseDateTimeOffset`
- **Type**: Boolean (default: `false`)
- **Description**: Uses `DateTimeOffset` instead of `DateTime` to model date-time properties, providing better timezone handling.

#### Target Framework
- **Property**: `TargetFramework`
- **Type**: Enum (default: `NetStandard21`)
- **Description**: Specifies the target .NET framework version
- **Available Values**:
  - `netstandard1.3` through `netstandard2.1`
  - `net47`, `net48`
  - `net6.0`, `net7.0`, `net8.0`, `net9.0`

#### Skip Form Model
- **Property**: `SkipFormModel`
- **Type**: Boolean (default: `true`)
- **Description**: Skips generation of models defined as form parameters in `requestBody`.

#### Templates Path
- **Property**: `TemplatesPath`
- **Type**: String (optional)
- **Description**: Path to a directory containing custom Mustache template files. Can be an absolute path or relative to the OpenAPI specification file.

#### Use Configuration File
- **Property**: `UseConfigurationFile`
- **Type**: Boolean (default: `true`)
- **Description**: Enables automatic detection and usage of `.config.json` or `.config.yaml` configuration files.

#### Generate Multiple Files
- **Property**: `GenerateMultipleFiles`
- **Type**: Boolean (default: `false`)
- **Description**: Generates multiple files (models, APIs, supporting files) instead of merging into a single file. This only works with SDK-style projects.

#### Version
- **Property**: `Version`
- **Type**: Enum (default: `Latest`)
- **Description**: Specifies which version of OpenAPI Generator to use
- **Available Versions**: 7.7.0, 7.8.0, 7.9.0, 7.10.0, 7.11.0, 7.12.0, 7.13.0, 7.14.0, 7.15.0, 7.16.0, 7.17.0, 7.18.0

#### HTTP User-Agent
- **Property**: `HttpUserAgent`
- **Type**: String (optional)
- **Description**: Sets the User-Agent header value for HTTP requests when fetching remote OpenAPI specifications.

#### Custom Additional Properties
- **Property**: `CustomAdditionalProperties`
- **Type**: String (optional)
- **Description**: Provides complete control over additional properties passed to OpenAPI Generator. When set, this overrides all other property settings except configuration files.
- **Example**: `--additional-properties packageName=MyClient,netCoreProjectFile=true`

## Configuration Files

Configuration files provide the most flexible way to customize OpenAPI Generator behavior. The extension automatically detects and uses configuration files when present.

### File Naming Convention

Place a configuration file next to your OpenAPI specification file with one of these naming patterns:

1. **Same extension as spec**: `[spec-name].config.[ext]`
   - For `swagger.json` → `swagger.config.json`
   - For `openapi.yaml` → `openapi.config.yaml`

2. **JSON config**: `[spec-name].config.json`
   - Works with both JSON and YAML specs

3. **YAML config**: `[spec-name].config.yaml`
   - Works with both JSON and YAML specs

### Configuration File Format

#### JSON Configuration Example

```json
{
  "useDateTimeOffset": true,
  "useCollection": true,
  "returnICollection": true,
  "interfacePrefix": "I",
  "targetFramework": "net8.0",
  "packageName": "MyApi.Client",
  "netCoreProjectFile": true,
  "optionalEmitDefaultValues": true,
  "optionalMethodArguments": true,
  "generatePropertyChanged": false,
  "hideGenerationTimestamp": true,
  "sortParamsByRequiredFlag": true,
  "nullableReferenceTypes": true
}
```

#### YAML Configuration Example

```yaml
useDateTimeOffset: true
useCollection: true
returnICollection: true
interfacePrefix: I
targetFramework: net8.0
packageName: MyApi.Client
netCoreProjectFile: true
optionalEmitDefaultValues: true
optionalMethodArguments: true
generatePropertyChanged: false
hideGenerationTimestamp: true
sortParamsByRequiredFlag: true
nullableReferenceTypes: true
```

### Common Configuration Properties

The configuration file supports all properties documented in the [OpenAPI Generator C# Client documentation](https://openapi-generator.tech/docs/generators/csharp). Here are some commonly used properties:

| Property | Type | Description |
|----------|------|-------------|
| `packageName` | string | C# package name (convention: Title.Case) |
| `targetFramework` | string | The target framework identifier |
| `netCoreProjectFile` | boolean | Use the new .csproj format (SDK-style) |
| `interfacePrefix` | string | Prefix for generated interface names |
| `enumNameSuffix` | string | Suffix for enum names |
| `enumValueSuffix` | string | Suffix for enum values |
| `modelPropertyNaming` | string | Naming convention for model properties (camelCase, PascalCase, snake_case) |
| `hideGenerationTimestamp` | boolean | Hide the generation timestamp to reduce source control diffs |
| `sortParamsByRequiredFlag` | boolean | Sort method parameters by required flag |
| `nullableReferenceTypes` | boolean | Use nullable reference types (C# 8.0+) |
| `validatable` | boolean | Generate model validation |
| `equatable` | boolean | Generate IEquatable implementation |
| `conditionalSerialization` | boolean | Use conditional serialization |

### Visual Examples

#### JSON Spec with JSON Config
![JSON Configuration Example](/images/openapi-generator-configuration-files.png)

#### YAML Spec with YAML Config
![YAML Configuration Example](/images/openapi-generator-configuration-files-yaml.png)

#### YAML Spec with JSON Config
![Mixed Format Example 1](/images/openapi-generator-configuration-files-yaml-spec-json.png)

#### JSON Spec with YAML Config
![Mixed Format Example 2](/images/openapi-generator-configuration-files-json-spec-yaml.png)

## Custom Tool Usage

The `OpenApiCodeGenerator` custom tool integrates directly with Visual Studio's build system.

### Setting Up the Custom Tool

1. Add your OpenAPI specification file to your project
2. Right-click the file in Solution Explorer
3. Select **Properties**
4. Set the **Custom Tool** property to `OpenApiCodeGenerator`
5. Save the file

Visual Studio will automatically:
- Generate the code file on first save
- Regenerate whenever the specification file changes
- Show the generated file nested under the specification file in Solution Explorer

### Custom Tool Variants

The extension provides different custom tool implementations:

- **OpenApiCodeGenerator**: For C# projects (generates `.cs` files)
- **OpenApiVisualBasicCodeGenerator**: For Visual Basic projects (generates `.vb` files)

### Context Menu Commands

Right-click on an OpenAPI specification file to access these commands:

- **Generate Code**: Manually trigger code generation
- **Set Custom Tool to OpenAPI Generator**: Automatically set the custom tool property

## Visual Studio Settings

Configure OpenAPI Generator behavior globally through Visual Studio settings.

### Accessing Settings

1. Go to **Tools** → **Options**
2. Navigate to **REST API Client Code Generator** → **OpenAPI Generator**

### Available Settings

The settings page provides UI controls for all the configuration options described in the [Configuration Options](#configuration-options) section. Settings configured here apply to all new code generation operations unless overridden by a configuration file.

![OpenAPI Generator Settings](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-openapigenerator.png)

### Settings Priority

Configuration is applied in the following order (highest to lowest priority):

1. **Configuration File** (`.config.json` or `.config.yaml`)
2. **Custom Additional Properties** (if specified)
3. **Visual Studio Settings** (Tools → Options)
4. **Default Values**

## Advanced Features

### Custom Mustache Templates

OpenAPI Generator uses Mustache templates to generate code. You can customize these templates to control the exact output.

#### Using Custom Templates

1. **Obtain the default templates**:
   - Download from the [OpenAPI Generator repository](https://github.com/OpenAPITools/openapi-generator/tree/master/modules/openapi-generator/src/main/resources/csharp)
   - Or extract from the JAR file

2. **Modify the templates**:
   - Edit the `.mustache` files to customize the generated code
   - Common files to customize: `api.mustache`, `model.mustache`, `modelGeneric.mustache`

3. **Configure the templates path**:
   - **In Visual Studio**: Set the **Templates Path** option
   - **In CLI**: Use the `--templates-path` argument
   - **In config file**: Set the `templatesDir` property

4. **Path resolution**:
   - Absolute paths are used as-is
   - Relative paths are resolved relative to the OpenAPI specification file location

#### Example Custom Template Usage

```bash
# CLI example
rapicgen csharp openapi swagger.json MyNamespace output.cs --templates-path ./custom-templates
```

In Visual Studio settings or configuration file:
```json
{
  "templatesDir": "./custom-templates"
}
```

### Multiple File Generation

For SDK-style projects, you can generate separate files for each API and model instead of a single merged file.

#### Benefits

- Better source control diff readability
- Easier to navigate large APIs
- Supports partial classes for customization
- Standard OpenAPI Generator output structure

#### How to Enable

**In Visual Studio Settings**:
- Set **Generate Multiple Files** to `true`

**In CLI**:
```bash
rapicgen csharp openapi swagger.json MyNamespace output.cs --generate-multiple-files
```

**In Configuration File**:
```json
{
  "netCoreProjectFile": true
}
```

#### Output Structure

Files are generated in the same directory as the OpenAPI specification:

```
YourProject/
├── swagger.json
├── Model/
│   ├── Pet.cs
│   ├── Order.cs
│   └── User.cs
├── Api/
│   ├── PetApi.cs
│   ├── StoreApi.cs
│   └── UserApi.cs
└── Client/
    ├── ApiClient.cs
    ├── ApiException.cs
    ├── ApiResponse.cs
    ├── Configuration.cs
    └── ExceptionFactory.cs
```

### Version Selection

The extension supports multiple versions of OpenAPI Generator, allowing you to:
- Use the latest features and bug fixes
- Lock to a specific version for consistency
- Test compatibility with different versions

#### Available Versions

| Version | Release Date | Notes |
|---------|--------------|-------|
| 7.18.0 | Latest | Default version |
| 7.17.0 | - | - |
| 7.16.0 | - | - |
| 7.15.0 | - | - |
| 7.14.0 | - | - |
| 7.13.0 | - | - |
| 7.12.0 | - | Introduces TokenCookieContainer to avoid conflicts |
| 7.11.0 | - | - |
| 7.10.0 | - | - |
| 7.9.0 | - | - |
| 7.8.0 | - | - |
| 7.7.0 | - | Minimum supported version |

#### Selecting a Version

**In Visual Studio Settings**:
- Set the **Version** dropdown to your preferred version

**In CLI**:
```bash
rapicgen csharp openapi swagger.json MyNamespace output.cs --version 7.18.0
```

### JAR File Management

The extension automatically downloads and caches the OpenAPI Generator JAR files:

- **Cache Location**: User's TEMP directory
- **Auto-download**: JAR files are downloaded on first use
- **Custom JAR**: You can specify a custom JAR file path in Visual Studio settings (Tools → Options → REST API Client Code Generator → General)

## CLI Usage

The OpenAPI Generator is available through the `rapicgen` command-line tool.

### Installation

```bash
dotnet tool install --global rapicgen
```

### Basic Command Structure

```bash
rapicgen csharp openapi <swaggerFile> <namespace> <outputFile> [options]
```

### Arguments

| Argument | Description |
|----------|-------------|
| `swaggerFile` | Path to the OpenAPI specification file (JSON or YAML) |
| `namespace` | Default namespace for generated code |
| `outputFile` | Output filename for the generated code |

### Options

| Option | Description |
|--------|-------------|
| `--emit-default-value` | Emit default values in serialization (default: true) |
| `--method-argument` | Use optional method arguments (default: true) |
| `--generate-property-changed` | Generate PropertyChanged notifications |
| `--use-collection` | Use Collection instead of List |
| `--use-datetimeoffset` | Use DateTimeOffset for date-time properties |
| `--target-framework <framework>` | Target framework (default: NetStandard21) |
| `--custom-additional-properties-props <props>` | Custom additional properties |
| `--skip-form-model` | Skip form parameter models (default: true) |
| `--templates-path <path>` | Path to custom Mustache templates |
| `--use-configuration-file` | Use configuration file if present (default: true) |
| `--generate-multiple-files` | Generate multiple files instead of one merged file |
| `--version <version>` | OpenAPI Generator version to use |
| `--http-user-agent <agent>` | User-Agent header for HTTP requests |

### Examples

#### Basic Generation

```bash
rapicgen csharp openapi swagger.json GeneratedCode ./OpenApiClient.cs
```

#### With Custom Options

```bash
rapicgen csharp openapi swagger.json MyApi.Client ./Client.cs \
  --use-datetimeoffset \
  --target-framework net8.0 \
  --generate-property-changed \
  --version 7.18.0
```

#### Using Configuration File

```bash
rapicgen csharp openapi swagger.json MyApi.Client ./Client.cs \
  --use-configuration-file
```

#### Multiple Files for SDK Project

```bash
rapicgen csharp openapi swagger.json MyApi.Client ./Client.cs \
  --generate-multiple-files \
  --target-framework net8.0
```

#### With Custom Templates

```bash
rapicgen csharp openapi swagger.json MyApi.Client ./Client.cs \
  --templates-path ./my-templates \
  --target-framework net8.0
```

#### Custom Additional Properties

```bash
rapicgen csharp openapi swagger.json MyApi.Client ./Client.cs \
  --custom-additional-properties-props "--additional-properties packageName=MyClient,validatable=true,equatable=true"
```

## Supported Versions

### OpenAPI Specification Versions

- **OpenAPI 3.0.x**: Full support
- **OpenAPI 3.1.x**: Full support
- **Swagger 2.0**: Full support

### Input Formats

- **JSON**: `.json` files
- **YAML**: `.yaml` and `.yml` files

### .NET Framework Targets

- **.NET Standard**: 1.3, 1.4, 1.5, 1.6, 2.0, 2.1
- **.NET Framework**: 4.7, 4.8
- **.NET**: 6.0, 7.0, 8.0, 9.0

## Generated Code Dependencies

The code generated by OpenAPI Generator depends on the following NuGet packages (automatically added by the extension):

### Required Packages

```xml
<PackageReference Include="RestSharp" Version="112.0.0" />
<PackageReference Include="JsonSubTypes" Version="2.0.1" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
```

### Optional Package

```xml
<PackageReference Include="Polly" Version="8.6.5" />
```

Note: Polly is included as a dependency of RestSharp but can be used separately for retry policies.

### Using the Generated Code

For detailed examples of how to use the generated code, see the **[Generated Code Usage Guide](GeneratedCodeUsage.md#openapi-generator)**.

Basic usage example:

```csharp
using System;
using System.Threading.Tasks;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

var config = new Configuration
{
    BasePath = "https://petstore.swagger.io/v2"
};

var petApi = new PetApi(config);
var pet = await petApi.GetPetByIdAsync(1);
Console.WriteLine($"Pet: {pet.Name}");
```

## Troubleshooting

### Common Issues

#### Java Not Found

**Error**: "java.exe not found" or "JAVA_HOME is not set"

**Solutions**:
1. Install Java Runtime Environment (JRE) 8 or higher
2. Set the `JAVA_HOME` environment variable to your Java installation directory
3. Configure a custom Java path:
   - Go to **Tools** → **Options** → **REST API Client Code Generator** → **General**
   - Set the **Java Path** field

#### JAR File Download Failed

**Error**: "Failed to download OpenAPI Generator JAR"

**Solutions**:
1. Check your internet connection
2. Verify firewall/proxy settings
3. Manually download the JAR from [OpenAPI Generator Releases](https://github.com/OpenAPITools/openapi-generator/releases)
4. Configure the custom JAR path in Visual Studio settings

#### Generated Code Won't Compile

**Error**: Various compilation errors in generated code

**Solutions**:
1. Ensure all required NuGet packages are installed (extension usually handles this automatically)
2. Verify the target framework setting matches your project
3. Check for naming conflicts with existing code
4. Try a different OpenAPI Generator version
5. Review the OpenAPI specification for errors using [Swagger Editor](https://editor.swagger.io/)

#### Configuration File Not Being Used

**Error**: Settings in `.config.json` or `.config.yaml` are ignored

**Solutions**:
1. Verify the file is named correctly (must match the specification filename)
2. Ensure **Use Configuration File** is enabled in settings
3. Check that the configuration file is valid JSON or YAML
4. Verify the file is in the same directory as the OpenAPI specification

#### Code Not Regenerating Automatically

**Error**: Changes to specification file don't trigger code regeneration

**Solutions**:
1. Verify **Custom Tool** is set to `OpenApiCodeGenerator` in file properties
2. Manually trigger generation by right-clicking the spec file and selecting **Generate Code**
3. Check the **Error List** window for any generation errors
4. Rebuild the project

#### Multiple Files Not Generated

**Error**: Only a single file is generated even with `GenerateMultipleFiles` enabled

**Solutions**:
1. Ensure your project is using the SDK-style project format (`.csproj`)
2. Verify the project target framework is set correctly
3. Check the output directory has write permissions

### Getting Help

For additional support:

1. **Extension Issues**: [GitHub Issues](https://github.com/christianhelle/apiclientcodegen/issues)
2. **OpenAPI Generator Issues**: [OpenAPI Generator GitHub](https://github.com/OpenAPITools/openapi-generator/issues)
3. **Documentation**: [OpenAPI Generator Docs](https://openapi-generator.tech/docs/generators/csharp)
4. **Community**: [Stack Overflow - openapi-generator tag](https://stackoverflow.com/questions/tagged/openapi-generator)

### Debug Mode

Enable verbose logging to troubleshoot issues:

**In CLI**:
```bash
rapicgen csharp openapi swagger.json MyNamespace output.cs --verbose
```

**In Visual Studio**:
- Check the **Output** window
- Select **Show output from: REST API Client Code Generator**
- Generation commands and output will be displayed

## Additional Resources

- **[OpenAPI Generator Documentation](https://openapi-generator.tech/)**
- **[OpenAPI Generator C# Client](https://openapi-generator.tech/docs/generators/csharp)**
- **[OpenAPI Specification](https://swagger.io/specification/)**
- **[Generated Code Usage Examples](GeneratedCodeUsage.md#openapi-generator)**
- **[REST API Client Code Generator Repository](https://github.com/christianhelle/apiclientcodegen)**

## Frequently Asked Questions

### Can I customize the generated code?

Yes, in several ways:
1. Use configuration files to control generation options
2. Create custom Mustache templates for complete control
3. Use partial classes to extend generated code without modifying it

### What's the difference between single file and multiple files mode?

- **Single File**: All generated code is merged into one file (default)
  - Pros: Simple, one file to manage
  - Cons: Large files, harder to navigate, all code regenerated on changes
  
- **Multiple Files**: Separate files for each API and model
  - Pros: Better organization, easier navigation, partial regeneration
  - Cons: More files to manage, requires SDK-style project

### How do I update to a newer OpenAPI Generator version?

1. Change the **Version** setting in Visual Studio options, or
2. Specify `--version` in CLI commands, or
3. The extension will automatically use the latest version if not specified

### Can I use this with .NET Framework projects?

Yes, set the appropriate target framework:
- For .NET Framework 4.7: Use `net47`
- For .NET Framework 4.8: Use `net48`

### How do I handle authentication in generated clients?

The generated code supports various authentication methods through the `Configuration` class. See the [Generated Code Usage Guide](GeneratedCodeUsage.md#openapi-generator) for examples.

### Can I generate TypeScript clients?

Yes, the extension also supports TypeScript generation. Use:
```bash
rapicgen typescript <generator> <swaggerFile> <outputPath>
```

Available TypeScript generators: Angular, Aurelia, Axios, Fetch, Inversify, jQuery, NestJS, Node, ReduxQuery, RxJS.

---

**Need more help?** Visit the [project repository](https://github.com/christianhelle/apiclientcodegen) or [open an issue](https://github.com/christianhelle/apiclientcodegen/issues).
