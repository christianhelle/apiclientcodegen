using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Swagger;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;

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
        if (inputFile == null)
        {
            return;
        }

        try
        {
            var generalOptions = await settingsProvider.GetGeneralOptionsAsync(cancellationToken);
            var generator = new SwaggerCSharpCodeGenerator(
                inputFile,
                defaultNamespace,
                options: generalOptions,
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
                "Error generating Swagger Codegen client code: {0}",
                e.Message);

            await this.WriteToOutputWindowAsync(
                "Error generating Swagger Codegen client code: " + e.Message,
                cancellationToken);
        }
    }
}
