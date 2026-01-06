using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.RpcContracts.ProgressReporting;
using NSwag.CodeGeneration.CSharp;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.NSwag;
using Rapicgen.Core.Generators.NSwagStudio;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using System.Text.Json;
using ProgressReporter = Microsoft.VisualStudio.Extensibility.Shell.ProgressReporter;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateNSwagCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateNSwagBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%NSwagCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(
            ClientContextKey.Shell.ActiveSelectionFileName, 
            ".(json|ya?ml)")
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken) =>
        await GenerateAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
}

[VisualStudioContribution]
public class GenerateNSwagStudioCommand(
    TraceSource traceSource, 
    ExtensionSettingsProvider settingsProvider)
    : Command
{
    public override CommandConfiguration CommandConfiguration => new("%NSwagStudioCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        Placements = [KnownPlacements.Node_IncludeExcludeGroup],
        VisibleWhen = ActivationConstraint.ClientContext(
            ClientContextKey.Shell.ActiveSelectionFileName,
            ".(nswag)")
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Generate NSwag Studio output");

        using var progress = await Extensibility
            .Shell()
            .StartProgressReportingAsync(
                "Generating code with NSwag Studio",
                new ProgressReporterOptions(true),
                cancellationToken);

        cancellationToken = CancellationTokenSource
            .CreateLinkedTokenSource(
                cancellationToken,
                progress.CancellationToken)
            .Token;

        try
        {
            string inputFile = await context.GetInputFileAsync(cancellationToken);
            var inputFilename = Path.GetFileName(inputFile);
            progress.Report(10, $"Starting NSwag Studio code generation for: {inputFilename}");

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(30, "Initializing NSwag Studio code generator...");

            var launcher = new ProcessLauncher();
            await Task.Run(async () =>
            {
                progress.Report(50, "Generating C# client code...");
                return new NSwagStudioCodeGenerator(
                    inputFile,
                    await settingsProvider.GetGeneralOptionsAsync(cancellationToken),
                    launcher,
                    new DependencyInstaller(
                        new NpmInstaller(launcher),
                        new FileDownloader(new WebDownloader()), launcher))
                    .GenerateCode(null);
            }, cancellationToken);

            progress.Report(100, "NSwag Studio code generation completed successfully");
        }
        catch (OperationCanceledException)
        {
            progress.Report(100, "NSwag Studio code generation was canceled.");
        }
        catch (Exception e)
        {
            Logger.Instance.TrackError(e);
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating code using NSwag Studio: {0}",
                e.Message);

            progress.Report(100, "Error generating code using NSwag Studio: " + e.Message);
        }
    }
}

[VisualStudioContribution]
public class GenerateNSwagNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateNSwagBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%NSwagCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken) =>
        await GenerateAsync(
            await this.AddNewOpenApiFileAsync(context, cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
}

public abstract class GenerateNSwagBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;

    public async Task GenerateAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Generate NSwag output");

        using var progress = await Extensibility
            .Shell()
            .StartProgressReportingAsync(
                "Generating code with NSwag",
                new ProgressReporterOptions(true),
                cancellationToken);

        cancellationToken = CancellationTokenSource
            .CreateLinkedTokenSource(
                cancellationToken,
                progress.CancellationToken)
            .Token;

        if (inputFile == null)
        {
            progress.Report(100, "No input file specified");
            return;
        }

        var inputFilename = Path.GetFileName(inputFile);
        progress.Report(10, $"Starting NSwag code generation for: {inputFilename}");

        using var dependencyContext = new DependencyContext("NSwag");
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            progress.Report(20, "Loading OpenAPI specification...");
            var documentFactory = new OpenApiDocumentFactory();
            var document = await documentFactory.GetDocumentAsync(inputFile);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(30, "Loading NSwag configuration...");
            var nswagOptions = await settingsProvider.GetNSwagOptionsAsync(cancellationToken);
            
            var generatorSettingsFactory = new NSwagCodeGeneratorSettingsFactory(
                defaultNamespace,
                nswagOptions);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(40, "Initializing NSwag code generator...");
            var generator = new CSharpClientGenerator(
                document,
                await GetGeneratorSettingsAsync(
                    inputFile,
                    document,
                    generatorSettingsFactory,
                    cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(60, "Generating C# client code...");
            var csharpCode = generator.GenerateFile();
            csharpCode = GeneratedCode.PrefixAutogeneratedCodeHeader(csharpCode, "NSwag", "v14.6.0");

            if (csharpCode is not null)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var outputFile = OutputFile.GetOutputFilename(inputFile);
                var outputFilename = Path.GetFileName(outputFile);
                progress.Report(90, $"Writing generated code to: {outputFilename}");

                await File.WriteAllTextAsync(
                    outputFile,
                    csharpCode,
                    cancellationToken);
                
                progress.Report(100, "NSwag code generation completed successfully");
            }
        }
        catch (OperationCanceledException)
        {
            progress.Report(100, "NSwag code generation was canceled.");
        }
        catch (Exception e)
        {
            Logger.Instance.TrackError(e);
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating NSwag client code: {0}",
                e.Message);

            progress.Report(100, "Error generating NSwag client code: " + e.Message);
        }
        finally
        {
            dependencyContext.Succeeded();
        }
    }

    private static async Task<CSharpClientGeneratorSettings> GetGeneratorSettingsAsync(
        string inputFile,
        NSwag.OpenApiDocument document,
        NSwagCodeGeneratorSettingsFactory generatorSettingsFactory,
        CancellationToken cancellationToken)
    {
        CSharpClientGeneratorSettings settings;

        if (inputFile.EndsWith(".nswag", StringComparison.OrdinalIgnoreCase))
        {
            var nswagFileContent = await File.ReadAllTextAsync(inputFile, cancellationToken);
            var nswagFile = JsonSerializer.Deserialize<NSwagFile>(nswagFileContent, JsonSerializerOptions.Web)!;
            settings = nswagFile.CodeGenerators.OpenApiToCSharpClient
                ?? nswagFile.CodeGenerators.SwaggerToCSharpClient
                ?? throw new InvalidOperationException();
        }
        else
        {
            settings = generatorSettingsFactory.GetGeneratorSettings(document);
        }

        return settings;
    }

    class NSwagFile
    {
        public required CodeGenerators CodeGenerators { get; set; }
    }

    class CodeGenerators
    {
        public CSharpClientGeneratorSettings? SwaggerToCSharpClient { get; set; }
        public CSharpClientGeneratorSettings? OpenApiToCSharpClient { get; set; }
    }
}
