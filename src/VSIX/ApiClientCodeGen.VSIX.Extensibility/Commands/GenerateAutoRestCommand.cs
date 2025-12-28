using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.AutoRest;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.AutoRest;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateAutoRestCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%AutoRestCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        var inputFile = await context.GetInputFileAsync(cancellationToken);
        var defaultNamespace = await context.GetDefaultNamespaceAsync(cancellationToken);
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
}