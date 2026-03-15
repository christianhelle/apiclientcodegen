using System;
using System.IO;
using System.Linq;

namespace Rapicgen.Core.Generators;

public static class FileValidator
{
    private static readonly string[] SupportedExtensions =
        { ".json", ".yaml", ".yml", ".nswag", ".refitter" };

    public static bool IsSupportedOpenApiFile(string filePath)
    {
        var extension = Path.GetExtension(filePath)?.ToLowerInvariant();
        return !string.IsNullOrEmpty(extension) && SupportedExtensions.Contains(extension);
    }

    public static bool IsNonOpenApiContent(string? content, string filePath)
    {
        if (string.IsNullOrEmpty(content))
            return false;

        var extension = Path.GetExtension(filePath)?.ToLowerInvariant();
        if (extension != ".yaml" && extension != ".yml")
            return false;

        var trimmed = content.TrimStart();
        return trimmed.StartsWith("version:", StringComparison.OrdinalIgnoreCase) ||
               trimmed.StartsWith("services:", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Contains("docker", StringComparison.OrdinalIgnoreCase);
    }
}
