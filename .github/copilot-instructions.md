# REST API Client Code Generator

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Architecture Overview

This is a **multi-platform code generation tool** that wraps external OpenAPI/Swagger code generators:
- **Core**: Shared .NET libraries implementing `ICodeGenerator` interface pattern
- **CLI Tool** (`rapicgen`): Cross-platform command-line interface 
- **IDE Extensions**: Visual Studio (VSIX), VS Code (TypeScript), Visual Studio for Mac, IntelliJ/Rider
- **External Dependencies**: Wraps NSwag, OpenAPI Generator (Java), Swagger Codegen (Java), AutoRest (NPM), Refitter (.NET), Kiota (.NET)

### Key Design Patterns
- **Factory Pattern**: `ICodeGeneratorFactory` creates generators based on `SupportedCodeGenerator` enum
- **Dependency Injection**: External tools installed on-demand via `IDependencyInstaller`
- **Process Abstraction**: `IProcessLauncher` wraps external CLI tool execution
- **Multi-file Merging**: Java-based generators output multiple files, then merged into single C# file

### Code Generation Flow
1. Parse OpenAPI spec via `IOpenApiDocumentFactory`
2. Create generator via `CodeGeneratorFactory.Create()`  
3. Install external dependencies if missing (`DependencyInstaller`)
4. Execute generator with `IProgressReporter` for UI feedback
5. Merge/process output files for single-file generators

## Working Effectively

### Prerequisites and Setup
Install required dependencies in this order:
- .NET 8.0 SDK (required for all builds and CLI tool)
- Java 17+ Runtime (required for code generators like Swagger Codegen, OpenAPI Generator)
- Node.js v20+ and NPM (required for VSCode extension and some code generators)

Verify installations:
```bash
dotnet --version  # Should show 8.0.x
java -version     # Should show 17+
node --version    # Should show v20+
npm --version     # Should show 10+
```

### Bootstrap, Build, and Test the Repository
Execute these commands in order:

1. **Navigate to source directory:**
   ```bash
   cd src
   ```

2. **Restore packages (60+ seconds):**
   ```bash
   dotnet restore Rapicgen.slnx
   ```
   - Takes ~60 seconds on first run
   - May show package warnings - these are normal

3. **Build the solution (15-25 seconds) - NEVER CANCEL:**
   ```bash
   dotnet build Rapicgen.slnx
   ```
   - Takes 15-25 seconds typically
   - May show nullable reference warnings - these are normal
   - Set timeout to 60+ seconds to be safe

4. **Run unit tests (5-10 seconds) - NEVER CANCEL:**
   ```bash
   dotnet test Core/ApiClientCodeGen.Core.Tests/ApiClientCodeGen.Core.Tests.csproj --no-build -f net8.0
   ```
   - Takes 5-10 seconds
   - Some tests may fail due to network connectivity (external URLs) - this is expected
   - Set timeout to 30+ minutes for safety

5. **Run integration tests (2+ minutes) - NEVER CANCEL:**
   ```bash
   dotnet test Core/ApiClientCodeGen.Core.IntegrationTests/ApiClientCodeGen.Core.IntegrationTests.csproj --no-build -f net8.0 -l "console;verbosity=minimal"
   ```
   - Takes 2+ minutes to complete
   - Many tests may be skipped or fail due to external dependencies - this is expected
   - **NEVER CANCEL** - Set timeout to 30+ minutes

### Test CLI Tool Functionality
The CLI tool (rapicgen) is the core component. Test it after building:

1. **Check CLI help:**
   ```bash
   dotnet run --project CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- --help
   ```

2. **Test code generation with local files:**
   ```bash
   # Test NSwag generator (2-3 seconds)
   dotnet run --project CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- csharp nswag Swagger.json GeneratedCode /tmp/test-output.cs
   
   # Test Refitter generator (2-3 seconds) 
   dotnet run --project CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- csharp refitter $(pwd)/Swagger.json GeneratedCode $(pwd)/Refitter-Output.cs
   ```
   - Should complete in 2-3 seconds
   - Check output files exist and contain generated code

