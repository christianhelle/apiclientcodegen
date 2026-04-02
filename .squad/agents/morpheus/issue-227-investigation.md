# Issue #227 Investigation: ReswFileCodeGenerator Disappears in VS 2026 WinUI3

## Problem Summary
Users report that the custom tool property value "ReswFileCodeGenerator" disappears from .resw file properties when using Visual Studio 2026 with WinUI3 projects. The property persists in .csproj XML but generates no code.

### Key Observations
- Issue reproducible on multiple machines with fresh WinUI3 projects
- VS 2026 is NOT in the VSIX manifest InstallationTarget version range
- The .csproj XML shows the custom tool entry, but no code is generated
- Works on console/basic project types in earlier VS versions

## Root Cause Hypotheses (Ranked)

### 1. **VSIX Extension Not Registered in VS 2026 (HIGHEST CONFIDENCE)**
**Theory:** The source.extension.vsixmanifest specifies installation targets for VS 14.0-17.0 only:
- Community 14.0 - 17.0 (x86)
- Community 17.0 - 18.0 (amd64, arm64)

VS 2026 is version 18.x (and requires version [17.0, 18.0)), but the manifest does NOT include [18.0, ...) target ranges.

**Evidence:**
- VSIX manifest explicitly limits: <InstallationTarget Id="Microsoft.VisualStudio.Community" Version="[17.0, 18.0)">
- No version 18.0+ range defined for Community edition
- Custom tool is registered via .pkgdef generation (GeneratePkgDefFile=true in .csproj)
- If VSIX doesn't install, the .pkgdef registry entries won't be created
- The custom tool COM registration (Guid attributes, CodeGeneratorRegistration) becomes invisible to VS

**Impact:**
- VSIX fails to install or activate in VS 2026
- Registry entries for ReswFileCodeGenerator never created
- VS cannot find the custom tool, even if .csproj XML contains the reference
- Property sheet doesn't populate the dropdown, allowing user to type but not persist

---

### 2. **Architecture/Platform Mismatch in VS 2026**
**Theory:** VS 2026 runs on a different architecture than the extension was built for.

**Evidence:**
- The manifest specifies ProductArchitecture: x86 (v14-17) vs amd64/arm64 (v17-18)
- VS 2026 may run on arm64 natively on newer Windows systems
- VSIX targets may have diverged

**Likelihood:** Medium (usually VS handles fallback gracefully, but WinUI3 projects may have stricter requirements)

---

### 3. **Breaking Change in VS 2026 IVsSingleFileGenerator Interface/Registration**
**Theory:** The IVsSingleFileGenerator interface or COM registration mechanism changed in VS 2026.

**Evidence:**
- Custom tool classes inherit from IVsSingleFileGenerator (legacy COM interface)
- VS 2026 may deprecate COM-based custom tools in favor of Roslyn generators or newer API
- The CodeGeneratorRegistration attribute (from Microsoft.VisualStudio.Shell) may have changed semantics

**Likelihood:** Low (VS maintains backward compatibility for custom tools, though WinUI3 projects may have different defaults)

---

### 4. **WinUI3 Project SDK Incompatibility**
**Theory:** WinUI3 projects (Windows App SDK) may use a different project system that doesn't recognize custom tool registrations the same way.

**Evidence:**
- Issue only reported for WinUI3 projects
- WinUI3 uses CsWinRT and different build system than classic Windows Forms/.NET Framework projects
- Custom tool registration may depend on classic project system features

**Likelihood:** Medium-Low (WinUI3 projects are based on newer SDK, but custom tools should work)

---

## Key File Surfaces to Investigate First

### Registry & VSIX Installation
1. **src/VSPackage/source.extension.vsixmanifest** — InstallationTarget version ranges
   - Check: Does it cover VS 2026? ([18.0, ...)?)
   - Check: Are all relevant architectures covered?

2. **src/VSPackage/VSPackage.csproj** — GeneratePkgDefFile, IncludeAssemblyInVSIXContainer
   - Verify .pkgdef generation is enabled
   - Check target framework compatibility

### Custom Tool Registration
3. **src/VSPackage/CustomTool/ReswFileCSharpCodeGenerator.cs** — CodeGeneratorRegistration attribute
   - GeneratorRegKeyName = "ReswFileCodeGenerator"
   - Registry key structure expected by VS 2026

4. **src/VSPackage/Guids.cs** — GUID mapping for C#/VB language services
   - ReswFileCSharpCodeGenerator GUID
   - Verify no conflicts with VS 2026 built-in tools

### Base Implementation
5. **src/VSPackage/CustomTool/ReswFileCodeGenerator.cs** — IVsSingleFileGenerator implementation
   - Check: Does it handle any VS 2026-specific contract changes?
   - Check: Error handling in Generate() method

### Build & Packaging
6. **src/build.cake** / **src/publish.ps1** — VSIX packaging process
   - Verify VSIX is built with correct version/architecture metadata

---

## Clarifying Questions for User (Christian)

1. **Has the VSIX manifest been updated since VS 2026 was released?**
   - When was VS 2026 released as stable?
   - Was there a preview period where this worked?

2. **Do users get an installation warning/error in VS 2026?**
   - Does the VSIX fail to install completely?
   - Or does it install but custom tools don't activate?

3. **Does the custom tool work on non-WinUI3 projects in VS 2026?**
   - This would confirm it's a project-type-specific issue, not VSIX-wide

4. **What is the exact VS 2026 version number?**
   - (The user showed v18.0.x in their system info)

5. **Have any breaking changes been announced for IVsSingleFileGenerator in VS 2026?**
   - Check Microsoft VS SDK release notes

---

## Next Investigation Steps

1. **Immediate:** Update VSIX manifest to include [18.0, 19.0) version range
2. **Validation:** Test VSIX installation and activation on VS 2026
3. **If (1) fails:** Investigate VS 2026 architecture support (arm64 vs amd64)
4. **If (2-3) fail:** Check for breaking changes in IVsSingleFileGenerator API or WinUI3 project system
5. **Final fallback:** Consider modernizing to Roslyn generators (if VS 2026+ recommends this)

---

## Architecture Context (for Ref)
- **Pattern:** COM-visible custom tool classes + CodeGeneratorRegistration attribute
- **Registration:** Automatic .pkgdef generation during VSIX build
- **Activation:** VSIX install → registry entries → VS loads custom tool at design time
- **Failure point:** VSIX version check fails → registry entries never created → property sheet can't find tool

The IVsSingleFileGenerator interface is 20+ years old, so deprecation is possible but unlikely to be the root cause without explicit VS 2026 breaking change notice.
