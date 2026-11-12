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
                var managedCentrally = GetManagePackageVersionsCentrally(projectFilePath);
                if (managedCentrally == false)
                    return null;

                var directory = new FileInfo(projectFilePath).Directory;
                while (directory != null)
                {
                    var propsFile = FindPackagesPropsFile(directory);
                    if (propsFile == null)
                        return null;

                    var document = XDocument.Load(propsFile);
                    var entries = FindEntries(document, packageId);
                    if (entries.Count > 0)
                        return managedCentrally == null ? UnresolvedVersion : GetVersion(entries);

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

        private static List<XElement> FindEntries(XDocument document, string packageId)
            => document
                .Descendants()
                .Where(e => e.Name.LocalName == "PackageVersion")
                .Where(e => IsPackage(e, "Include", packageId) || IsPackage(e, "Update", packageId))
                .ToList();

        private static bool IsPackage(XElement element, string attributeName, string packageId)
            => string.Equals(
                ((string?)element.Attribute(attributeName))?.Trim(),
                packageId,
                StringComparison.OrdinalIgnoreCase);

        // Items are evaluated in document order: an Update only affects items declared before it
        private static string GetVersion(List<XElement> entries)
        {
            if (entries.Any(HasCondition))
                return UnresolvedVersion;

            var includes = entries.Where(e => e.Attribute("Include") != null).ToList();
            if (includes.Count > 1)
                return UnresolvedVersion;

            var updates = includes.Count == 0
                ? entries
                : entries.Skip(entries.IndexOf(includes[0]) + 1).ToList();

            var entry = updates.LastOrDefault() ?? includes[0];
            var version = (string?)entry.Attribute("Version") ??
                          entry.Elements().FirstOrDefault(e => e.Name.LocalName == "Version")?.Value;

            return string.IsNullOrWhiteSpace(version) ? UnresolvedVersion : version!.Trim();
        }

        private static bool HasCondition(XElement element)
            => element.AncestorsAndSelf().Any(e => e.Attribute("Condition") != null);

        /// <summary>
        /// Returns the last assignment of ManagePackageVersionsCentrally in the project file,
        /// true when not set, or null when it can't be resolved without evaluating MSBuild
        /// </summary>
        private static bool? GetManagePackageVersionsCentrally(string projectFilePath)
        {
            if (!File.Exists(projectFilePath))
                return true;

            var assignments = XDocument.Load(projectFilePath)
                .Descendants()
                .Where(e => e.Name.LocalName == "ManagePackageVersionsCentrally")
                .ToList();

            if (assignments.Count == 0)
                return true;

            if (assignments.Any(HasCondition))
                return null;

            return bool.TryParse(assignments.Last().Value.Trim(), out var value) ? value : (bool?)null;
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
