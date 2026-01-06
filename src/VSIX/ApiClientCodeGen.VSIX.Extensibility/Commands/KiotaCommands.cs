using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;

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
        CancellationToken cancellationToken) =>
        await GenerateAsync(
            await context.GetInputFileAsync(cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
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
        CancellationToken cancellationToken) =>
        await GenerateAsync(
            await this.AddNewOpenApiFileAsync(context, cancellationToken),
            await context.GetDefaultNamespaceAsync(cancellationToken),
            cancellationToken);
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
        Logger.Instance.WriteLine($"Starting Kiota code generation for: {inputFile}");
        
        if (inputFile == null)
        {
            Logger.Instance.WriteLine("No input file specified");
            return;
        }

        try
        {
            Logger.Instance.WriteLine("Loading Kiota configuration...");
            var kiotaOptions = await settingsProvider.GetKiotaOptionsAsync(cancellationToken);
            
            Logger.Instance.WriteLine("Initializing Kiota code generator...");
            var generator = new KiotaCodeGenerator(
                inputFile,
                defaultNamespace,
                options: kiotaOptions,
                processLauncher: new ProcessLauncher(),
                dependencyInstaller: new CustomDependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()));

            Logger.Instance.WriteLine("Generating C# client code...");
            var csharpCode = await Task.Run(() => generator.GenerateCode(null));
            if (csharpCode is not null)
            {
                var outputFile = OutputFile.GetOutputFilename(inputFile);
                Logger.Instance.WriteLine($"Writing generated code to: {outputFile}");
                await File.WriteAllTextAsync(
                    outputFile,
                    csharpCode,
                    cancellationToken);
                Logger.Instance.WriteLine("Kiota code generation completed successfully");
            }
        }
        catch (Exception e)
        {
            Logger.Instance.WriteLine($"Kiota code generation failed: {e.Message}");
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating Kiota client code: {0}",
                e.Message);

            await this.WriteToOutputWindowAsync(
                "Error generating Kiota client code: " + e.Message,
                cancellationToken);
        }
    }
}
