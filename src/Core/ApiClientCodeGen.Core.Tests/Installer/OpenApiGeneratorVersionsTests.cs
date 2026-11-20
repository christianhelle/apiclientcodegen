using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.OpenApiGenerator;
using Xunit;

namespace Rapicgen.Core.Tests.Installer;

public class OpenApiGeneratorVersionsTests
{
    [Fact]
    public void GetVersion_V7270_ReturnsExpectedDownloadInfo()
    {
        // Act
        var version = OpenApiGeneratorVersions.GetVersion(OpenApiSupportedVersion.V7270);

        // Assert
        Assert.Equal("7.27.0", version.Version);
        Assert.Equal(
            "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.27.0/openapi-generator-cli-7.27.0.jar",
            version.DownloadUrl);
        Assert.Equal("1c5eb64ca278d5b018342b15594353a6569fbc9a", version.SHA1);
        Assert.Equal("0cc2b0c253954bbf3e357f66a6909c39", version.MD5);
    }

    [Fact]
    public void GetVersion_V7260_ReturnsExpectedDownloadInfo()
    {
        // Act
        var version = OpenApiGeneratorVersions.GetVersion(OpenApiSupportedVersion.V7260);

        // Assert
        Assert.Equal("7.26.0", version.Version);
        Assert.Equal(
            "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.26.0/openapi-generator-cli-7.26.0.jar",
            version.DownloadUrl);
        Assert.Equal("b96b148fcf482808037f68fbad99cb4d7e65d40b", version.SHA1);
        Assert.Equal("24654520b8740d66f2794535545b22fe", version.MD5);
    }

    [Fact]
    public void GetLatestVersion_MatchesLatestSupportedVersion()
    {
        // Act
        var latest = OpenApiGeneratorVersions.GetLatestVersion();
        var expected = OpenApiGeneratorVersions.GetVersion(OpenApiSupportedVersionExtensions.Latest);

        // Assert
        Assert.Equal(expected, latest);
        Assert.Equal("7.27.0", latest.Version);
    }

    [Fact]
    public void Resources_MatchLatestVersion()
    {
        // Act
        var latest = OpenApiGeneratorVersions.GetLatestVersion();

        // Assert
        Assert.Equal(latest.DownloadUrl, Resource.OpenApiGenerator_DownloadUrl);
        Assert.Equal(latest.SHA1, Resource.OpenApiGenerator_SHA1);
        Assert.Equal(latest.MD5, Resource.OpenApiGenerator_MD5);
    }
}
