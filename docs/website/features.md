---
layout: page
title: "Features"
description: "Discover the powerful features of REST API Client Code Generator"
---

## Visual Studio Integration

REST API Client Code Generator provides seamless integration across all major Visual Studio platforms with consistent functionality and user experience.

<div class="screenshot">
  <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/add-new-menu.png" alt="Add new REST API Client menu option" />
  <p><em>Add a new REST API Client directly from the Visual Studio Add menu</em></p>
</div>

<div class="screenshot">
  <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/add-new-dialog.png" alt="REST API Client configuration dialog" />
  <p><em>Configure your OpenAPI specification URL, namespace, and code generator</em></p>
</div>

### Supported Platforms
- **Visual Studio 2017, 2019, 2022** - Full integration with all modern Visual Studio versions
- **Visual Studio Code** - Context menu integration for JSON and YAML files  
- **Visual Studio for Mac** - Complete feature parity across platforms

<div class="screenshot">
  <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/solution-explorer-context-menu.jpg" alt="Solution Explorer context menu options" />
  <p><em>Right-click context menu options in Solution Explorer for quick access to code generation</em></p>
</div>

## Code Generators

### Custom Tools
Custom tools let you associate a tool with an item in a project and run that tool whenever the file is saved, enabling automatic code regeneration when your OpenAPI specifications change.

<div class="cards">
  <div class="card">
    <h3>NSwagCodeGenerator</h3>
    <p><strong>Version:</strong> v14.4.0</p>
    <p>Generates a single file C# REST API Client using the NSwag.CodeGeneration.CSharp NuGet package. Produces clean, strongly-typed code with full async support and comprehensive error handling.</p>
    <div class="config-section">
      <h4>Key Features:</h4>
      <ul>
        <li>Strongly-typed client generation</li>
        <li>Full async/await support</li>
        <li>Comprehensive exception handling</li>
        <li>Integration with ASP.NET Core</li>
      </ul>
    </div>
  </div>

  <div class="card">
    <h3>OpenApiCodeGenerator</h3>
    <p><strong>Version:</strong> v7.14.0</p>
    <p>Generates a single file C# REST API Client using OpenAPI Generator. The output file merges all generated files using the command: <code>generate -g csharp --input-spec [swagger file] --output [output file] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite</code></p>
    <div class="config-section">
      <h4>Configuration Options:</h4>
      <ul>
        <li>Generate Multiple Files support</li>
        <li>Extensive customization properties</li>
        <li>Template customization</li>
        <li>Multiple output formats</li>
      </ul>
    </div>
  </div>

  <div class="card">
    <h3>KiotaCodeGenerator</h3>
    <p><strong>Version:</strong> v1.27.0</p>
    <p>Generates a single file C# REST API Client using Microsoft Kiota. The output merges files generated with: <code>generate -l CSharp -d [swagger file] -o [output file] -n [namespace]</code></p>
    <div class="config-section">
      <h4>Microsoft Integration:</h4>
      <ul>
        <li>Microsoft Graph API support</li>
        <li>Azure authentication patterns</li>
        <li>Modern .NET practices</li>
        <li>Strong typing and validation</li>
      </ul>
    </div>
  </div>

  <div class="card">
    <h3>SwaggerCodeGenerator</h3>
    <p><strong>Version:</strong> v3.0.34</p>
    <p>Generates a single file C# REST API Client using Swagger Codegen CLI. Uses the command: <code>generate -l csharp --input-spec [swagger file] --output [output file] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite</code></p>
    <div class="config-section">
      <h4>Proven Reliability:</h4>
      <ul>
        <li>Extensive language support</li>
        <li>Legacy project compatibility</li>
        <li>Established workflow integration</li>
        <li>Community-driven templates</li>
      </ul>
    </div>
  </div>

  <div class="card">
    <h3>AutoRestCodeGenerator</h3>
    <p><strong>Version:</strong> v3.0.0-beta.20210504.2 (v3), v2.0.4417 (v2)</p>
    <p>Generates a single file C# REST API Client using AutoRest. Equivalent to: <code>--csharp --input-file=[swagger file] --output-file=[output file] --namespace=[namespace] --add-credentials</code></p>
    <div class="config-section">
      <h4>Enterprise Features:</h4>
      <ul>
        <li>Azure service optimization</li>
        <li>Advanced authentication</li>
        <li>Retry policies and timeout configuration</li>
        <li>Enterprise-grade reliability</li>
      </ul>
    </div>
  </div>

  <div class="card">
    <h3>RefitterCodeGenerator</h3>
    <p><strong>Version:</strong> v1.6.0</p>
    <p>Generates a single file C# REST API Client interface for Refit using Refitter.Core. Combines a Refit interface generated by Refitter with contracts generated using NSwag.CodeGeneration.CSharp.</p>
    <div class="config-section">
      <h4>Reactive Patterns:</h4>
      <ul>
        <li>Refit interface generation</li>
        <li>Reactive programming support</li>
        <li>Clean interface design</li>
        <li>Comprehensive testing capabilities</li>
      </ul>
    </div>
  </div>
