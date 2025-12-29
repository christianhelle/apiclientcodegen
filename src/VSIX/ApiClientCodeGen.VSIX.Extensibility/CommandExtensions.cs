using ApiClientCodeGen.VSIX.Extensibility.Dialogs;
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

    public static async Task<string> AddNewOpenApiFileAsync(
        this Command command,
        IClientContext context,
        CancellationToken cancellationToken)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        var dialog = new AddNewInputDialog();
        await command.Extensibility.Shell().ShowDialogAsync(dialog, cancellationToken);
#pragma warning restore CA2000 // Dispose objects before losing scope

        string inputFile = null!;
        if (!string.IsNullOrWhiteSpace(dialog.Url))
        {
            var uri = new Uri(dialog.Url);
            var path = await context.GetSelectedPathAsync(cancellationToken);
            inputFile = Path.Combine(
                Path.GetDirectoryName(path.AbsolutePath)!,
                uri.GetComponents(UriComponents.Path, UriFormat.Unescaped)
                    .Split('/', StringSplitOptions.RemoveEmptyEntries)
                    .Last());

            using var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(uri, cancellationToken);
            await File.WriteAllTextAsync(inputFile, content, cancellationToken);
        }

        return inputFile;
    }

    public static string GetExtensionAssemblyVersion(this Command command)
        => typeof(CommandExtensions).Assembly.GetName().Version!.ToString();
}
