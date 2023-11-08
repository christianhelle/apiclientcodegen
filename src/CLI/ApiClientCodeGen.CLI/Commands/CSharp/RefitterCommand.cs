using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;

namespace Rapicgen.CLI.Commands.CSharp;

[Command("refitter", Description = "Refitter (v0.8.4)")]
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

    public override ICodeGenerator CreateGenerator() =>
        factory.Create(SwaggerFile, DefaultNamespace, options);

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
}