## REST API Client Code Generator (IntelliJ / Rider Plugin)

Early preview plugin providing similar code generation features as the VS Code extension, inside JetBrains IDEs (target Rider 2025.1.5).

### Preview Features Implemented

- Context menu on OpenAPI spec files (`.json`, `.yaml`, `.yml`):
	- Generate C# Client (NSwag)
	- Generate TypeScript Client (Angular)
- Context menu on `.refitter` files: Generate Refitter Output (Refit interface + contracts)
- Prompts for namespace (C#) or output folder (TypeScript)
- Uses installed `rapicgen` .NET tool; shows guidance if missing

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
Implementation in progress – additional generators and options forthcoming.
