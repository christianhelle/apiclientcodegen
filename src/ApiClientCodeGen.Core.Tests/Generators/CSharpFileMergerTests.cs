using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;

namespace ApiClientCodeGen.Core.Tests.Generators
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpFileMergerTests
    {
        [Xunit.Fact]
        public void Can_Merge_CSharp_Files()
            => CSharpFileMerger.MergeFiles(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\"))
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
