using System;
using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet
{
    public sealed class PackageDependency
    {
        public PackageDependency(
            string name,
            Version version,
            bool forceUpdate = true,
            bool isSystemLibrary = false)
            : this(
                name,
                version.ToString(),
                forceUpdate,
                isSystemLibrary)
        {
        }

        public PackageDependency(
            string name,
            string version,
            bool forceUpdate = true,
            bool isSystemLibrary = false)
        {
            Name = name;
            Version = version;
            ForceUpdate = forceUpdate;
            IsSystemLibrary = isSystemLibrary;
        }

        public string Name { get; }
        public string Version { get; }
        public bool ForceUpdate { get; }
        public bool IsSystemLibrary { get; }

        public override bool Equals(object obj)
        {
            return obj is PackageDependency dependency &&
                   Name == dependency.Name &&
                   EqualityComparer<string>.Default.Equals(Version, dependency.Version);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Version != null ? Version.GetHashCode() : 0);
            }
        }
    }
}