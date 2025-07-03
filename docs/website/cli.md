---
layout: page
title: "CLI Tool"
description: "Cross-platform command line tool for generating REST API clients"
---

The REST API Client Code Generator includes a powerful cross-platform command line tool that implements all the features available in the Visual Studio extensions.

## Installation

Install the tool as a .NET global tool:

```bash
dotnet tool install --global rapicgen
```

To install a specific version:

```bash
dotnet tool install --global rapicgen --version 1.28.0
```

## Requirements

- **.NET 6.0 runtime** or later
- **Java Runtime Environment** (for Swagger Codegen CLI and OpenAPI Generator)
- **NPM** (for AutoRest and NSwag CLI installations)

## Basic Usage

The tool is launched from any command line interface by calling `rapicgen`. When called without arguments, it displays help information:

```
Usage: rapicgen [command] [options]

Options:
  -v|--verbose       Show verbose output
  -?|-h|--help       Show help information.

Commands:
  csharp             Generate C# API clients
  jmeter             Generate Apache JMeter test plans
  openapi-generator  Generate code using OpenAPI Generator (v7.14.0).
                     See supported generators at https://openapi-generator.tech/docs/generators/
  typescript         Generate TypeScript API clients

Run 'rapicgen [command] -?|-h|--help' for more information about a command.
```

## C# Code Generation

Generate C# API clients using various generators:

```bash
rapicgen csharp [generator] [options] <swaggerFile> <namespace> <outputFile>
```

### Available C# Generators

Get help for C# generators:

```bash
rapicgen csharp -?
```

Output:
```
Generate C# API clients

Usage: rapicgen csharp [command] [options]

Options:
  -?|-h|--help  Show help information.

Commands:
  autorest      AutoRest (v3.0.0-beta.20210504.2)
  kiota         Microsoft Kiota (v1.27.0)
  nswag         NSwag (v14.4.0)
  openapi       OpenAPI Generator (v7.14.0)
  refitter      Refitter (v1.6.0)
  swagger       Swagger Codegen CLI (v3.0.34)
```

### Examples

**AutoRest:**
```bash
rapicgen csharp autorest swagger.json MyNamespace ./AutoRestOutput.cs
```

**Microsoft Kiota:**
```bash
rapicgen csharp kiota swagger.json MyNamespace ./KiotaOutput.cs
```

**NSwag:**
```bash
rapicgen csharp nswag swagger.json MyNamespace ./NSwagOutput.cs
```

**OpenAPI Generator:**
```bash
rapicgen csharp openapi swagger.json MyNamespace ./OpenApiOutput.cs
```

**Refitter:**
```bash
rapicgen csharp refitter swagger.json MyNamespace ./RefitterOutput.cs
```

**Swagger Codegen CLI:**
```bash
rapicgen csharp swagger swagger.json MyNamespace ./SwaggerOutput.cs
```

## TypeScript Generation

Generate TypeScript API clients for various frameworks:

```bash
rapicgen typescript [generator] <swaggerFile> [outputPath]
```

### Available TypeScript Generators

```bash
rapicgen typescript -?
```

Output:
```
Generate TypeScript API client

Usage: rapicgen typescript [options] <generator> <swaggerFile> <outputPath>

Arguments:
  generator         The tech stack to use for the generated client library
                    Allowed values are: Angular, Aurelia, Axios, Fetch, Inversify, 
                    JQuery, NestJS, Node, ReduxQuery, Rxjs.
                    Default value is: Angular.
  swaggerFile       Path to the Swagger / Open API specification file
  outputPath        Output folder to write the generated code to
                    Default value is: typescript-generated-code.

Options:
  -nl|--no-logging  Disables Analytics and Error Reporting
  -?|-h|--help      Show help information.
```

### Examples

**Angular:**
```bash
rapicgen typescript Angular swagger.json ./angular-client
```

**React with Axios:**
```bash
rapicgen typescript Axios swagger.json ./react-client
```

**Node.js:**
```bash
rapicgen typescript Node swagger.json ./node-client
```

## JMeter Test Generation

Generate Apache JMeter test plans from OpenAPI specifications:

```bash
rapicgen jmeter swagger.json
```

This creates a JMeter test plan (.jmx file) that includes:
- Test scenarios for all API endpoints
- Parameterized requests
- Response validation
- Load testing configuration

## OpenAPI Generator Integration

Use OpenAPI Generator directly with extended options:

```bash
rapicgen openapi-generator [generator] [options] <inputSpec> <outputDir>
```

This provides access to all OpenAPI Generator features and languages. See the [OpenAPI Generator documentation](https://openapi-generator.tech/docs/generators/) for all available generators.

## Examples with Real APIs

### Swagger Petstore API

Download the Swagger Petstore specification:

```bash
# PowerShell
Invoke-WebRequest -Uri https://petstore.swagger.io/v3/swagger.json -OutFile swagger.json

# curl
curl https://petstore.swagger.io/v3/swagger.json -o swagger.json
```

Generate clients:

```bash
# C# with NSwag
rapicgen csharp nswag swagger.json PetStore.Client ./PetStoreClient.cs

# TypeScript for Angular
rapicgen typescript Angular swagger.json ./petstore-angular

# JMeter test plan
rapicgen jmeter swagger.json
```

## CI/CD Integration

The CLI tool is perfect for CI/CD pipelines:

### GitHub Actions Example

```yaml
name: Generate API Clients

on:
  push:
    paths:
      - 'api/swagger.json'

jobs:
  generate:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '6.0.x'
        
    - name: Install rapicgen
      run: dotnet tool install --global rapicgen
      
    - name: Generate C# client
      run: rapicgen csharp nswag api/swagger.json MyApi.Client src/ApiClient.cs
      
    - name: Commit generated code
      run: |
        git config --local user.email "action@github.com"
        git config --local user.name "GitHub Action"
        git add src/ApiClient.cs
        git commit -m "Update generated API client" || exit 0
        git push
```

### Azure DevOps Example

```yaml
trigger:
  paths:
    include:
    - api/swagger.json

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: UseDotNet@2
  inputs:
    version: '6.0.x'

- script: dotnet tool install --global rapicgen
  displayName: 'Install rapicgen'

- script: rapicgen csharp nswag api/swagger.json MyApi.Client src/ApiClient.cs
  displayName: 'Generate API client'

- script: |
    git config user.email "build@company.com"
    git config user.name "Azure DevOps"
    git add src/ApiClient.cs
    git commit -m "Update generated API client"
    git push origin HEAD:$(Build.SourceBranchName)
  displayName: 'Commit generated code'
```

## Options and Configuration

### Verbose Output

Use the `-v` or `--verbose` flag for detailed output:

```bash
rapicgen -v csharp nswag swagger.json MyNamespace ./output.cs
```

### Disable Analytics

Use the `-nl` or `--no-logging` flag to disable analytics:

```bash
rapicgen typescript -nl Angular swagger.json ./output
```

## Troubleshooting

### Common Issues

1. **Java not found**: Ensure Java is installed and in your PATH
2. **NPM not found**: Install Node.js and NPM
3. **Permission errors**: Run with appropriate permissions for file creation
4. **Network issues**: Check firewall and proxy settings for dependency downloads

### Getting Help

For detailed help on any command:

```bash
rapicgen [command] [subcommand] -?
```

Example:
```bash
rapicgen csharp autorest -?
```