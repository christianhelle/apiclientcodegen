# Skill: Update OpenAPI Generator Version

## When to Use
For recurring requests like `Update OpenAPI Generator to vX.X.X`.

## Default Workflow
1. Work from the repo root on a dedicated `openapi-generator-X.X.X` branch with a clean worktree.
2. Run the automation script instead of hand-editing version strings:

   ```powershell
   .\scripts\update-openapi-generator.ps1 -NewVersion "X.X.X"
   ```

3. If you already have the Maven Central hashes, reuse them:

   ```powershell
   .\scripts\update-openapi-generator.ps1 -NewVersion "X.X.X" -SHA1 "<sha1>" -MD5 "<md5>" -SkipDownload
   ```

4. If you need to review the diff or avoid auto-committing from a dirty worktree, add `-SkipCommit` and stage the four buckets manually.
5. Use `-SkipBuild` only when you are intentionally deferring validation.

## What the Script Covers
The script auto-detects the current version, downloads the JAR when needed, computes SHA1/MD5, updates the OpenAPI Generator surfaces across the same four buckets used in PR #1481, runs restore/build plus targeted version tests, and creates the four standard commits unless skipped.

## Standard Commit Sequence
1. `Add OpenAPI Generator vX.X.X version entry and hashes`
2. `Update CLI description and tests for OpenAPI Generator vX.X.X`
3. `Update documentation for OpenAPI Generator vX.X.X`
4. `Update IDE extensions for OpenAPI Generator vX.X.X`

All commits include:
`Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>`

## Validation
After the script runs, confirm:
1. `dotnet build src\Rapicgen.slnx` succeeded (the script already does restore/build unless `-SkipBuild` was used).
2. `dotnet test src\Core\ApiClientCodeGen.Core.Tests\ApiClientCodeGen.Core.Tests.csproj --no-build -f net8.0 --filter "FullyQualifiedName~OpenApiVersionExtensionsTests"` passed, or the script ran the equivalent command from `src\`.
3. `dotnet run --project src\CLI\ApiClientCodeGen.CLI\ApiClientCodeGen.CLI.csproj -- csharp --help` shows `OpenAPI Generator (vX.X.X)`.
4. No stale old-version or old-enum references remain in the diff.

## Manual Review Spots
- Ensure the new enum value follows `V` + major + 2-digit minor + patch (example: `7.21.0` -> `V7210 = 7210`).
- If the script misses `EnumValues_MatchExpectedIntValues`, add the newest `[InlineData]` manually.
- Verify `Resource.Designer.cs` hash comment replacements look correct.
- Use `csharp --help`, not top-level `--help`, when checking the versioned OpenAPI subcommand text.

## Reference
- PR #1481 established the script-backed four-commit pattern.
