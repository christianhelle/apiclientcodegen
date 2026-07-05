# Architecture Context

Vocabulary for the code-generation architecture, so future changes (human or AI) reuse the same
seams instead of reinventing them. Domain terms first, then the shared modules.

## Domain

- **Code generator** — a module that turns an OpenAPI/Swagger spec into client source. Each
  generator implements `ICodeGenerator.GenerateCode(...)` (`Rapicgen.Core.Generators`). Concrete
  generators: NSwag, OpenAPI Generator, Swagger Codegen (`[Obsolete]`), Refitter, Kiota,
  plus TypeScript/JMeter variants.
- **External tool** — the third-party CLI/JAR a generator shells out to (NSwag, refitter, kiota,
  swagger-codegen, openapi-generator). Tools are installed on demand.
- **Supported code generator** — the `SupportedCodeGenerator` enum the VSIX host dispatches on.

## Shared modules (the seams to reuse)

- **`ExternalTools` registry** (`Rapicgen.Core.ExternalTool` / `ExternalTools`) — the single source
  of truth for each external tool's display name, pinned version and (where applicable) package id.
  Route every tool name/version literal through this registry rather than hard-coding strings, so a
  version bump touches one place. `VersionLabel` yields the `"v"+Version` form used in code headers.
- **`ToolRunner`** (`Rapicgen.Core.External.ToolRunner`) — concentrates the launch boilerplate every
  generator used to repeat: `Run(tool, command, arguments, workingDirectory?)` wraps the
  `DependencyContext` telemetry span around `IProcessLauncher.Start` and marks it succeeded;
  `RunJava(tool, options, arguments, workingDirectory?)` resolves the Java runtime first for
  JAR-based tools. Generators construct a `ToolRunner` from their injected `IProcessLauncher`, so the
  process launcher remains the seam that generator tests mock.
- **`DependencyInstaller`** (`Rapicgen.Core.Installer`) — installs/updates external tools. Its
  `InstallOrUpdateDotNetTool(tool, update)` helper concentrates the check→install/update dance for
  `dotnet tool` based generators (Kiota, Refitter).
- **Code-generator factories** — two deliberately different styles per host; see
  `docs/adr/0001-code-generator-factory-styles.md`. The shared cross-host seam is `ToolRunner` +
  `ExternalTools`, not the factory layer.
