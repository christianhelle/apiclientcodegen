# Decisions Archive

> Superseded or historical decisions archived for reference. Current binding decisions are in decisions.md.

**Archival Date:** 2026-04-28  
**Criteria:** Entries dated before 2026-03-29 (older than 30 days)

---

## Decision: AutoRest Deprecation — 3-Phase Rollout with Terminology & Timeline

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-04  
**Status:** Superseded by Phase 1 completion (2026-03-22)

AutoRest is deprecated by Microsoft (retired July 1, 2026). Three-phase rollout: Phase 1 (deprecation notices now), Phase 2 (monitor months 2-5), Phase 3 (full removal month 6).

**Key Points:**
- Terminology: "Deprecated" (not "Outdated")
- Keep fully functional during deprecation
- Both enum values (AutoRest v2 and AutoRestV3) deprecated together
- [Obsolete] attributes on all public types
- Runtime deprecation warnings on invocation
- No menu removal during Phase 1
- Migration guide document required

---

## Decision: Inventory Findings — AutoRest Surface Coverage

**Date:** 2026-03-04  
**Status:** Reference (Phase 1 execution complete)

**Neo's Core Inventory (9 surfaces):**  
SupportedCodeGenerator enum, AutoRestCSharpCodeGenerator, AutoRestArgumentProvider, AutoRestConstants, options interfaces, dependency installer, package dependencies, CLI registration, factory pattern.

**Trinity's Extension Inventory (11 surfaces):**  
VS Code, VSIX, VS Mac, JetBrains plugins + 9 documentation surfaces.

**Tank's Test Inventory (8 surfaces):**  
CLI help, VSIX UI, VS Code, factory routing, output messages, enum integrity, documentation, dependency installation.

---

## Decision: AutoRest Deprecation — Canonical Wording & Rollout Guardrails

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-22  
**Status:** Accepted and implemented

**Short Form:** `"AutoRest (Deprecated)"`  
**Long Form:** "AutoRest is deprecated by Microsoft and will be retired on July 1, 2026. AutoRest support will be removed from this tool in a future major version. Use NSwag, Refitter, or Kiota instead."

---

## Decision: AutoRest Deprecation — Phase 1 Code Review

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-22  
**Status:** Approved

Build verified (0 errors, 37 expected CS0618 warnings). Core [Obsolete] attributes, CLI runtime warning, IDE label changes, documentation updates, test guardrails all implemented correctly.

---

## Decision: Neo Implementation — AutoRest Deprecation

**Date:** 2026-03-22  
**Status:** Complete

Added [Obsolete] to 8 public types, CLI help updated to canonical form, runtime warning added, migration guide created, documentation updated. 15 files modified. Zero breaking changes.

---

## Decision: Neo Gap Closure — AutoRest Deprecation Annotation

**Date:** 2026-03-22  
**Status:** Complete

Fixed 2 missing annotations: SyncMethodOptions enum and DependencyInstaller.InstallAutoRest() method. All 10 public AutoRest-facing types now have complete deprecation coverage.

---

## Decision: Trinity Implementation — AutoRest IDE Deprecation

**Date:** 2026-03-22  
**Status:** Complete

Updated VS Code, VSIX, IntelliJ, VS Mac with canonical `"AutoRest (Deprecated)"` labels. Documentation updated. All IDE surfaces consistent.

---

## Decision: Trinity Gap Closure — AutoRest Non-Blocking Warning

**Date:** 2026-03-22  
**Status:** Complete

VS Code warning made non-blocking (removed await). VSIX label updated to canonical form. All IDE surfaces now use "Deprecated" terminology consistently.

---

## Decision: Trinity Final Sweep — AutoRest Label Consistency

**Date:** 2026-03-22  
**Status:** Complete

VSIX settings display updated. IntelliJ feature list, marketplace description, action definitions updated to canonical form. All extension surfaces consistent.

---

## Decision: Tank Implementation — AutoRest Deprecation Testing

**Date:** 2026-03-22  
**Status:** Complete

Added CliHelpTests.cs (4 file-based integration tests), AutoRestEnumRoutingSafetyTests.cs (7 enum safety tests), enhanced SupportedCodeGeneratorTests.cs, added generators.test.ts. Fixed 2 product code issues: missing `using System;` in AutoRestArgumentProvider, missing CancellationToken in AutoRestCommand. 519 tests pass. Build clean.

---

## Decision: Tank Gap Closure — AutoRest CLI Help Test

**Date:** 2026-03-22  
**Status:** Complete

Replaced placeholder CliHelpTests.cs with 4 meaningful file-based integration tests validating CLI help description, runtime warning, [Obsolete] attribute, and CS0618 suppression. All 4 tests pass.

---

## Decision: Tank Warning Suppression — AutoRest CS0618 Cleanup

**Date:** 2026-03-22  
**Status:** Complete

Added scoped #pragma warning disable CS0618 suppressions to 10 test files (9 Core.Tests, 1 CLI.Tests). Test results: Core 473/473, CLI 46/46. Build now clean.

---

## Decision: Tank Final Validation — AutoRest Phase 1 Gate

**Date:** 2026-03-22  
**Status:** PASSED

Build succeeded (0 errors, 18 expected CS0618 warnings). Core.Tests 473/473, CLI.Tests 46/46, VS Code extension passed lint + compile. Smoke tests confirm all 3 generators functional. CLI help and runtime warning verified. Zero regressions. Quality gate PASSED.

---

## Decision: OpenAPI Generator v7.21.0 Update Scope

**Authority:** Neo (Core Dev)  
**Date:** 2026-03-26  
**Status:** Complete

5 commits (4 standard + 1 enum test) following PR #1481 pattern. All 22 files updated across Core, CLI, tests, docs, IDE extensions. Hashes: SHA1 `19480dd1572a344c69a26c7488eda13f3caaf14e`, MD5 `5925081963d078083af5380fd62317d4`.

---

## Decision: OpenAPI Generator Update — Reusable Pattern Documentation

**Authority:** Morpheus (Lead)  
**Date:** 2026-03-26  
**Status:** Complete

Formalized v7.21.0 bump automation as reusable guidance. Created `.squad/skills/update-openapi-generator/SKILL.md` and added "Recurring Tasks" section to `.github/copilot-instructions.md`. Binding rule: All agents must use automation script (manual editing prohibited).

---

## Decision: OpenAPI Generator v7.21.0 Validation Results

**Authority:** Tank (QA)  
**Date:** 2026-03-26  
**Status:** APPROVED

24/24 validation checks passed. Hashes verified, enum mapping correct, build succeeded (0 errors), tests passed (477 Core, 46 CLI, 53 OpenApiVersionExtensionsTests), documentation complete (9 files), IDE extensions complete (5 platforms). APPROVED FOR MERGE.

---

## Decision: OpenAPI Generator v7.21.0 PR Created

**Date:** 2026-03-26  
**Authority:** Morpheus (Lead)  
**Status:** Complete

PR #1523 created with comprehensive body. Title "OpenAPI Generator v7.21.0", includes 4-section structure (version buckets, reusable guidance, validation results, automation narrative). Branch merged into origin.

---

## Decision: Binding — OpenAPI Generator Version Update Process

**Authority:** Team (from PR #1481, #1523)  
**Date:** 2026-03-26  
**Status:** Binding (enforced for v7.22.0)

All version updates must: (1) Use automation script, (2) Preserve 4-commit grouping, (3) Run validation checklist, (4) Reference SKILL.md, (5) QA review all 10 quality gates before merge. Script + skill + QA = repeatable high-confidence process.

---

**Archive maintained by:** Scribe  
**Last updated:** 2026-04-28
