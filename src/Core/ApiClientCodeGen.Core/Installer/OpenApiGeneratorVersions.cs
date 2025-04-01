using System.Linq;

namespace Rapicgen.Core.Installer;

public static class OpenApiGeneratorVersions
{
    private const string DownloadUrlPrefix =
        "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli";

    private static readonly OpenApiGeneratorVersion[] Versions =
    [
        new(
            "7.12.0",
            $"{DownloadUrlPrefix}/7.12.0/openapi-generator-cli-7.12.0.jar",
            "2c7d5141384d2caaa2d11d370ed172525855c157",
            "40770e424b885e5878471d4a19ba951b"
        ),
        new(
            "7.11.0",
            $"{DownloadUrlPrefix}/7.11.0/openapi-generator-cli-7.11.0.jar",
            "9261333ecbfd8738956b09be0beff611b47fbaff",
            "4931bca886d30823b3ba2c7f36607925"
        )
    ];


    public static OpenApiGeneratorVersion GetVersion(string version)
    {
        return Versions.FirstOrDefault(v => v.Version == version) ?? GetLatestVersion();
    }

    public static OpenApiGeneratorVersion GetLatestVersion()
    {
        return Versions[0];
    }
}

public record OpenApiGeneratorVersion(
    string Version,
    string DownloadUrl,
    string SHA1,
    string MD5);