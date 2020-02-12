using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    public class CSharpFileMergerTests
    {
        [TestMethod]
        public void Can_Merge_CSharp_Files()
            => CSharpFileMerger.MergeFiles(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\"))
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
