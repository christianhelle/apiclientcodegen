using System;
using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.NuGet
{
    public class PackageDependency
    {
        public PackageDependency(string name, Version version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; set; }
        public Version Version { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PackageDependency dependency &&
                   Name == dependency.Name &&
                   EqualityComparer<Version>.Default.Equals(Version, dependency.Version);
        }

        protected bool Equals(PackageDependency other)
        {
            return string.Equals(Name, other.Name) && Equals(Version, other.Version);
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