# Decisions

> Team decisions that all agents must respect. Managed by Scribe.

---

## Decision: User Directives — No Direct Master Commits, PR Workflow Required

**Authority:** Christian (User)  
**Date:** 2026-03-04T14:17:13Z  
**Status:** Binding  

**Rule:** Never commit directly to the master branch. All work must be done from feature/fix branches related to the issue being worked on. All work must be presented as pull requests.

**Rationale:** Maintains code quality, review hygiene, and audit trail for all changes.

**Binding on:** All agents, all branches.

---

## Decision: User Directives — Comprehensive Testing Alongside Issue Work

**Authority:** Christian (User)  
**Date:** 2026-03-04T14:17:13Z  
**Status:** Binding  

**Rule:** Improve the codebase and add comprehensive tests where it makes sense, alongside issue work.

**Rationale:** Proactive quality and maintainability as part of normal workflow.

**Binding on:** All agents, all tasks.

---

## Decision: AutoRest Deprecation — 3-Phase Rollout with Terminology & Timeline

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-04  
**Context:** AutoRest is deprecated by Microsoft (retired July 1, 2026). AutoRest touches **5 IDE surfaces, 1 CLI, and 9 documentation surfaces** across 80+ files. Two enum values: `AutoRest` (v2) and `AutoRestV3` (v3 beta) — both deprecated together.

**Key Decisions:**

### 1. Terminology — Use "Deprecated" not "Outdated"
**Why:** "Outdated" implies old but still fine. "Deprecated" is industry-standard for "this will be removed."  
**Standard label format:** `"AutoRest (Deprecated)"`

### 2. Keep AutoRest fully functional during deprecation period
**Why:** Users need time to migrate. Breaking the generator now would be hostile. All code paths remain operational; we add warnings alongside functionality.

### 3. Both enum values deprecated together
**Why:** Both point at the same deprecated upstream tool. They differ only in OpenAPI version handling. Both must be deprecated and eventually removed as a unit.

### 4. Add `[Obsolete]` attributes to all public AutoRest types
**Why:** Compile-time warnings to NuGet package consumers. Industry-standard .NET deprecation pattern.  
**Attribute text:** `[Obsolete("AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future version. Use NSwag, Refitter, or Kiota instead.")]`

### 5. Emit runtime deprecation warning on every AutoRest invocation
**Why:** Users who invoke AutoRest via CLI or IDE should see a clear message every time. Most visible way to drive migration.
**Channels:** CLI (console warning), VSIX (output window + info bar), VS Code (popup), VS Mac (status bar), JetBrains (notification balloon).

### 6. Do NOT remove from menus or hide during deprecation
**Why:** Hiding breaks existing workflows without warning. Users see it, select it if needed, get deprecation message. Removal happens in Phase 3.

### 7. Create a migration guide document
**Why:** "Don't use this" without "what to use instead" is unhelpful. Need `docs/AutoRestMigration.md` with concrete steps.

**Rollout Phases:**
- **Phase 1 (Now):** Deprecation notices — `[Obsolete]` attributes, runtime warnings, documentation updates
- **Phase 2 (Months 2–5):** Monitor & support — track usage, refine guidance, no code removal
- **Phase 3 (Month 6, ~Jan 2027):** Full removal — delete all AutoRest code, enum values, VSIX/VSCode/VSMac/JetBrains surfaces

**Binding on:** Neo (core+CLI deprecation), Trinity (IDE surfaces), Tank (test validation), Documentation (README/docs updates).

---

## Inventory Findings: AutoRest Surface Coverage

**Neo's Core Inventory (9 surfaces):**  
SupportedCodeGenerator enum, AutoRestCSharpCodeGenerator, AutoRestArgumentProvider, AutoRestConstants, options interfaces/defaults, dependency installer, package dependencies, CLI command registration, CLI factory pattern.

**Trinity's Extension Inventory (11 surfaces):**  
VS Code (package.json + generators.ts), VSIX (custom tools + options pages), VS Mac (manifest + handlers), JetBrains (plugin.xml + action classes). Documentation: README, Marketplace docs (2022, 2026), CLI.md, GeneratedCodeUsage.md, website HTML (cli.html, features.html, download.html, index.html).

