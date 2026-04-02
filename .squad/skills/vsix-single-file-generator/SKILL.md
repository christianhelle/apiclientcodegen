# VSIX Single-File Generator Registration Pattern

**Status:** Legacy (Deprecated in VS 2026)  
**Scope:** Visual Studio 2013-2022 extensions  
**Keywords:** IVsSingleFileGenerator, CodeGeneratorRegistration, custom tools, ResW, design-time generation

## Overview

Single-file generators in Visual Studio extensions (like ResW file generators, text templating, code generation) use a classic COM-based registration pattern via the `[CodeGeneratorRegistration]` attribute.

## Architecture

```csharp
[Guid("UNIQUE-GUID-HERE")]
[ComVisible(true)]
[ProvideObject(typeof(MyCodeGenerator))]
[CodeGeneratorRegistration(
    typeof(MyCodeGenerator),
    "Friendly Name",
    Guids.LanguageGuid,           // e.g., C# = {FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}
    GeneratesDesignTimeSource = true,
    GeneratorRegKeyName = "MyCodeGenerator")]
public class MyCodeGenerator : IVsSingleFileGenerator
{
    [ComVisible(true)]
    public int Generate(string inputFilePath, string inputFileContents, 
                       string defaultNamespace, IntPtr[] outputFileContents,
                       out uint outputFileLength, IVsGeneratorProgress progress)
    {
        // Generate code and return via IntPtr marshaling
        // Return 0 for success
    }
}
```

## Registration Flow

1. **VSIX Installation:** Package manifests registers assembly via `<Asset Type="Microsoft.VisualStudio.VsPackage">`
2. **.pkgdef Generation:** Project file with `<GeneratePkgDefFile>true</GeneratePkgDefFile>` auto-generates registry entries from attributes
3. **Registry Registration:** `[CodeGeneratorRegistration]` writes to:
   ```
   HKLM\SOFTWARE\Microsoft\VisualStudio\{Version}\Generators\{LanguageGuid}\{GeneratorRegKeyName}
   ```
4. **Manifest Registration:** Manifest lists `InstallationTarget` version range
5. **Runtime Lookup:** When user sets custom tool, VS queries registry for CLSID, calls `CoCreateInstance()` to load COM object

## Critical Points

### Version Compatibility
**CRITICAL:** Manifest must declare all supported VS versions:

```xml
<InstallationTarget Id="Microsoft.VisualStudio.Community" Version="[17.0, 19.0)">
  <ProductArchitecture>amd64</ProductArchitecture>
</InstallationTarget>
```

If version is `[17.0, 18.0)` but user runs VS 2026 (v18.0), the VSIX won't install, breaking the entire extension.

### Project System Compatibility
- **VS 2013-2022:** Legacy VSPROJECT COM interfaces support `IVsSingleFileGenerator`
- **VS 2026+:** CPS 3.0 (Common Project System) deprecated legacy generators
  - Modern projects (WinUI3, .NET 8+) use MSBuild-based design-time generation
  - Legacy `IVsSingleFileGenerator` COM objects no longer queried by project system
  - **Result:** Custom tool disappears after rebuild; no code generation occurs

### COM Visibility Requirements
All types must be COM-visible and have valid GUIDs:

```csharp
[Guid("98983F6D-BC77-46AC-BA5A-8D9E8763F0D2")]
[ComVisible(true)]
public class MyGenerator : IVsSingleFileGenerator { ... }
```

Missing GUID or `ComVisible(false)` prevents COM registration.

## Real-World Example

**reswcodegen issue #227:**
- Symptom: Custom tool disappears in VS 2026 WinUI3 projects
- Root cause: (1) Manifest version `[17.0, 18.0)` excludes v18.0, (2) CPS 3.0 ignores `IVsSingleFileGenerator`
- Fix: Update manifest to `[17.0, 19.0)` (short-term); migrate to modern CPS provider (long-term)

## Migration Path for VS 2026+

For VS 2026 and beyond, single-file generators should:

1. **Add modern provider interface** (CPS 3.0):
   - `IProjectItemProvider` or equivalent
   - Register via new attributes (if available)
   - Or use MSBuild design-time generation hooks

2. **Keep legacy support** (optional for backward compatibility):
   - Maintain `IVsSingleFileGenerator` for VS 2022 users
   - Wrap in conditional compilation or runtime detection

3. **Update manifest:**
   - Expand version range: `[17.0, 99.0)`
   - Add new asset types for modern providers

## Checklist for VSIX Custom Tool Issues

When debugging custom tool registration problems:

- [ ] **Manifest version constraint:** Includes user's VS version? Check `[X.0, Y.0)` range
- [ ] **Assembly COM visibility:** All generator classes have `[ComVisible(true)]`
- [ ] **GUIDs unique:** Each generator has distinct `[Guid("...")]`
- [ ] **pkgdef file generated:** Project has `<GeneratePkgDefFile>true</GeneratePkgDefFile>`
- [ ] **Registry entry present:** Check `HKLM\SOFTWARE\Microsoft\VisualStudio\{Ver}\Generators\{LangGuid}`
- [ ] **Project system compatibility:** Legacy generators fail on modern projects (WinUI3) in VS 2026+
- [ ] **Interface implementation:** Class implements `IVsSingleFileGenerator` correctly
- [ ] **Language GUID correct:** C# = `{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}`

## References

- **Visual Studio SDK:** IVsSingleFileGenerator (legacy interface)
- **Common Project System:** CPS 3.0 (VS 2026+)
- **Example:** christianhelle/reswcodegen (ResW custom tool)

## See Also

- `.squad/decisions/inbox/trinity-issue-227.md` (reswcodegen issue investigation)
- `src/VSPackage/source.extension.vsixmanifest` (manifest version constraints)
- `src/VSPackage/CustomTool/ReswFileCSharpCodeGenerator.cs` (registration attributes)
