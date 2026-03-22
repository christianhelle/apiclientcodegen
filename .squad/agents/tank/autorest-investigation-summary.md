# AutoRest Deprecation Investigation — Executive Summary

**Investigation Date:** 2026-03-04  
**Requested By:** Christian  
**Scope:** Read-only assessment of test coverage for AutoRest deprecation messaging & future removal  

---

## 🎯 What We Found

AutoRest is **deeply woven** into the codebase across 8 distinct surfaces:

| Surface | Location | Entry Point | Current Test Coverage |
|---------|----------|-------------|----------------------|
| **CLI Command** | `Program.cs:43-45` | `rapicgen csharp autorest` | ✅ Command tests exist, ❌ help text untested |
| **VSIX Settings** | `AutoRestSettings.cs` | Tools → Options → AutoRest | ❌ No UI surface tests |
| **VSIX "Add New"** | `NewAutoRestClientCommand.cs` | Right-click → Add New REST Client | ❌ Dialog not tested |
| **VSIX Context Menu** | `AutoRestCodeGeneratorCustomToolSetter.cs` | Custom tool property | ✅ Limited integration tests |
| **VS Code Extension** | `generators.ts:18` | Command palette | ❌ Generator list not tested |
| **Core Generator** | `AutoRestCSharpCodeGenerator.cs` | Factory.Create() | ✅ Unit & integration tests |
| **Enum System** | `SupportedCodeGenerator.cs` | Generator selection routing | ✅ Enum presence test (fragile) |
| **Documentation** | README.md, Marketplace*.md, CLI.md | User-facing references | ❌ No validation |

---

## ✅ Current Test Coverage (Strengths)

1. **Core Logic:** Generator produces valid C# code
   - Files: `AutoRestCSharpCodeGeneratorTests.cs`, integration variants
   - Confidence: HIGH

2. **Argument Building:** Correct CLI arguments generated
   - Files: `AutoRestArgumentProvider*Tests.cs`
   - Confidence: HIGH

3. **Dependency Management:** Tool installation works
   - Files: `DependencyInstallerTests.cs`
   - Confidence: MEDIUM (no uninstall test)

4. **Factory Creation:** Generator instantiation succeeds
   - Files: `AutoRestCodeGeneratorFactoryTests.cs`
   - Confidence: MEDIUM (no routing validation)

5. **Enum Presence:** AutoRest is in SupportedCodeGenerator enum
   - Files: `SupportedCodeGeneratorTests.cs`
   - Confidence: MEDIUM (count assertion brittle)

---

## 🔴 Critical Gaps (Risks)

| Gap | Risk Level | Impact | Solution |
|-----|-----------|--------|----------|
| CLI help text not tested | 🟠 Medium | Deprecation notice could break `--help` parsing | Add `CliHelpTests.cs` → invoke CLI, capture stdout |
| VSIX UI surfaces untested | 🔴 High | Deprecation UI (badges, dialogs) could render incorrectly | Add VSIX output/UI capture tests, mock DTE |
| VS Code extension generator list untested | 🟠 Medium | Extension could crash if deprecation breaks command registration | Add `extension.test.ts` for generators array |
| Factory routing not explicitly path-covered | 🟠 Medium | Deprecation in factory could create unreachable code | Add dedicated `CodeGeneratorFactoryAutoRestTests.cs` |
| Enum count test fails on removal, not deprecation | 🟡 Low | Removal phase will require test updates (expected) | Document as "expected failure during Phase 2" |
| OutputWindow message format untested | 🟡 Low | Deprecation log could break message parsing in IDE | Add output message format validation |
| Documentation has no automated checks | 🟡 Low | Stale docs misleading users on deprecated tool | Add grep-based markdown validation |

---

## 📋 Recommended Test Additions (Phase 1: Deprecation)

### Critical (Must Have)
1. **CliHelpTests.cs**
   - Test: `rapicgen csharp --help` shows AutoRest with deprecation notice
   - Test: `rapicgen csharp autorest --help` shows full command help + deprecation banner
   - Type: CLI integration test

2. **Enhanced SupportedCodeGeneratorTests.cs**
   - Add: `Enum_Contains_AutoRest()` — explicit test for AutoRest presence
   - Add: `Enum_Contains_AutoRestV3()` — explicit test for AutoRestV3 presence
   - Existing count test will remain = 8 until removal

3. **VS Code Generator List Tests**
   - Test: Generators array includes AutoRest command
   - Test: Display name is "AutoREST"
   - Type: Unit test in `suite/extension.test.ts`

### High Priority (Should Have)
4. **Enhanced Factory Tests**
   - Test: Factory.Create(SupportedCodeGenerator.AutoRest) returns AutoRestCSharpCodeGenerator
   - Test: Factory.Create(SupportedCodeGenerator.AutoRestV3) uses correct version
   - Type: Unit test, could be in `AutoRestCodeGeneratorFactoryTests.cs`

5. **VSIX Output Window Tests**
   - Test: AutoRest generation logs don't break message format
   - Test: Deprecation notice appears in output window
   - Type: VSIX integration test

6. **Documentation Validation**
   - Test: README.md contains "AutoRest" reference (for now)
   - Test: CLI.md lists AutoRest as supported generator
   - Test: Marketplace descriptions mention AutoRest
   - Type: Markdown grep-based unit test

