using ApiClientCodeGen.VSIX.Extensibility.Dialogs;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class AboutCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%AboutCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        var dialog = new AboutDialog(
            displayName: "REST API Client Code Generator",
            description: "Generate REST API client code from OpenAPI/Swagger specifications",
            version: "1.0.0",
            publisher: "Christian Resma Helle",
            extensionId: "Rapicgen");
#pragma warning restore CA2000 // Dispose objects before losing scope

        await Extensibility.Shell().ShowDialogAsync(dialog, cancellationToken);
    }
}
