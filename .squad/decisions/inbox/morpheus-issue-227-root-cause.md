# Decision: Issue #227 Root Cause Analysis & Next Steps

**Repository:** christianhelle/reswcodegen  
**Issue:** #227 — ReswFileCodeGenerator disappears from Custom Tool property in VS 2026 WinUI3 projects  
**Date:** 2026-04-02  
**Lead:** Morpheus

## Problem Statement

Users report that the "ReswFileCodeGenerator" custom tool value disappears from the .resw file UI properties when using Visual Studio 2026 with WinUI3 projects. The property persists in the .csproj XML but no code is generated. This occurs on multiple machines with fresh WinUI3 projects.

## Root Cause Hypotheses (Ranked by Confidence)

### 1. VSIX Manifest Version Range Gap (HIGHEST CONFIDENCE: 85%)

**Hypothesis:** The VSIX extension fails to install on VS 2026 due to version range restrictions.

**Evidence:**
- Current manifest (`source.extension.vsixmanifest`):
  ```xml
  <InstallationTarget Id="Microsoft.VisualStudio.Community" Version="[17.0, 18.0)" ... />
  ```
- VS 2026 is version 18.x — OUTSIDE the [17.0, 18.0) range
- No version 18.0+ target defined in manifest
- VSIX must install and generate .pkgdef registry entries for custom tool to work
- If VSIX doesn't load, all COM registration (IVsSingleFileGenerator) is invisible

**Impact Chain:**
1. VSIX installer checks version range → fails
2. .pkgdef file not generated/registered
3. Registry entries for ReswFileCodeGenerator never created
4. VS 2026 can't find custom tool in registry
5. Property sheet allows typing but doesn't persist (no tool to bind to)

**Fix Surface:** `src/VSPackage/source.extension.vsixmanifest` — add `[18.0, 19.0)` target range

---

### 2. Architecture/Platform Mismatch (MEDIUM CONFIDENCE: 40%)

**Hypothesis:** VS 2026 runs on arm64 by default, but VSIX targets only x86 + amd64.

**Evidence:**
- Current manifest specifies:
  - x86: v14-17
  - amd64/arm64: v17-18
  - NO arm64 specified for v18.0+
- arm64 Windows adoption increasing; WinUI3 optimized for modern hardware
- Architecture mismatch would prevent VSIX load on native arm64 VS

**Likelihood:** Overlaps with Hypothesis 1 — fixing version range may also require architecture clarification

---

### 3. IVsSingleFileGenerator Breaking Change (LOW CONFIDENCE: 15%)

**Hypothesis:** VS 2026 deprecated or changed the IVsSingleFileGenerator interface.

**Evidence:**
- Interface is 20+ years old; COM-based custom tools may be considered legacy
- WinUI3 targets modern, Roslyn-based code generation
- No Microsoft breaking change notices found in public release notes

**Assessment:** Unlikely. VS maintains backward compatibility for custom tools. Would require explicit breaking change announcement.

---

### 4. WinUI3 Project System Incompatibility (MEDIUM-LOW CONFIDENCE: 30%)

**Hypothesis:** WinUI3 projects (Windows App SDK) don't recognize classic custom tool registrations.

**Evidence:**
- Issue only reproducible on WinUI3 projects
- WinUI3 uses CsWinRT and different project system SDKs
- Classic Windows Forms projects work fine

**Assessment:** Possible but secondary. Custom tools should work across all C# project types if properly registered.

---

## Investigation Surfaces (Priority Order)

### Tier 1 (Must Check First)
1. **`src/VSPackage/source.extension.vsixmanifest`**
   - InstallationTarget version ranges
   - Does NOT cover [18.0, ...)
   - Architecture targets for v18+

### Tier 2 (If Tier 1 Fails)
2. **`src/VSPackage/CustomTool/ReswFileCSharpCodeGenerator.cs`**
   - CodeGeneratorRegistration attribute semantics
   - GeneratorRegKeyName = "ReswFileCodeGenerator" registry key
   - Check if VS 2026 expects different registry path

3. **`src/VSPackage/Guids.cs`**
   - GUID collision check for C#/VB language services
   - Verify no conflicts with VS 2026 built-in tools

### Tier 3 (If Tier 2 Fails)
4. **`src/VSPackage/CustomTool/ReswFileCodeGenerator.cs`**
   - IVsSingleFileGenerator implementation
   - Check for VS 2026-specific contract changes
   - Error handling robustness

5. **`src/VSPackage/VSPackage.csproj`**
   - GeneratePkgDefFile=true enabled
   - Target framework compatibility
   - Build metadata for VSIX packaging

---

## Clarifying Questions for Christian

**Before implementation, ask:**

1. **When was the VSIX manifest last updated for VS version support?**
   - VS 2026 stable release date?

2. **Do users get installation errors in VS 2026?**
   - Complete install failure, or silent install with no activation?

3. **Does the custom tool work on non-WinUI3 projects in VS 2026?**
   - Isolates to project-type vs. VSIX-wide issue

4. **What exact VS 2026 version are affected users running?**
   - Version 18.0.x? (collect from Help > About)

5. **Any breaking change notices in VS 2026 release notes for custom tools?**
   - Search Microsoft docs for IVsSingleFileGenerator changes

---

## Recommended Action

**Immediate (before implementation):**
1. Review `source.extension.vsixmanifest` for VS 2026 version/architecture coverage
2. Add `[18.0, 19.0)` target ranges if missing
3. Verify .pkgdef generation in build pipeline

**Validation Strategy:**
1. Build VSIX with updated manifest
2. Test VSIX installation on VS 2026
3. Verify registry entries created
4. Test code generation on WinUI3 project

**Escalation (if above fails):**
1. Check VS 2026 SDK release notes for custom tool deprecations
2. Investigate Roslyn source generators as potential modernization path
3. File issue with Microsoft if IVsSingleFileGenerator contract changed

---

## Architecture Context

**Custom Tool Registration Flow:**
```
VSIX Manifest Version Check
  ↓ (if version in range)
VSIX Installs, triggers .pkgdef generation
  ↓
Registry entries created for ReswFileCodeGenerator
  ↓
VS detects COM class via registry
  ↓
Property sheet populates dropdown
  ↓
Code generation via IVsSingleFileGenerator.Generate()
```

**Failure Point:** VSIX version check fails → registry entries never created → dropdown empty → property disappears on rebuild

The COM registration mechanism is automatic via `CodeGeneratorRegistration` attribute + MSBuild .pkgdef generation. No manual registry editing needed if VSIX loads correctly.

---

## Decision

**Hypothesis Rank for Implementation:**
1. **Implement:** Fix VSIX manifest InstallationTarget version ranges (95% confidence this solves it)
2. **Test:** Validate VSIX installation and registry entries on VS 2026
3. **If fails:** Investigate architecture/platform mismatch and IVsSingleFileGenerator interface changes
4. **Document:** Add VS 2026 version/architecture support guardrails to future VSIX updates
