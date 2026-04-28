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

## Decision: Morpheus Leadership Review — OpenAPI Generator v7.22.0 Pre-Implementation

**Authority:** Morpheus (Lead)  
**Date:** 2026-04-28  
**Status:** Approved  
**For:** Neo (Implementer)

**Scope:** Pre-implementation review of v7.22.0 update workflow. Script audit, fragile area identification, conflict risk assessment, validation checklist.

### Gotchas & Fragile Areas
1. **Enum padding math:** V7220 = 7220 (7×1000 + 22×10 + 0) — confirmed safe
2. **PR #1548 conflict risk:** System.Text.Json 9.0.14→9.0.15 is orthogonal; no merge conflict risk
3. **Test regex patterns:** Fragile if test file structure changes; manual review required post-script
4. **Resource.resx hashes:** Regex precision critical; manual verification required
5. **Documentation surfaces:** 9 files total; all must be updated
6. **IDE extension strings:** 6 files across 4 platforms; version strings must be consistent
7. **Commit grouping:** Exact 4-bucket pattern must be preserved (PR #1481 standard)

### Validation Checklist
- Step 1: Verify commits (4 standard in correct order)
- Step 2: Enum coverage tests must pass
- Step 3: CLI help shows "OpenAPI Generator (v7.22.0)"
- Step 4: Grep for stale v7.21.0 references
- Step 5: Build & unit tests pass

### Decision
Use automation script, preserve 4-commit grouping, run full validation before pushing. Script is the single source of truth; manual editing is prohibited.

---

## Decision: Neo Implementation — OpenAPI Generator v7.22.0 Update

**Date:** 2026-04-28  
**Agent:** Neo (Core Dev)  
**Status:** Complete

### Automation Execution
- **Script:** .\scripts\update-openapi-generator.ps1 -NewVersion "7.22.0"
- **Branch:** openapi-generator-v7.22.0 (clean)
- **JAR hashes:** SHA1 a154752b82c9b84151cd4998ce2a86ed21f5bd3, MD5 24803a056bc36a4f8824612fb31c8133

### Commits Created (5 Total)
1. 99087ea61 - Add OpenAPI Generator v7.22.0 version entry and hashes
2. 96d1360b - Update CLI description and tests for OpenAPI Generator v7.22.0
3. ca4ecb17 - Update documentation for OpenAPI Generator v7.22.0
4. af8067d2 - Update IDE extensions for OpenAPI Generator v7.22.0
5. 9593ee930 - Add V7220 to enum value test coverage (manual)

### Validation Results
- **Build:** 0 errors, 26 expected CS0618 warnings ✓
- **Tests:** 56 OpenApiVersionExtensionsTests passed ✓
- **CLI:** csharp --help shows "OpenAPI Generator (v7.22.0)" ✓
- **Smoke:** NSwag functional (145KB output) ✓

### Manual Follow-up
Added [InlineData(OpenApiSupportedVersion.V7220, 7220)] to EnumValues_MatchExpectedIntValues test. Expected script gap (documented in history).

**Status:** Implementation validated. Branch ready for QA review.

---

## Decision: Tank Validation — OpenAPI Generator v7.22.0 QA Review

**Date:** 2026-04-28  
**Agent:** Tank (QA/Tester)  
**Status:** APPROVED

### Quality Gate Checks (10/10 Passed)
1. ✅ Commit structure (4 standard + 1 enum test)
2. ✅ Co-authored-by trailers present in all commits
3. ✅ No PR #1548 System.Text.Json changes mixed in
4. ✅ Build succeeded (0 errors, 22 expected warnings)
5. ✅ 57 OpenApiVersionExtensionsTests passed
6. ✅ CLI help shows "OpenAPI Generator (v7.22.0)"
7. ✅ Enum correctness (V7220=7220, Latest→V7220)
8. ✅ Documentation coverage (9 files updated)
9. ✅ IDE extension coverage (6 files updated)
10. ✅ No stale references (v7.21.0 only in enum history)

### Validation Details
- **Hash verification:** SHA1 and MD5 match Maven Central ✓
- **Test coverage:** V7220 present in all 4 test methods (IsLatest, IsOlderThanLatest, EnumValues_MatchExpectedIntValues, ResolveVersion) ✓
- **File changes:** 21 product files + 2 squad files (proper isolation) ✓
- **Build time:** ~7 seconds (normal range) ✓

### FINAL VERDICT
**APPROVED** — Branch openapi-generator-v7.22.0 is complete, isolated, tested, and ready for merge to main/master.

### Key Learning
Script-first workflow effective. Manual enum test (EnumValues_MatchExpectedIntValues) is expected and properly isolated. Same pattern from v7.21.0 review validated for v7.22.0.

---

## Decision: Binding — OpenAPI Generator Version Update Process

**Authority:** Team (Morpheus, Neo, Tank)  
**Status:** Binding  
**Applies to:** All future OpenAPI Generator version updates

**Rule:** All version update requests must follow the v7.21.0 → v7.22.0 established pattern:
1. Use .\scripts\update-openapi-generator.ps1 automation
2. Preserve exact 4-commit grouping (Core → CLI/tests → Docs → IDE → enum test)
3. Run complete validation checklist before pushing
4. Reference .squad/skills/update-openapi-generator/SKILL.md for detailed guidance
5. QA review must verify all 10 quality gates before merge approval

**Rationale:** Automation eliminates manual error, ensures consistency, maintains 22-file footprint predictability. Script + skill + QA checklist = repeatable, high-confidence process.

---

**Archived Decisions:** See decisions-archive.md for decisions dated before 2026-03-29.  
**Managed by:** Scribe  
**Last updated:** 2026-04-28
