# Morpheus Leadership Review: OpenAPI Generator v7.22.0 Update

**Authority:** Morpheus (Lead)  
**Date:** 2026 (Current Session)  
**Status:** Pre-Implementation Guidance  
**For:** Neo (Implementer)

---

## Overview

This document captures my pre-implementation review of the v7.22.0 update workflow. The script is solid, but Neo should be aware of specific gotchas, commit grouping expectations, conflict risk vectors, and validation checkpoints.

---

## Gotchas & Fragile Areas

### 1. **Enum Value Naming — Padding Math is Exact**
The script generates enum names using zero-padded minor version:
- v7.22.0 → `V7220 = 7220` (major=7, minor=22 → "22", patch=0)
- **Gotcha:** If OpenAPI Generator jumps from v7.X.Y to v8.0.0, the enum would be `V8000 = 8000`. Verify the math works: `7 + "00" + 0 = 7000`, `8 + "00" + 0 = 8000`. ✓ Confirmed safe.
- **Manual Review Spot:** After the script runs, grep for the new enum in `OpenApiSupportedVersion.cs` and confirm:
  1. `[Description("7.22.0")]` precedes `V7220 = 7220,`
  2. `Latest => OpenApiSupportedVersion.V7220;` is updated
  3. No leftover references to `V7210` in the "Latest" property

### 2. **PR #1548 Conflict Risk — Minimal but Monitor**
PR #1548 was mentioned as a System.Text.Json 9.0.14 → 9.0.15 upgrade in one `.csproj` file only. Status unknown (draft, merged, or pending).

**Risk Assessment:**
- **If merged before v7.22.0 branch rebases:** No conflict — System.Text.Json and OpenAPI Generator updates are independent file sets.
- **If pending:** Neo should fetch latest master before creating the branch or running the script to avoid sync issues.
- **Mitigation:** Rebase `openapi-generator-v7.22.0` branch from master right before running the script: `git rebase master`.

**Action for Neo:**
1. Check if PR #1548 is merged or still pending.
2. If pending, DO NOT wait — run the script from current master. The two changes are orthogonal and won't conflict during merge.
3. If already merged, no action needed.

### 3. **Commit Grouping — Exact Four-Bucket Pattern Must Be Preserved**
The script creates exactly 4 commits in this order:

