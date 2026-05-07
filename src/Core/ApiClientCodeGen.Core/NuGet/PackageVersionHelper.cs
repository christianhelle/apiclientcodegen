using System;

namespace Rapicgen.Core.NuGet
{
    public static class PackageVersionHelper
    {
        public static bool IsVersionGreaterOrEqual(string installedVersion, string requiredVersion)
        {
            try
            {
                ParseSemVer(installedVersion, out var instBase, out var instPre);
                ParseSemVer(requiredVersion, out var reqBase, out var reqPre);

                if (instBase == null || reqBase == null)
                    return false;

                var cmp = instBase.CompareTo(reqBase);
                if (cmp != 0)
                    return cmp > 0;

                // Base versions are equal — apply SemVer pre-release precedence rules:
                // a stable release is higher than its pre-release counterpart
                if (instPre == null && reqPre == null) return true;
                if (instPre == null) return true;   // stable >= pre-release
                if (reqPre == null) return false;   // pre-release < stable

                return ComparePreRelease(instPre, reqPre) >= 0;
            }
            catch
            {
                // Fall through to false if parsing fails
            }

            return false;
        }

        private static void ParseSemVer(string version, out Version? baseVersion, out string? preRelease)
        {
            baseVersion = null;
            preRelease = null;

            if (string.IsNullOrWhiteSpace(version))
                return;

            // Strip build metadata (e.g. "+build.1")
            var plusIndex = version.IndexOf('+');
            if (plusIndex >= 0)
                version = version.Substring(0, plusIndex);

            var dashIndex = version.IndexOf('-');
            if (dashIndex >= 0)
            {
                preRelease = version.Substring(dashIndex + 1);
                version = version.Substring(0, dashIndex);
            }

            Version.TryParse(version, out baseVersion);
        }

        private static int ComparePreRelease(string a, string b)
        {
            var aParts = a.Split('.');
            var bParts = b.Split('.');

            var len = Math.Min(aParts.Length, bParts.Length);
            for (var i = 0; i < len; i++)
            {
                var aIsInt = int.TryParse(aParts[i], out var aNum);
                var bIsInt = int.TryParse(bParts[i], out var bNum);

                if (aIsInt && bIsInt)
                {
                    var cmp = aNum.CompareTo(bNum);
                    if (cmp != 0) return cmp;
                }
                else if (aIsInt)
                {
                    return -1; // numeric identifiers have lower precedence than alphanumeric
                }
                else if (bIsInt)
                {
                    return 1;
                }
                else
                {
                    var cmp = string.Compare(aParts[i], bParts[i], StringComparison.Ordinal);
                    if (cmp != 0) return cmp;
                }
            }

            return aParts.Length.CompareTo(bParts.Length);
        }

        /// <summary>
        /// Strips the pre-release suffix from a version string (e.g., "3.0.0-beta.20210218.1" → "3.0.0").
        /// Useful for display or logging where only the base version is needed.
        /// Note: version comparison logic in <see cref="IsVersionGreaterOrEqual"/> uses SemVer-aware
        /// parsing and does not rely on this method.
        /// </summary>
        public static string NormalizeVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return version;

            // Strip pre-release suffixes for display purposes (e.g., "3.0.0-beta.20210218.1" -> "3.0.0")
            var dashIndex = version.IndexOf('-');
            return dashIndex >= 0 ? version.Substring(0, dashIndex) : version;
        }
    }
}
