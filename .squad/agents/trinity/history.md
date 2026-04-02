# Trinity — History

## Learnings

- **2026-03-04:** Team initialized. Extension dev for REST API Client Code Generator. Key paths: `src/VSCode/` (TypeScript), `src/VSIX/` (Visual Studio), `src/IntelliJ/` (JetBrains). User is Christian.

- **2026-03-04:** AutoRest deprecation inventory complete. Found 11 distinct surfaces where AutoRest is user-visible:
   - **VS Code:** `package.json` command definition + `generators.ts` registry + README/CHANGELOG
   - **VSIX:** `CodeGeneratorFactory.cs` + `NewAutoRestClientCommand.cs` + manifest tags
   - **JetBrains:** `plugin.xml` action definitions with "Outdated" label already applied
   - **VS Mac:** `Manifest.addin.xml` + 4 custom tool handler files
   - **Docs:** `README.md`, `Marketplace*.md`, `CLI.md`, `GeneratedCodeUsage.md`, website HTML
   - UI labels already mark as "Outdated" (VSCode, VSIX, JetBrains, VS Mac) but no selection warning dialogs exist
   - Documentation surfaces (markdown, website) don't mention deprecation status
   - Risk: Duplicated command registrations in VSIX; website HTML is manually maintained and could diverge
   - Recommendation: Add consistent warning dialog on selection + deprecation section in all docs

- **Architecture pattern:** AutoRest is implemented via same factory/generator interface as other tools. Removal would require:
   - Deleting `IAutoRestCodeGeneratorFactory` implementations
   - Removing enum case `SupportedCodeGenerator.AutoRest`
   - Updating all 4 IDE extensions' UI registrations
   - Migrating test coverage that may reference AutoRest
   - No architectural blocker; clean removal possible but high surface area

- **2026-03-04:** AutoRest deprecation implementation complete across all extension surfaces:
   - **Updated labels:** Changed "Outdated" to "Deprecated" in all 4 IDE extensions (VS Code, VSIX, IntelliJ, VS Mac)
   - **Non-blocking warnings:** Added user-friendly warning dialog in VS Code when AutoRest is selected
   - **Documentation:** Added deprecation notices with migration guide links in all extension-facing docs
   - **Verification:** VS Code extension lint and compile passed successfully
   - **Pattern established:** Consistent short label format `"AutoRest (Deprecated)"` and full deprecation message across all surfaces
   - **Kept functional:** AutoRest remains fully operational with warnings per Phase 1 plan
   - **Migration guidance:** All docs now reference `docs/AutoRestMigration.md` for user guidance

- **2026-03-04:** AutoRest deprecation final sweep complete. Updated remaining user-facing labels:
   - **VSIX:** Settings page display name changed from plain "AutoRest" to "AutoRest (Deprecated)" in string-resources.json
   - **IntelliJ:** Updated feature list, marketplace description, and action menu text for consistency
   - Validated JSON/XML syntax for both files
   - All extension surfaces now use canonical short label `"AutoRest (Deprecated)"` consistently
   - Decision documented in `.squad/decisions/inbox/trinity-autorest-final-sweep.md`

- **2026-03-22 — AutoRest Phase 1 IDE Deprecation Complete:**
  - **Implementation scope:** All 4 IDE extensions + all extension-facing documentation surfaces
  - **Label updates:** Converted "Outdated" → canonical "Deprecated" across VS Code, VSIX, IntelliJ, VS Mac
  - **Warnings:** VS Code shows non-blocking dialog on selection (doesn't interrupt workflow)
  - **Documentation:** All extension docs updated with deprecation notice + migration guide links
  - **Website:** Features.html, index.html, cli.html all marked with deprecation warnings
  - **Marketplace:** Descriptions updated to reference July 1, 2026, retirement date
  - **Verification:** VS Code npm ci/lint/compile all passed ✅
  - **Gaps closed:** (1) VS Code warning made non-blocking, (2) VSIX label updated to canonical form, (3) IntelliJ marketplace + action descriptions finalized
  - **Code review:** Morpheus approved — consistent terminology and non-breaking approach validated
  - **Status:** IDE extension scope 100% complete. All user-facing surfaces uniformly deprecated. Ready for Tank validation phase.

- **2026-04-02 — Issue #227 Investigation (reswcodegen | VSIX Custom Tool):**
  - **Problem:** Custom tool `ReswFileCodeGenerator` disappears after rebuild in VS 2026 WinUI3 projects despite entry remaining in .csproj
  - **Root cause:** VS 2026 incompatibility from two layers: (1) manifest installation target hardcoded to `[17.0, 18.0)` excludes v18.0, (2) legacy `IVsSingleFileGenerator` COM pattern not recognized by CPS 3.0 modern project system
  - **Primary blocker:** If VSIX can't install on VS 2026, registry entries never register, making generator unavailable regardless of code correctness
  - **Secondary blocker:** Legacy registration using `[CodeGeneratorRegistration]` attribute on `IVsSingleFileGenerator` implements pre-CPS 3.0 pattern; modern project system doesn't query registry for it
  - **Architecture pattern learned:** Single-file generators in VS extensions use `[CodeGeneratorRegistration]` + `[ComVisible(true)]` + `IVsSingleFileGenerator` interface; this is legacy (VS 2013-2022) and EOL in VS 2026
  - **Critical file paths:** `src/VSPackage/source.extension.vsixmanifest` (version constraints), `src/VSPackage/CustomTool/ReswFileCSharpCodeGenerator.cs` (registration attributes), `src/VSPackage/VSPackage.csproj` (pkgdef generation)
  - **Reproduction checklist:** (1) Update manifest `[17.0, 19.0)`, (2) Verify registry hive `Generators/{FAE04EC1...}/ReswFileCodeGenerator`, (3) Inspect .pkgdef content, (4) Test console vs WinUI3 project behavior
  - **Immediate fix:** Expand installation target version in manifest to include VS 2026 (likely sufficient for legacy generators)
  - **Deep fix:** If immediate fix fails, investigate CPS 3.0 migration — may require new provider interface or modern design-time generation registration
  - **Decision document:** `.squad/decisions/inbox/trinity-issue-227.md` with detailed analysis

