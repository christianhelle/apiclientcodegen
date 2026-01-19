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
            "7.19.0",
            $"{DownloadUrlPrefix}/7.19.0/openapi-generator-cli-7.19.0.jar",
            "03beec991655b1532c3663f0bbbb9401dd6ff8b2",
            "b5b8eb46aeeba16c7a1445d4ee3a88d6"
        ),
        new(
            "7.18.0",
            $"{DownloadUrlPrefix}/7.18.0/openapi-generator-cli-7.18.0.jar",
            "8bd615a50b15ebf5be30e612af112526a6e81ac4",
            "ac35c9e3e4e43bf68c93f6341f6d4f97"
        ),
        new(
            "7.17.0",
            $"{DownloadUrlPrefix}/7.17.0/openapi-generator-cli-7.17.0.jar",
            "7ddf2ce9a8b745c8c8c01046435b05362d0bee2d",
            "ffae757c04ce558d311964b9ec2321d0"
        ),
        new(
            "7.16.0",
            $"{DownloadUrlPrefix}/7.16.0/openapi-generator-cli-7.16.0.jar",
            "2ca15745dbb9261a43392e7d9a530e5b7473e929",
            "eff25f7e653b5986786eee6fc4722e47"
        ),
        new(
            "7.15.0",
            $"{DownloadUrlPrefix}/7.15.0/openapi-generator-cli-7.15.0.jar",
            "bb58e257f724fb46b7f2b309a9fa98e63fd7199f",
            "b617d5137b022a9b3304b1a26d0b3110"
        ),
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
