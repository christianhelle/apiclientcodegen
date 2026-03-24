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

- **2026-03-04:** AutoRest CLI Help Test Gap Fixed. Replaced placeholder `CliHelpTests.cs` with 4 meaningful file-based integration tests:
  - **Pattern:** Read source files (Program.cs, AutoRestCommand.cs) to validate actual deployed behavior
  - **Coverage:** CLI help description, runtime warning emission, [Obsolete] attribute presence, CS0618 suppression in DI registration
  - **Results:** All 4 new tests pass. Full CLI test suite: 46/46 tests pass (no regressions).
  - **Decision rationale:** File-based tests validate real behavior without process spawning overhead. Simpler and faster than E2E CLI invocation. Guards against accidental deprecation message removal during refactoring.
  - **Impact:** Tests will fail if deprecation messaging is removed/changed (Phase 1 protection). Tests document what will break in Phase 3 removal. Delete `CliHelpTests.cs` when AutoRest is fully removed.
  - **Pattern for future:** Consider file-based tests for VSIX help text, VS Code command registration, documentation consistency where integration points lack unit test coverage.

- **2026-03-04:** AutoRest CS0618 Warning Suppression Complete. Cleaned up test-only build warnings from deprecated AutoRest types:
  - **Problem:** Morpheus-approved deprecation changes emitted expected CS0618 warnings from test files that intentionally reference deprecated AutoRest types, cluttering build output.
  - **Solution:** Added scoped `#pragma warning disable CS0618` suppressions to 10 test files (9 Core.Tests, 1 CLI.Tests) that intentionally validate AutoRest during deprecation period.
  - **Pattern:** Class-level suppression with inline comment explaining reason, properly restored at class end. NOT applied to files using `AutoRestDeprecatedTestClass` base (already suppressed).
  - **Files modified:** SupportedCodeGeneratorTests, AutoRestEnumRoutingSafetyTests, ProjectFileUpdaterTests, CodeGeneratorNameExtensionsTests, GetDependenciesTests, 3 PackageDependency test files, DependencyInstallerTests, SupportedCodeGeneratorNameTests.
  - **Test results:** All tests pass - Core.Tests: 473/473 ✅, CLI.Tests: 46/46 ✅. Build now completely clean (0 CS0618 warnings from test files).
  - **Rationale:** Tests remain valuable to prevent accidental AutoRest removal in Phase 1/2. Suppressions are scoped, documented, and will be deleted with AutoRest code in Phase 3.
  - **Decision documented:** `.squad/decisions/inbox/tank-autorest-warning-suppression.md` for team review.

- **2026-03-22 — AutoRest Phase 1 Testing & Validation Complete:**
  - **Test implementation:** 20+ new test cases across Core, CLI, VSCode extensions + 2 product code fixes
  - **CLI tests:** 4 file-based integration tests validating help description, runtime warning, obsolete, DI suppression
  - **Enum safety:** 7 tests preventing accidental enum value removal during deprecation period
  - **VS Code:** Generator configuration tests validating AutoRest registration + deprecation label
  - **Product fixes:** Added missing using directive (AutoRestArgumentProvider) and CancellationToken parameter (AutoRestCommand)
  - **Warning cleanup:** Applied scoped CS0618 suppressions to 10 test files - build now clean
  - **Final validation:** 519 tests pass (473 Core + 46 CLI). Build succeeded (0 errors, 18 expected warnings). Smoke tests confirm all 3 generators functional.
  - **Code review:** Morpheus approved implementation - no blocking issues
  - **Quality gate:** PASSED - branch ready for PR submission
  - **Status:** Test and validation scope 100% complete. All Phase 1 objectives met. Ready for PR/merge.

