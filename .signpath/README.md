# SignPath configuration

Artifact configurations used by the GitHub Actions pipelines. SignPath reads them from its own UI, not from this repo: paste each file into the SignPath project with the matching slug.

| File | Slug |
| --- | --- |
| `artifact-configurations/cli-nuget.xml` | `cli-nuget` |
| `artifact-configurations/vsix.xml` | `vsix` |
| `artifact-configurations/intellij-plugin.xml` | `intellij-plugin` |

Signing policies: `release-signing` (release.yml), `test-signing` (CI workflows on master).

Repository settings required:

- Secret `SIGNPATH_API_TOKEN`
- Variables `SIGNPATH_ORGANIZATION_ID`, `SIGNPATH_PROJECT_SLUG`

Only own binaries are signed (SignPath Foundation forbids signing third-party binaries).
