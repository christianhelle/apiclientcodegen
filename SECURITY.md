# Security Policy

Thank you for helping keep **REST API Client Code Generator** (the Visual Studio / VS Code / JetBrains Rider
extensions and the `Rapicgen` .NET CLI tool, package id `Rapicgen`) and its users safe.

## Supported Versions

Security fixes are provided for the latest released version of each distribution channel. Older versions are
generally not patched — please update to the latest release before reporting.

| Component | Supported |
| --- | --- |
| Visual Studio 2022 extension (latest Marketplace release) | ✅ |
| Visual Studio 2019 extension (latest Marketplace release) | ✅ |
| Visual Studio Code extension (latest Marketplace release) | ✅ |
| JetBrains Rider / IntelliJ plugin (latest release) | ✅ |
| `Rapicgen` .NET global tool (latest NuGet release) | ✅ |
| Older / pre-release versions | ❌ |

## Reporting a Vulnerability

**Please do not report security vulnerabilities through public GitHub issues, discussions, or pull
requests.**

Report privately using **GitHub Security Advisories**:

➡️ <https://github.com/christianhelle/apiclientcodegen/security/advisories/new>

(If you cannot use GitHub Security Advisories, you may instead contact the maintainer privately and ask for a
secure channel before sharing details.)

To help us triage quickly, please include:

- A description of the vulnerability and its **impact** (e.g., code execution, arbitrary file read/write,
  SSRF, denial of service).
- The affected component and version (VS 2022/2019, VS Code, Rider, or `Rapicgen` CLI + version).
- The **input that triggers it** — typically a minimal OpenAPI/Swagger specification (and which generator
  backend it targets: NSwag, AutoRest, OpenAPI Generator, Kiota, swagger-codegen, or Refitter).
- Step-by-step reproduction instructions and, if possible, a self-contained proof of concept.
- Any relevant environment details (OS, .NET version, installed generator versions).

## Our Commitment / Response Targets

- **Acknowledgement:** within **3 business days**.
- **Initial assessment & severity (CVSS):** within **10 business days**.
- **Fix or mitigation plan:** communicated as soon as the assessment is complete; timelines depend on
  severity and complexity.
- We follow **coordinated disclosure**. We request that you give us a reasonable embargo period (we suggest
  **90 days**, or until a fix ships, whichever is sooner) before any public disclosure, and we will keep you
  updated on progress.
- With your permission, we are happy to **credit you** in the advisory and release notes. If a CVE is
  warranted, we will request one (or you may, via GitHub's advisory workflow).

## Threat Model & Scope

This tool **generates source code from OpenAPI / Swagger documents**. A generated client, the generation
process, and the host running it can be affected by the contents of the input specification. Treat OpenAPI
documents (and any `$ref` targets they reference, local or remote) as **potentially untrusted input** —
especially specs that are downloaded from a URL, pulled from a registry, fetched in CI, or published by a
third party.

**In scope** (please report):

- A crafted specification field (e.g. `info.title`, schema/property names, descriptions, parameter names,
  enum values, `servers`, media types) that causes unsafe behavior on the host or in the generated code —
  for example **code/command injection, identifier/comment breakout, arbitrary file read or write / path
  traversal, or SSRF/LFI** during generation.
- Unsafe handling of `$ref` resolution (remote fetch / local file read / path traversal) at generation time.
- Unsafe construction of sub-generator command lines or configuration (e.g., argument injection) from
  spec-derived or user-supplied values.
- Insecure download/installation of bundled toolchains/dependencies (missing integrity/TLS verification).

**Out of scope / report upstream:**

- Vulnerabilities **inside the third-party generators** this tool invokes
  (NSwag, AutoRest, OpenAPI Generator, Kiota, swagger-codegen, Refitter) that are not caused by how this tool
  invokes or configures them. Please report those to the respective projects. (We are still glad to hear
  about them so we can adjust how we call those tools or pin safer versions.)
- Issues that require the attacker to already have local code-execution or to control the developer's
  machine/project files.
- Findings in the generated *sample* / test fixtures in this repository that do not reflect a real
  generation path.

## Hardening Notes for Users

- Review/trust the OpenAPI specifications you generate clients from, particularly when the spec is fetched
  from a URL or provided by a third party.
- Prefer running generation in a sandboxed or least-privilege environment (e.g., CI with a restricted
  workspace and no access to internal networks/metadata endpoints) when processing untrusted specs.

## Safe Harbor

We will not pursue or support legal action against researchers who:

- make a good-faith effort to comply with this policy,
- avoid privacy violations, data destruction, and service degradation, and
- give us a reasonable opportunity to remediate before public disclosure.

Thank you for contributing to the security of this project.