### Low Priority (Nice to Have)
7. **VSIX UI Surface Tests**
   - Test: Settings page still renders AutoRest category
   - Test: "Add New AutoRest Client" command still visible
   - Type: VSIX integration test (complex, requires DTE mocking)

---

## 🗺️ Implementation Validation Checklist

When adding deprecation messaging:

- [ ] Build project → no new compiler errors
- [ ] Run unit tests → all pass (especially SupportedCodeGeneratorTests)
- [ ] Run integration tests → AutoRest code generation still works
- [ ] CLI help output → contains deprecation notice, no parsing breakage
- [ ] VS Code extension → builds & loads, AutoRest command still available
- [ ] VSIX → deploys, AutoRest settings & commands still visible
- [ ] Manual smoke test → generate code with deprecated tool, verify output valid

When removing AutoRest (Phase 2):

- [ ] Remove enum members → SupportedCodeGeneratorTests count becomes 6 (update test)
- [ ] Delete files → 40+ files across Core, VSIX, CLI, VSCode, Docs
- [ ] Update factory → remove switch case, verify no unreachable code
- [ ] Full build → no references to removed types
- [ ] All remaining generators → still work (NSwag, Kiota, Refitter, OpenAPI, Swagger)
- [ ] Release notes → explain removal, link migration guides

---

## 🏗️ File Summary

**Core Implementation (to test):**
- `src/Core/ApiClientCodeGen.Core/Generators/AutoRest/` (3 files)
- `src/Core/ApiClientCodeGen.Core/Options/AutoRest/` (2 files)

**VSIX Implementation (to test):**
- `src/VSIX/ApiClientCodeGen.VSIX.Shared/CustomTool/AutoRest/` (3 files)
- `src/VSIX/ApiClientCodeGen.VSIX.Shared/Options/AutoRest/` (2 files)
- `src/VSIX/ApiClientCodeGen.VSIX.Shared/Commands/AddNew/NewAutoRestClientCommand.cs`
- `src/VSIX/ApiClientCodeGen.VSIX.Shared/Commands/CustomTool/AutoRestCodeGeneratorCustomToolSetter.cs`

**CLI Implementation (to test):**
- `src/CLI/ApiClientCodeGen.CLI/Commands/CSharp/AutoRestCommand.cs`
- `src/CLI/ApiClientCodeGen.CLI/Commands/CSharp/AutoRestCodeGeneratorFactory.cs`

**VS Code Implementation (to test):**
- `src/VSCode/src/services/generators.ts` (AutoRest in array)

**Test Files (existing, to enhance):**
- `src/Core/ApiClientCodeGen.Core.Tests/Generators/AutoRest/` (3 test classes)
- `src/Core/ApiClientCodeGen.Core.Tests/SupportedCodeGeneratorTests.cs` (enhance enum tests)
- `src/Core/ApiClientCodeGen.Core.Tests/Extensions/CodeGeneratorNameExtensionsTests.cs` (enhance)
- `src/CLI/ApiClientCodeGen.CLI.Tests/Command/AutoRestCommand*.cs` (2 test classes)
- `src/CLI/ApiClientCodeGen.CLI.Tests/Extensions/SupportedCodeGeneratorNameTests.cs`

**Docs (to check):**
- `README.md` (feature list)
- `docs/CLI.md` (command list)
- `docs/Marketplace.md`, `Marketplace2022.md`, `Marketplace2026.md` (feature descriptions)

---

## Key Insights

1. **AutoRest has dual versions:** `AutoRest` (v2, for OpenAPI v2) and `AutoRestV3` (v3, for OpenAPI v3). Both must be tested/removed together.

2. **Enum count assertion is fragile:** SupportedCodeGeneratorTests checks for exactly 8 members. When AutoRest is removed, this test **will fail** (expected). Document this before removal.

3. **No deprecation messaging in code yet:** Current tests pass whether or not deprecation text appears. Tests must be written to validate deprecation *content*, not just presence.

4. **Test patterns are consistent:** All tests use xUnit [Fact]/[Theory], Moq, AutoFixture. No exotic frameworks. Tests should follow same patterns when added.

5. **Integration tests exist but are selective:** Some surfaces (VSIX UI, VS Code extension) lack integration tests. These are needed to prevent regression from deprecation/removal.

6. **Documentation has no automated enforcement:** No test ensures README/docs mention AutoRest. Stale docs are a risk during deprecation phase.

---

## Next Steps for Christian

**If proceeding with deprecation:**
1. Review full plan in `.squad/decisions/inbox/tank-autorest-validation-plan.md`
2. Assign test creation work (Tank volunteering!)
3. Define deprecation messaging format (warning level, placement in help/UI)
4. Coordinate timing with release notes & migration guide

**If proceeding toward removal:**
1. Use "Phase 2" removal checklist in detailed plan
2. Prepare migration documentation (NSwag, Kiota, Refitter alternatives)
3. Plan multi-release deprecation period
4. Identify AutoRest users in community for feedback

---

**Report Location:** `.squad/decisions/inbox/tank-autorest-validation-plan.md`  
**History Updated:** `.squad/agents/tank/history.md`
