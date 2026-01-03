using ApiClientCodeGen.VSIX.Extensibility.Dialogs;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Rapicgen.Core.Logging;

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
        var dialog = new AboutDialog(
            extensibility: Extensibility,
            displayName: "REST API Client Code Generator (PREVIEW)",
            description: "Generate REST API client code from OpenAPI/Swagger specifications",
            version: this.GetExtensionAssemblyVersion(),
            publisher: "Christian Resma Helle",
            extensionId: "f7530eb1-1ce9-46ac-8fab-165b68cf3d61",
            supportKey: SupportInformation.GetSupportKey());

        await Extensibility.Shell().ShowDialogAsync(dialog, cancellationToken);
    }
}
