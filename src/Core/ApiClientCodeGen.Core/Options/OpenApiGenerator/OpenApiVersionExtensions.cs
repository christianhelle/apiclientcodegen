namespace Rapicgen.Core.Options.OpenApiGenerator;

public static class OpenApiVersionExtensions
{
    /// <summary>
    /// Gets the effective version, resolving Default to the actual latest version
    /// </summary>
    /// <param name="version">The version to resolve</param>
    /// <returns>The effective version - the latest version if Default is provided</returns>
    public static OpenApiSupportedVersion ResolveVersion(this OpenApiSupportedVersion version)
    {
        return version == OpenApiSupportedVersion.Default
            ? OpenApiSupportedVersionExtensions.Latest 
            : version;
    }

    /// <summary>
    /// Checks if the current version is greater than or equal to the specified version
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <param name="compareToVersion">The version to compare against</param>
    /// <returns>True if current version is greater than or equal to the specified version</returns>
    public static bool IsAtLeast(this OpenApiSupportedVersion currentVersion, OpenApiSupportedVersion compareToVersion)
    {
        var resolvedCurrent = currentVersion.ResolveVersion();
        var resolvedCompareTo = compareToVersion.ResolveVersion();
        return (int)resolvedCurrent >= (int)resolvedCompareTo;
    }
    
    /// <summary>
    /// Checks if the current version is less than the specified version
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <param name="compareToVersion">The version to compare against</param>
    /// <returns>True if current version is less than the specified version</returns>
    public static bool IsLessThan(this OpenApiSupportedVersion currentVersion, OpenApiSupportedVersion compareToVersion)
    {
        var resolvedCurrent = currentVersion.ResolveVersion();
        var resolvedCompareTo = compareToVersion.ResolveVersion();
        return (int)resolvedCurrent < (int)resolvedCompareTo;
    }
    
    /// <summary>
    /// Checks if the current version is between the specified versions (inclusive of minVersion, exclusive of maxVersion)
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <param name="minVersion">The minimum version (inclusive)</param>
    /// <param name="maxVersion">The maximum version (exclusive)</param>
    /// <returns>True if current version is between the specified range</returns>
    public static bool IsBetween(
        this OpenApiSupportedVersion currentVersion, 
        OpenApiSupportedVersion minVersion, 
        OpenApiSupportedVersion maxVersion)
    {
        return currentVersion.IsAtLeast(minVersion) && currentVersion.IsLessThan(maxVersion);
    }

    /// <summary>
    /// Checks if the current version is the latest supported version
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <returns>True if current version is the latest supported version</returns>
    public static bool IsLatest(this OpenApiSupportedVersion currentVersion)
    {
        return currentVersion == OpenApiSupportedVersion.Default || currentVersion == OpenApiSupportedVersionExtensions.Latest;
    }
    
    /// <summary>
    /// Checks if the current version is older than the latest supported version
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <returns>True if current version is older than the latest supported version</returns>
    public static bool IsOlderThanLatest(this OpenApiSupportedVersion currentVersion)
    {
        var resolvedCurrent = currentVersion.ResolveVersion();
        return (int)resolvedCurrent < (int)OpenApiSupportedVersionExtensions.Latest;
    }
}