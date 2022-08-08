# Cross Platform Command Line Tool
All custom tools mentioned above are also implemented in a cross platform command line application

#### Requirements
- .NET Core 3.1 runtime
- Java Runtime Environment
- NPM

### Installation
The tool can be installed as a .NET Core global tool that you can call from the shell / command line
```
dotnet tool install --global rapicgen
```
or by following the instructions [here](https://www.nuget.org/packages/rapicgen) to install a specific version of tool

### Usage
Since the tool is published as a .NET Core Tool, it can be launched from anywhere using any command line interface by calling **rapicgen**.
The help information is displayed when not specifying any arguments to **rapicgen**

```
Usage: rapicgen [command] [options]

Options:
  -v|--verbose   Show verbose output
  -?|-h|--help   Show help information.

Commands:
  apache-jmeter  Generate Apache JMeter script using OpenAPI Generator
  autorest       Generate C# API client using AutoRest
  nswag          Generate C# API client using NSwag
  openapi        Generate C# API client using OpenAPI Generator
  swagger        Generate C# API client using Swagger Codegen CLI

Run 'rapicgen [command] -?|-h|--help' for more information about a command.
```

Some help information is also provided per command and can be launched by 

```
$ rapicgen [command name] -?
```

For example:

```
$ rapicgen autorest -?
```

will output this:

```
Generate Swagger / Open API client using AutoRest

Usage: run autorest [options] <swaggerFile> <namespace> <outputFile>

Arguments:
  swaggerFile   Path to the Swagger / Open API specification file
  namespace     Default namespace to in the generated code
  outputFile    Output filename to write the generated code to. Default is the swaggerFile .cs

Options:
  -?|-h|--help  Show help information
```

## Usage Examples:

Let's say we have a OpenAPI Specifications document called **Swagger.json**

For starters, we can use the Swagger Petstore spec. Here's an example powershell script for downloading it

```
Invoke-WebRequest -Uri https://petstore.swagger.io/v2/swagger.json -OutFile Swagger.json
```

In case you don't have the CLI tool installed you can install it by

```
dotnet tool install --global rapicgen
```

Here's an example of how to generate code using **AutoRest**

```
$ rapicgen autorest Swagger.json GeneratedCode ./AutoRestOutput.cs
```

Here's an example of how to generate code using **NSwag**

```
$ rapicgen nswag Swagger.json GeneratedCode ./NSwagOutput.cs
```

Here's an example of how to generate code using **Swagger Codegen CLI**

```
$ rapicgen swagger Swagger.json GeneratedCode ./SwaggerOutput.cs
```

And last but but not the least, here's an example of how to generate code using **OpenAPI Generator**

```
$ rapicgen openapi Swagger.json GeneratedCode ./OpenApiOutput.cs
```