| Commit # | Files | Message |
|----------|-------|---------|
| 1 | Core hashes (OpenApiGeneratorVersions.cs, OpenApiSupportedVersion.cs, Resource.resx, Resource.Designer.cs) | `Add OpenAPI Generator v7.22.0 version entry and hashes` |
| 2 | CLI + tests (Program.cs, OpenApiVersionExtensionsTests.cs) | `Update CLI description and tests for OpenAPI Generator v7.22.0` |
| 3 | Docs (README.md, docs/*.md, java/README.md) | `Update documentation for OpenAPI Generator v7.22.0` |
| 4 | IDE extensions (VSCode, VSIX, VSMac, IntelliJ) | `Update IDE extensions for OpenAPI Generator v7.22.0` |

**Gotcha:** Do NOT manually re-order or combine commits. The 4-bucket pattern is a team standard (PR #1481, PR #1523, SKILL.md). Code review expects this structure.

**Validation:**
- After script runs, verify with `git log --oneline -4`:
  ```
  XXXXXXXX Update IDE extensions for OpenAPI Generator v7.22.0
  XXXXXXXX Update documentation for OpenAPI Generator v7.22.0
  XXXXXXXX Update CLI description and tests for OpenAPI Generator v7.22.0
  XXXXXXXX Add OpenAPI Generator v7.22.0 version entry and hashes
  ```

### 4. **Version Enum Coverage in Tests — InlineData Rules**
The script updates `OpenApiVersionExtensionsTests.cs` with these test categories:

1. **IsLatest:** Old version becomes `[InlineData(V7210, false)]`, new version becomes `[InlineData(V7220, true)]`
2. **IsOlderThanLatest:** Old version becomes `[InlineData(V7210, true)]` (moves to "older" section)
3. **ResolveVersion:** `Latest` maps to `V7220`, new version gets its own resolve entry
4. **EnumValues_MatchExpectedIntValues:** The SKILL.md notes that if this test fails, you must manually add the new `[InlineData]` for the enum value check.

**Fragile Spot:** The script uses string replacement for test updates. If test file structure changes (e.g., comment format, spacing), the replacement may fail silently. **Manual Review After Script Run:**
- Open `OpenApiVersionExtensionsTests.cs`
- Grep for `V7220` — should appear 3-4 times (IsLatest, IsOlderThanLatest resolve, enum value test)
- Verify the old version `V7210` appears in "IsOlderThanLatest" section with `true` parameter
- Run the test with `dotnet test ... --filter "OpenApiVersionExtensionsTests"` — must pass

### 5. **Resource.resx Hash Updates — Regex Precision Critical**
The script replaces MD5 and SHA1 hashes in two places:

1. **Resource.resx (XML):** Uses regex to replace 32-char hex (MD5) and 40-char hex (SHA1) in `<value>` tags
2. **Resource.Designer.cs (auto-generated comments):** Uses regex to replace hex sequences in "Looks up a localized string similar to `<hash>`."

**Gotcha:** If the resx file has been edited manually or the Designer.cs comments don't follow the exact format, the regex may miss a hash. **Manual Review:**
- Open both files in an editor and verify the new hashes are present and the old ones are gone
- Check for stale v7.21.0 references — there should be NONE in hashes or version strings

### 6. **Documentation Surface Coverage — 9 Files, All Must Update**
The script updates:
- README.md (top-level repo docs)
- docs/CLI.md, docs/Marketplace.md, docs/Marketplace2022.md, docs/VisualStudioForMac.md
- docs/website/ (cli.html, features.html, index.html)
- java/README.md

**Gotcha:** If v7.22.0 release notes include breaking changes or significant features (like in v7.21.0 with Spring Boot 3.x), the script will only do version string replacement. Neo may need to manually add release notes or breaking change documentation.

**For v7.22.0 specifically:** Check the [OpenAPI Generator v7.22.0 release notes](https://github.com/OpenAPITools/openapi-generator/releases/tag/v7.22.0) for:
- Breaking changes in C# codegen
- New features that need documentation
- If found, add a manual update after script runs

### 7. **IDE Extensions — 6 Files Across 4 Platforms**
The script updates version strings in:
- VSCode: package.json
- VSIX (Visual Studio): VSCommandTable.vsct (2 files), string-resources.json
- VSMac: Manifest.addin.xml
- IntelliJ/Rider: plugin.xml

**Gotcha:** If version strings are embedded in UI labels or descriptions (e.g., "OpenAPI Generator 7.21.0"), the script will update them. Verify these are user-facing and intentional. **Manual Check:**
- Open VSCode/package.json and search for "7.22.0" — should be in description or version reference
- Spot-check VSIX string resources for proper context (shouldn't break help text or tooltips)

---

## Validation Checklist for Neo

After the script completes (with or without `-SkipBuild`), run these checks in order:

### ✓ Step 1: Verify Commits
```powershell
git log --oneline -4
# Should show exactly 4 commits in reverse order:
# 1. "Update IDE extensions for OpenAPI Generator v7.22.0"
# 2. "Update documentation for OpenAPI Generator v7.22.0"
# 3. "Update CLI description and tests for OpenAPI Generator v7.22.0"
# 4. "Add OpenAPI Generator v7.22.0 version entry and hashes"
```

### ✓ Step 2: Verify Enum Coverage
```powershell
cd src
dotnet test Core\ApiClientCodeGen.Core.Tests\ApiClientCodeGen.Core.Tests.csproj --no-build -f net8.0 --filter "FullyQualifiedName~OpenApiVersionExtensionsTests"
# Must PASS
```

### ✓ Step 3: Verify CLI Help
```powershell
dotnet run --project CLI\ApiClientCodeGen.CLI\ApiClientCodeGen.CLI.csproj -- csharp --help
# Output must contain: "OpenAPI Generator (v7.22.0)"
```

### ✓ Step 4: Grep for Stale References
```powershell
cd ..  # back to repo root
# Search for v7.21.0 in non-comment lines (should ONLY appear in version history, not active code)
Get-ChildItem -Recurse -Include "*.cs", "*.md", "*.json", "*.xml" -Exclude @("*.Designer.cs") | xargs findstr /R "7\.21\.0" | grep -v "// " | grep -v "<!-- "
# Should return ZERO matches (v7.21.0 should only appear in docs/release notes/history, not live code)
```

### ✓ Step 5: Build & Unit Tests (if `-SkipBuild` was used)
```powershell
cd src
dotnet restore Rapicgen.slnx
dotnet build Rapicgen.slnx
# Should complete without errors
```

---

## Workflow Recommendations

### Pre-Flight Checklist (Before Running Script)
1. [ ] Branch is `openapi-generator-v7.22.0` and clean (`git status` shows no uncommitted changes)
2. [ ] Latest master is fetched: `git fetch origin master && git rebase origin/master`
3. [ ] Check if PR #1548 (System.Text.Json) is merged; if pending, no action needed (orthogonal changes)
4. [ ] Have internet connection (script downloads JAR file, ~60 MB)
5. [ ] Java 17+ is installed: `java -version`
6. [ ] .NET 8.0 SDK is installed: `dotnet --version`

### Script Invocation
```powershell
cd C:\projects\christianhelle\apiclientcodegen
.\scripts\update-openapi-generator.ps1 -NewVersion "7.22.0"
```
**Why full invocation:** This allows the script to auto-download the JAR, compute hashes, run build & tests, and commit. Total time: ~3-5 minutes.

**Alternative (if hashes are known):**
```powershell
# Requires pre-computing SHA1 and MD5 from Maven Central
.\scripts\update-openapi-generator.ps1 -NewVersion "7.22.0" -SHA1 "<hash>" -MD5 "<hash>" -SkipDownload -SkipBuild
```
Use `-SkipBuild` ONLY if you intend to validate manually afterward.

### Post-Script (Before Pushing)
1. [ ] Run validation checklist above (all 5 steps pass)
2. [ ] Spot-check Resource.Designer.cs for correct hash comments
3. [ ] Verify no file corruption: `git diff --name-status openapi-generator-v7.22.0 master | wc -l` (should match expected 22-file footprint)

---

## Team Context: PR #1523 Precedent

PR #1523 (v7.21.0) followed the exact same workflow:
- 4 standard commits ✓
- All 22 files updated (Core, CLI, Tests, Docs, IDE extensions) ✓
- Same SKILL.md guidance ✓
- Merged without issues ✓

**v7.22.0 should follow identical pattern.**

---

## Decision: Process Binding

**For Neo & all implementers:**
1. Use the script (`.\scripts\update-openapi-generator.ps1`) — do NOT hand-edit version strings
2. Preserve the 4-commit grouping exactly as defined
3. Run the full validation checklist before pushing
4. If the script fails, escalate to Morpheus (do NOT continue with manual edits)

**Reason:** The script is the single source of truth for OpenAPI Generator updates. It ensures consistency, catch fragile areas, and maintains the 22-file footprint predictably. Manual editing introduces merge conflicts, missed surfaces, and inconsistent commit history.

---

## Escalation Criteria for Neo

Contact Morpheus (escalate) if:
1. Script fails during download/hash computation (network issue or Maven Central unavailable)
2. Test validation fails after `-SkipBuild` or `-SkipCommit` runs
3. Enum value naming doesn't follow the pattern (e.g., OpenAPI Generator moves to v8.0.0 and enum logic fails)
4. Any file in the 22-file footprint is missing or corrupt after script run
5. PR #1548 creates unexpected merge conflicts

---

## Reference Materials

- **SKILL.md:** `.squad/skills/update-openapi-generator/SKILL.md` — canonical playbook
- **Script:** `scripts/update-openapi-generator.ps1`
- **PR #1523:** Merged v7.21.0 — use as reference for commit structure
- **PR #1481:** Original automation script + four-commit pattern
- **OpenAPI Generator Releases:** https://github.com/OpenAPITools/openapi-generator/releases

---

**End of Leadership Review**
