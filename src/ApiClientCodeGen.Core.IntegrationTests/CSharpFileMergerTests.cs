using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.Core.IntegrationTests
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    public class CSharpFileMergerTests
    {
        [Retry(3)]
        public void Can_Merge_CSharp_Files()
            => CSharpFileMerger.MergeFiles(
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName)
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