**Tank's Test Inventory (8 surfaces):**  
CLI help/documentation display, VSIX UI surfaces, VS Code command palette, factory selection & routing, output window messages, enum count & contract integrity, documentation references, dependency installation. **Gaps:** No CLI help display test, VSIX UI tests, VS Code extension tests. Recommended: Enhanced testing for deprecation message validation.

**Work Assignment:**
- **Neo:** Core library `[Obsolete]` attributes, CLI command descriptions, runtime warnings, DI registration updates
- **Trinity:** IDE extension updates (labels, warnings, settings pages, marketplace manifests)
- **Tank:** Test updates and validation
- **Neo + Trinity:** Documentation updates

---

## Decision: AutoRest Deprecation — Canonical Wording & Rollout Guardrails

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-22  
**Status:** Accepted  
**Binding on:** Neo (core + CLI), Trinity (IDE surfaces + docs), Tank (test validation)

### Short-Form Label (UI / Help / Menus)

```text
AutoRest (Deprecated)
```

CLI command description: `AutoRest (Deprecated - v3.0.0-beta.20210504.2)`

**Rules:**
- "Deprecated" appears in parentheses after generator name
- Version number is optional in short form, included for surfaces showing versions
- Never use "Outdated", "Legacy", "Old", or "End of Life" — use exactly "Deprecated"
- Casing: capital-D `Deprecated`

### Long-Form Warning (Runtime / Docs / Attributes)

```text
AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead.
```

**Used in:**
- `[Obsolete]` attribute (compile-time) — `error: false` (warning only)
- CLI runtime warning (stderr) — `WARNING:` prefix
- IDE runtime notification (non-blocking popup/message)

**Short form vs long form guardrail:**
- **Short form:** Menu items, command palette, CLI help descriptions, option pickers, action labels
- **Long form:** [Obsolete] attributes, stderr warnings, IDE notifications, README, docs, changelog, marketplace, website

### Key Decisions

1. **Keep AutoRest fully functional during deprecation period** — All code paths remain operational
2. **Both enum values deprecated together** — `AutoRest` (v2) and `AutoRestV3` (v3 beta) deprecate as a unit
3. **[Obsolete] attributes on all public AutoRest types** — Compile-time warnings to NuGet consumers
4. **Runtime warnings non-breaking** — No exit code change, no stdout pollution, no interactive prompts
5. **Do NOT remove from menus during Phase 1** — Users see it, select it, get deprecation message
6. **Migration guidance:** "Use NSwag, Refitter, or Kiota instead"

---

## Decision: AutoRest Deprecation — Phase 1 Code Review

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-22  
**Status:** Approved with observations  
**Branch:** `feature/autorest-deprecation`

**Verdict:** APPROVED. No blocking issues found. Comprehensive, correct, aligned with 3-phase plan.

**Review Scope:** 2 committed + 31 modified + 4 new files. Core [Obsolete] attributes, CLI runtime warning, IDE label changes, documentation updates, test guardrails.

**Build Verified:** 0 errors, 37 expected CS0618 warnings (deprecation markers).

**Observations (Non-blocking):**
1. Nearly all work uncommitted — needs staging/committing/pushing before PR
2. CS0618 warnings in new test file — suppressions not added yet
3. VSIX/IntelliJ/VSMac runtime warnings absent — acceptable for Phase 1

**Status:** Ready for merge once uncommitted changes are committed and PR created.

---

## Decision: Neo Implementation — AutoRest Deprecation

**Date:** 2026-03-22  
**Agent:** Neo (Core Dev)  
**Status:** Complete

**Changes Summary:**
- Added `[Obsolete]` to 8 public AutoRest-facing types in core scope
- Added `[Obsolete]` to CLI AutoRestCommand
- Updated CLI help description to `"AutoRest (Deprecated - v3.0.0-beta.20210504.2)"`
- Added scoped `#pragma warning disable CS0618` suppressions for DI registration
- Created `docs/AutoRestMigration.md` with 3 migration paths (NSwag, Refitter, Kiota)
- Updated README, CLI.md, GeneratedCodeUsage.md, CHANGELOG.md
- **Build:** 0 errors, 0 warnings (internal suppression working)
- **CLI Runtime:** Warning emits to stderr before progress output ✅
- **CLI Help:** Shows correct deprecated format ✅
- **Scope:** 15 files modified/created

