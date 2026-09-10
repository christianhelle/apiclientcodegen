## REST API Client Code Generator (IntelliJ / Rider Plugin)

Early preview plugin providing similar code generation features as the VS Code extension, inside JetBrains IDEs.

### IDE Compatibility

The plugin is built against IntelliJ Platform 2025.1 and declares `since-build=251` with no
upper bound, so it loads in any 2025.1 or newer JetBrains IDE (Rider included). Do not set
`pluginUntilBuild` in `gradle.properties` unless you deliberately want to cap compatibility --
an upper bound makes newer IDEs reject the plugin as incompatible.

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

### Verify IDE Compatibility

Runs the JetBrains Plugin Verifier against the recommended IDEs plus the Rider release named by
`riderVersion` in `gradle.properties`. This is what catches "the plugin won't load in my IDE".

```powershell
./gradlew verifyPlugin
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
