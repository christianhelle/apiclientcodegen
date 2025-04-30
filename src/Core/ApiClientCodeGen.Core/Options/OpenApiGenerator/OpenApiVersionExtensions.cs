namespace Rapicgen.Core.Options.OpenApiGenerator;

public static class OpenApiVersionExtensions
{
    /// <summary>
    /// Checks if the current version is greater than or equal to the specified version
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <param name="compareToVersion">The version to compare against</param>
    /// <returns>True if current version is greater than or equal to the specified version</returns>
    public static bool IsAtLeast(this OpenApiSupportedVersion currentVersion, OpenApiSupportedVersion compareToVersion)
    {
        return (int)currentVersion >= (int)compareToVersion;
    }
    
    /// <summary>
    /// Checks if the current version is less than the specified version
    /// </summary>
    /// <param name="currentVersion">The current version being used</param>
    /// <param name="compareToVersion">The version to compare against</param>
    /// <returns>True if current version is less than the specified version</returns>
    public static bool IsLessThan(this OpenApiSupportedVersion currentVersion, OpenApiSupportedVersion compareToVersion)
    {
        return (int)currentVersion < (int)compareToVersion;
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
}