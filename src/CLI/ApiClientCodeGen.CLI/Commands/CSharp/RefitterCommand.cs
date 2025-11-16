using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Rapicgen.CLI.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class RefitterCommandSettings : CodeGeneratorCommand<RefitterCommandSettings>.Settings
{
    [CommandOption("--skip-generate-contracts")]
    [Description("Set this to skip generating the contract types (default: Enabled)")]
    public bool SkipGenerateContracts { get; set; }

    [CommandOption("--skip-generate-xml-doc-code-comments")]
    [Description("Set this to skip generating XML doc style code comments (default: Enabled)")]
    public bool SkipGenerateXmlDocCodeComments { get; set; }

    [CommandOption("--return-api-response")]
    [Description("Set this to wrap the returned the contract types in IApiResponse<T> (default: Disabled)")]
    public bool ReturnIApiResponse { get; set; }

    [CommandOption("--generate-internal-types")]
    [Description(
        "Set this to generate the API interface and contract types using the internal accessibility modifier (default modifier: public)")]
    public bool GenerateInternalTypes { get; set; }

    [CommandOption("--cancellation-tokens")]
    [Description("Set this to generate the API interface with Cancellation Tokens")]
    public bool UseCancellationTokens { get; set; }

    [CommandOption("--no-operation-headers")]
    [Description("Don't generate operation headers")]
    public bool NoOperationHeaders { get; set; }

    [CommandOption("--multiple-files")]
    [Description("Generate multiple files")]
    public bool GenerateMultipleFiles { get; set; }

    [CommandOption("--settings-file")]
    [Description("Path to a .refitter settings file to use for code generation")]
    public string? SettingsFile { get; set; }
}

public class RefitterCommand : CodeGeneratorCommand<RefitterCommandSettings>
{
    private readonly IRefitterCodeGeneratorFactory factory;
    private readonly IRefitterOptions options;
    private readonly IProcessLauncher processLauncher;
    private readonly IDependencyInstaller dependencyInstaller;

    public RefitterCommand(
        IConsoleOutput console,
        IProgressReporter? progressReporter,
        IRefitterCodeGeneratorFactory factory,
        IRefitterOptions options,
        IProcessLauncher processLauncher,
        IDependencyInstaller dependencyInstaller)
        : base(console, progressReporter)
    {
        this.factory = factory;
        this.options = options;
        this.processLauncher = processLauncher;
        this.dependencyInstaller = dependencyInstaller;
    }

    public override int Execute(CommandContext context, RefitterCommandSettings settings, CancellationToken cancellationToken)
    {
        // Map settings to options
        options.GenerateContracts = !settings.SkipGenerateContracts;
        options.GenerateXmlDocCodeComments = !settings.SkipGenerateXmlDocCodeComments;
        options.ReturnIApiResponse = settings.ReturnIApiResponse;
        options.GenerateInternalTypes = settings.GenerateInternalTypes;
        options.UseCancellationTokens = settings.UseCancellationTokens;
        options.GenerateHeaderParameters = !settings.NoOperationHeaders;
        options.GenerateMultipleFiles = settings.GenerateMultipleFiles;

        // If a settings file is specified, validate it exists and use it
        if (!string.IsNullOrEmpty(settings.SettingsFile))
        {
            if (!File.Exists(settings.SettingsFile))
                throw new FileNotFoundException($"Settings file '{settings.SettingsFile}' not found.");
            
            // Use settings file as the SwaggerFile
            settings.SwaggerFile = settings.SettingsFile;
        }
        else if (string.IsNullOrEmpty(settings.SwaggerFile))
        {
            throw new ArgumentException("Either swaggerFile argument or --settings-file option must be provided.");
        }

        // Call the base implementation
        return base.Execute(context, settings, cancellationToken);
    }

    public override ICodeGenerator CreateGenerator(RefitterCommandSettings settings) =>
        factory.Create(settings.SettingsFile ?? settings.SwaggerFile, settings.DefaultNamespace, processLauncher, dependencyInstaller, options);
}