using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility;

#pragma warning disable VSEXTPREVIEW_OUTPUTWINDOW // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

internal static class CommandExtensions
{
    public static async Task WriteToOutputWindowAsync(
        this Command command,
        string message,
        CancellationToken cancellationToken)
    {
        var outputChannel = await command.Extensibility
            .Views()
            .Output
            .CreateOutputChannelAsync(
                "REST API Client Code Generator",
                cancellationToken);

        if (outputChannel != null)
            await outputChannel.WriteLineAsync(message);
    }
}
