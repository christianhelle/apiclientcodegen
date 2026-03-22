# Neo — History

## Learnings

- **2026-03-04:** Team initialized. Core dev for REST API Client Code Generator. Key paths: `src/Core/`, `src/CLI/`. Build: `dotnet build Rapicgen.slnx` from `src/`. User is Christian.

- **AutoRest Deprecation Surface Inventory:** AutoRest appears in 78+ files across core, CLI, VSIX, VSMac, and docs. **Shared abstraction patterns** (ICodeGenerator, IDependencyInstaller, CodeGeneratorFactory) make safe deprecation/removal feasible. Two enum values exist: `SupportedCodeGenerator.AutoRest` (v2 legacy) and `AutoRestV3` (v3.0.0-beta.20210504.2 from NPM). Key surfaces: SupportedCodeGenerator enum, AutoRestCSharpCodeGenerator class, IDependencyInstaller.InstallAutoRest(), CLI Program.cs registration, AutoRestCommand, PackageDependencyListProvider (two cases), README/CLI/docs. Breaking points on removal: enum values, interface method, CLI command, VSIX custom tool mappings, project files with `<Generator>AutoRest</Generator>`. Recommended: Phase 1 (now) add `[Obsolete]` attributes to core types + update help/docs; Phase 2-3 track usage via telemetry & monitor migration; Phase 4 remove after 6 months. **Trinity (VSIX team) must coordinate** on options page and custom tool deprecation UI.

- **2026-03-04 - AutoRest Deprecation Implementation:** Successfully implemented Phase 1 deprecation for core/CLI scope. Added `[Obsolete]` attributes to 6 public AutoRest types (SupportedCodeGenerator enum values, AutoRestCSharpCodeGenerator, IAutoRestArgumentProvider, AutoRestArgumentProvider, IAutoRestOptions, DefaultAutoRestOptions, AutoRestConstants, IAutoRestCodeGeneratorFactory, AutoRestCodeGeneratorFactory, IDependencyInstaller.InstallAutoRest). Added runtime stderr warning in AutoRestCommand.Execute(). Updated CLI description to "AutoRest (Deprecated - v3.0.0-beta.20210504.2)". Used scoped `#pragma warning disable CS0618` for internal usage in constructors, DI registration, and factory implementations. Created comprehensive [AutoRestMigration.md](../../docs/AutoRestMigration.md) guide with 3 migration paths (NSwag, Refitter, Kiota). Updated README.md, docs/CLI.md, docs/GeneratedCodeUsage.md, CHANGELOG.md. Build verified (0,6s, 0 warnings). CLI help and runtime warning tested successfully. 15 files changed. Zero breaking changes - all existing functionality preserved. Ready for Trinity (IDE extensions) and Tank (test validation) phases.

- **2026-03-04 - AutoRest Deprecation Gap Closure:** Coordinator review identified remaining core-scope gaps. Fixed 2 missing annotations: (1) `SyncMethodOptions` enum now has `[Obsolete]` attribute, (2) `DependencyInstaller.InstallAutoRest()` concrete method now has XML doc comment with deprecation remarks plus `[Obsolete]` attribute for consistency. All 10 public AutoRest-facing types in Core scope now have complete, consistent deprecation coverage. Build verified (0 errors, 0 warnings). Decision documented in `.squad/decisions/inbox/neo-autorest-gap-fix.md`. Core scope AutoRest deprecation surface complete.

- **2026-03-22 — AutoRest Phase 1 Deprecation Complete:**
  - **Implementation outcome:** All core/CLI deprecation objectives achieved
  - **Annotations:** 10 public AutoRest-facing types now have [Obsolete] with canonical warning message
  - **CLI runtime:** Warning emits to stderr before generation, no exit code change, no stdout pollution
  - **CLI help:** Shows `"AutoRest (Deprecated - v3.0.0-beta.20210504.2)"` format
  - **Documentation:** Migration guide created (3 paths: NSwag, Refitter, Kiota) + README/CLI/ChangeLog updated
  - **Gaps closed:** SyncMethodOptions enum + DependencyInstaller concrete method both annotated
  - **Code review:** Morpheus approved — no blocking issues, ready for PR
  - **Status:** Core scope 100% complete. All public types consistently deprecated. Ready for Trinity + Tank phases to complete Phase 1.
