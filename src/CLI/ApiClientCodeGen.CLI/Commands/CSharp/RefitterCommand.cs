using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Rapicgen.CLI.Commands.CSharp;

[Command("refitter", Description = "Refitter (v1.5.5)")]
public class RefitterCommand : CodeGeneratorCommand
{
    private readonly IRefitterCodeGeneratorFactory factory;
    private readonly IRefitterOptions options;

    public RefitterCommand(
        IConsoleOutput console,
        IProgressReporter? progressReporter,
        IRefitterCodeGeneratorFactory factory,
        IRefitterOptions options)
        : base(console, progressReporter)
    {
        this.factory = factory;
        this.options = options;
    }
    
    [Option(
        ShortName = "sf",
        LongName = "settings-file",
        Description = "Path to a .refitter settings file to use for code generation")]
    [FileExists]
    public string? SettingsFile { get; set; }

    public override ICodeGenerator CreateGenerator() =>
        factory.Create(SettingsFile ?? SwaggerFile, DefaultNamespace, options);
        
    // Use 'new' instead of 'override' since the base method is not marked as virtual
    public new int OnExecute()
    {
        // If a settings file is specified, validate it exists
        if (!string.IsNullOrEmpty(SettingsFile))
        {
            // Use settings file as the SwaggerFile
            SwaggerFile = SettingsFile;
        }
        
        // Call the base implementation
        return base.OnExecute();
    }

    [Option(
        ShortName = "nocontracts",
        LongName = "skip-generate-contracts",
        Description = "Set this to skip generating the contract types (default: Enabled)")]
    public bool GenerateContracts
    {
        get => options.GenerateContracts;
        set => options.GenerateContracts = !value;
    }

    [Option(
        ShortName = "noxml",
        LongName = "skip-generate-xml-doc-code-comments",
        Description = "Set this to skip generating XML doc style code comments (default: Enabled)")]
    public bool GenerateXmlDocCodeComments
    {
        get => options.GenerateXmlDocCodeComments;
        set => options.GenerateXmlDocCodeComments = !value;
    }

    [Option(
        ShortName = "apiresponse",
        LongName = "return-api-response",
        Description = "Set this to wrap the returned the contract types in IApiResponse<T> (default: Disabled)")]
    public bool ReturnIApiResponse
    {
        get => options.ReturnIApiResponse;
        set => options.ReturnIApiResponse = value;
    }

    [Option(
        ShortName = "internal",
        LongName = "generate-internal-types",
        Description =
            "Set this to generate the API interface and contract types using the internal accessbility modifier (default modifier: public)")]
    public bool GenerateInternalTypes
    {
        get => options.GenerateInternalTypes;
        set => options.GenerateInternalTypes = value;
    }

    [Option(
        ShortName = "ct",
        LongName = "cancellation-tokens",
        Description = "Set this to generate the API interface with Cancellation Tokens")]
    public bool UseCancellationTokens
    {
        get => options.UseCancellationTokens;
        set => options.UseCancellationTokens = value;
    }

    [Option(
        ShortName = "noheaders",
        LongName = "no-operation-headers",
        Description = "Don't generate operation headers")]
    public bool NoOperationHeaders 
    {
        get => !options.GenerateHeaderParameters;
        set => options.GenerateHeaderParameters = !value;
    }

    [Option(ShortName = "mf", LongName = "multiple-files", Description = "Generate multiple files")]
    public bool GenerateMultipleFiles
    {
        get => options.GenerateMultipleFiles;
        set => options.GenerateMultipleFiles = value;
    }
}