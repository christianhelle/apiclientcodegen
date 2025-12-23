using Rapicgen.Core.Options.OpenApiGenerator;
using Xunit;

namespace Rapicgen.Core.Tests.Options.OpenApiGenerator;

public class OpenApiVersionExtensionsTests
{
    [Theory]
    [InlineData(OpenApiSupportedVersion.V7140, OpenApiSupportedVersion.V7140, true)]   // Equal versions
    [InlineData(OpenApiSupportedVersion.V7140, OpenApiSupportedVersion.V7120, true)]   // Higher version
    [InlineData(OpenApiSupportedVersion.V7070, OpenApiSupportedVersion.V7120, false)]  // Lower version
    public void IsAtLeast_ReturnsExpectedResult(
        OpenApiSupportedVersion currentVersion,
        OpenApiSupportedVersion compareToVersion,
        bool expectedResult)
    {
        // Act
        bool result = currentVersion.IsAtLeast(compareToVersion);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(OpenApiSupportedVersion.V7070, OpenApiSupportedVersion.V7120, true)]   // Lower version
    [InlineData(OpenApiSupportedVersion.V7140, OpenApiSupportedVersion.V7120, false)]  // Higher version
    [InlineData(OpenApiSupportedVersion.V7120, OpenApiSupportedVersion.V7120, false)]  // Equal versions
    public void IsLessThan_ReturnsExpectedResult(
        OpenApiSupportedVersion currentVersion,
        OpenApiSupportedVersion compareToVersion,
        bool expectedResult)
    {
        // Act
        bool result = currentVersion.IsLessThan(compareToVersion);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(OpenApiSupportedVersion.V7090, OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7100, true)]   // Within range
    [InlineData(OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7100, true)]   // At min boundary
    [InlineData(OpenApiSupportedVersion.V7100, OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7100, false)]  // At max boundary
    [InlineData(OpenApiSupportedVersion.V7070, OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7100, false)]  // Below min
    [InlineData(OpenApiSupportedVersion.V7110, OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7100, false)]  // Above max
    public void IsBetween_ReturnsExpectedResult(
        OpenApiSupportedVersion currentVersion,
        OpenApiSupportedVersion minVersion,
        OpenApiSupportedVersion maxVersion,
        bool expectedResult)
    {
        // Act
        bool result = currentVersion.IsBetween(minVersion, maxVersion);

        // Assert
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData(OpenApiSupportedVersion.V7180, true)]   // Latest version
    [InlineData(OpenApiSupportedVersion.V7170, false)]  // Not latest version
    [InlineData(OpenApiSupportedVersion.V7120, false)]  // Not latest version
    [InlineData(OpenApiSupportedVersion.V7070, false)]  // Not latest version
    public void IsLatest_ReturnsExpectedResult(
        OpenApiSupportedVersion currentVersion,
        bool expectedResult)
    {
        // Act
        bool result = currentVersion.IsLatest();

        // Assert
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData(OpenApiSupportedVersion.V7170, true)]   // Older than latest version
    [InlineData(OpenApiSupportedVersion.V7120, true)]   // Older than latest version
    [InlineData(OpenApiSupportedVersion.V7070, true)]   // Older than latest version
    [InlineData(OpenApiSupportedVersion.V7180, false)]  // Equal to latest version
    public void IsOlderThanLatest_ReturnsExpectedResult(
        OpenApiSupportedVersion currentVersion, 
        bool expectedResult)
    {
        // Act
        bool result = currentVersion.IsOlderThanLatest();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(OpenApiSupportedVersion.Latest, 0)]
    [InlineData(OpenApiSupportedVersion.V7140, 7140)]
    [InlineData(OpenApiSupportedVersion.V7130, 7130)]
    [InlineData(OpenApiSupportedVersion.V7120, 7120)]
    [InlineData(OpenApiSupportedVersion.V7110, 7110)]
    [InlineData(OpenApiSupportedVersion.V7100, 7100)]
    [InlineData(OpenApiSupportedVersion.V7090, 7090)]
    [InlineData(OpenApiSupportedVersion.V7080, 7080)]
    [InlineData(OpenApiSupportedVersion.V7070, 7070)]
    public void EnumValues_MatchExpectedIntValues(OpenApiSupportedVersion version, int expectedValue)
    {
        // Act & Assert
        Assert.Equal(expectedValue, (int)version);
    }

    [Theory]
    [InlineData(OpenApiSupportedVersion.Latest, OpenApiSupportedVersion.V7180)]
    [InlineData(OpenApiSupportedVersion.V7180, OpenApiSupportedVersion.V7180)]
    [InlineData(OpenApiSupportedVersion.V7170, OpenApiSupportedVersion.V7170)]
    [InlineData(OpenApiSupportedVersion.V7160, OpenApiSupportedVersion.V7160)]
    [InlineData(OpenApiSupportedVersion.V7140, OpenApiSupportedVersion.V7140)]
    [InlineData(OpenApiSupportedVersion.V7120, OpenApiSupportedVersion.V7120)]
    [InlineData(OpenApiSupportedVersion.V7110, OpenApiSupportedVersion.V7110)]
    [InlineData(OpenApiSupportedVersion.V7100, OpenApiSupportedVersion.V7100)]
    [InlineData(OpenApiSupportedVersion.V7090, OpenApiSupportedVersion.V7090)]
    [InlineData(OpenApiSupportedVersion.V7080, OpenApiSupportedVersion.V7080)]
    [InlineData(OpenApiSupportedVersion.V7070, OpenApiSupportedVersion.V7070)]
    public void ResolveVersion_ReturnsExpectedResult(
        OpenApiSupportedVersion inputVersion,
        OpenApiSupportedVersion expectedVersion)
    {
        // Act
        var result = inputVersion.ResolveVersion();

        // Assert
        Assert.Equal(expectedVersion, result);
    }
    
    [Fact]
    public void Default_HasValueOfZero()
    {
        // Act & Assert
        Assert.Equal(0, (int)OpenApiSupportedVersion.Latest);
    }
    
    [Theory]
    [InlineData(OpenApiSupportedVersion.Latest, OpenApiSupportedVersion.V7140, true)]   // Default is Latest
    [InlineData(OpenApiSupportedVersion.Latest, OpenApiSupportedVersion.V7120, true)]   // Default is Latest which is higher than V7120
    [InlineData(OpenApiSupportedVersion.V7070, OpenApiSupportedVersion.Latest, false)]  // V7070 is lower than Default (Latest)
    public void IsAtLeast_WithDefault_ReturnsExpectedResult(
        OpenApiSupportedVersion currentVersion,
        OpenApiSupportedVersion compareToVersion,
        bool expectedResult)
    {
        // Act
        bool result = currentVersion.IsAtLeast(compareToVersion);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
