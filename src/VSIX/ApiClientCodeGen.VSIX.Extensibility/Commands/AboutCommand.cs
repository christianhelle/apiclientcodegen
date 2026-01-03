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
#pragma warning disable CA2000 // Dispose objects before losing scope
        var dialog = new AboutDialog(
            extensibility: Extensibility,
            displayName: "REST API Client Code Generator (PREVIEW)",
            description: "Generate REST API client code from OpenAPI/Swagger specifications",
            version: this.GetExtensionAssemblyVersion(),
            publisher: "Christian Resma Helle",
            extensionId: "f7530eb1-1ce9-46ac-8fab-165b68cf3d61",
            supportKey: SupportInformation.GetSupportKey(),
            analyticsDescription: "This extension collects errors and tracks feature usages to Exceptionless and Azure Application Insights by default and you may opt-out of telemetry collection at any time.\n\nUser tracking is done anonymously using a Support Key (shown in the About dialog) and a generated anonymous identity based on a secure hash algorithm of username@host.\n\nOnly the following user actions are tracked:\n- Custom Tool executions\n- Clicking on \"New REST API Client\" from the Solution Explorer\n- Clicking on \"Generate NSwag Studio Output\" from the Solution Explorer\n\nThe purpose of all this is to know what features I should put the most effort in and for support scenarios where users are to specify their support key when reporting issues.");
#pragma warning restore CA2000 // Dispose objects before losing scope

        await Extensibility.Shell().ShowDialogAsync(dialog, cancellationToken);
    }
}
