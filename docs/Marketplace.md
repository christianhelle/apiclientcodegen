# REST API Client Code Generator
A collection of Visual Studio C# custom tool code generators for Swagger / OpenAPI specification files

## Features

- Supports Visual Studio 2017 and 2019
- Add New REST API Client to a project from an OpenAPI specification URL (e.g https://petstore.swagger.io/v2/swagger.json) using [AutoRest](https://github.com/Azure/autorest), [NSwag](https://github.com/RicoSuter/NSwag), [Swagger Codegen](https://github.com/swagger-api/swagger-codegen), or [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator)
- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the OpenAPI specification json file (Swagger.json)
- Generate code using an [NSwag Studio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio) file by including it in the project and using the **Generate with NSwag Studio** context menu


### Custom Tools

- AutoRestCodeGenerator - Generates a single file C# REST API Client using AutoRest. 
The resulting file is the equivalent of using the AutoRest CLI tool with:
` --csharp --input-file=[swaggerFile] --output-file=[outputFile] --namespace=[namespace] --add-credentials`

- NSwagCodeGenerator - Generates a single file C# REST API Client using the [NSwag.CodeGeneration.CSharp](https://github.com/RSuter/NSwag/wiki/SwaggerToCSharpClientGenerator) [nuget package](https://www.nuget.org/packages/NSwag.CodeGeneration.CSharp/)

- SwaggerCodeGenerator - Generates a single file C# REST API Client using Swagger Codegen CLI.
The output file is the result of merging all the files generated using the Swagger Codegen CLI tool with:
` generate -l csharp --input-spec [swaggerFile] --output [output] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`

- OpenApiCodeGenerator - Generates a single file C# REST API Client using OpenAPI Generator.
The output file is the result of merging all the files generated using the OpenAPI Generator tool with:
` generate -g csharp --input-spec [swaggerFile] --output [output] -DapiTests=false -DmodelTests=false -DpackageName=[namespace] --skip-overwrite`


### Dependencies

The custom tool code generators piggy back on top of well known Open API client code generators like **AutoRest**, **NSwag**, **OpenAPI Generator**, and **Swagger Codegen**. These tools require [NPM](https://www.npmjs.com/get-npm), **AutoRest**, and the [Java SDK](https://java.com/en/download/manual.jsp) to be installed on the developers machine. Alternative Java SDK implementations such as the [OpenJDK](https://adoptopenjdk.net) works fine with this extension. By default, the path to **java.exe** is read from the **JAVA_HOME** environment variable, but is also configurable in the Settings screen

The **Swagger Codegen CLI** and **OpenAPI Generator** are downloaded on demand but requires the Java SDK to be installed on the machine. This also means that using the **SwaggerCodeGenerator** and **OpenApiCodeGenerator** custom tools have a initial delay upon first time use. The generated code that these tools produce depends on the [RestSharp v105.1.0](https://www.nuget.org/packages/RestSharp/105.1.0) and [JsonSubTypes v1.2.0](https://www.nuget.org/packages/JsonSubTypes/1.2.0) nuget packages. 

The **AutoRest** code generator produces code that depends on the [Microsoft.Rest.ClientRuntime v2.3.20](https://www.nuget.org/packages/Microsoft.Rest.ClientRuntime/2.3.20) and [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/12.0.2) nuget packages. 

The **NSwag** code generator produces code that depends on the [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/12.0.2) nuget package.

This Visual Studio Extension will automatically add the required nuget packages that the generated code depends on


## Screenshots

![Add - API Client from OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/add-new-menu.png)

![Enter URL to OpenAPI Specification](https://github.com/christianhelle/apiclientcodegen/raw/master/images/add-new-dialog.png)

![Solution Explorer Context Menus](https://github.com/christianhelle/apiclientcodegen/raw/master/images/solution-explorer-context-menu.jpg)

![NSwag Studio Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/nswagstudio-context-menu.jpg)


### Settings

This extension will by default make some assumptions on the installation paths for **Java**, **NSwag** and **NPM** but also provides option pages for configuring this. The **Swagger Codegen CLI** and the **OpenAPI Generator** JAR files are by default downloaded to the user TEMP folder but it is also possible to specify to use existing JAR files

![Options - General](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-general.png)

![Options - NSwag](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-nswag.png)

![Options - NSwag Studio](https://github.com/christianhelle/apiclientcodegen/raw/master/images/options-nswagstudio.png)


For tips and tricks on software development, check out [my blog](https://christian-helle.blogspot.com)

If you find this useful and feel a bit generous then feel free to [buy me a coffee](https://www.buymeacoffee.com/christianhelle) :)

