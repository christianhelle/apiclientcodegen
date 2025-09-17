# Version Bump Scripts

This directory (`.github/workflows`) contains PowerShell scripts to automatically bump the VERSION environment variable in GitHub workflow files located in this same directory.

## Usage Location

**Important**: These scripts must be run from the `.github/workflows` directory where they are located.

```powershell
# Navigate to the workflows directory first
cd .github/workflows

# Then run the scripts
.\bump-version.ps1 -DryRun
```

## Scripts

### `bump-version.ps1` - Main Version Bumper

The comprehensive script that handles all version bumping scenarios with detailed options and safety features.

#### Usage

```powershell
# Increment minor version (1.32 -> 1.33)
.\bump-version.ps1

# Increment major version (1.32 -> 2.0)  
.\bump-version.ps1 -IncrementType major

# Set specific version
.\bump-version.ps1 -NewVersion "2.5"

# Dry run to see what would change
.\bump-version.ps1 -DryRun

# Dry run with major increment
.\bump-version.ps1 -IncrementType major -DryRun
```

#### Parameters

- **IncrementType**: `"major"` or `"minor"` (default: `"minor"`)
  - `major`: Increments first number (1.32 → 2.0)
  - `minor`: Increments second number (1.32 → 1.33)

- **NewVersion**: Set exact version (e.g., `"2.5"`, `"1.45"`)
  - Overrides IncrementType when specified
  - Must be in format `x.y`

- **WorkflowPath**: Path to workflows (default: `"."` - current directory)

- **DryRun**: Preview changes without modifying files

### `quick-bump.ps1` - Simple Version Bumper

A simplified wrapper for common scenarios.

#### Basic Usage

```powershell
# Increment minor version
.\quick-bump.ps1

# Increment major version
.\quick-bump.ps1 major

# Set specific version
.\quick-bump.ps1 1.45
```

## What These Scripts Do

The scripts scan all GitHub workflow YAML files in the current directory (`.github/workflows/`) and update the VERSION environment variable from:

```yaml
env:
  VERSION: 1.32.${{ github.run_number }}
```

To (for example, with minor increment):

```yaml
env:
  VERSION: 1.33.${{ github.run_number }}
```

## Currently Affected Files

The following workflow files contain VERSION environment variables:

- `cli-tool.yml`
- `intellij.yml`
- `vscode.yml`
- `vsix.yml`

## Safety Features

- **Dry Run Mode**: Use `-DryRun` to preview changes
- **Validation**: Checks for consistent versions across files
- **Error Handling**: Validates version formats and file existence
- **Backup Recommendation**: Always review changes with `git diff` before committing

## Workflow Examples

### Minor Version Bump (Most Common)

```powershell
# Navigate to workflows directory
cd .github/workflows

# Preview the change
.\bump-version.ps1 -DryRun

# Apply the change
.\bump-version.ps1

# Review and commit (from repository root)
cd ../..
git diff
git add .
git commit -m "Bump version to 1.33"
git push
```

### Major Version Bump

```powershell
# Navigate to workflows directory
cd .github/workflows

# Preview the change  
.\bump-version.ps1 -IncrementType major -DryRun

# Apply the change
.\bump-version.ps1 -IncrementType major

# Review and commit (from repository root)
cd ../..
git diff
git add .
git commit -m "Bump version to 2.0"
git push
```

### Setting Specific Version

```powershell
# Navigate to workflows directory
cd .github/workflows

# Set to version 2.5
.\bump-version.ps1 -NewVersion "2.5"

# Review and commit (from repository root)
cd ../..
git diff
git add .  
git commit -m "Set version to 2.5"
git push
```

## Error Handling

The scripts include comprehensive error handling for:

- Missing workflow directory
- No workflow files found
- Inconsistent versions across files
- Invalid version formats
- File access issues

## Requirements

- PowerShell 5.1 or later
- Write access to GitHub workflow files (run from `.github/workflows/` directory)
- Git repository context (for best practices)

## Best Practices

1. **Always use dry run first**: `.\bump-version.ps1 -DryRun`
2. **Review changes**: `git diff` before committing
3. **Test workflows**: Ensure GitHub Actions still work after version bump
4. **Consistent versioning**: Don't manually edit version numbers in individual files
5. **Backup**: Commit current state before running version bump scripts
