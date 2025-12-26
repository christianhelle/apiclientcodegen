using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.Refitter;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;
using System.Diagnostics;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class GenerateRefitterCommand(TraceSource traceSource) : Command
{
    public override CommandConfiguration CommandConfiguration => new("%RefitterCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        VisibleWhen = ActivationConstraint.ClientContext(ClientContextKey.Shell.ActiveSelectionFileName, ".(json|ya?ml)")
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        Logger.Instance.TrackFeatureUsage("Generate Refitter output");

        var item = await context.GetSelectedPathAsync(cancellationToken);
        var inputFile = item.AbsolutePath;

        var csharpCode = await Task.Run(() => GenerateCode(inputFile));
        if (csharpCode is not null)
        {
            await File.WriteAllTextAsync(
                inputFile.Replace(new FileInfo(inputFile).Extension, "cs"),
                csharpCode,
                cancellationToken);
        }
    }

    private string? GenerateCode(string inputFile)
    {
        try
        {
            var generator = new RefitterCodeGenerator(
                inputFile,
                default!,
                new ProcessLauncher(),
                new DependencyInstaller(
                    new NpmInstaller(new ProcessLauncher()),
                    new FileDownloader(new WebDownloader()),
                    new ProcessLauncher()),
                new DefaultRefitterOptions());

            return generator.GenerateCode(null);
        }
        catch (Exception e)
        {
            traceSource.TraceEvent(TraceEventType.Error, 0, $"Error generating Refitter code: {e}");
            return null;
        }
    }
}