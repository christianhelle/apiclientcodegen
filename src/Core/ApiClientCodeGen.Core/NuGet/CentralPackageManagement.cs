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

                var propsFile = FindPackagesPropsFile(new FileInfo(projectFilePath).Directory);
                if (propsFile == null)
                    return null;

                var version = FindVersionInFile(propsFile, packageId, new HashSet<string>(StringComparer.OrdinalIgnoreCase));
                return version != null && managedCentrally == null ? UnresolvedVersion : version;
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

        private static string? FindVersionInFile(string file, string packageId, ISet<string> visited)
        {
            var state = new LookupState();
            Evaluate(file, packageId, visited, state);

            if (state.Unresolved)
                return UnresolvedVersion;

            if (state.Version == null && state.UnresolvedImport)
                return UnresolvedVersion;

            return state.Version;
        }

        // Walks items and imports in document order, as MSBuild does:
        // an Update only affects items declared (or imported) before it
        private static void Evaluate(string file, string packageId, ISet<string> visited, LookupState state)
        {
            if (!visited.Add(Path.GetFullPath(file)))
                return;

            var elements = XDocument.Load(file)
                .Descendants()
                .Where(e => e.Name.LocalName is "Import" or "PackageVersion");

            foreach (var element in elements)
            {
                if (element.Name.LocalName == "Import")
                {
                    var importedFile = ResolveImport(file, (string?)element.Attribute("Project"));
                    if (importedFile == null)
                        state.UnresolvedImport = true;
                    else if (File.Exists(importedFile))
                        Evaluate(importedFile, packageId, visited, state);
                    continue;
                }

                var isInclude = IsPackage(element, "Include", packageId);
                var isUpdate = IsPackage(element, "Update", packageId);
                if (!isInclude && !isUpdate)
                    continue;

                if (HasCondition(element) || (isInclude && state.Version != null))
                {
                    state.Unresolved = true;
                    continue;
                }

                if (isUpdate && state.Version == null)
                {
                    if (state.UnresolvedImport)
                        state.Unresolved = true;
                    continue;
                }

                if (isUpdate && !HasVersion(element))
                    continue;

                state.Version = ReadVersion(element);
            }
        }

        private sealed class LookupState
        {
            public string? Version { get; set; }
            public bool Unresolved { get; set; }
            public bool UnresolvedImport { get; set; }
        }

        private static string? ResolveImport(string importingFile, string? project)
        {
            if (string.IsNullOrWhiteSpace(project))
                return null;

            var directory = Path.GetDirectoryName(Path.GetFullPath(importingFile))!;
            if (project!.Contains("GetPathOfFileAbove") && project.Contains(PackagesPropsFileName))
                return FindPackagesPropsFile(new DirectoryInfo(directory).Parent) ?? string.Empty;

            var path = project
                .Replace("$(MSBuildThisFileDirectory)", directory + Path.DirectorySeparatorChar)
                .Trim();

            if (path.Contains("$("))
                return null;

            return Path.GetFullPath(Path.Combine(directory, path));
        }

        private static bool IsPackage(XElement element, string attributeName, string packageId)
            => (((string?)element.Attribute(attributeName)) ?? string.Empty)
                .Split(';')
                .Any(id => string.Equals(id.Trim(), packageId, StringComparison.OrdinalIgnoreCase));

        private static bool HasVersion(XElement entry)
            => entry.Attribute("Version") != null ||
               entry.Elements().Any(e => e.Name.LocalName == "Version");

        private static string ReadVersion(XElement entry)
        {
            var version = (string?)entry.Attribute("Version") ??
                          entry.Elements().FirstOrDefault(e => e.Name.LocalName == "Version")?.Value;

            return string.IsNullOrWhiteSpace(version) ? UnresolvedVersion : version!.Trim();
        }

        private static bool HasCondition(XElement element)
            => element.AncestorsAndSelf().Any(e => e.Attribute("Condition") != null);

        /// <summary>
        /// Returns the last assignment of ManagePackageVersionsCentrally in the nearest Directory.Build.props and the project file,
        /// true when not set, or null when it can't be resolved without evaluating MSBuild
        /// </summary>
        private static bool? GetManagePackageVersionsCentrally(string projectFilePath)
        {
            var buildProps = FindFileAbove(new FileInfo(projectFilePath).Directory, "Directory.Build.props");
            var assignments = new[] { buildProps, projectFilePath }
                .Where(f => f != null && File.Exists(f))
                .SelectMany(f => XDocument.Load(f).Descendants())
                .Where(e => e.Name.LocalName == "ManagePackageVersionsCentrally")
                .ToList();

            if (assignments.Count == 0)
                return true;

            if (assignments.Any(HasCondition))
                return null;

            return bool.TryParse(assignments.Last().Value.Trim(), out var value) ? value : (bool?)null;
        }

        private static string? FindPackagesPropsFile(DirectoryInfo? directory)
            => FindFileAbove(directory, PackagesPropsFileName);

        private static string? FindFileAbove(DirectoryInfo? directory, string fileName)
        {
            while (directory != null)
            {
                var candidate = Path.Combine(directory.FullName, fileName);
                if (File.Exists(candidate))
                    return candidate;
                directory = directory.Parent;
            }

            return null;
        }
    }
}
