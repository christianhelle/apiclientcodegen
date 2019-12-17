using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests
{
    [TestClass]
    public class NpmHelerTests
    {
        [TestMethod]
        public void Can_GetNpmPath()
            => NpmHelper.GetNpmPath()
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void FileExists_GetNpmPath()
            => File.Exists(NpmHelper.GetNpmPath())
                .Should()
                .BeTrue();

        [TestMethod]
        public void Can_GetNpmPrefixPath()
            => NpmHelper.GetPrefixPath()
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void DirectoryExists_GetNpmPrefixPath()
            => Directory.Exists(NpmHelper.GetPrefixPath())
                .Should()
                .BeTrue();
    }
}