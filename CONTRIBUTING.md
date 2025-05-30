# Contributing to REST API Client Code Generator

Thank you for your interest in contributing to the REST API Client Code Generator! This document provides guidelines and conventions to help maintain code quality and consistency across the project.

## Development Setup

### Prerequisites

- .NET SDK (latest version)
- Visual Studio 2019/2022 or VS Code
- Node.js (for VS Code extension development)
- Git

### Repository Structure

This repository contains multiple components:

- **`src/Core/`** - Core .NET libraries and business logic
- **`src/CLI/`** - Command-line interface tool (Rapicgen)
- **`src/VSIX/`** - Visual Studio extension
- **`src/VSCode/`** - Visual Studio Code extension
- **`src/VSMac/`** - Visual Studio for Mac extension
- **`test/`** - Test files and test data
- **`docs/`** - Documentation

### Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/christianhelle/apiclientcodegen.git
   cd apiclientcodegen
   ```

2. **Build the project**
   ```bash
   cd src
   dotnet restore
   dotnet build
   ```

3. **Run tests**
   ```bash
   dotnet test
   ```

For specific component development, see the respective directories for additional setup instructions.

## Code Patterns and Style Guidelines

### C# Code Standards

Follow these established patterns found throughout the codebase:

#### Test Naming Conventions
- Use descriptive test method names that clearly state what is being tested
- Follow the pattern: `MethodName_Scenario_ExpectedResult`
- Examples:
  ```csharp
  [Fact]
  public void Constructor_Requires_XDocument()
  
  [Fact] 
  public void Updates_PropertyGroups()
  
  [Fact]
  public void InstallOpenApiGenerator_Invokes_DownloadFile()
  ```

#### Test Structure
- Use the **Arrange-Act-Assert** pattern
- Utilize **FluentAssertions** for assertions
- Use **xUnit** as the test framework
- Apply **AutoMoqData** attribute for parameterized tests with mocking
- Example:
  ```csharp
  [Theory, AutoMoqData]
  public void MethodName_Should_DoSomething(
      [Frozen] IDependency dependency,
      ClassUnderTest sut)
  {
      // Arrange
      // setup test data
      
      // Act
      var result = sut.MethodUnderTest();
      
      // Assert
      result.Should().NotBeNull();
      Mock.Get(dependency).Verify(/* verification */);
  }
  ```

#### General C# Guidelines
- Use meaningful variable and method names
- Follow Microsoft's C# coding conventions
- Use dependency injection patterns where appropriate
- Implement interfaces for testability
- Use nullable reference types where supported

### TypeScript/JavaScript (VS Code Extension)
- Follow the existing patterns in `src/VSCode/`
- Use ESLint configuration provided
- Maintain TypeScript type safety
- Use webpack for bundling

## Testing Guidelines

### Unit Tests
- Write unit tests for all new functionality
- Aim for high code coverage
- Use descriptive test names that explain the scenario
- Mock external dependencies using Moq
- Test both positive and negative scenarios

### Integration Tests
- Add integration tests for end-to-end scenarios
- Use appropriate test data from the `test/` directory
- Ensure tests are isolated and can run independently

### Test Organization
- Place tests in corresponding `*.Tests` projects
- Mirror the source code structure in test projects
- Group related tests in the same test class

## Pull Request Guidelines

### PR Description Requirements
**PR descriptions must be as verbose as possible.** Include:

1. **Clear summary** of what the PR accomplishes
2. **Detailed explanation** of changes made
3. **Reasoning** behind the approach taken
4. **Testing performed** - describe what tests were added/modified
5. **Breaking changes** if any
6. **Related issues** using keywords like "Fixes #123" or "Closes #456"

#### Example PR Description:
```markdown
## Summary
Add support for generating TypeScript clients using OpenAPI Generator

## Changes Made
- Extended the core generator interface to support TypeScript output
- Added new TypeScript-specific configuration options
- Updated CLI tool to accept typescript as a generator option
- Added comprehensive unit tests for the new functionality

## Reasoning
This change addresses user requests for TypeScript client generation 
capability, expanding the tool's usefulness in JavaScript/TypeScript 
ecosystems.

## Testing
- Added 15 new unit tests covering TypeScript generation scenarios
- Verified integration with existing OpenAPI Generator workflows
- Tested with sample OpenAPI specifications

## Breaking Changes
None - this is a purely additive feature.

Fixes #123, Closes #456
```

### README Maintenance
**Keep README.md up to date with changes:**

- Update feature lists when adding new capabilities
- Add new installation instructions for new components
- Update usage examples when APIs change
- Maintain accuracy in supported platforms/versions
- Update badges and links as needed

### Technical Requirements
1. **All tests must pass** - ensure existing functionality isn't broken
2. **Add tests for new features** - maintain or improve test coverage
3. **Update CHANGELOG.md** with your changes in the "Unreleased" section
4. **Follow semantic versioning** guidelines for version impacts
5. **Update documentation** if adding new features or changing behavior

### Code Review Process
- PRs require approval before merging
- Address all review feedback
- Keep commits focused and atomic
- Use meaningful commit messages

## Build and Development Workflow

### VS Code Extension Development
See [`src/VSCode/CONTRIBUTING.md`](src/VSCode/CONTRIBUTING.md) for specific VS Code extension development guidelines.

## Continuous Integration

The project uses GitHub Actions for CI/CD:
- Unit tests run on all PRs
- Integration tests validate changes
- Code quality checks via SonarCloud
- Automated releases when merging to main

## Getting Help

- Review existing code for patterns and examples
- Check the [documentation](docs/) for detailed guides
- Look at recent PRs for examples of good contributions
- Open an issue for questions or clarifications

## Code of Conduct

Please be respectful and inclusive in all interactions related to this project. We welcome contributions from developers of all backgrounds and experience levels.

---

Thank you for contributing to making this tool better for the development community!