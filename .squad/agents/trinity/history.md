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