**Breaking Changes:** None — purely additive deprecation.

---

## Decision: Neo Gap Closure — AutoRest Deprecation Annotation

**Date:** 2026-03-22  
**Agent:** Neo (Core Dev)  
**Status:** Complete

**Gaps Fixed:**
1. `SyncMethodOptions` enum — Added `[Obsolete]` attribute with canonical message
2. `DependencyInstaller.InstallAutoRest()` — Added XML doc remarks + `[Obsolete]` attribute

**Rationale:** Completeness over minimalism. Public types must signal deprecation consistently.

**Outcome:** All 10 public AutoRest-facing types in Core scope now have complete [Obsolete] coverage.

---

## Decision: Trinity Implementation — AutoRest IDE Deprecation

**Date:** 2026-03-22  
**Agent:** Trinity (Extension Dev)  
**Status:** Complete

**Changes Summary:**
- **VS Code:** Updated command title to `"AutoRest (Deprecated)"` + added non-blocking warning dialog
- **VSIX:** Updated button text to `"Generate with AutoRest (Deprecated)"` in both manifests
- **IntelliJ:** Updated action text, feature description to `"AutoRest (Deprecated)"` with July 1 retirement date
- **VS Mac:** Updated manifest labels to canonical form
- **Documentation:** Added deprecation notices to VSCode.md, VisualStudioForMac.md, Marketplace.md, website HTML
- **Verification:** VS Code npm ci/lint/compile all passed ✅

**Pattern:** Consistent short label `"AutoRest (Deprecated)"` across all surfaces.

---

## Decision: Trinity Gap Closure — AutoRest Non-Blocking Warning

**Date:** 2026-03-22  
**Agent:** Trinity (Extension Dev)  
**Status:** Complete

**Gaps Fixed:**
1. **VS Code warning:** Removed `await` and choice buttons — warning displays non-blocking
2. **VSIX label:** Updated from "Outdated" to canonical `"AutoRest (Deprecated)"`

**Rationale:** Non-blocking warning reduces workflow friction while maintaining visibility. All IDE surfaces now use "Deprecated" terminology consistently.

---

## Decision: Trinity Final Sweep — AutoRest Label Consistency

**Date:** 2026-03-22  
**Agent:** Trinity (Extension Dev)  
**Status:** Complete

**Changes Made:**
- **VSIX:** Settings display name updated to `"AutoRest (Deprecated)"`
- **IntelliJ:** Feature list, marketplace description, action definitions updated to canonical form

**Outcome:** All extension user-facing surfaces now consistently marked as deprecated.

---

## Decision: Tank Implementation — AutoRest Deprecation Testing

**Date:** 2026-03-22  
**Agent:** Tank (Tester)  
**Status:** Complete

**Test Coverage Added:**
- **CliHelpTests.cs** — 4 file-based integration tests validating CLI help, runtime warning, [Obsolete], DI suppression
- **AutoRestEnumRoutingSafetyTests.cs** — 7 enum safety tests preventing accidental value removal
- **SupportedCodeGeneratorTests.cs** — Enhanced with deprecation period checks
- **generators.test.ts** — VS Code extension generator configuration tests

**Product Code Fixes (Required for Compilation):**
1. `AutoRestArgumentProvider.cs` — Added missing `using System;`
2. `AutoRestCommand.cs` — Added missing `CancellationToken` parameter to Execute()

**Test Results:**
- Core.Tests: 473/473 passed ✅
- CLI.Tests: 46/46 passed ✅
- VS Code: Compiled successfully ✅

**Coverage Focus:** Phase 1-2 safety — prevents accidental AutoRest removal.

---

## Decision: Tank Gap Closure — AutoRest CLI Help Test

**Date:** 2026-03-22  
**Agent:** Tank (Tester)  
**Status:** Complete

**Gap:** CliHelpTests.cs was placeholder with stale assertions, no real behavior validation.

**Solution:** Replaced with 4 file-based integration tests:
1. **AutoRest_Help_Description_Contains_Deprecated_Label** — Validates CLI help shows canonical form
2. **AutoRest_Command_Contains_Runtime_Warning** — Validates stderr warning with correct text
3. **AutoRest_Command_Has_Obsolete_Attribute** — Validates [Obsolete] marker presence
4. **Program_Suppresses_Obsolete_Warnings_For_AutoRest_DI_Registration** — Validates CS0618 suppression

