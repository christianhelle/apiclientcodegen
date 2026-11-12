using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Rapicgen.Core.NuGet
{
    public static class ProjectTargetFrameworks
    {
        private const int MinimumNetVersion = 8;
        private const int MinimumNetFrameworkVersion = 462;

        public static IReadOnlyList<string> Parse(string projectFileContents)
        {
            try
            {
                var elements = XDocument.Parse(projectFileContents).Descendants().ToList();

                var frameworks = elements
                    .Where(e => e.Name.LocalName is "TargetFramework" or "TargetFrameworks")
                    .SelectMany(e => e.Value.Split(';'))
                    .Concat(elements
                        .Where(e => e.Name.LocalName == "TargetFrameworkVersion")
                        .Select(e => "net" + e.Value.Trim().TrimStart('v', 'V').Replace(".", string.Empty)))
                    .Select(f => f.Trim())
                    .Where(f => f.Length > 0)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                return frameworks;
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        public static bool AreSupportedByLatestRefit(IEnumerable<string> targetFrameworks)
        {
            var list = targetFrameworks.ToList();
            return list.Count > 0 && list.All(IsSupportedByLatestRefit);
        }

        public static bool IsSupportedByLatestRefit(string targetFramework)
        {
            var tfm = (targetFramework ?? string.Empty).Trim().ToLowerInvariant();
            var platformIndex = tfm.IndexOf('-');
            if (platformIndex >= 0)
                tfm = tfm.Substring(0, platformIndex);

            if (!tfm.StartsWith("net") || tfm.StartsWith("netstandard") || tfm.StartsWith("netcoreapp"))
                return false;

            var version = tfm.Substring(3);
            if (version.Contains("."))
            {
                return Version.TryParse(version, out var netVersion) &&
                       netVersion.Major >= MinimumNetVersion;
            }

            if (!int.TryParse(version, out var netFrameworkVersion))
                return false;

            if (version.Length == 2)
                netFrameworkVersion *= 10;

            return netFrameworkVersion >= MinimumNetFrameworkVersion;
        }
    }
}
