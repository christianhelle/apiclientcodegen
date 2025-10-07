using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Kiota;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class KiotaCommandSettings : CodeGeneratorCommand<KiotaCommandSettings>.Settings, IKiotaOptions
{
    [CommandOption("--generate-multiple-files|-m")]
    [Description("Set this to TRUE to generate multiple files (default: FALSE)")]
    public bool GenerateMultipleFiles { get; set; }

    [CommandOption("--type-access-modifier")]
    [Description("Set the access modifier for the generated types (default: public)")]
    public TypeAccessModifier TypeAccessModifier { get; set; }

    [CommandOption("--backing-store|-b")]
    [Description("Generate EF backing store code in models (default: FALSE)")]
    public bool UsesBackingStore { get; set; }
}

public class KiotaCommand : CodeGeneratorCommand<KiotaCommandSettings>
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

    public override ICodeGenerator CreateGenerator(KiotaCommandSettings settings) =>
        new KiotaCodeGenerator(
            settings.SwaggerFile,
            settings.DefaultNamespace,
            processLauncher,
            dependencyInstaller,
            settings);
}