### VSCode Extension Development

1. **Navigate to VSCode extension directory:**
   ```bash
   cd VSCode
   ```

2. **Install dependencies (60-90 seconds) - NEVER CANCEL:**
   ```bash
   npm ci
   ```
   - Takes 60-90 seconds on first run
   - May show deprecation warnings - these are normal
   - Set timeout to 10+ minutes

3. **Run linting (1-2 seconds):**
   ```bash
   npm run lint
   ```
   - Should complete quickly
   - May show TypeScript version warnings - these are normal

4. **Compile extension (2-3 seconds):**
   ```bash
   npm run compile
   ```
   - Creates dist/extension.js
   - Should complete in 2-3 seconds

5. **Package extension (optional):**
   ```bash
   npm run package
   npm run vsix
   ```

### VSIX Extension (Visual Studio)
**Note:** VSIX extension requires Windows for full build. On Linux:

1. **Restore packages (45+ seconds) - NEVER CANCEL:**
   ```bash
   dotnet restore VSIX.slnx
   ```
   - Takes 45+ seconds
   - Will show many package downgrade warnings - these are expected
   - Set timeout to 10+ minutes

2. **Build (Windows only):**
   ```bash
   msbuild VSIX.slnx /property:Configuration=Release /t:Rebuild
   ```

## Validation Scenarios

### Always Test These Scenarios After Making Changes:

1. **Core CLI Functionality:**
   - Generate code using at least two different generators (NSwag and Refitter)
   - Verify output files are created and contain valid C# code
   - Test with both local Swagger.json and Swagger.yaml files

2. **Build Validation:**
   - Build succeeds without errors (warnings are acceptable)
   - Core unit tests run (network failures are expected)
   - Integration tests run for at least 2 minutes

3. **VSCode Extension (if modified):**
   - npm ci completes successfully
   - npm run lint passes
   - npm run compile produces dist/extension.js

### Manual Validation Commands:
```bash
# Quick validation script
cd src
dotnet build Rapicgen.slnx
dotnet run --project CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- csharp nswag Swagger.json Test /tmp/validation.cs
ls -la /tmp/validation.cs
head -10 /tmp/validation.cs

# VSCode extension validation
cd VSCode
npm run lint
npm run compile
ls -la dist/extension.js
```

## Common Tasks and Timing Expectations

### Build Times (with buffer for timeouts):
- **dotnet restore**: ~60 seconds (set timeout: 10+ minutes)
- **dotnet build**: ~20 seconds (set timeout: 60+ seconds) 
- **Unit tests**: ~5 seconds (set timeout: 30+ minutes)
- **Integration tests**: ~2 minutes (set timeout: 30+ minutes)
- **npm ci**: ~80 seconds (set timeout: 10+ minutes)
- **CLI code generation**: ~3 seconds (set timeout: 60+ seconds)

### Key Solution Files:
- **Rapicgen.slnx** - Main solution with CLI and Core projects
- **VSIX.slnx** - Visual Studio extension solution  
- **All.slnx** - Complete solution including all projects
- **VSCode/package.json** - VSCode extension configuration

### Repository Structure:
```
src/
├── Core/                     # Core .NET libraries
│   ├── ApiClientCodeGen.Core/
│   ├── ApiClientCodeGen.Core.Tests/
│   └── ApiClientCodeGen.Core.IntegrationTests/
├── CLI/                      # Command-line tool (rapicgen)
│   ├── ApiClientCodeGen.CLI/
│   └── ApiClientCodeGen.CLI.Tests/
├── VSIX/                     # Visual Studio extensions
├── VSCode/                   # VS Code extension
├── VSMac/                    # Visual Studio for Mac extension
├── Swagger.json              # Test OpenAPI specification
├── Swagger.yaml              # Test OpenAPI specification (YAML)
└── build.ps1                 # PowerShell build script
```

## Critical Build and Test Information

