# REST API Client Code Generator for Zed

Generate REST API client code from OpenAPI/Swagger specifications directly in the [Zed](https://zed.dev/) editor.

This extension wraps the [rapicgen](https://www.nuget.org/packages/rapicgen) .NET global tool and provides slash commands in Zed's AI Assistant for code generation.

## Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet) (required)
- [Java 17+](https://adoptium.net/) (required for OpenAPI Generator, Swagger Codegen, and all TypeScript generators)

The `rapicgen` .NET global tool will be installed automatically on first use.

## Usage

Open Zed's AI Assistant and use the following slash commands:

### Generate C# Client

```
/generate-csharp <generator> <spec-file> [namespace]
```

**Available generators:**
| Generator | Description |
|-----------|-------------|
| `nswag` | NSwag (v14.6.3) |
| `refitter` | Refitter (v1.7.3) |
| `openapi` | OpenAPI Generator (v7.20.0) — requires Java |
| `kiota` | Microsoft Kiota (v1.30.0) |
| `swagger` | Swagger Codegen (v3.0.34) — requires Java |
| `autorest` | AutoRest (v3.0.0-beta) |

**Examples:**
```
/generate-csharp nswag ./swagger.json
/generate-csharp refitter ./openapi.yaml MyApp.Client
/generate-csharp openapi ./api-spec.json GeneratedCode
```

### Generate TypeScript Client

```
/generate-typescript <generator> <spec-file>
```

**Available generators:**
| Generator | Framework |
|-----------|-----------|
| `angular` | Angular |
| `aurelia` | Aurelia |
| `axios` | Axios |
| `fetch` | Fetch API |
| `inversify` | Inversify |
| `jquery` | JQuery |
| `nestjs` | NestJS |
| `node` | Node.js |
| `reduxquery` | Redux Query |
| `rxjs` | RxJS |

All TypeScript generators require Java.

**Examples:**
```
/generate-typescript axios ./swagger.json
/generate-typescript angular ./openapi.yaml
```

### Generate from Refitter Settings

```
/generate-refitter <settings-file>
```

**Example:**
```
/generate-refitter ./api.refitter
```

## Output

- **C# generators** produce a single `.cs` file next to the spec file (e.g., `swagger.nswag.cs`)
- **TypeScript generators** produce a directory next to the spec file (e.g., `swagger-axios-typescript/`)
- **Refitter settings** generate output as configured in the `.refitter` file

## Development

This extension is written in Rust and compiled to WebAssembly. To build locally:

```bash
cd src/Zed
cargo build --target wasm32-wasip1
```

To install as a dev extension in Zed, use `zed: install dev extension` and select the `src/Zed` directory.

## License

[MIT](LICENSE)
