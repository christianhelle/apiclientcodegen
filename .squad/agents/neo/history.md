# Neo — History

## Learnings

- **2026-03-04:** Team initialized. Core dev for REST API Client Code Generator. Key paths: `src/Core/`, `src/CLI/`. Build: `dotnet build Rapicgen.slnx` from `src/`. User is Christian.

- **AutoRest Deprecation Surface Inventory:** AutoRest appears in 78+ files across core, CLI, VSIX, VSMac, and docs. **Shared abstraction patterns** (ICodeGenerator, IDependencyInstaller, CodeGeneratorFactory) make safe deprecation/removal feasible. Two enum values exist: `SupportedCodeGenerator.AutoRest` (v2 legacy) and `AutoRestV3` (v3.0.0-beta.20210504.2 from NPM). Key surfaces: SupportedCodeGenerator enum, AutoRestCSharpCodeGenerator class, IDependencyInstaller.InstallAutoRest(), CLI Program.cs registration, AutoRestCommand, PackageDependencyListProvider (two cases), README/CLI/docs. Breaking points on removal: enum values, interface method, CLI command, VSIX custom tool mappings, project files with `<Generator>AutoRest</Generator>`. Recommended: Phase 1 (now) add `[Obsolete]` attributes to core types + update help/docs; Phase 2-3 track usage via telemetry & monitor migration; Phase 4 remove after 6 months. **Trinity (VSIX team) must coordinate** on options page and custom tool deprecation UI.