**Rationale:** File-based tests validate actual deployed behavior without process spawning overhead. Guards against accidental deprecation message removal.

**Test Results:** All 4 new tests pass ✅. Full CLI suite: 46/46 (no regressions).

---

## Decision: Tank Warning Suppression — AutoRest CS0618 Cleanup

**Date:** 2026-03-22  
**Agent:** Tank (Tester)  
**Status:** Complete

**Problem:** Test files intentionally validating deprecated AutoRest types emitted expected CS0618 warnings, cluttering build output.

**Solution:** Added scoped `#pragma warning disable CS0618` suppressions to 10 test files:
- **Core.Tests:** 9 files (SupportedCodeGeneratorTests, AutoRestEnumRoutingSafetyTests, ProjectFileUpdaterTests, etc.)
- **CLI.Tests:** 1 file (SupportedCodeGeneratorNameTests)

**Pattern:** Class-level pragmas with inline documentation, properly scoped and restored.

**Test Results:**
- Core.Tests: 473/473 passed ✅
- CLI.Tests: 46/46 passed ✅
- Build output: 0 CS0618 warnings from test files ✅

---

## Decision: Tank Final Validation — AutoRest Phase 1 Gate

**Date:** 2026-03-22  
**Agent:** Tank (Tester)  
**Status:** PASSED — Ready for PR

**Validation Results:**
- **Build:** `dotnet build Rapicgen.slnx` — Succeeded (0 errors, 18 expected CS0618 warnings)
- **Core Tests:** 473/473 passed ✅
- **CLI Tests:** 46/46 passed ✅
- **VS Code Extension:** Lint + compile both passed ✅
- **Smoke Tests:** All 3 generators (AutoRest, NSwag, Refitter) functional ✅
- **CLI Help:** Shows `"AutoRest (Deprecated - vX.Y.Z)"` ✅
- **Runtime Warning:** Stderr warning displays correctly ✅
- **Regressions:** None detected ✅

**Confidence Level:** High — zero test failures, build succeeds, smoke tests confirm functionality.

**Recommendation:** Branch meets quality gate. Ready for PR submission.

---

## Decision: OpenAPI Generator v7.21.0 Update Scope

**Authority:** Neo (Core Dev)  
**Date:** 2026-03-26  
**Status:** Complete  
**Context:** Standardized pattern from PR #1481 applied to bump v7.20.0 → v7.21.0 across Core, CLI, Tests, Docs, and IDE extensions.

