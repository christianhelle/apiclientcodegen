using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Rapicgen.Core.NuGet
{
    public static class CentralPackageManagement
    {
        private const string PackagesPropsFileName = "Directory.Packages.props";

        public static string? FindCentralPackageVersion(string projectFilePath, string packageId)
        {
            try
            {
                var propsFile = FindPackagesPropsFile(projectFilePath);
                if (propsFile == null)
                    return null;

                var document = XDocument.Load(propsFile);
                return document
                    .Descendants()
                    .Where(e => e.Name.LocalName == "PackageVersion")
                    .Where(e => string.Equals(
                        (string?)e.Attribute("Include"),
                        packageId,
                        StringComparison.OrdinalIgnoreCase))
                    .Select(e => (string?)e.Attribute("Version"))
                    .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
            }
            catch
            {
                return null;
            }
        }

        private static string? FindPackagesPropsFile(string projectFilePath)
        {
            var directory = new FileInfo(projectFilePath).Directory;
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
