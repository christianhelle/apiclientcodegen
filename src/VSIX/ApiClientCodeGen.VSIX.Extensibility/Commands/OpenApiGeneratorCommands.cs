using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateOpenApiCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateOpenApiBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%OpenApiGeneratorCommand.DisplayName%")
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
public class GenerateOpenApiNewCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateOpenApiBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%OpenApiGeneratorCommand.DisplayName%")
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

public abstract class GenerateOpenApiBaseCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : Command
{
    private readonly ExtensionSettingsProvider settingsProvider = settingsProvider;

    public async Task GenerateAsync(
        string? inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        Logger.Instance.WriteLine($"Starting OpenAPI Generator code generation for: {inputFile}");
        
        if (inputFile == null)
        {
            Logger.Instance.WriteLine("No input file specified");
            return;
        }

        try
        {
            Logger.Instance.WriteLine("Loading OpenAPI Generator configuration...");
            var generalOptions = await settingsProvider.GetGeneralOptionsAsync(cancellationToken);
            var openApiOptions = await settingsProvider.GetOpenApiGeneratorOptionsAsync(cancellationToken);
            
            Logger.Instance.WriteLine("Initializing OpenAPI Generator code generator...");
            var generator = new OpenApiCSharpCodeGenerator(
                inputFile,
                defaultNamespace,
                options: generalOptions,
                openApiGeneratorOptions: openApiOptions,
                processLauncher: new ProcessLauncher(),
                dependencyInstaller: new DependencyInstaller(
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
                Logger.Instance.WriteLine("OpenAPI Generator code generation completed successfully");
            }
        }
        catch (Exception e)
        {
            Logger.Instance.WriteLine($"OpenAPI Generator code generation failed: {e.Message}");
            traceSource.TraceEvent(
                TraceEventType.Error,
                0,
                "Error generating OpenAPI Generator client code: {0}",
                e.Message);

            await this.WriteToOutputWindowAsync(
                "Error generating OpenAPI Generator client code: " + e.Message,
                cancellationToken);
        }
    }
}
