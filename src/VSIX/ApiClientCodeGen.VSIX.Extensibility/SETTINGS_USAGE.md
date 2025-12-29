# Settings Usage Guide

This document explains how to use the new Visual Studio Extensibility Settings API in the ApiClientCodeGen.VSIX.Extensibility project.

## Overview

The settings have been ported from the old `DialogPage` based options pages to the new Visual Studio Extensibility Settings API. Settings are now defined in `SettingDefinitions.cs` and organized into the following categories:

1. **General** - File paths and general options
2. **Analytics** - Telemetry settings
3. **AutoRest** - AutoRest code generator settings
4. **NSwag** - NSwag code generator settings
5. **NSwagStudio** - NSwag Studio code generator settings
6. **OpenApiGenerator** - OpenAPI Generator settings
7. **Refitter** - Refitter code generator settings
8. **Kiota** - Kiota code generator settings

## How Settings Work

### Setting Definition

Settings are defined in `SettingDefinitions.cs` using the following pattern:

```csharp
[VisualStudioContribution]
internal static SettingCategory RefitterCategory { get; } = new("refitter", "%Settings.Refitter.Category.DisplayName%")
{
    Description = "%Settings.Refitter.Category.Description%",
    GenerateObserverClass = true,
};

[VisualStudioContribution]
internal static Setting.Boolean GenerateContractsSetting { get; } = new("generateContracts", "%Settings.Refitter.GenerateContracts.DisplayName%", RefitterCategory, defaultValue: true)
{
    Description = "%Settings.Refitter.GenerateContracts.Description%",
};
```

### Observer Classes

When `GenerateObserverClass = true` is set on a `SettingCategory`, the build process generates an observer class that can be injected into commands. The observer class is named `{CategoryName}CategoryObserver` and is generated under the `Settings` namespace.

For example, for the `RefitterCategory`, a `RefitterCategoryObserver` class is generated.

### Reading Settings in Commands

To read settings in a command, follow these steps:

1. **Inject the observer** in the command constructor:

```csharp
public class GenerateRefitterCommand(
    TraceSource traceSource,
    Settings.RefitterCategoryObserver refitterSettingsObserver)
    : Command
{
    // ...
}
```

2. **Read settings** using the observer:

```csharp
// Get current settings snapshot
var settingsSnapshot = await refitterSettingsObserver.GetSnapshotAsync(cancellationToken);

// Access individual settings
bool generateContracts = settingsSnapshot.GenerateContractsSetting.ValueOrDefault(defaultValue: true);
bool generateXmlDocs = settingsSnapshot.GenerateXmlDocCodeCommentsSetting.ValueOrDefault(defaultValue: true);
```

3. **Monitor settings changes** (optional):

```csharp
// Subscribe to settings changes
refitterSettingsObserver.Changed += OnSettingsChanged;

private Task OnSettingsChanged(Settings.RefitterCategorySnapshot settingsSnapshot)
{
    // Handle settings change
    return Task.CompletedTask;
}
```

### Writing Settings

To update settings programmatically:

```csharp
await this.Extensibility.Settings().WriteAsync(
    batch =>
    {
        batch.WriteSetting(SettingDefinitions.GenerateContractsSetting, false);
        batch.WriteSetting(SettingDefinitions.GenerateXmlDocCodeCommentsSetting, true);
    },
    description: "Update Refitter settings",
    cancellationToken);
```

## Settings Location

Settings are stored in JSON files in the Visual Studio settings directory:
- User settings: `%LOCALAPPDATA%\Microsoft\VisualStudio\{version}\extensibility.settings.json`

Users can access settings via:
- **Menu**: Extensions → Extension Settings (experimental) → User Scope (current install)

## Migration from Old Options Pages

The old options pages were defined in `src/VSIX/ApiClientCodeGen.VSIX.Shared/VsPackage.cs` using the `[ProvideOptionPage]` attribute. These have been replaced by the new Settings API which provides:

- Better performance (settings are read/written as JSON)
- Better extensibility (settings can be read by other extensions)
- Better observability (settings changes can be monitored)
- Better IDE integration (settings are shown in the new Settings UI)

## Example: Refitter Command Integration

Here's how to integrate settings into the Refitter command:

```csharp
public abstract class GenerateRefitterBaseCommand(
    TraceSource traceSource,
    Settings.RefitterCategoryObserver? refitterSettingsObserver = null)
    : Command
{
    public async Task<string> GenerateCodeAsync(string inputFile, string defaultNamespace, CancellationToken cancellationToken)
    {
        // Get settings
        var options = new DefaultRefitterOptions();
        if (refitterSettingsObserver != null)
        {
            var settingsSnapshot = await refitterSettingsObserver.GetSnapshotAsync(cancellationToken);
            options = new RefitterOptionsFromSettings(settingsSnapshot);
        }

        // Use options to configure code generation
        var settings = new RefitGeneratorSettings
        {
            OpenApiPath = inputFile,
            Namespace = defaultNamespace,
            AddAutoGeneratedHeader = options.AddAutoGeneratedHeader,
            GenerateContracts = options.GenerateContracts,
            GenerateXmlDocCodeComments = options.GenerateXmlDocCodeComments,
            ReturnIApiResponse = options.ReturnIApiResponse,
            UseCancellationTokens = options.UseCancellationTokens,
            GenerateOperationHeaders = options.GenerateHeaderParameters,
            GenerateMultipleFiles = options.GenerateMultipleFiles
        };

        // Generate code...
    }
}
```

## Current Limitations

The settings API is currently experimental and has several limitations:

- Extensions can only read or write settings from themselves or other extensions. Core Visual Studio settings are not available.
- There is no UI for extension settings in Visual Studio 2022. They can only be changed by editing the JSON files.
- The observer classes are generated at compile-time and may not be visible in IntelliSense until after a successful build.
