![build status](https://christianhelle.visualstudio.com/API%20Client%20Code%20Generator/_apis/build/status/CI%20Build)

# REST API Client Code Generator
A collection of Visual Studio C# custom tool code generators for Swagger / OpenAPI specification files

**Features**

- Supports Visual Studio 2017 and 2019
- Add New REST API Client to a project from an OpenAPI specification URL (e.g https://petstore.swagger.io/v2/swagger.json) using **AutoRest**, **NSwag**, **Swagger Codegen**, or **OpenAPI Codegen**
- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the OpenAPI specification json file (Swagger.json)
- Generate code using an NSwag Studio file by including it in the project and using the **Generate with NSwag Studio** context menu


**Custom Tools**

- AutoRestCodeGenerator - Generates a single file C# REST API Client using AutoRest. 
The resulting file is the equivalent of using the AutoRest CLI tool with:
` --csharp --input-file=[swaggerFile] --output-file=[outputFile] --namespace=[namespace] --add-credentials`

- NSwagCodeGenerator - Generates a single file C# REST API Client using the [NSwag.CodeGeneration.CSharp](https://github.com/RSuter/NSwag/wiki/SwaggerToCSharpClientGenerator) [nuget package](https://www.nuget.org/packages/NSwag.CodeGeneration.CSharp/)

- SwaggerCodeGenerator - Generates a single file C# REST API Client using Swagger Codegen CLI.
The output file is the result of merging all the files generated using the Swagger Codegen CLI tool with:
` generate -l csharp --input-spec [swaggerFile] --output [output] --api-package=[namespace] --model-package=[namespace] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`

- OpenApiCodeGenerator - Generates a single file C# REST API Client using OpenAPI Generator.
The output file is the result of merging all the files generated using the OpenAPI Generator tool with:
` generate -l csharp --input-spec [swaggerFile] --output [output] --api-package=[namespace] --model-package=[namespace] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`


**Important note:**

The custom tool code generators piggy back on top of well known Open API client code generators like **AutoRest**, **NSwag**, and **Swagger Codegen**. These tools require **NPM**, **AutoRest**, and the **Java SDK** to be installed on the developers machine. 

The **Swagger Codegen CLI** and **OpenAPI Generator** are downloaded on demand but requires the Java SDK to be installed on the machine. This also means that using the **SwaggerCodeGenerator** and **OpenApiCodeGenerator** custom tools have a initial delay upon first time use. The generated code that these tools produce depends on the [RestSharp v105.1.0](https://www.nuget.org/packages/RestSharp/105.1.0) and [JsonSubTypes v1.2.0](https://www.nuget.org/packages/JsonSubTypes/1.2.0) nuget packages

The **AutoRestCodeGenerator** code generator produces code that depends on the [Microsoft.Rest.ClientRuntime v2.3.20](https://www.nuget.org/packages/Microsoft.Rest.ClientRuntime/2.3.20) nuget package

This Visual Studio Extension will automatically add the required nuget packages that the generated code depends on


**Screenshots**

![Add - API Client from OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/add-new-menu.png)

![Enter URL to OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/add-new-dialog.png)

![Solution Explorer Context Menus](https://github.com/christianhelle/apiclientcodegen/raw/master/images/solution-explorer-context-menu.jpg)

![NSwag Studio Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/nswagstudio-context-menu.jpg)

![AutoRestCodeGenerator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/autorestcodegenerator-custom-tool.jpg)

![NSwagCodeGenerator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/nswagcodegenerator-custom-tool.jpg)

![SwaggerCodeGenerator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/swaggercodegenerator-custom-tool.jpg)

![OpenApiCodeGenerator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/openapicodegenerator-custom-tool.jpg)


For tips and tricks on software development, check out [my blog](https://christian-helle.blogspot.com)
