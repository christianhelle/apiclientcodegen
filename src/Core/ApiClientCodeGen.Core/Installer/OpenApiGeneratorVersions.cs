using System.ComponentModel;
using System.Linq;
using Rapicgen.Core.Options.OpenApiGenerator;

namespace Rapicgen.Core.Installer;

public static class OpenApiGeneratorVersions
{
    private const string DownloadUrlPrefix =
        "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli";

    private static readonly OpenApiGeneratorVersion[] Versions =
    [
        new(
            "7.14.0",
            $"{DownloadUrlPrefix}/7.14.0/openapi-generator-cli-7.14.0.jar",
            "d4fb1233b57b165728ecdcacf3fc0146b816e159",
            "a1a91708715efb01eaebaf7ab68c48c3"
        ),
        new(
            "7.13.0",
            $"{DownloadUrlPrefix}/7.13.0/openapi-generator-cli-7.13.0.jar",
            "a72c42381d7bba007d7fb17d57a6ed7bff93ce64",
            "1a90c7cecebe2e3956b5bc5d914b643a"
        ),
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
        ),
        new(
            "7.10.0",
            $"{DownloadUrlPrefix}/7.10.0/openapi-generator-cli-7.10.0.jar",
            "e9cc6e4cd2dcb77a86b647a1ed4352fd08f187ce",
            "cebbfd626e0b2e32509a4a34811c4dc6"
        ),
        new(
            "7.9.0",
            $"{DownloadUrlPrefix}/7.9.0/openapi-generator-cli-7.9.0.jar",
            "369eafe4a877ad496504c3fd0eebfd3586666d16",
            "27b7f5e4f233a3981902332c67edb722"
        ),
        new(
            "7.8.0",
            $"{DownloadUrlPrefix}/7.8.0/openapi-generator-cli-7.8.0.jar",
            "450160b7a1978b64e784cb730a868019db0aa59c",
            "33ec880b9f4cf598136d5eb445d3e354"
        ),
        new(
            "7.7.0",
            $"{DownloadUrlPrefix}/7.7.0/openapi-generator-cli-7.7.0.jar",
            "41f26774ba0dbed788613a9c3062ddc6b9fcfdad",
            "6fd37c8e4a83ef8de0bc741c0927c599"
        )
    ];


    public static OpenApiGeneratorVersion GetVersion(string version)
    {
        return Versions.FirstOrDefault(v => v.Version == version) ?? GetLatestVersion();
    }

    public static OpenApiGeneratorVersion GetVersion(OpenApiSupportedVersion version)
    {
        // If it's the Default value (0), return the latest version directly
        if (version == OpenApiSupportedVersion.Latest)
            return GetLatestVersion();
            
        var versionString = version.GetType()
            .GetField(version.ToString())
            ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() is DescriptionAttribute descriptionAttribute
              ? descriptionAttribute.Description
              : version.ToString();

        return GetVersion(versionString);
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
