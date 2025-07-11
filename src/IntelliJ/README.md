# API Client Code Generator for IntelliJ-based IDEs

This plugin provides functionality to generate API client code from OpenAPI/Swagger specifications directly within IntelliJ-based IDEs, mirroring the features available in the VS Code extension.

## Features

- Generate C# clients using NSwag, Refitter, OpenAPI Generator, Microsoft Kiota, Swagger Codegen CLI, and AutoREST.
- Generate TypeScript clients for various frameworks like Angular, Aurelia, Axios, Fetch, Inversify, JQuery, NestJS, Node, Redux Query, and RxJS.
- Context menu integration for `.json`, `.yaml`, `.yml`, and `.refitter` files.

## Building and Testing

To build the plugin, run the `build.ps1` script:

```powershell
./build.ps1
```

To test the plugin locally in an IDE instance, run the `test.ps1` script:

```powershell
./test.ps1
```