- **2026-03-04 — OpenAPI Generator v7.21.0 Update Planning:**
  - **Audit scope:** Analyzed PR #1481 (v7.20.0 update) patterns, test coverage, fragile areas, automation script (324 lines in `scripts/update-openapi-generator.ps1`)
  - **Test surface mapping:** Identified 4 test suites: Core.Tests (473 tests), CLI.Tests (46 tests), Integration tests (~15 network-dependent), VSCode extensions (TypeScript)
  - **Core unit tests:** Progress reporting (OpenApiCSharpCodeGeneratorTests), error handling (ExceptionTests), version enum logic (OpenApiVersionExtensionsTests — 13+ cases)
  - **CLI tests:** Command creation, factory routing, CLI help text validation (file-based CliHelpTests), settings objects
  - **Integration tests:** Real JAR execution via OpenApiCodeGeneratorFixture, code compilation validation via BuildHelper.BuildCSharp(), JSON+YAML, OpenAPI v2+v3, JMeter variants
  - **Extension tests:** VSCode package.json version string, TypeScript generator registration, npm lint/compile validation
  - **Fragile areas identified:** 
    - JAR download + SHA1/MD5 hash validation (Maven Central CDN dependency)
    - Enum comparison logic for v7.12.0 CookieContainer workaround (may need update if v7.21.0 introduces new conflicts)
    - Test enum value assertions (13+ InlineData hardcoded; script auto-updates, but requires manual diff review)
    - Generated code output differences between versions (caught by BuildHelper.BuildCSharp())
    - Script regex pattern failures (test structure changes, enum field reordering)
  - **Critical files to update:** OpenApiSupportedVersion.cs (enum + Latest property), OpenApiGeneratorVersions.cs (JAR URL + hashes), OpenApiVersionExtensionsTests.cs (test data), Program.cs (CLI help), Resource.resx/.Designer.cs (embedded hash resources), package.json (VSCode command label), IDE manifests (VSIX/VSMac/IntelliJ), documentation (9 files)
  - **Validation checklist:** 40-point comprehensive checklist covering pre-update verification, script execution, build validation, unit/CLI tests, integration test execution, CLI smoke tests (NSwag, OpenAPI), extension validation, orphaned string cleanup, final quality gate
  - **Risk assessment:** 6 risks identified and ranked (JAR hash mismatch: Low/Impact Medium; Enum regex fail: Medium/High; Generated code incompatibility: Low/High; Version conflicts: Very Low/High; Test structure changes: Very Low/Low; Network unavailability: Low/Medium)
  - **Reference pattern:** PR #1481 added 324-line automation script; 22 files updated; 4 sequential git commits (Core registry, CLI+tests, Docs, Extensions); validated with build+OpenApiVersionExtensionsTests only (not full integration suite)
  - **Deliverable:** `.squad/decisions/inbox/tank-openapi-generator-v7.21.0-validation-plan.md` — 350+ lines, comprehensive validation framework
  - **Key insight:** Automation script reduces manual error; validation focus is on enum consistency (most fragile), JAR hash verification (network-dependent), generated code compilation (reveals output differences), and orphaned string cleanup (catches regex failures)

- **2026-03-24 — OpenAPI Generator v7.21.0 Implementation Review (APPROVED):**
   - **Branch state:** openapi-generator-7.21.0 with 5 commits representing Buckets 1-4 + enum test coverage
   - **Commit verification:**
     - 042e587ae: OpenApiGeneratorVersions.cs (v7.21.0 JAR + hashes), OpenApiSupportedVersion.cs enum, Resource files
     - 9d7fc280b: Program.cs CLI help (v7.21.0), OpenApiVersionExtensionsTests.cs test data
     - 3fecc5076: 9 documentation files updated (README, CLI.md, Marketplace, website, VS Mac, Java)
     - e2d1b8f07: IDE manifests (VSCode package.json, VSIX x2, IntelliJ, VS Mac)
     - fe1e94724: V7210 enum test coverage in EnumValues_MatchExpectedIntValues
   - **Hash validation:** SHA1=19480dd1572a344c69a26c7488eda13f3caaf14e, MD5=5925081963d078083af5380fd62317d4
   - **Enum correctness:** V7210=7210, Latest property → V7210, XML docs updated
   - **Test validation:** Core.Tests 477/477 (includes 53 OpenApiVersionExtensionsTests), CLI.Tests 46/46, CLI help shows v7.21.0, VSCode lint clean
   - **Build validation:** dotnet build succeeded, 0 errors, 24 expected CS0618 AutoRest warnings
   - **Documentation:** 9 files updated, no stale v7.20.0 references
   - **IDE coverage:** VSCode, VSIX (Dev17 + 2019), VS Mac, IntelliJ all updated
   - **Fragile areas:** Enum numeric (7210 correct), hash distribution (consistent), Latest pointer (correct), test coverage (V7210 in tests)
   - **Quality gate: 24/24 validation checks PASSED**
   - **FINAL VERDICT: APPROVED — Complete, coherent, ready for merge. No blocking issues.**

- **2026-03-26 — OpenAPI Generator v7.21.0 Final Validation & Logging:**
  - **Review scope completed:** All orchestration log entries verified, inbox decisions reviewed and merged to decisions.md
  - **Validation summary:** 24/24 quality gate checks passed across hash integrity, enum mapping, test coverage, build, CLI integration, documentation, and IDE extensions
  - **Commit verification:** All 5 commits follow PR #1481 pattern with proper scope grouping (Core registry, CLI+tests, docs, IDE, enum coverage)
  - **Orchestration logged:** 3 orchestration log files created (Neo, Morpheus, Tank execution logs)
  - **Session log created:** `.squad/log/2026-03-26T000000Z-openapi-generator-v7210-complete.md` documents overall session completion
  - **Decisions merged:** 3 new entries added to decisions.md from inbox files; 6 inbox files deleted
  - **History updated:** Neo and Morpheus agent history extended with implementation notes
  - **Status:** Ready for PR submission to main/master. All artifacts organized in .squad/ structure.
