using System.IO;
using Rapicgen.Core.Generators;
using FluentAssertions;

namespace Rapicgen.IntegrationTests.Generators.CSharp
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpFileMergerTests
    {
        [Xunit.Fact]
        public void Can_Merge_CSharp_Files()
        {
            var folder = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Generators");

            CSharpFileMerger.MergeFiles(
                    folder)
                .Should()
                .NotBeNullOrWhiteSpace(folder);
        }
    }
}
