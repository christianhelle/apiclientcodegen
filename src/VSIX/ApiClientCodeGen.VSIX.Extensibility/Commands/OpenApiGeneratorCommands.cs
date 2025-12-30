using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;
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
        string inputFile,
        string defaultNamespace,
        CancellationToken cancellationToken)
    {
        if (inputFile == null)
        {
            return;
        }

        try
        {
            var generalOptions = await settingsProvider.GetGeneralOptionsAsync(cancellationToken);
            var openApiOptions = await settingsProvider.GetOpenApiGeneratorOptionsAsync(cancellationToken);
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

            var csharpCode = await Task.Run(() => generator.GenerateCode(null));
            if (csharpCode is not null)
            {
                await File.WriteAllTextAsync(
                    OutputFile.GetOutputFilename(inputFile),
                    csharpCode,
                    cancellationToken);
            }
        }
        catch (Exception e)
        {
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
