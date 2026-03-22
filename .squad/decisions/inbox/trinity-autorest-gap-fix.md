# Decision: AutoRest Warning Non-Blocking + Label Consistency

**Agent:** Trinity (Extension Dev)  
**Date:** 2026-03-04  
**Status:** Implemented  

## Context

Coordinator review identified two gaps in the AutoRest deprecation implementation:

1. **VS Code blocking warning:** The AutoRest warning in `generators.ts` required user confirmation ("Continue Anyway" / "Cancel"), blocking code generation until user responded.
2. **VSIX label inconsistency:** The VSIX Extensibility string resources used "Outdated" instead of the canonical "Deprecated" short form.

## Decision

1. **Changed VS Code warning to non-blocking:** Removed the `await` and choice buttons from `showWarningMessage()`. The warning now displays but does not require confirmation and does not prevent code generation from proceeding.

2. **Updated VSIX label to canonical form:** Changed `"Generate with AutoRest (v3.0.0-beta.20210504.2 - Outdated)"` to `"Generate with AutoRest (Deprecated)"` in `string-resources.json`.

## Rationale

- **Non-blocking warning:** Aligns with the Phase 1 deprecation strategy of keeping AutoRest fully functional. Users see the warning but aren't forced to interact with it every time. This reduces friction while still providing visibility.
- **Label consistency:** The canonical short form `"AutoRest (Deprecated)"` is now used consistently across all IDE surfaces (VS Code, VSIX, JetBrains, VS Mac).

## Implementation

**Files Changed:**
- `src/VSCode/src/services/generators.ts` — Removed `await` and choice buttons from AutoRest warning
- `src/VSIX/ApiClientCodeGen.VSIX.Extensibility/.vsextension/string-resources.json` — Updated display name to canonical short form

**Validation:**
- VS Code: `npm run lint` — PASSED
- VS Code: `npm run compile` — PASSED (extension.js generated successfully)
- Audit: No other "Outdated" or "Legacy" references found in extension surfaces

## Impact

- AutoRest users in VS Code will see the deprecation warning but won't be interrupted in their workflow
- All IDE surfaces now use consistent "Deprecated" terminology
- No breaking changes to functionality
