---
layout: page
title: "Download"
description: "Download REST API Client Code Generator for your development environment"
---

## Visual Studio Extensions

### Visual Studio 2022
The latest and most feature-complete version of the extension.

<a href="https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ApiClientCodeGenerator2022" class="btn btn-primary" target="_blank" rel="noopener">
  Download for VS 2022
</a>

**Features:**
- Full support for all code generators
- Latest Visual Studio integration
- Enhanced performance and stability

### Visual Studio 2019
Stable version with all core features for Visual Studio 2019.

<a href="https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator" class="btn btn-primary" target="_blank" rel="noopener">
  Download for VS 2019
</a>

### Visual Studio 2017
Legacy support for older Visual Studio installations.

<a href="https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ApiClientCodeGenerator2017" class="btn btn-primary" target="_blank" rel="noopener">
  Download for VS 2017
</a>

## Visual Studio Code

Cross-platform extension for VS Code with context menu integration.

<a href="https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.apiclientcodegen" class="btn btn-primary" target="_blank" rel="noopener">
  Download for VS Code
</a>

### Manual Installation for VS Code

1. Download the VSIX file from the [GitHub releases](https://github.com/christianhelle/apiclientcodegen/releases/latest) page
2. Open VS Code
3. Go to Extensions view (Ctrl+Shift+X)
4. Click on the "..." menu in the top right corner
5. Select "Install from VSIX..."
6. Select the downloaded .vsix file

## Visual Studio for Mac

### Option 1: Extension Repository

1. Open Visual Studio for Mac Extension Manager
2. Go to **Visual Studio** → **Extensions**
3. Select the **Gallery** tab
4. Expand the Repository dropdown and select **Manage Repositories**
5. Press **Add** and register: `https://christianhelle.com/vsmac/main.mrep`
6. Install from the Gallery tab

### Option 2: Direct Installation

1. Download the latest **.mpack file** from [GitHub Releases](https://github.com/christianhelle/apiclientcodegen/releases/latest)
2. Open Visual Studio for Mac
3. Go to **Visual Studio** → **Extensions**
4. Click **Install from File**
5. Browse to and select the .mpack file
6. Click **Install** and restart Visual Studio for Mac

<a href="https://github.com/christianhelle/apiclientcodegen/releases/latest" class="btn btn-primary" target="_blank" rel="noopener">
  Download for VS Mac
</a>

## Command Line Tool

Cross-platform CLI tool for automation and CI/CD scenarios.

### Installation

Install as a .NET global tool:

```bash
dotnet tool install --global rapicgen
```

### Update

Update to the latest version:

```bash
dotnet tool update --global rapicgen
```

### NuGet Package

<a href="https://www.nuget.org/packages/rapicgen" class="btn btn-secondary" target="_blank" rel="noopener">
  View on NuGet
</a>

## Installation Requirements

### For Visual Studio Extensions
- Visual Studio 2017, 2019, or 2022
- .NET Framework 4.7.2 or later

### For VS Code Extension
- Visual Studio Code
- .NET SDK (for CLI tool execution)

### For Command Line Tool
- .NET 6.0 runtime or later
- Java Runtime Environment (for Swagger Codegen CLI and OpenAPI Generator)
- NPM (for AutoRest and NSwag CLI)

## Installation Tips

1. **First-time use delay** - Some code generators (Swagger Codegen CLI, OpenAPI Generator, AutoRest) are downloaded on demand, so there may be an initial delay on first use.

2. **Java configuration** - If you have multiple Java installations, you can configure the path in the extension settings.

3. **Network access** - Ensure your development environment has internet access for downloading dependencies.

4. **Proxy settings** - If you're behind a corporate proxy, make sure NPM and Java tools are configured correctly.

## Getting Started

After installation:

1. **Restart your IDE** to ensure the extension is properly loaded
2. **Add a new REST API Client** from the Add New Item dialog
3. **Enter your OpenAPI specification URL**
4. **Choose your preferred code generator**
5. **Start generating client code!**

## Support

If you encounter any issues during installation:

- Check the [GitHub Issues](https://github.com/christianhelle/apiclientcodegen/issues) page
- Review the [documentation](https://github.com/christianhelle/apiclientcodegen/blob/master/README.md)
- Submit a new issue with installation details