</div>

## Configuration File Support

<div class="screenshot-gallery">
  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/nswagstudio-context-menu.jpg" alt="NSwag Studio context menu" />
    <p><em>Generate code from NSwag Studio .nswag configuration files</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/refitter-command.png" alt="Refitter configuration" />
    <p><em>Refitter .refitter settings file integration</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/generate-kiota-output.png" alt="Kiota code generation" />
    <p><em>Microsoft Kiota kiota-lock.json configuration support</em></p>
  </div>
</div>

Generate code using configuration files:
- **`.nswag`** configuration files from NSwagStudio by including it in the project and using the **Generate NSwag Studio output** context menu
- **`.refitter`** settings files from Refitter by including it in the project and using the **Generate Refitter output** context menu  
- **`kiota-lock.json`** configuration files from Microsoft Kiota by including it in the project and using the **Generate Kiota output** context menu

## Auto-Generation Features

<div class="feature-highlight">
  <h3>🔄 Automatic Code Regeneration</h3>
  <p>Custom tools automatically regenerate client code whenever your OpenAPI specification files change, keeping your code always in sync with your API.</p>
</div>

- **Auto-updating** of generated code file when changes are made to the OpenAPI specification JSON or YAML file
- **Custom namespace** definition for the generated file
- **Multiple file generation** support for OpenAPI Generator and Kiota (configurable in settings)
- **Context menu integration** for quick access to generation options

## Advanced Configuration

### Settings and Customization

The extension provides extensive configuration options for each code generator, accessible through the Visual Studio Tools menu.

<div class="screenshot-gallery">
  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/options-general.png" alt="General options" />
    <p><em>General settings for Java, NPM, and tool paths</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/options-nswag.png" alt="NSwag options" />
    <p><em>NSwag-specific configuration options</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/options-openapigenerator.png" alt="OpenAPI Generator options" />
    <p><em>OpenAPI Generator customization settings</em></p>
  </div>
</div>

### Customizable Settings

<div class="config-section">
  <h4>Environment Configuration</h4>
  <ul>
    <li><strong>Java path configuration</strong> - Specify custom Java installation paths</li>
    <li><strong>NPM and Node.js paths</strong> - Configure custom Node.js environments</li>
    <li><strong>JAR file locations</strong> - Use existing Swagger Codegen CLI and OpenAPI Generator JAR files</li>
  </ul>
</div>

<div class="config-section">
  <h4>Generator-Specific Options</h4>
  <ul>
    <li><strong>AutoRest Customization</strong> - Support for all C# generator settings, custom authentication schemes, retry policies</li>
    <li><strong>NSwag Customization</strong> - Full access to NSwag package properties, custom serialization settings, advanced type mapping</li>
    <li><strong>OpenAPI Generator Options</strong> - Access to additional properties, template customization, multiple output formats</li>
  </ul>
</div>

<div class="screenshot-gallery">
  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/options-autorest.png" alt="AutoRest options" />
    <p><em>AutoRest generator configuration</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/refitter-options.png" alt="Refitter options" />
    <p><em>Refitter customization settings</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/options-kiota.png" alt="Kiota options" />
    <p><em>Microsoft Kiota configuration options</em></p>
  </div>
</div>

## Cross-Platform Support

### Visual Studio Code Integration

<div class="screenshot-gallery">
  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/vscode-command-palette.png" alt="VS Code command palette" />
    <p><em>VS Code command palette integration</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/vscode-context-menu.png" alt="VS Code context menu" />
    <p><em>Context menu support for C# generation in VS Code</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/vscode-context-menu-typescript.png" alt="VS Code TypeScript context menu" />
    <p><em>TypeScript code generation support in VS Code</em></p>
  </div>
