# SignPath configuration

Artifact configurations used by the GitHub Actions pipelines. SignPath reads them from its own UI, not from this repo: paste each file into the SignPath project with the matching slug.

| File | Slug |
| --- | --- |
| `artifact-configurations/cli-nuget.xml` | `cli-nuget` |
| `artifact-configurations/vsix.xml` | `vsix` |
| `artifact-configurations/intellij-plugin.xml` | `intellij-plugin` |

## Signing policies

The policy is chosen per workflow, not per branch. Every build runs on the `master` default branch, so only the calling workflow decides whether a build is a test build or a release build.

| Workflow | Policy | Purpose |
| --- | --- | --- |
| `release.yml` | `release-signing` | Release artifacts (CLI nupkg, VSIX, IntelliJ plugin) |
| `cli-tool.yml` | `test-signing` | CI builds of the CLI tool |
| `vsix.yml` | `test-signing` | CI builds of the Visual Studio extensions |
| `intellij.yml` | `test-signing` | CI builds of the IntelliJ plugin |

Do not restrict the `release-signing` policy to a branch or tag in the SignPath UI. `release.yml` is always dispatched from `master`, including dry runs, and a branch restriction rejects the signing request with `The selected signing policy is not valid`.

Repository settings required:

- Secret `SIGNPATH_API_TOKEN`
- Variables `SIGNPATH_ORGANIZATION_ID`, `SIGNPATH_PROJECT_SLUG`

Only own binaries are signed (SignPath Foundation forbids signing third-party binaries).
