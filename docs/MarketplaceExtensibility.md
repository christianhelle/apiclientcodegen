There is a separate VSIX installer for the stable versions available for **[Visual Studio 2022/2026](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator2022)**, **[Visual Studio 2019](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator)**, **[Visual Studio 2017](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ApiClientCodeGenerator2017)**, and **[Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.apiclientcodegen)**

# REST API Client Code Generator (PREVIEW)

A collection of Visual Studio C# code generators for Swagger / OpenAPI specification files

## Features

- Based on the new out-of-process Visual Studio Extensibility model
- Add New REST API Client to a project from an OpenAPI specification URL (e.g <https://petstore.swagger.io/v2/swagger.json>) using the following code generators:
  - [Refitter](https://github.com/christianhelle/refitter)
  - [NSwag](https://github.com/RicoSuter/NSwag)
  - [OpenAPI Generator](https://github.com/OpenAPITools/openapi-generator)
  - [Microsoft Kiota](https://github.com/microsoft/kiota)
  - [Swagger Codegen](https://github.com/swagger-api/swagger-codegen)
  - [AutoRest](https://github.com/Azure/autorest)
- Generate code using configuration files with the following methods:
  - `.nswag` configuration files from [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio) by including it in the project and using the **Generate NSwag Studio output** context menu
  - `.refitter` settings files from [Refitter](https://github.com/christianhelle/refitter) by including it in the project and using the **Generate Refitter output** context menu

## Screenshots

![Solution Explorer Context Menus](https://github.com/christianhelle/apiclientcodegen/raw/master/images/solution-explorer-context-menu.jpg)

![NSwag Studio Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/nswagstudio-context-menu.jpg)

![Refitter Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/refitter-command.png)

![Kiota Context Menu](https://github.com/christianhelle/apiclientcodegen/raw/master/images/generate-kiota-output.png)

### Settings

![Options - General](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-general.png)

![Options - AutoRest](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-autorest.png)

![Options - NSwag](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-nswag.png)

![Options - NSwag Studio](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-nswag-studio.png)

![Options - OpenAPI Generator](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-openapi-generator.png)

![Options - Refitter](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-refitter.png)

![Options - Kiota](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-kiota.png)

![Options - Analytics](https://github.com/christianhelle/apiclientcodegen/raw/master/images/vs-settings-analytics.png)
