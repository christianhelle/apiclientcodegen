# Two deliberate code-generator factory styles (VSIX enum-dispatch vs CLI per-generator)

The VSIX host uses a single `ICodeGeneratorFactory` whose `Create(...)` switches on the
`SupportedCodeGenerator` enum, because Visual Studio invokes a custom tool through exactly one
seam (the single-file generator) and only knows the generator as an enum value resolved from the
file's custom-tool name. The CLI host instead exposes one typed factory per generator
(`INSwagCodeGeneratorFactory`, `IRefitterCodeGeneratorFactory`, …), because each generator is its
own Spectre.Console command (`csharp nswag`, `csharp refitter`, …) that binds a distinct set of
command-line options and constructs a generator-specific options object; the per-generator factory
is the seam each command's unit test mocks to verify it wires the right generator without launching
external tools.

We deliberately keep these two styles separate rather than forcing a single shared factory.
Collapsing the CLI factories into one enum-dispatch factory would require a union of every
generator's parameters (some need `IOpenApiDocumentFactory`, options types differ) and would remove
the per-command mock seam, reducing testability and locality in the CLI. The shared seam that actually concentrates external-tool behaviour across both hosts is
`Rapicgen.Core.External.ToolRunner` together with the `Rapicgen.Core.ExternalTools` version
registry, not the factory layer.