</div>

### Visual Studio for Mac

<div class="screenshot-gallery">
  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/vsmac-add-new-menu.png" alt="VS Mac add new menu" />
    <p><em>Visual Studio for Mac "Add New" menu integration</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/vsmac-add-new-dialog.png" alt="VS Mac add new dialog" />
    <p><em>OpenAPI specification configuration dialog on macOS</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/vsmac-generate-code.png" alt="VS Mac generate code" />
    <p><em>Code generation context menu in Visual Studio for Mac</em></p>
  </div>
</div>

## Dependencies and Requirements

### Automatic Dependency Management
The Visual Studio Extension automatically adds the required NuGet packages that the generated code depends on:

<div class="cards">
  <div class="card">
    <h3>NSwag Dependencies</h3>
    <ul>
      <li><strong>Newtonsoft.Json</strong> - JSON serialization</li>
    </ul>
  </div>

  <div class="card">
    <h3>OpenAPI Generator Dependencies</h3>
    <ul>
      <li><strong>RestSharp</strong> - HTTP client library</li>
      <li><strong>JsonSubTypes</strong> - Polymorphic JSON serialization</li>
      <li><strong>Polly</strong> - Resilience and fault-handling library</li>
      <li><strong>Newtonsoft.Json</strong> - JSON serialization</li>
    </ul>
  </div>

  <div class="card">
    <h3>Microsoft Kiota Dependencies</h3>
    <ul>
      <li><strong>Microsoft.Kiota.Abstractions</strong></li>
      <li><strong>Microsoft.Kiota.Http.HttpClientLibrary</strong></li>
      <li><strong>Microsoft.Kiota.Serialization.*</strong> packages</li>
      <li><strong>Microsoft.Kiota.Authentication.Azure</strong></li>
      <li><strong>Azure.Identity</strong></li>
    </ul>
  </div>

  <div class="card">
    <h3>Refitter Dependencies</h3>
    <ul>
      <li><strong>Refit</strong> - REST library for .NET</li>
    </ul>
  </div>

  <div class="card">
    <h3>Swagger Codegen CLI Dependencies</h3>
    <ul>
      <li><strong>RestSharp</strong> - HTTP client library</li>
      <li><strong>JsonSubTypes</strong> - Polymorphic JSON serialization</li>
    </ul>
  </div>

  <div class="card">
    <h3>AutoRest Dependencies</h3>
    <ul>
      <li><strong>Microsoft.Rest.ClientRuntime</strong></li>
      <li><strong>Newtonsoft.Json</strong> - JSON serialization</li>
    </ul>
  </div>
</div>

### Runtime Requirements
- **Java Runtime Environment** - Required for Swagger Codegen CLI and OpenAPI Generator
- **NPM** - Required for AutoRest and NSwag CLI installations
- **.NET 7.0** - Required for Microsoft Kiota

<div class="feature-highlight">
  <h3>📦 On-Demand Installation</h3>
  <p>The Swagger Codegen CLI and OpenAPI Generator are distributed as JAR files and downloaded on demand. AutoRest is installed via NPM as a global tool, and Microsoft Kiota is installed as a .NET Tool - all automatically managed for you.</p>
</div>

## Analytics and Error Reporting

<div class="screenshot">
  <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/support-key.png" alt="Analytics configuration" />
  <p><em>Analytics and error reporting configuration options</em></p>
</div>

- **Anonymous usage tracking** - Helps improve the tool through Exceptionless and Azure Application Insights
- **Error reporting** - Automatic error collection to improve stability and user experience
- **Privacy-focused** - Uses secure hash of username@host for anonymous identification
- **Configurable** - Can be completely disabled through the settings panel

## Custom Tool Examples

<div class="screenshot-gallery">
  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/nswagcodegenerator-custom-tool.jpg" alt="NSwag custom tool" />
    <p><em>NSwag custom tool configuration in file properties</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/openapicodegenerator-custom-tool.jpg" alt="OpenAPI Generator custom tool" />
    <p><em>OpenAPI Generator custom tool setup</em></p>
  </div>

  <div class="screenshot">
    <img src="https://raw.githubusercontent.com/christianhelle/apiclientcodegen/master/images/swaggercodegenerator-custom-tool.jpg" alt="Swagger Codegen custom tool" />
    <p><em>Swagger Codegen CLI custom tool configuration</em></p>
  </div>
</div>