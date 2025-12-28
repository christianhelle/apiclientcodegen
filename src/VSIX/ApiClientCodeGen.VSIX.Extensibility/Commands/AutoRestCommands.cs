using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.AutoRest;
using System.Diagnostics;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateAutoRestCommand(TraceSource traceSource)
    : GenerateAutoRestBaseCommand(traceSource)
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
public class GenerateAutoRestNewCommand(TraceSource traceSource)
    : GenerateAutoRestBaseCommand(traceSource)
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

public abstract class GenerateAutoRestBaseCommand(TraceSource traceSource) : Command
{
    public async Task GenerateAsync(
        string inputFile, 
        string defaultNamespace, 
        CancellationToken cancellationToken)
    {
        try
        {
            var generator = new AutoRestCSharpCodeGenerator(
            inputFile,
            defaultNamespace,
            options: new DefaultAutoRestOptions(),
            processLauncher: new ProcessLauncher(),
            documentFactory: new OpenApiDocumentFactory(),
            dependencyInstaller: new DependencyInstaller(
                new NpmInstaller(new ProcessLauncher()),
                new FileDownloader(new WebDownloader()),
                new ProcessLauncher()),
            new AutoRestArgumentProvider(new DefaultAutoRestOptions()));

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
                "Error generating AutoRest client code: {0}",
                e.Message);

            await this.WriteToOutputWindowAsync(
                "Error generating AutoRest client code: " + e.Message,
                cancellationToken);
        }
    }
}