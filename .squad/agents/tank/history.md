# Tank — History

## Core Context

**Team:** REST API Client Code Generator (multi-platform .NET tool)  
**Role:** QA/Test specialist  
**Test paths:** `src/Core/ApiClientCodeGen.Core.Tests/`, `src/Core/ApiClientCodeGen.Core.IntegrationTests/`, `src/CLI/ApiClientCodeGen.CLI.Tests/`  
**User:** Christian

**AutoRest Deprecation (2026-03-04 to 2026-03-22):** Completed Phase 1 testing. Added CliHelpTests.cs (4 file-based integration tests), AutoRestEnumRoutingSafetyTests.cs (7 enum safety tests), enhanced SupportedCodeGeneratorTests. Applied scoped CS0618 suppressions to 10 test files. Final validation: 519 tests pass (473 Core + 46 CLI), build clean (0 errors, 18 expected warnings). All 3 generators (AutoRest, NSwag, Refitter) functional in smoke tests. Ready for PR.

**OpenAPI Generator v7.21.0 Planning (2026-03-04):** Analyzed PR #1481 patterns (4-commit structure, 324-line automation script, 22-file footprint). Identified fragile areas: JAR hash validation (Maven Central dependency), enum consistency (most fragile), test data assertions (script auto-updates). Built comprehensive 40-point validation checklist covering pre-update, script execution, build, unit/CLI/integration tests, smoke tests, extension validation, orphaned strings, quality gates. Key insight: Automation script eliminates manual error; validation focus on enum consistency, JAR hash integrity, generated code compilation, and orphaned string cleanup.

---

## Learnings

- **2026-03-24 — OpenAPI Generator v7.21.0 Implementation Review (APPROVED):**

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

- **2026-05-20 — OpenAPI Generator v7.22.0 QA Review (APPROVED):**
  - **Branch:** openapi-generator-v7.22.0 with 5 commits (4 standard + 1 enum test)
  - **Commit verification:** All commits have required Co-authored-by trailers ✅
    - 99087ea61: Core registry (OpenApiGeneratorVersions.cs, OpenApiSupportedVersion.cs, Resource files)
    - a96d1360b: CLI description (Program.cs) + test updates (OpenApiVersionExtensionsTests.cs)
    - bca4ecb17: Documentation (9 files: README, CLI.md, Marketplace docs, website, java/README.md)
    - baf8067d2: IDE extensions (6 files: VSCode, VSIX×2, IntelliJ, VS Mac)
    - 9593ee930: Enum test coverage (EnumValues_MatchExpectedIntValues with V7220=7220)
  - **Enum correctness:** V7220=7220, Latest property → V7220, XML docs updated ✅
  - **Hash validation:** SHA1=aa154752b82c9b84151cd4998ce2a86ed21f5bd3, MD5=24803a056bc36a4f8824612fb31c8133 ✅
  - **Build validation:** Succeeded with 0 errors, 22 expected CS0618 AutoRest warnings ✅
  - **Test validation:** 57 OpenApiVersionExtensionsTests passed (includes V7220 in 4 test methods) ✅
  - **CLI integration:** `rapicgen csharp --help` shows "OpenAPI Generator (v7.22.0)" ✅
  - **Documentation coverage:** All 9 doc files updated from 7.21.0 to 7.22.0 ✅
  - **IDE extensions:** All 6 IDE extension files updated ✅
  - **Stale references:** No stale 7.21.0 references found (only expected historical enum values) ✅
  - **PR #1548 isolation:** No System.Text.Json changes mixed into OpenAPI Generator commits ✅
  - **Test coverage completeness:** V7220 properly added to IsLatest, IsOlderThanLatest, EnumValues_MatchExpectedIntValues, and ResolveVersion tests
  - **Commit grouping:** Follows PR #1523 pattern exactly (Core → CLI/tests → Docs → IDE → enum test)
  - **Quality gate:** 10/10 validation checks PASSED
  - **FINAL VERDICT: APPROVED — Complete, isolated, ready for merge. No blocking issues.**
  - **Key learning:** Script-first workflow works perfectly. Manual enum test addition took <1 minute and was properly isolated in its own commit.
