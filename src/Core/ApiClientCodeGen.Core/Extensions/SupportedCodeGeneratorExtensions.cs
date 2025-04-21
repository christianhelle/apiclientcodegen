using Rapicgen.Core.NuGet;
using Rapicgen.Core.Options.OpenApiGenerator;
using System.Collections.Generic;

namespace Rapicgen.Core.Extensions
{
    public static class SupportedCodeGeneratorExtensions
    {
        private static readonly PackageDependencyListProvider DependencyListProvider
            = new PackageDependencyListProvider();

        public static IEnumerable<PackageDependency> GetDependencies(
            this SupportedCodeGenerator generator)
            => DependencyListProvider
                .GetDependencies(generator);

        public static IEnumerable<PackageDependency> GetDependencies(
            this SupportedCodeGenerator generator,
            OpenApiSupportedVersion version)
            => generator == SupportedCodeGenerator.OpenApi
                ? DependencyListProvider.GetDependencies(version)
                : DependencyListProvider.GetDependencies(generator);
    }
}