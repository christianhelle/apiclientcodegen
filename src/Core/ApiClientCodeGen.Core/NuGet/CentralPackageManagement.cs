using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Rapicgen.Core.NuGet
{
    public static class CentralPackageManagement
    {
        private const string PackagesPropsFileName = "Directory.Packages.props";

        /// <summary>
        /// Returned when a central version exists but can't be resolved without
        /// evaluating MSBuild (conditional or duplicate declarations)
        /// </summary>
        public const string UnresolvedVersion = "unresolved";

        public static string? FindCentralPackageVersion(string projectFilePath, string packageId)
        {
            try
            {
                if (IsCentralPackageManagementDisabled(projectFilePath))
                    return null;

                var directory = new FileInfo(projectFilePath).Directory;
                while (directory != null)
                {
                    var propsFile = FindPackagesPropsFile(directory);
                    if (propsFile == null)
                        return null;

                    var document = XDocument.Load(propsFile);
                    var entries = FindEntries(document, packageId, "Update");
                    if (entries.Count == 0)
                        entries = FindEntries(document, packageId, "Include");

                    if (entries.Count > 0)
                        return GetVersion(entries);

                    if (!document.Descendants().Any(e => e.Name.LocalName == "Import"))
                        return null;

                    directory = new FileInfo(propsFile).Directory?.Parent;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the version to install, or null when the central version
        /// can't be resolved and the package should be left untouched
        /// </summary>
        public static string? GetVersionToInstall(PackageDependency dependency, string? centralVersion)
        {
            if (string.IsNullOrWhiteSpace(centralVersion))
                return dependency.Version;

            if (!Version.TryParse(PackageVersionHelper.NormalizeVersion(centralVersion!), out _))
                return null;

            if (dependency.ForceUpdate &&
                !PackageVersionHelper.IsVersionGreaterOrEqual(centralVersion!, dependency.Version))
                return dependency.Version;

            return centralVersion!;
        }

        private static List<XElement> FindEntries(XDocument document, string packageId, string attributeName)
            => document
                .Descendants()
                .Where(e => e.Name.LocalName == "PackageVersion")
                .Where(e => string.Equals(
                    ((string?)e.Attribute(attributeName))?.Trim(),
                    packageId,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

        private static string GetVersion(IReadOnlyList<XElement> entries)
        {
            if (entries.Count > 1 || entries.Any(HasCondition))
                return UnresolvedVersion;

            var entry = entries[0];
            var version = (string?)entry.Attribute("Version") ??
                          entry.Elements().FirstOrDefault(e => e.Name.LocalName == "Version")?.Value;

            return string.IsNullOrWhiteSpace(version) ? UnresolvedVersion : version!.Trim();
        }

        private static bool HasCondition(XElement element)
            => element.AncestorsAndSelf().Any(e => e.Attribute("Condition") != null);

        private static bool IsCentralPackageManagementDisabled(string projectFilePath)
        {
            if (!File.Exists(projectFilePath))
                return false;

            return XDocument.Load(projectFilePath)
                .Descendants()
                .Where(e => e.Name.LocalName == "ManagePackageVersionsCentrally")
                .Any(e => string.Equals(e.Value.Trim(), "false", StringComparison.OrdinalIgnoreCase));
        }

        private static string? FindPackagesPropsFile(DirectoryInfo? directory)
        {
            while (directory != null)
            {
                var candidate = Path.Combine(directory.FullName, PackagesPropsFileName);
                if (File.Exists(candidate))
                    return candidate;
                directory = directory.Parent;
            }

            return null;
        }
    }
}
