using System.Collections.Generic;
using Rapicgen.Core.NuGet;

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
    }
}