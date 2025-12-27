using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.External;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateOpenApiCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%OpenApiGeneratorCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        var inputFile = await context.GetInputFileAsync(cancellationToken);
        var defaultNamespace = await context.GetDefaultNamespaceAsync(cancellationToken);
        var generator = new OpenApiCSharpCodeGenerator(
            inputFile,
            defaultNamespace,
            options: new DefaultGeneralOptions(),
            openApiGeneratorOptions: new DefaultOpenApiGeneratorOptions(),
            processLauncher: new ProcessLauncher(),
            dependencyInstaller: new DependencyInstaller(
                new NpmInstaller(new ProcessLauncher()),
                new FileDownloader(new WebDownloader()),
                new ProcessLauncher()));

        var csharpCode = await Task.Run(() => generator.GenerateCode(null));
        if (csharpCode is not null)
        {
            await File.WriteAllTextAsync(
                inputFile.Replace(new FileInfo(inputFile).Extension, ".cs"),
                csharpCode,
                cancellationToken);
        }
    }
}