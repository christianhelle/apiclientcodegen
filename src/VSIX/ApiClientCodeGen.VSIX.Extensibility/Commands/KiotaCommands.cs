using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.RpcContracts.ProgressReporting;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using ProgressReporter = Microsoft.VisualStudio.Extensibility.Shell.ProgressReporter;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateKiotaCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateKiotaBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%KiotaCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Kiota");
        await GenerateAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
    }
}

[VisualStudioContribution]
public class GenerateKiotaNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateKiotaBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%KiotaCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
    };

    public override async Task ExecuteCommandAsync(
        IClientContext context,
        CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("New REST API Client (Kiota)");
        await GenerateAsync(
            await this.AddNewOpenApiFileAsync(context, cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
    }
}

[VisualStudioContribution]
public abstract class GenerateKiotaBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;

    public async Task GenerateAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        using var progress = await Extensibility
            .Shell()
            .StartProgressReportingAsync(
                "Generating code with Kiota",
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
        progress.Report(10, $"Starting Kiota code generation for: {inputFilename}");

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            progress.Report(20, "Loading Kiota configuration...");
            var kiotaOptions = await settingsProvider.GetKiotaOptionsAsync(cancellationToken);
            
            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(30, "Initializing Kiota code generator...");
            var generator = new KiotaCodeGenerator(
                inputFile,
                defaultNamespace,
                options: kiotaOptions,
                processLauncher: new ProcessLauncher(),
                dependencyInstaller: new CustomDependencyInstaller(
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
                
                progress.Report(100, "Kiota code generation completed successfully");
            }
        }
        catch (OperationCanceledException)
        {
            progress.Report(100, "Kiota code generation was canceled.");
        }
        catch (Exception e)
        {
            Logger.Instance.TrackError(e);
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating Kiota client code: {0}",
                e.Message);

            progress.Report(100, "Error generating Kiota client code: " + e.Message);
        }
    }
}
