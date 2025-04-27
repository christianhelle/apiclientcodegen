# Cross Platform Command Line Tool

All custom tools mentioned above are also implemented in a cross platform command line application

## Requirements

- .NET 6.0 runtime
- Java Runtime Environment
- NPM

## Installation

The tool can be installed as a .NET Core global tool that you can call from the shell / command line

```sh
dotnet tool install --global rapicgen
```

or by following the instructions [here](https://www.nuget.org/packages/rapicgen) to install a specific version of tool

## Usage

Since the tool is published as a .NET Core Tool, it can be launched from anywhere using any command line interface by calling **rapicgen**.
The help information is displayed when not specifying any arguments to **rapicgen**

```sh
Usage: rapicgen [command] [options]

Options:
  -v|--verbose       Show verbose output
  -?|-h|--help       Show help information.

Commands:
  csharp             Generate C# API clients
  jmeter             Generate Apache JMeter test plans
  openapi-generator  Generate code using OpenAPI Generator (v7.13.0).
                     See supported generators at https://openapi-generator.tech/docs/generators/
  typescript         Generate TypeScript API clients

Run 'rapicgen [command] -?|-h|--help' for more information about a command.
```

Some help information is also provided per command and can be launched by 

```sh
rapicgen [command name] -?
```

or

```sh
rapicgen [command name] [sub command name] -?
```

For example:

```sh
rapicgen csharp -?
```

will output this:

```sh
Generate C# API clients

Usage: rapicgen csharp [command] [options]

Options:
  -?|-h|--help  Show help information.

Commands:
  autorest      AutoRest (v3.0.0-beta.20210504.2)
  kiota         Microsoft Kiota (v1.25.1)
  nswag         NSwag (v14.3.0)
  openapi       OpenAPI Generator (v7.13.0)
  refitter      Refitter (v1.5.4)
  swagger       Swagger Codegen CLI (v3.0.34)

Run 'csharp [command] -?|-h|--help' for more information about a command.
```

and

```sh
rapicgen csharp autorest -?
```

will output this:

```sh
Generate Swagger / Open API client using AutoRest

Usage: run autorest [options] <swaggerFile> <namespace> <outputFile>

Arguments:
  swaggerFile   Path to the Swagger / Open API specification file
  namespace     Default namespace to in the generated code
  outputFile    Output filename to write the generated code to. Default is the swaggerFile .cs

Options:
  -?|-h|--help  Show help information
```

or

```sh
rapicgen typescript -?
```

will output this:

```sh
Generate TypeScript API client

Usage: rapicgen typescript [options] <generator> <swaggerFile> <outputPath>

Arguments:
  generator         The tech stack to use for the generated client library
                    Allowed values are: Angular, Aurelia, Axios, Fetch, Inversify, JQuery, NestJS, Node, ReduxQuery,
                    Rxjs.
                    Default value is: Angular.
  swaggerFile       Path to the Swagger / Open API specification file
  outputPath        Output folder to write the generated code to
                    Default value is: typescript-generated-code.

Options:
  -nl|--no-logging  Disables Analytics and Error Reporting
  -?|-h|--help      Show help information.
```

## Usage Examples

Let's say we have a OpenAPI Specifications document called **Swagger.json**

For starters, we can use the Swagger Petstore spec. Here's an example powershell script for downloading it

```sh
Invoke-WebRequest -Uri https://petstore.swagger.io/v3/swagger.json -OutFile Swagger.json
```

In case you don't have the CLI tool installed you can install it by

```sh
dotnet tool install --global rapicgen
```

Here's an example of how to generate code using **AutoRest**

```sh
rapicgen csharp autorest Swagger.json GeneratedCode ./AutoRestOutput.cs
```

Here's an example of how to generate code using **Kiota**

```sh
rapicgen csharp kiota Swagger.json GeneratedCode ./OpenApiOutput.cs
```

Here's an example of how to generate code using **NSwag**

```sh
rapicgen csharp nswag Swagger.json GeneratedCode ./NSwagOutput.cs
```

Here's an example of how to generate code using **Swagger Codegen CLI**

```sh
rapicgen csharp swagger Swagger.json GeneratedCode ./SwaggerOutput.cs
```

Here's an example of how to generate code using **OpenAPI Generator**

```sh
rapicgen csharp openapi Swagger.json GeneratedCode ./OpenApiOutput.cs
```

Here's an example of how to generate code **JMeter** test plans

```sh
rapicgen jmeter Swagger.json
```

Here's an example of how to generate code for **TypeScript**

```sh
rapicgen typescript Angular Swagger.json
```
