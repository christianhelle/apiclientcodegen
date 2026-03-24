# Morpheus — History

## Learnings

- **2026-03-04:** Team initialized. Project is REST API Client Code Generator — multi-platform .NET tool wrapping OpenAPI code generators. User is Christian.
- **2026-03-04:** AutoRest deprecation investigation complete. AutoRest touches 80+ files across 5 IDE surfaces (VS Code, VSIX, VS Mac, JetBrains, CLI), 9 documentation surfaces, and 17+ test files. Two enum values exist: `AutoRest` (v2) and `AutoRestV3` (v3 beta) — both deprecated together. Key architecture insight: the `ICodeGenerator`/`IDependencyInstaller`/`CodeGeneratorFactory` abstraction pattern means removal is surgical — no cascading breaks across generators. AutoRest is already labeled "Outdated" in VS Code, VS Mac, and JetBrains UIs but has NO `[Obsolete]` attributes, NO runtime warnings, and NO migration guidance. Proposed 3-phase rollout: (1) deprecation notices now, (2) monitor months 2-5, (3) removal at month 6 as part of v2.0 major version bump. Full plan written to `.squad/decisions/inbox/morpheus-autorest-deprecation-plan.md`. Christian prefers the term "Deprecated" over "Outdated." The IntelliJ plugin lives at `src/IntelliJ/` (Kotlin/Gradle), NOT in the `java/` directory (which is just JDK binaries).

- **2026-03-22 — AutoRest Deprecation Wording & Review Complete:**
  - **Wording decision finalized:** Defined canonical short-form `"AutoRest (Deprecated)"` and long-form warning with July 1, 2026 retirement date + migration guidance
  - **Guardrails established:** Short form for UI/menus, long form for runtime/docs — binding on all team members
  - **Code review approved:** No blocking issues in Neo + Trinity implementation. Build succeeded (0 errors, 37 expected CS0618 warnings)
  - **Observations noted (non-blocking):** Uncommitted state, CS0618 in tests, VSIX/IntelliJ/VSMac runtime warnings absent (acceptable for Phase 1)
  - **Status:** Phase 1 implementation approved for PR/merge. Decision documents finalized in decisions.md

- **2026-03-24 — OpenAPI Generator Update Pattern Analysis:**
  - **PR #1481 pattern identified:** 22 files across 4 change buckets (Core hashes, CLI+tests, docs, IDE extensions), 4 granular commits, branch naming `openapi-generator-X.X.X`
  - **Automation script exists:** `scripts/update-openapi-generator.ps1` (added in PR #1481) handles the complete workflow — JAR download, SHA1/MD5 computation, all file updates, build, test, commit
  - **Key files for version updates:** `OpenApiGeneratorVersions.cs` (version registry), `OpenApiSupportedVersion.cs` (enum + Latest pointer), `Resource.resx`/`Resource.Designer.cs` (hashes), `Program.cs` (CLI description), `OpenApiVersionExtensionsTests.cs` (test data), 9 doc files, 6 IDE extension files
  - **v7.21.0 release notes reviewed:** 3 breaking changes (Spring Boot 3.x default, C pointer types, Go fallback types) — none affect C# codegen. C# fixes are minor (implicit casts, central package versions, file support). No caution areas for this repo.
  - **Reusable instruction surface decision:** Squad skill (`.squad/skills/update-openapi-generator/SKILL.md`) as primary, copilot instructions addition recommended as secondary. GitHub Action rejected as over-engineering for monthly cadence.
  - **User preference:** Christian does this exercise every time a new version is released — wants one-prompt automation

- **2026-03-24 — OpenAPI Generator Guidance Finalized:**
  - **Primary future-update guidance:** `.squad/skills/update-openapi-generator/SKILL.md` is the durable playbook for recurring `Update OpenAPI Generator to vX.X.X` requests.
  - **Short-form repo reminder:** `.github/copilot-instructions.md` now points agents to `.\scripts\update-openapi-generator.ps1` and the expected workflow.
  - **Commit guardrail:** Prefer a clean `openapi-generator-X.X.X` branch before allowing the script to commit; if the worktree is not clean, use `-SkipCommit` and preserve the 4-bucket commit pattern manually.
  - **Validation guardrail:** Rely on the script for restore/build/targeted version tests, then manually verify `csharp --help` and check for stale old-version references.

- **2026-03-26 — OpenAPI Generator v7.21.0 guidance finalized and archived:**
  - **Lead reviewed:** Confirmed Squad skill + copilot-instructions addition are the right abstraction
  - **Skill file created:** `.squad/skills/update-openapi-generator/SKILL.md` documents complete pattern (trigger, pre-flight, script command, 4-commit breakdown, fragile areas, validation checklist, escalation)
  - **Copilot instructions updated:** Added "Recurring Tasks" section in `.github/copilot-instructions.md` with script invocation and skill reference
  - **Binding decision finalized:** All agents **must** use script for future `Update OpenAPI Generator to vX.X.X` requests; manual file editing prohibited
  - **Decision documented:** 3 entries added to decisions.md (scope, pattern, validation)
  - **Orchestration logged:** Lead summary written to orchestration-log
  - **Status:** Durable guidance established. Ready for next OpenAPI Generator release.
