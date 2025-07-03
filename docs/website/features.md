---
layout: page
title: "Features"
description: "Discover the powerful features of REST API Client Code Generator"
---

## Visual Studio Integration

- **Supports Visual Studio 2017, 2019, 2022** - Full integration with all modern Visual Studio versions
- **Visual Studio Code support** - Context menu integration for JSON and YAML files
- **Visual Studio for Mac** - Complete feature parity across platforms

## Code Generators

### Custom Tools
Custom tools let you associate a tool with an item in a project and run that tool whenever the file is saved.

- **NSwagCodeGenerator** - Generates a single file C# REST API Client using the NSwag.CodeGeneration.CSharp NuGet package v14.4.0
- **OpenApiCodeGenerator** - Generates a single file C# REST API Client using OpenAPI Generator v7.14.0
- **KiotaCodeGenerator** - Generates a single file C# REST API Client using Microsoft Kiota v1.27.0
- **SwaggerCodeGenerator** - Generates a single file C# REST API Client using Swagger Codegen CLI v3.0.34
- **AutoRestCodeGenerator** - Generates a single file C# REST API Client using AutoRest v3.0.0-beta.20210504.2
- **RefitterCodeGenerator** - Generates a single file C# REST API Client interface for Refit using Refitter.Core v1.6.0

### Configuration File Support

Generate code using configuration files:
- `.nswag` configuration files from NSwagStudio
- `.refitter` settings files from Refitter
- `kiota-lock.json` configuration files from Microsoft Kiota

## Auto-Generation Features

- **Auto-updating** of generated code file when changes are made to the OpenAPI specification JSON or YAML file
- **Custom namespace** definition for the generated file
- **Multiple file generation** support for OpenAPI Generator and Kiota
- **Context menu integration** for quick access to generation options

## Advanced Configuration

### Customizable Settings
- **Java path configuration** - Specify custom Java installation paths
- **NPM and Node.js paths** - Configure custom Node.js environments
- **JAR file locations** - Use existing Swagger Codegen CLI and OpenAPI Generator JAR files
- **Generator-specific options** - Detailed configuration for each code generator

### AutoRest Customization
- Support for all C# generator settings that the AutoRest CLI tool provides
- Custom authentication schemes
- Retry policies and timeout configuration

### NSwag Customization
- Full access to properties exposed by the NSwag NuGet package
- Custom serialization settings
- Advanced type mapping options

### OpenAPI Generator Options
- Access to additional optional properties that the OpenAPI Generator CLI tool provides
- Template customization support
- Multiple output format options

## Dependencies and Requirements

### Automatic Dependency Management
The Visual Studio Extension automatically adds the required NuGet packages that the generated code depends on:

**NSwag dependencies:**
- Newtonsoft.Json

**OpenAPI Generator dependencies:**
- RestSharp
- JsonSubTypes
- Polly
- Newtonsoft.Json

**Microsoft Kiota dependencies:**
- Microsoft.Kiota.Abstractions
- Microsoft.Kiota.Http.HttpClientLibrary
- Microsoft.Kiota.Serialization.Form
- Microsoft.Kiota.Serialization.Text
- Microsoft.Kiota.Serialization.Json
- Microsoft.Kiota.Serialization.Multipart
- Microsoft.Kiota.Authentication.Azure
- Azure.Identity

**Refitter dependencies:**
- Refit

**Swagger Codegen CLI dependencies:**
- RestSharp
- JsonSubTypes

**AutoRest dependencies:**
- Microsoft.Rest.ClientRuntime
- Newtonsoft.Json

### Runtime Requirements
- **Java Runtime Environment** - Required for Swagger Codegen CLI and OpenAPI Generator
- **NPM** - Required for AutoRest and NSwag CLI installations
- **.NET 7.0** - Required for Microsoft Kiota

## Analytics and Error Reporting

- **Anonymous usage tracking** - Helps improve the tool through Exceptionless and Azure Application Insights
- **Error reporting** - Automatic error collection to improve stability
- **Privacy-focused** - Uses secure hash of username@host for anonymous identification
- **Configurable** - Can be disabled through the settings panel