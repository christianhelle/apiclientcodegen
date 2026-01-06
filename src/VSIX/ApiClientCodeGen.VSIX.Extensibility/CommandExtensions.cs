using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;
using Rapicgen.Core.Logging;

namespace ApiClientCodeGen.VSIX.Extensibility;

#pragma warning disable VSEXTPREVIEW_OUTPUTWINDOW // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

internal static class CommandExtensions
{
    private static readonly HttpClient s_httpClient = new() { Timeout = TimeSpan.FromSeconds(30) };

    public static async Task<string?> AddNewOpenApiFileAsync(
        this Command command,
        IClientContext context,
        CancellationToken cancellationToken)
    {
        var inputUrl = await command.Extensibility.Shell().ShowPromptAsync(
            "Enter URL to OpenAPI Specifications",
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
            Logger.Instance.WriteLine($"Invalid URL: {url}");
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
            Logger.Instance.WriteLine($"Failed to create directory '{directory}': {dex.Message}");
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
            Logger.Instance.WriteLine($"The URL is malformed: {uex.Message}");
            return null!;
        }
        catch (HttpRequestException hex)
        {
            Logger.Instance.WriteLine($"Network error while downloading '{url}': {hex.Message}");
            return null!;
        }
        catch (TaskCanceledException)
        {
            Logger.Instance.WriteLine($"Request timed out while downloading '{url}'");
            return null!;
        }
        catch (Exception ex)
        {
            Logger.Instance.WriteLine($"Failed to download or save file: {ex.Message}");
            return null!;
        }

        return inputFile;
    }

    public static string GetExtensionAssemblyVersion(this Command command)
        => typeof(CommandExtensions).Assembly.GetName().Version!.ToString();
}
