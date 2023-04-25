using McMaster.Extensions.CommandLineUtils;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;

namespace Rapicgen.CLI.Commands.CSharp;

[Command("refitter", Description = "Refitter (v0.4.2)")]
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
        LongName = "skipGenerateContracts",
        Description = "Set this to skip generating the contract types (default: Enabled)")]
    public bool GenerateContracts
    {
        get => options.GenerateContracts;
        set => options.GenerateContracts = !value;
    }

    [Option(
        ShortName = "noxml",
        LongName = "skipGenerateXmlDocCodeComments",
        Description = "Set this to skip generating XML doc style code comments (default: Enabled)")]
    public bool GenerateXmlDocCodeComments
    {
        get => options.GenerateXmlDocCodeComments;
        set => options.GenerateXmlDocCodeComments = !value;
    }

    [Option(
        ShortName = "apiresponse",
        LongName = "returnApiResponse",
        Description = "Set this to wrap the returned the contract types in IApiResponse<T> (default: Disabled)")]
    public bool ReturnIApiResponse
    {
        get => options.ReturnIApiResponse;
        set => options.ReturnIApiResponse = value;
    }
}