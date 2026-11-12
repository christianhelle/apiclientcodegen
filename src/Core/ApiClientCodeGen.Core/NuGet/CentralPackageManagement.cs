using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

            if (IsExpression(centralVersion!))
                return null;

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
                    var importedFile = ResolveImportElement(file, element, out var unresolved);
                    if (unresolved)
                    {
                        // The import could declare or update the package
                        state.UnresolvedImport = true;
                        if (state.Version != null)
                            state.Unresolved = true;
                    }
                    else if (importedFile != null)
                    {
                        Evaluate(importedFile, packageId, visited, state);
                    }

                    continue;
                }

                if (HasExpressionIdentity(element))
                {
                    state.Unresolved = true;
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

        /// <summary>
        /// Returns the imported file to evaluate, or null when it should be skipped.
        /// Sets unresolved when the import or its conditions can't be evaluated without MSBuild
        /// </summary>
        private static string? ResolveImportElement(string importingFile, XElement import, out bool unresolved)
        {
            unresolved = false;
            foreach (var condition in import.AncestorsAndSelf().Select(e => (string?)e.Attribute("Condition")))
            {
                if (condition == null)
                    continue;

                var exists = EvaluateExistsCondition(importingFile, condition);
                if (exists == null)
                {
                    unresolved = true;
                    return null;
                }

                if (exists == false)
                    return null;
            }

            var path = ResolvePath(importingFile, (string?)import.Attribute("Project"));
            if (path == null)
            {
                unresolved = true;
                return null;
            }

            return File.Exists(path) ? path : null;
        }

        private static readonly Regex ExistsCondition =
            new Regex(@"^\s*Exists\(\s*'([^']*)'\s*\)\s*$", RegexOptions.IgnoreCase);

        private static readonly Regex GetPathOfFileAbove =
            new Regex(@"GetPathOfFileAbove\(\s*'?([^,')]+?)'?\s*,", RegexOptions.IgnoreCase);

        // Only a single Exists('path') with a resolvable path can be evaluated
        private static bool? EvaluateExistsCondition(string file, string condition)
        {
            var match = ExistsCondition.Match(condition);
            if (!match.Success)
                return null;

            var path = ResolvePath(file, match.Groups[1].Value);
            if (path == null)
                return null;

            return File.Exists(path) || Directory.Exists(path);
        }

        private static string? ResolvePath(string importingFile, string? project)
        {
            if (string.IsNullOrWhiteSpace(project))
                return null;

            var directory = Path.GetDirectoryName(Path.GetFullPath(importingFile))!;
            var fileAbove = GetPathOfFileAbove.Match(project!);
            if (fileAbove.Success)
                return FindFileAbove(new DirectoryInfo(directory).Parent, fileAbove.Groups[1].Value.Trim()) ?? string.Empty;

            var path = project!
                .Replace("$(MSBuildThisFileDirectory)", directory + Path.DirectorySeparatorChar)
                .Trim();

            if (IsExpression(path))
                return null;

            return Path.GetFullPath(Path.Combine(directory, path));
        }

        private static bool IsExpression(string value)
            => value.Contains("$(") || value.Contains("@(") || value.Contains("%(");

        private static bool IsPackage(XElement element, string attributeName, string packageId)
            => (((string?)element.Attribute(attributeName)) ?? string.Empty)
                .Split(';')
                .Any(id => string.Equals(id.Trim(), packageId, StringComparison.OrdinalIgnoreCase));

        private static bool HasVersion(XElement entry)
            => entry.Attribute("Version") != null ||
               entry.Elements().Any(e => e.Name.LocalName == "Version");

        private static string ReadVersion(XElement entry)
        {
            var versionElements = entry.Elements().Where(e => e.Name.LocalName == "Version").ToList();
            if (versionElements.Count > 1 || versionElements.Any(e => e.Attribute("Condition") != null))
                return UnresolvedVersion;

            var version = (string?)entry.Attribute("Version") ?? versionElements.FirstOrDefault()?.Value;

            return string.IsNullOrWhiteSpace(version) ? UnresolvedVersion : version!.Trim();
        }

        // An identity built from properties or item lists could evaluate to any package
        private static bool HasExpressionIdentity(XElement element)
            => new[] { "Include", "Update" }
                .Select(name => (string?)element.Attribute(name) ?? string.Empty)
                .Any(IsExpression);

        private static bool HasCondition(XElement element)
            => element.AncestorsAndSelf().Any(e => e.Attribute("Condition") != null);

        /// <summary>
        /// Returns the effective ManagePackageVersionsCentrally from the nearest Directory.Build.props
        /// (following its imports) and the project file, true when not set,
        /// or null when it can't be resolved without evaluating MSBuild
        /// </summary>
        private static bool? GetManagePackageVersionsCentrally(string projectFilePath)
        {
            var state = new PropertyState();
            var buildProps = FindFileAbove(new FileInfo(projectFilePath).Directory, "Directory.Build.props");
            if (buildProps != null)
                EvaluateProperty(buildProps, true, new HashSet<string>(StringComparer.OrdinalIgnoreCase), state);

            if (File.Exists(projectFilePath))
                EvaluateProperty(projectFilePath, false, new HashSet<string>(StringComparer.OrdinalIgnoreCase), state);

            return state.Unresolved ? null : state.Value ?? true;
        }

        private static void EvaluateProperty(string file, bool followImports, ISet<string> visited, PropertyState state)
        {
            if (!visited.Add(Path.GetFullPath(file)))
                return;

            var elements = XDocument.Load(file)
                .Descendants()
                .Where(e => e.Name.LocalName is "Import" or "ManagePackageVersionsCentrally");

            foreach (var element in elements)
            {
                if (element.Name.LocalName == "Import")
                {
                    if (!followImports)
                        continue;

                    var importedFile = ResolveImportElement(file, element, out var unresolved);
                    if (unresolved)
                        state.Unresolved = true;
                    else if (importedFile != null)
                        EvaluateProperty(importedFile, true, visited, state);

                    continue;
                }

                // A later unconditional assignment overrides anything before it
                if (!HasCondition(element) && bool.TryParse(element.Value.Trim(), out var value))
                {
                    state.Value = value;
                    state.Unresolved = false;
                }
                else
                {
                    state.Unresolved = true;
                }
            }
        }

        private sealed class PropertyState
        {
            public bool? Value { get; set; }
            public bool Unresolved { get; set; }
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
