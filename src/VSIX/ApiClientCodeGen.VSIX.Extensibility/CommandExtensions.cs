using ApiClientCodeGen.VSIX.Extensibility.Dialogs;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace ApiClientCodeGen.VSIX.Extensibility;

#pragma warning disable VSEXTPREVIEW_OUTPUTWINDOW // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

internal static class CommandExtensions
{
    private static readonly System.Net.Http.HttpClient s_httpClient = new System.Net.Http.HttpClient { Timeout = System.TimeSpan.FromSeconds(30) };

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
            var url = dialog.Url.Trim();

            // Validate URL first
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                await command.WriteToOutputWindowAsync($"Invalid URL: {url}", cancellationToken);
                return null!;
            }

            var path = await context.GetSelectedPathAsync(cancellationToken);

            // Safely determine target directory and ensure it exists
            var directory = Path.GetDirectoryName(path.AbsolutePath);
            if (string.IsNullOrEmpty(directory))
            {
                directory = path.AbsolutePath ?? string.Empty;
            }

            try
            {
                if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
                    System.IO.Directory.CreateDirectory(directory);
            }
            catch (Exception dex)
            {
                await command.WriteToOutputWindowAsync($"Failed to create directory '{directory}': {dex.Message}", cancellationToken);
                return null!;
            }

            // Determine file name from URL path; fallback to a default name
            var fileName = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped)
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault() ?? "openapi.json";

            inputFile = System.IO.Path.Combine(directory, fileName);

            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                // Apply a reasonable per-request timeout in addition to the provided token
                cts.CancelAfter(System.TimeSpan.FromSeconds(30));

                var response = await s_httpClient.GetAsync(uri, cts.Token);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync(cts.Token);

                await System.IO.File.WriteAllTextAsync(inputFile, content, cancellationToken);
            }
            catch (UriFormatException uex)
            {
                await command.WriteToOutputWindowAsync($"The URL is malformed: {uex.Message}", cancellationToken);
                return null!;
            }
            catch (System.Net.Http.HttpRequestException hex)
            {
                await command.WriteToOutputWindowAsync($"Network error while downloading '{url}': {hex.Message}", cancellationToken);
                return null!;
            }
            catch (TaskCanceledException)
            {
                await command.WriteToOutputWindowAsync($"Request timed out while downloading '{url}'", cancellationToken);
                return null!;
            }
            catch (Exception ex)
            {
                await command.WriteToOutputWindowAsync($"Failed to download or save file: {ex.Message}", cancellationToken);
                return null!;
            }
        }

        return inputFile;
    }

    public static string GetExtensionAssemblyVersion(this Command command)
        => typeof(CommandExtensions).Assembly.GetName().Version!.ToString();
}
