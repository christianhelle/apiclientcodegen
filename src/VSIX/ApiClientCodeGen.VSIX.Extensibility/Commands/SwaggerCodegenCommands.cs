using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.RpcContracts.ProgressReporting;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using ProgressReporter = Microsoft.VisualStudio.Extensibility.Shell.ProgressReporter;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateSwaggerCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateSwaggerBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%SwaggerCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
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
public class GenerateSwaggerNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateSwaggerBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%SwaggerCommand.DisplayName%")
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

public abstract class GenerateSwaggerBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;

    public async Task GenerateAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Generate Swagger Codegen output");

        using var progress = await Extensibility
            .Shell()
            .StartProgressReportingAsync(
                "Generating code with Swagger Codegen",
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
        progress.Report(10, $"Starting Swagger Codegen code generation for: {inputFilename}");

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            progress.Report(20, "Loading Swagger Codegen configuration...");
            var generalOptions = await settingsProvider.GetGeneralOptionsAsync(cancellationToken);
            
            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(30, "Initializing Swagger Codegen code generator...");
            var generator = new SwaggerCSharpCodeGenerator(
                inputFile,
                defaultNamespace,
                options: generalOptions,
                processLauncher: new ProcessLauncher(),
                dependencyInstaller: new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()));

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(50, "Generating C# client code...");
            var csharpCode = await Task.Run(() => generator.GenerateCode(null), cancellationToken);
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
                
                progress.Report(100, "Swagger Codegen code generation completed successfully");
            }
        }
        catch (OperationCanceledException)
        {
            progress.Report(100, "Swagger Codegen code generation was canceled.");
        }
        catch (Exception e)
        {
            Logger.Instance.TrackError(e);
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating Swagger Codegen client code: {0}",
                e.Message);

            progress.Report(100, "Error generating Swagger Codegen client code: " + e.Message);
        }
    }
}