**NEVER CANCEL builds or tests** - this project has complex dependency chains and long-running integration tests that are normal. Always set appropriate timeouts:

- **Builds**: Minimum 60 seconds timeout
- **Package restore**: Minimum 10 minutes timeout  
- **Integration tests**: Minimum 30 minutes timeout
- **npm ci**: Minimum 10 minutes timeout

**Expected failures**: Network-dependent tests will fail in isolated environments. This is normal and expected.

**Dependencies**: This tool generates code using external tools (Java-based OpenAPI generators, Node.js-based NSwag CLI) so initial runs download dependencies and take longer.

### External Tool Integration Patterns

**Dependency Installation Strategy** (`DependencyInstaller`):
- **NPM tools** (AutoRest, NSwag): Install via `npm install -g package-name`  
- **Java tools** (OpenAPI Generator, Swagger): Download JAR files with SHA verification
- **.NET tools** (Kiota, Refitter): Install via `dotnet tool install --global`
- **Version checking**: Parse tool output to determine if updates needed

**Code Generator Implementation Pattern**:
```csharp
public class [Tool]CodeGenerator : ICodeGenerator
{
    public string GenerateCode(IProgressReporter? progress)
    {
        progress?.Progress(10);
        dependencyInstaller.Install[Tool]();        // Install if missing
        progress?.Progress(30);  
        processLauncher.Start(command, arguments);   // Execute external tool
        progress?.Progress(80);
        return ProcessOutput();                      // Merge files if needed
    }
}
```

**Multi-platform Extension Strategy**:
- **VSIX**: COM-visible `IVsSingleFileGenerator` custom tools for Visual Studio
- **VSCode**: TypeScript extension with command palette + context menu integration
- **CLI**: Spectre.Console commands with progress reporting and telemetry
- **VSMac/IntelliJ**: Platform-specific custom tool implementations

### Testing Strategy and Patterns

**Test Categories** (use `[Trait("Category", "...")]`):
- **Unit tests**: Mock external dependencies, test logic only
- **Integration tests**: Execute real external tools, expect network failures
- **`SkipWhenLiveUnitTesting`**: Skip slow tests during development

**Common Test Patterns**:
```csharp
[Fact]
public void GenerateCode_ReportsProgress()
    => mock.Verify(c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()));
    
[SkippableFact(typeof(ProcessLaunchException))]  // Handle network/tool failures
public void InstallTool_Returns_Path() { ... }
```

## Validation Scenarios

### Always Test These Scenarios After Making Changes:

1. **Core CLI Functionality:**
   - Generate code using at least two different generators (NSwag and Refitter)
   - Verify output files are created and contain valid C# code
   - Test with both local Swagger.json and Swagger.yaml files

2. **Build Validation:**
   - Build succeeds without errors (warnings are acceptable)
   - Core unit tests run (network failures are expected)
   - Integration tests run for at least 2 minutes

3. **VSCode Extension (if modified):**
   - npm ci completes successfully
   - npm run lint passes
   - npm run compile produces dist/extension.js

### Manual Validation Commands:
```bash
# Quick validation script
cd src
dotnet build Rapicgen.slnx
dotnet run --project CLI/ApiClientCodeGen.CLI/ApiClientCodeGen.CLI.csproj -- csharp nswag Swagger.json Test /tmp/validation.cs
ls -la /tmp/validation.cs
head -10 /tmp/validation.cs

# VSCode extension validation
cd VSCode
npm run lint
npm run compile
ls -la dist/extension.js
```

## Always Run Before Committing:
1. `dotnet build Rapicgen.slnx` - Must succeed
2. Test CLI tool with local files - Must generate valid code
3. If VSCode extension modified: `npm run lint && npm run compile` - Must succeed
4. Manual validation scenario - Must work end-to-end

The project builds and runs successfully with the above commands. Some tests may fail due to external network dependencies, but the core functionality works correctly.

## Git
Commit every logical small change with a brief commit description for a detailed history of a changes