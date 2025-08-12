## REST API Client Code Generator (IntelliJ / Rider Plugin)

Early preview plugin providing similar code generation features as the VS Code extension, inside JetBrains IDEs (target Rider 2025.1.5).

### Features

- (Planned) Context menu on OpenAPI spec files (`.json`, `.yaml`, `.yml`) to generate C# or TypeScript clients
- (Planned) `.refitter` settings file support to generate Refit interfaces
- Uses installed `rapicgen` .NET tool; will prompt if missing

### Requirements

- .NET 8.0 SDK, Java 17+, Node.js + NPM

### Build

```powershell
./gradlew buildPlugin
```

### Run Sandbox

```powershell
./gradlew runIde
```

### Helper Scripts

- `build-intellij.ps1` – build distribution
- `run-intellij.ps1` – run sandbox

---
Implementation in progress.
