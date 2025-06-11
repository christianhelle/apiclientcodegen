using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;

namespace Rapicgen.CLI.Commands.CSharp;

[Command("kiota", Description = "Microsoft Kiota (v1.27.0)")]
public class KiotaCommand : CodeGeneratorCommand, IKiotaOptions
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
            dependencyInstaller,
            this);

    [Option(
        ShortName = "m",
        LongName = "generate-multiple-files",
        Description = "Set this to TRUE to generate multiple files (default: FALSE)")]
    public bool GenerateMultipleFiles { get; set; }

    [Option(
        ShortName = "tam",
        LongName = "type-access-modifier",
        Description = "Set the access modifier for the generated types (default: public)")]
    public TypeAccessModifier TypeAccessModifier { get; }
}
