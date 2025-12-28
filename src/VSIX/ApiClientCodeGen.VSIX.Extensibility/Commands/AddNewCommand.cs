using ApiClientCodeGen.VSIX.Extensibility.Dialogs;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility.Commands;

[VisualStudioContribution]
public class AddNewCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("%AddNewCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
    };

    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        var dialog = new AddNewInputDialog();
#pragma warning restore CA2000 // Dispose objects before losing scope

        await Extensibility.Shell().ShowDialogAsync(dialog, cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(dialog.Url))
        {
            // TODO: Process the URL and generate the REST API client
            // For now, just log that we received the URL
        }
    }
}
