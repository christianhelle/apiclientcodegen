using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Kiota;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.Kiota;
using System.Diagnostics;
using ApiClientCodeGen.VSIX.Extensibility.Settings;
using ApiClientCodeGen.VSIX.Extensibility.Commands.Placements;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateKiotaCommand(TraceSource traceSource, ExtensionSettingsProvider settingsProvider) : GenerateKiotaBaseCommand(traceSource, settingsProvider)
{
    public override CommandConfiguration CommandConfiguration => new("%KiotaCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        Placements = [KnownPlacements.ItemNode],
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
        if (inputFile == null)
        {
            return;
        }

        try
        {
            var kiotaOptions = await settingsProvider.GetKiotaOptionsAsync(cancellationToken);
            var generator = new KiotaCodeGenerator(
                inputFile,
                defaultNamespace,
                options: kiotaOptions,
                processLauncher: new ProcessLauncher(),
                dependencyInstaller: new CustomDependencyInstaller(
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
                "Error generating Kiota client code: {0}",
                e.Message);

            await this.WriteToOutputWindowAsync(
                "Error generating Kiota client code: " + e.Message,
                cancellationToken);
        }
    }
}
