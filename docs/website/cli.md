---
layout: page
title: "CLI Tool"
description: "Cross-platform command line tool for generating REST API clients"
---

The REST API Client Code Generator includes a powerful cross-platform command line tool that implements all the features available in the Visual Studio extensions. Perfect for automation, CI/CD pipelines, and developers who prefer command-line workflows.

<div class="feature-highlight">
  <h3>⚡ Cross-Platform Power</h3>
  <p>One tool, multiple platforms - Windows, macOS, and Linux support with identical functionality across all environments.</p>
</div>

## Installation

Install the tool as a .NET global tool with a single command:

<div class="cli-example">
  <h4>Fresh Installation</h4>
  <pre><code>dotnet tool install --global rapicgen</code></pre>
</div>

<div class="cli-example">
  <h4>Update to Latest Version</h4>
  <pre><code>dotnet tool update --global rapicgen</code></pre>
</div>

<div class="cli-example">
  <h4>Install Specific Version</h4>
  <pre><code>dotnet tool install --global rapicgen --version 1.28.0</code></pre>
</div>

<div class="cli-example">
  <h4>Verify Installation</h4>
  <pre><code>rapicgen --help</code></pre>
</div>

## System Requirements

<div class="config-section">
  <h4>Core Requirements</h4>
  <ul>
    <li><strong>.NET 6.0 runtime or later</strong> - Foundation for all operations</li>
    <li><strong>Java Runtime Environment</strong> - Required for Swagger Codegen CLI and OpenAPI Generator</li>
    <li><strong>NPM (Node.js)</strong> - Required for AutoRest and NSwag CLI installations</li>
    <li><strong>Internet connectivity</strong> - For downloading tools and dependencies on first use</li>
  </ul>
</div>

## Command Structure and Help

The tool provides comprehensive help at every level. When called without arguments, it displays the main help:

<div class="cli-example">
  <h4>Main Help Screen</h4>
  <pre><code>rapicgen --help</code></pre>
  <strong>Output:</strong>
  <pre><code>Usage: rapicgen [command] [options]

Options:
  -v|--verbose       Show verbose output
  -?|-h|--help       Show help information.

Commands:
  csharp             Generate C# API clients
  jmeter             Generate Apache JMeter test plans
  openapi-generator  Generate code using OpenAPI Generator (v7.14.0).
                     See supported generators at https://openapi-generator.tech/docs/generators/
  typescript         Generate TypeScript API clients

Run 'rapicgen [command] -?|-h|--help' for more information about a command.</code></pre>
</div>

### Getting Help for Specific Commands

<div class="cli-example">
  <h4>C# Generator Help</h4>
  <pre><code>rapicgen csharp --help</code></pre>
  <strong>Output:</strong>
  <pre><code>Generate C# API clients

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

Run 'csharp [command] -?|-h|--help' for more information about a command.</code></pre>
</div>

<div class="cli-example">
  <h4>Specific Generator Help</h4>
  <pre><code>rapicgen csharp autorest --help</code></pre>
  <strong>Output:</strong>
  <pre><code>Generate Swagger / Open API client using AutoRest

Usage: run autorest [options] &lt;swaggerFile&gt; &lt;namespace&gt; &lt;outputFile&gt;

Arguments:
  swaggerFile   Path to the Swagger / Open API specification file
  namespace     Default namespace to in the generated code
  outputFile    Output filename to write the generated code to. Default is the swaggerFile .cs

Options:
  -?|-h|--help  Show help information</code></pre>
</div>

## C# Code Generation

Generate C# API clients using any of the supported generators with consistent command patterns.

### NSwag - Clean and Strongly-Typed

<div class="cli-example">
  <h4>Basic NSwag Generation</h4>
  <pre><code>rapicgen csharp nswag swagger.json MyNamespace ./NSwagClient.cs</code></pre>
</div>

<div class="cli-example">
  <h4>Real-World Example - Swagger Petstore</h4>
  <pre><code># Download the Swagger Petstore specification
curl https://petstore.swagger.io/v2/swagger.json -o petstore.json

# Generate C# client using NSwag
rapicgen csharp nswag petstore.json PetStore.Client ./PetStoreClient.cs</code></pre>
</div>

