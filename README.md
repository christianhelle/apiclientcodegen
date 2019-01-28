![build status](https://christianhelle.visualstudio.com/API%20Client%20Code%20Generator/_apis/build/status/CI%20Build)

# REST API Client Code Generator
A collection of Visual Studio custom tool code generators for Swagger / OpenAPI specification files

**Features**

- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the Swagger.json file
- Supports Visual Studio 2017 and 2019


**Custom Tools**

- AutoRestCodeGenerator - Generates a single file C# REST API Client using AutoRest. 
The resulting file is the equivalent of using the AutoRest CLI tool with:
` --csharp " +--input-file=[swaggerFile] --output-file=[outputFile] --namespace=[namespace] --add-credentials`

- NSwagCodeGenerator - Generates a single file C# REST API Client using the [NSwag.CodeGeneration.CSharp](https://github.com/RSuter/NSwag/wiki/SwaggerToCSharpClientGenerator) [nuget package](https://www.nuget.org/packages/NSwag.CodeGeneration.CSharp/)

- SwaggerCodeGenerator - Generates a single file C# REST API Client using Swagger Codegen CLI.
The output file is the result of merging all the files generated using the Swagger Codegen CLI tool with:
` generate -l csharp --input-spec [swaggerFile] --output [output] --api-package=[namespace] --model-package=[namespace] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`


**Important note:**

The custom tool code generators piggy back on top of well known Open API client code generators like **AutoRest**, **NSwag**, and **Swagger Codegen**. And requires **NPM**, **AutoRest**, and **Java SDK** to be installed on the developers machine. The **Swagger Codegen CLI** is downloaded on demand but requires the Java SDK to be installed on the machine


**Screenshots**

![AutoRestCodeGenerator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/autorestcodegenerator-custom-tool.jpg)

![NSwagCodeGenerator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/nswagcodegenerator-custom-tool.jpg)


***More custom tools coming soon...***
