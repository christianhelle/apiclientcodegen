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