### Microsoft Kiota - Modern .NET Client

<div class="cli-example">
  <h4>Kiota for Microsoft Graph</h4>
  <pre><code># Download Microsoft Graph OpenAPI spec
curl https://raw.githubusercontent.com/microsoftgraph/msgraph-metadata/master/openapi/v1.0/openapi.yaml -o graph.yaml

# Generate Kiota client
rapicgen csharp kiota graph.yaml Microsoft.Graph.Client ./GraphClient.cs</code></pre>
</div>

### OpenAPI Generator - Comprehensive Options

<div class="cli-example">
  <h4>OpenAPI Generator with Custom Configuration</h4>
  <pre><code># Generate with extensive configuration
rapicgen csharp openapi swagger.json MyApi.Client ./OpenApiClient.cs

# The tool automatically applies optimal settings:
# -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite</code></pre>
</div>

### Refitter - Refit Interface Generation

<div class="cli-example">
  <h4>Generate Refit Interfaces</h4>
  <pre><code># Perfect for reactive programming patterns
rapicgen csharp refitter swagger.json MyApi.Contracts ./RefitClient.cs</code></pre>
</div>

### AutoRest - Azure-Optimized

<div class="cli-example">
  <h4>AutoRest for Azure APIs</h4>
  <pre><code># Ideal for Azure services and enterprise APIs
rapicgen csharp autorest swagger.json Azure.MyService ./AzureClient.cs</code></pre>
</div>

### Swagger Codegen CLI - Proven Reliability

<div class="cli-example">
  <h4>Classic Swagger Generation</h4>
  <pre><code># Battle-tested code generation
rapicgen csharp swagger swagger.json Legacy.Client ./SwaggerClient.cs</code></pre>
</div>

## TypeScript Generation

Generate TypeScript clients for various frontend frameworks and platforms.

<div class="cli-example">
  <h4>TypeScript Help</h4>
  <pre><code>rapicgen typescript --help</code></pre>
  <strong>Supported Frameworks:</strong>
  <pre><code>Angular, Aurelia, Axios, Fetch, Inversify, JQuery, NestJS, Node, ReduxQuery, Rxjs</code></pre>
</div>

### Frontend Framework Examples

<div class="cli-example">
  <h4>Angular Client</h4>
  <pre><code># Generate Angular HTTP client
rapicgen typescript Angular swagger.json ./src/app/api-client

# Generates complete Angular service with HttpClient integration</code></pre>
</div>

<div class="cli-example">
  <h4>React with Axios</h4>
  <pre><code># Generate Axios-based client for React
rapicgen typescript Axios swagger.json ./src/api

# Perfect for React applications using Axios</code></pre>
</div>

<div class="cli-example">
  <h4>Node.js Backend</h4>
  <pre><code># Generate Node.js client for backend services
rapicgen typescript Node swagger.json ./lib/api-client

# Ideal for Node.js microservices communication</code></pre>
</div>

<div class="cli-example">
  <h4>Modern Fetch API</h4>
  <pre><code># Generate modern fetch-based client
rapicgen typescript Fetch swagger.json ./src/api-client

# Uses modern JavaScript fetch API</code></pre>
</div>

## JMeter Test Generation

Generate comprehensive Apache JMeter test plans from OpenAPI specifications.

<div class="cli-example">
  <h4>JMeter Test Plan Generation</h4>
  <pre><code># Generate JMeter test plan (.jmx file)
rapicgen jmeter swagger.json

# Creates comprehensive test scenarios with:
# - All API endpoints
# - Parameterized requests  
# - Response validation
# - Load testing configuration</code></pre>
</div>

<div class="feature-highlight">
  <h3>🧪 Complete Test Coverage</h3>
  <p>Generated JMeter plans include test scenarios for all endpoints, parameterized requests, response validation, and load testing configuration.</p>
</div>

## OpenAPI Generator Direct Access

Access the full power of OpenAPI Generator with all its supported languages and frameworks.

<div class="cli-example">
  <h4>Explore Available Generators</h4>
  <pre><code># See all available generators
rapicgen openapi-generator list

# Generate Java client
rapicgen openapi-generator java swagger.json ./java-client

