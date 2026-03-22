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
