using Microsoft.VisualStudio.Extensibility;

namespace ApiClientCodeGen.VSIX.Extensibility;

public static class ClientContextExtensions
{
    public static async Task<string> GetInputFileAsync(
        this IClientContext context,
        CancellationToken cancellationToken)
    {
        var item = await context.GetSelectedPathAsync(cancellationToken);
        var inputFile = item.AbsolutePath;
        return inputFile;
    }

    public static async Task<string> GetDefaultNamespaceAsync(
        this IClientContext context,
        CancellationToken cancellationToken)
    {
        var project = (await context.GetActiveProjectAsync(cancellationToken))!;
        try
        {
            return project.DefaultNamespace!;
        }
        catch
        {
            try
            {
                return project.Name ?? "GeneratedCode";
            }
            catch
            {
                var fileInfo = new FileInfo(project.Path!);
                return fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
            }
        }
    }
}