using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;

namespace ApiClientCodeGen.VSIX.Extensibility;

#pragma warning disable VSEXTPREVIEW_OUTPUTWINDOW // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

internal static class CommandExtensions
{
    private static readonly HttpClient s_httpClient = new() { Timeout = TimeSpan.FromSeconds(30) };

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

    public static async Task<string?> AddNewOpenApiFileAsync(
        this Command command,
        IClientContext context,
        CancellationToken cancellationToken)
    {
        var inputUrl = await command.Extensibility.Shell().ShowPromptAsync(
            $"Enter URL to OpenAPI Specifications",
            new InputPromptOptions
            {
                DefaultText = "Example: https://petstore3.swagger.io/api/v3/openapi.json",
                Icon = ImageMoniker.KnownValues.URLInputBox,
                Title = "REST API Client Code Generator",
            },
            cancellationToken);

        if (string.IsNullOrWhiteSpace(inputUrl))
        {
            return null;
        }

        var url = inputUrl.Trim();
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            await command.WriteToOutputWindowAsync($"Invalid URL: {url}", cancellationToken);
            return null!;
        }

        var path = await context.GetSelectedPathAsync(cancellationToken);
        var directory = Path.GetDirectoryName(path.AbsolutePath);
        if (string.IsNullOrEmpty(directory))
        {
            directory = path.AbsolutePath ?? string.Empty;
        }

        try
        {
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        catch (Exception dex)
        {
            await command.WriteToOutputWindowAsync($"Failed to create directory '{directory}': {dex.Message}", cancellationToken);
            return null!;
        }

        var fileName = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped)
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .LastOrDefault() ?? "openapi.json";

        var inputFile = Path.Combine(directory, fileName);
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(30));

            var response = await s_httpClient.GetAsync(uri, cts.Token);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cts.Token);

            await File.WriteAllTextAsync(inputFile, content, cancellationToken);
        }
        catch (UriFormatException uex)
        {
            await command.WriteToOutputWindowAsync($"The URL is malformed: {uex.Message}", cancellationToken);
            return null!;
        }
        catch (HttpRequestException hex)
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

        return inputFile;
    }

    public static string GetExtensionAssemblyVersion(this Command command)
        => typeof(CommandExtensions).Assembly.GetName().Version!.ToString();
}
