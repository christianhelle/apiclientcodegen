using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;

namespace Rapicgen.CLI.Commands.CSharp;

[Command("kiota", Description = "Microsoft Kiota (v1.13.0)")]
public class KiotaCommand : CodeGeneratorCommand
{
    private readonly IProcessLauncher processLauncher;
    private readonly IDependencyInstaller dependencyInstaller;

    public KiotaCommand(
        IConsoleOutput console,
        IProgressReporter? progressReporter,
        IProcessLauncher processLauncher,
        IDependencyInstaller dependencyInstaller)
        : base(console, progressReporter)
    {
        this.processLauncher = processLauncher;
        this.dependencyInstaller = dependencyInstaller;
    }

    public override ICodeGenerator CreateGenerator() =>
        new KiotaCodeGenerator(
            SwaggerFile,
            DefaultNamespace,
            processLauncher,
            dependencyInstaller);
}