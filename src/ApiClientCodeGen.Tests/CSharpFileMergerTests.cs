using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class CSharpFileMergerTests
    {
        [TestMethod]
        public void Can_Merge_CSharp_Files()
            => new CSharpFileMerger()
                .MergeFiles(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\"))
                .Should()
                .NotBeNullOrWhiteSpace();
    }
}
