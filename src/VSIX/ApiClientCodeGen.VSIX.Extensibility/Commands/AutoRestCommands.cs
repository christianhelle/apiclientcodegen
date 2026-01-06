using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateAutoRestCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateAutoRestBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration
        => new("%AutoRestCommand.DisplayName%")
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
public class GenerateAutoRestNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider)
    : GenerateAutoRestBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration
        => new("%AutoRestCommand.DisplayName%")
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

public abstract class GenerateAutoRestBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;

    public async Task GenerateAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        Logger.Instance.WriteLine($"Starting AutoRest code generation for: {inputFile}");
        
        if (inputFile == null)
        {
            Logger.Instance.WriteLine("No input file specified");
            return;
        }

        try
        {
            Logger.Instance.WriteLine("Loading AutoRest configuration...");
            var options = await settingsProvider.GetAutoRestOptionsAsync(cancellationToken);
            
            Logger.Instance.WriteLine("Initializing AutoRest code generator...");
            var generator = new AutoRestCSharpCodeGenerator(
            inputFile,
            defaultNamespace,
            options: options,
            processLauncher: new ProcessLauncher(),
            documentFactory: new OpenApiDocumentFactory(),
            dependencyInstaller: new DependencyInstaller(
                new NpmInstaller(new ProcessLauncher()),
                new FileDownloader(new WebDownloader()),
                new ProcessLauncher()),
            new AutoRestArgumentProvider(options));

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
                Logger.Instance.WriteLine("AutoRest code generation completed successfully");
            }
        }
        catch (Exception e)
        {
            Logger.Instance.WriteLine($"AutoRest code generation failed: {e.Message}");
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating AutoRest client code: {0}",
                e.Message);

            Logger.Instance.WriteLine("Error generating AutoRest client code: " + e.Message);
        }
    }
}
