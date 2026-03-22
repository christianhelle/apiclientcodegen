# Tank — History

## Learnings

- **2026-03-04:** Team initialized. Tester for REST API Client Code Generator. Test paths: `src/Core/ApiClientCodeGen.Core.Tests/`, `src/Core/ApiClientCodeGen.Core.IntegrationTests/`, `src/CLI/ApiClientCodeGen.CLI.Tests/`. User is Christian.

- **2026-03-04:** AutoRest Deprecation Investigation Complete. Key findings:
  - **AutoRest is in 8 surfaces:** Core generators, CLI command, VSIX UI (settings/dialogs/context menu), VS Code extension, VSMac, IntelliJ
  - **Existing coverage (good):** Unit tests for generator logic, factory creation, argument building, integration tests for real execution. SupportedCodeGeneratorTests validates enum.
  - **Critical gaps:** No tests for CLI help display, VSIX UI surfaces, VS Code command registration, enum contract enforcement, factory routing, output window messages, or documentation references.
  - **Risk zones:** Deprecation notice in help could break parsing; VS Code extension doesn't test generator list registration; factory switch/case not explicitly path-covered; enum count test fragile.
  - **Phase 1 approach:** Add CliHelpTests.cs, enhance existing enum tests, add VS Code generator tests, validate output window format. Deprecation notice should NOT change code logic.
  - **Phase 2 preparation:** Document removal checklist covering 40+ files across Core, VSIX, CLI, VSCode, VSMac, Docs. Enum removal will break SupportedCodeGeneratorTests:54 count assertion (expected).
  - **Test infrastructure:** All surfaces use xUnit [Fact]/[Theory], Moq for mocking, AutoFixture. No async/await needed for deprecated tool tests. Existing fixtures (AutoRestCodeGeneratorFixture) are comprehensive.
