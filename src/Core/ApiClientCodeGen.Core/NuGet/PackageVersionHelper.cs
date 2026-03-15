using System;

namespace Rapicgen.Core.NuGet
{
    public static class PackageVersionHelper
    {
        public static bool IsVersionGreaterOrEqual(string installedVersion, string requiredVersion)
        {
            try
            {
                if (Version.TryParse(NormalizeVersion(installedVersion), out var installed) &&
                    Version.TryParse(NormalizeVersion(requiredVersion), out var required))
                {
                    return installed >= required;
                }
            }
            catch
            {
                // Fall through to false if parsing fails
            }

            return false;
        }

        public static string NormalizeVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return version;

            // Strip pre-release suffixes for comparison (e.g., "3.0.0-beta.20210218.1" -> "3.0.0")
            var dashIndex = version.IndexOf('-');
            return dashIndex >= 0 ? version.Substring(0, dashIndex) : version;
        }
    }
}