# Generate Python client
rapicgen openapi-generator python swagger.json ./python-client

# Generate Go client
rapicgen openapi-generator go swagger.json ./go-client</code></pre>
</div>

## CI/CD Integration Examples

### GitHub Actions Workflow

<div class="cli-example">
  <h4>Complete GitHub Actions Workflow</h4>
  <pre><code>name: Generate API Clients

on:
  push:
    paths:
      - 'api-specs/*.json'
      - 'api-specs/*.yaml'
  pull_request:
    paths:
      - 'api-specs/*.json'
      - 'api-specs/*.yaml'

jobs:
  generate-clients:
    runs-on: ubuntu-latest
    
    strategy:
      matrix:
        generator: [nswag, openapi, kiota, refitter]
    
    steps:
    - name: Checkout repository
      uses: actions/checkout@v4
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '8.0.x'
    
    - name: Setup Java (for OpenAPI Generator)
      uses: actions/setup-java@v4
      with:
        distribution: 'temurin'
        java-version: '17'
    
    - name: Setup Node.js (for AutoRest)
      uses: actions/setup-node@v4
      with:
        node-version: '18'
    
    - name: Install rapicgen
      run: dotnet tool install --global rapicgen
    
    - name: Generate C# clients
      run: |
        for spec in api-specs/*.json; do
          filename=$(basename "$spec" .json)
          rapicgen csharp ${{ matrix.generator }} "$spec" "MyApi.Generated" "src/clients/${filename}-${{ matrix.generator }}.cs"
        done
    
    - name: Generate TypeScript clients
      run: |
        rapicgen typescript Angular api-specs/main.json ./typescript-clients/angular
        rapicgen typescript Axios api-specs/main.json ./typescript-clients/axios
    
    - name: Generate JMeter tests
      run: |
        for spec in api-specs/*.json; do
          rapicgen jmeter "$spec"
        done
    
    - name: Commit generated code
      run: |
        git config --local user.email "action@github.com"
        git config --local user.name "GitHub Action"
        git add src/clients/ typescript-clients/ *.jmx
        git diff --staged --quiet || git commit -m "Update generated API clients [skip ci]"
        git push</code></pre>
</div>

### Azure DevOps Pipeline

<div class="cli-example">
  <h4>Azure DevOps YAML Pipeline</h4>
  <pre><code>trigger:
  paths:
    include:
    - api-specs/*
    - openapi/*

pool:
  vmImage: 'ubuntu-latest'

variables:
  dotnetVersion: '8.0.x'
  javaVersion: '17'
  nodeVersion: '18.x'

stages:
- stage: GenerateClients
  displayName: 'Generate API Clients'
  jobs:
  - job: GenerateCode
    displayName: 'Generate Client Code'
    steps:
    
    - task: UseDotNet@2
      displayName: 'Setup .NET SDK'
      inputs:
        version: $(dotnetVersion)
    
    - task: JavaToolInstaller@0
      displayName: 'Setup Java'
      inputs:
        versionSpec: $(javaVersion)
        jdkArchitectureOption: 'x64'
        jdkSourceOption: 'PreInstalled'
    
    - task: NodeTool@0
      displayName: 'Setup Node.js'
      inputs:
        versionSpec: $(nodeVersion)
    
    - script: dotnet tool install --global rapicgen
      displayName: 'Install rapicgen CLI'
    
    - script: |
        # Generate multiple C# clients
        rapicgen csharp nswag openapi/petstore.json PetStore.NSwag ./src/PetStore.NSwag.cs
        rapicgen csharp kiota openapi/petstore.json PetStore.Kiota ./src/PetStore.Kiota.cs
        rapicgen csharp refitter openapi/petstore.json PetStore.Refitter ./src/PetStore.Refitter.cs
        
        # Generate TypeScript clients
        rapicgen typescript Angular openapi/petstore.json ./clients/angular
        rapicgen typescript Axios openapi/petstore.json ./clients/axios
        
        # Generate JMeter test plans
        rapicgen jmeter openapi/petstore.json
      displayName: 'Generate API clients'
    
    - script: |
        git config user.email "build@company.com"
        git config user.name "Azure DevOps Build"
        git add src/ clients/ *.jmx
        git diff --staged --quiet || git commit -m "Update generated API clients [skip ci]"
        git push origin HEAD:$(Build.SourceBranchName)
      displayName: 'Commit generated code'
      condition: and(succeeded(), eq(variables['Build.SourceBranch'], 'refs/heads/main'))</code></pre>
</div>

### Docker Integration

<div class="cli-example">
  <h4>Dockerfile for API Client Generation</h4>
  <pre><code>FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Java for OpenAPI Generator and Swagger Codegen
RUN apt-get update && apt-get install -y openjdk-17-jdk

# Install Node.js and NPM for AutoRest
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs

# Install rapicgen
RUN dotnet tool install --global rapicgen

# Set environment variables
ENV PATH="$PATH:/root/.dotnet/tools"
ENV JAVA_HOME="/usr/lib/jvm/java-17-openjdk-amd64"

WORKDIR /workspace

# Copy OpenAPI specifications
COPY api-specs/ ./api-specs/

# Generate clients
RUN rapicgen csharp nswag api-specs/main.json MyApi.Client ./src/ApiClient.cs && \
    rapicgen typescript Angular api-specs/main.json ./clients/angular && \
    rapicgen jmeter api-specs/main.json

# Production stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /workspace/src/ ./generated/
COPY --from=build /workspace/clients/ ./clients/</code></pre>
</div>

## Advanced Usage Patterns

### Batch Processing Multiple APIs

<div class="cli-example">
  <h4>Process Multiple OpenAPI Specifications</h4>
  <pre><code>#!/bin/bash
# Batch process multiple API specifications

# Define APIs and their configurations
declare -A apis=(
    ["petstore"]="https://petstore.swagger.io/v2/swagger.json"
    ["jsonplaceholder"]="https://jsonplaceholder.typicode.com/openapi.json"
    ["httpbin"]="https://httpbin.org/spec.json"
)

# Create output directories
mkdir -p generated/{csharp,typescript,jmeter}

# Process each API
for name in "${!apis[@]}"; do
    url="${apis[$name]}"
    
    echo "Processing $name from $url"
    
    # Download specification
    curl -s "$url" -o "specs/${name}.json"
    
    # Generate C# clients with multiple generators
    rapicgen csharp nswag "specs/${name}.json" "${name^}.NSwag" "generated/csharp/${name}-nswag.cs"
    rapicgen csharp openapi "specs/${name}.json" "${name^}.OpenApi" "generated/csharp/${name}-openapi.cs"
    rapicgen csharp kiota "specs/${name}.json" "${name^}.Kiota" "generated/csharp/${name}-kiota.cs"
    
    # Generate TypeScript clients
    rapicgen typescript Angular "specs/${name}.json" "generated/typescript/${name}-angular"
    rapicgen typescript Axios "specs/${name}.json" "generated/typescript/${name}-axios"
    
    # Generate JMeter test plan
    rapicgen jmeter "specs/${name}.json"
    mv *.jmx "generated/jmeter/" 2>/dev/null || true
    
    echo "Completed processing $name"
done

echo "All APIs processed successfully!"</code></pre>
</div>

### Configuration-Driven Generation

<div class="cli-example">
  <h4>YAML Configuration File</h4>
  <pre><code># api-generation-config.yaml
apis:
  - name: "petstore"
    spec: "https://petstore.swagger.io/v2/swagger.json"
    generators:
      - type: "csharp"
        generator: "nswag"
        namespace: "PetStore.Client"
        output: "./src/PetStore.Client.cs"
      - type: "typescript"
        generator: "Angular"
        output: "./src/app/api"
      - type: "jmeter"
        output: "./tests/"
        
  - name: "jsonplaceholder"
    spec: "https://jsonplaceholder.typicode.com/openapi.json"
    generators:
      - type: "csharp"
        generator: "kiota"
        namespace: "JsonPlaceholder.Client"
        output: "./src/JsonPlaceholder.Client.cs"</code></pre>
</div>

## Performance Optimization Tips

<div class="config-section">
  <h4>Speed Up Generation</h4>
  <ul>
    <li><strong>Pre-download JAR files</strong> - Cache Swagger Codegen and OpenAPI Generator JARs</li>
    <li><strong>Use local specifications</strong> - Download specs once, generate multiple times</li>
    <li><strong>Parallel generation</strong> - Use shell backgrounding for multiple clients</li>
    <li><strong>Docker layer caching</strong> - Cache tool installations in Docker builds</li>
  </ul>
</div>

### Parallel Generation Example

<div class="cli-example">
  <h4>Parallel Client Generation</h4>
  <pre><code>#!/bin/bash
# Generate multiple clients in parallel

spec_file="swagger.json"
namespace="MyApi.Client"

# Start background processes
rapicgen csharp nswag "$spec_file" "$namespace.NSwag" "./nswag-client.cs" &
rapicgen csharp openapi "$spec_file" "$namespace.OpenApi" "./openapi-client.cs" &
rapicgen csharp kiota "$spec_file" "$namespace.Kiota" "./kiota-client.cs" &
rapicgen typescript Angular "$spec_file" "./angular-client" &
rapicgen typescript Axios "$spec_file" "./axios-client" &

# Wait for all background processes to complete
wait

echo "All clients generated successfully!"</code></pre>
</div>

## Troubleshooting Common Issues

<div class="config-section">
  <h4>Java-Related Issues</h4>
  <ul>
    <li><strong>Java not found:</strong> Ensure Java 8+ is installed and in PATH</li>
    <li><strong>JAVA_HOME not set:</strong> Set environment variable or use --java-home option</li>
    <li><strong>Version conflicts:</strong> Use Java 17+ for best compatibility</li>
  </ul>
</div>

<div class="config-section">
  <h4>NPM and Node.js Issues</h4>
  <ul>
    <li><strong>NPM not found:</strong> Install Node.js which includes NPM</li>
    <li><strong>Permission errors:</strong> Use npm prefix or install with --unsafe-perm</li>
    <li><strong>Corporate proxies:</strong> Configure NPM proxy settings</li>
  </ul>
</div>

<div class="config-section">
  <h4>Network and Download Issues</h4>
  <ul>
    <li><strong>Slow downloads:</strong> Pre-cache JAR files and use local specs</li>
    <li><strong>SSL certificate errors:</strong> Update certificates or use --ignore-ssl-errors</li>
    <li><strong>Firewall blocking:</strong> Whitelist GitHub, NPM, and Maven Central</li>
  </ul>
</div>

### Debug Mode and Verbose Output

<div class="cli-example">
  <h4>Enable Verbose Logging</h4>
  <pre><code># Get detailed output for troubleshooting
rapicgen -v csharp nswag swagger.json MyNamespace ./output.cs

# Disable analytics for corporate environments
rapicgen typescript -nl Angular swagger.json ./output</code></pre>
</div>

## Real-World Integration Examples

### Microservices Architecture

<div class="cli-example">
  <h4>Generate Clients for Multiple Services</h4>
  <pre><code># User Service
rapicgen csharp nswag user-service/openapi.json UserService.Client ./clients/UserService.cs

# Order Service  
rapicgen csharp nswag order-service/openapi.json OrderService.Client ./clients/OrderService.cs

# Payment Service
rapicgen csharp kiota payment-service/openapi.json PaymentService.Client ./clients/PaymentService.cs

# Generate TypeScript clients for frontend
rapicgen typescript Angular user-service/openapi.json ./frontend/src/app/services/user
rapicgen typescript Angular order-service/openapi.json ./frontend/src/app/services/order</code></pre>
</div>

### API Gateway Integration

<div class="cli-example">
  <h4>Generate Clients from API Gateway Specs</h4>
  <pre><code># Download from AWS API Gateway
aws apigateway get-export --rest-api-id your-api-id --stage-name prod --export-type swagger swagger.json

# Generate comprehensive client
rapicgen csharp nswag swagger.json Gateway.Client ./src/ApiGatewayClient.cs

# Generate test plans for load testing
rapicgen jmeter swagger.json</code></pre>
</div>

The CLI tool provides enterprise-grade functionality with the flexibility to integrate into any development workflow, making it an essential tool for modern API-driven development.