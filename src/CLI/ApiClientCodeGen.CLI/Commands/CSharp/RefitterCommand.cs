using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Refitter;
using Rapicgen.Core.Logging;

namespace Rapicgen.CLI.Commands.CSharp;

[Command("refitter", Description = "Refitter (v0.3.3)")]
public class RefitterCommand : CodeGeneratorCommand
{
    public RefitterCommand(
        IConsoleOutput console,
        IProgressReporter? progressReporter)
        : base(console, progressReporter)
    {
    }

    public override ICodeGenerator CreateGenerator() =>
        new RefitterCodeGenerator(SwaggerFile, DefaultNamespace);
}