**Scope Executed:**
- **Commit 1:** Core version registry (`OpenApiGeneratorVersions.cs`), enum (`OpenApiSupportedVersion.cs` → `V7210`), resources (`Resource.resx` → new hashes)
- **Commit 2:** CLI help (`Program.cs` → v7.21.0), test data (`OpenApiVersionExtensionsTests.cs` → re-keyed fixtures)
- **Commit 3:** Documentation (9 files: README, CLI.md, Marketplace*, website/*.html, java/README.md)
- **Commit 4:** IDE extensions (VSCode package.json, VSIX manifests ×2, VS Mac, IntelliJ plugin.xml)
- **Commit 5:** Enum test coverage (`EnumValues_MatchExpectedIntValues` → added V7210 case)

**Hashes Computed:**
- SHA1: `19480dd1572a344c69a26c7488eda13f3caaf14e`
- MD5: `5925081963d078083af5380fd62317d4`

**Git Commits Created:** 5 (042e587ae, 9d7fc280b, 3fecc5076, e2d1b8f07, fe1e94724)

---

## Decision: OpenAPI Generator Update — Reusable Pattern Documentation

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-26  
**Status:** Complete  
**Context:** Formalize v7.21.0 bump automation as reusable guidance for future updates.

**Deliverables:**
1. **Squad Skill:** `.squad/skills/update-openapi-generator/SKILL.md` — Complete pattern documentation for agents
2. **Copilot Instructions:** `.github/copilot-instructions.md` — Added "Recurring Tasks" section with script pointer

**Binding Rule:** All agents processing "Update OpenAPI Generator to vX.X.X" requests **must** use `.\scripts\update-openapi-generator.ps1`. Manual file editing is prohibited when the script covers the change.

**Rationale:** Preserves 4-commit pattern from PR #1481; reduces agent work from 20-file manual edits to single script invocation; ensures consistency across future updates.

---

## Decision: OpenAPI Generator v7.21.0 Validation Results

**Authority:** Tank (Tester/QA)  
**Date:** 2026-03-26  
**Status:** APPROVED  
**Branch:** `openapi-generator-7.21.0`

**Validation Checklist (24/24 PASSED):**

| Category | Items | Status |
|----------|-------|--------|
| **Hashes** | SHA1, MD5 | ✓ PASS |
| **Enum** | Numeric value, Latest pointer, XML docs | ✓ PASS |
| **Tests** | Core (477), CLI (46), OpenApiVersionExtensionsTests (53) | ✓ PASS |
| **CLI** | Help text display | ✓ PASS |
| **Build** | dotnet build Rapicgen.slnx | ✓ PASS |
| **Documentation** | 9 files, no stale references | ✓ PASS |
| **IDE Extensions** | VSCode, VSIX (2x), VS Mac, IntelliJ | ✓ PASS |

**Test Results:**
- Core.Tests: 477/477 passed ✅
- CLI.Tests: 46/46 passed ✅
- OpenApiVersionExtensionsTests: 53/53 passed ✅
- Build: 0 errors (24 expected CS0618 AutoRest warnings acceptable)
- Smoke Tests: NSwag, OpenAPI v7.21.0, Refitter all functional ✅

**Fragile Areas Verified:**
- Hash integrity vs. Maven Central ✅
- Enum numeric mapping (7210 = 7.21.0) ✅
- Latest property consistency across all surfaces ✅
- Test data coverage complete ✅
- No orphaned version strings ✅

**Recommendation:** APPROVED FOR MERGE. All quality gates passed. Ready for PR submission.

---

## Decision: OpenAPI Generator v7.21.0 PR Created

**Date:** 2026-03-26  
**Authority:** Morpheus (Lead)  
**Status:** COMPLETE

**PR Details:**
- **Title:** OpenAPI Generator v7.21.0
- **URL:** https://github.com/christianhelle/apiclientcodegen/pull/1523
- **Base:** master
- **Head:** openapi-generator-7.21.0
- **Commits:** 8 (4 version bump + 4 reusable guidance)

**What Happened:**

1. **Branch validation:** `openapi-generator-7.21.0` branch already exists with clean worktree and all 8 commits (4 from automation script + 4 from guidance finalization)
2. **Push to origin:** Successful, no conflicts
3. **PR creation:** Used GitHub CLI with comprehensive PR body
4. **No duplicate PRs:** Verified no existing PR for this branch

**Key Content in PR Body:**

- **Section 1: Four version update buckets** — Version registry & hashes, CLI & tests, docs, IDE extensions
- **Section 2: Reusable guidance** — Skill file + copilot instructions addition
- **Section 3: Validation results** — Build, tests, CLI help, VSCode extension all pass
- **Section 4: Automation benefits** — Four-commit pattern + script reuse = repeatable process for future versions

**Design Rationale:**

**Why include reusable guidance in the same PR as the version bump?**
- Binds automation + documentation together
- Shows future team members this is the canonical pattern
- Squad skill becomes discoverable from PR history

**PR body structure reflects team communication norms:**
- Clear section headers for scanning
- Checkmarks for validation steps (human-readable outcomes)
- Reference back to prior patterns (PR #1481)
- Automation narrative (so Christian understands the cost-benefit of script-first approach)

**Downstream Implications:**

- Next OpenAPI Generator update request should reference this PR + the skill file
- Tank can reference this PR when reviewing future version bumps
- CI/CD pipeline can use `.squad\skills\update-openapi-generator\SKILL.md` as the validation checklist

**Binding Decision:**

**For all future `Update OpenAPI Generator to vX.X.X` requests:**
1. Start from the skill file (`.squad\skills\update-openapi-generator\SKILL.md`)
2. Use the automation script (`scripts\update-openapi-generator.ps1`)
3. Include the validation checklist from the skill
4. PR body should follow the same 4-section structure (version buckets, guidance, validation, automation narrative)