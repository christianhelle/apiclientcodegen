using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [TestClass]
    public class NpmHelperTests
    {
        [TestMethod]
        public void Can_GetNpmPath()
            => NpmHelper.GetNpmPath()
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void GetNpmPath_IgnorePath_Returns_Npm()
            => NpmHelper.GetNpmPath(true)
                .Should()
                .Be("npm");

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

        [TestMethod]
        public void TryGetNpmPrefixPathFromNpmConfig()
        {
            var mock = new Mock<IProcessLauncher>();
            mock.Setup(
                    c => c.Start(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Action<string>>(),
                        It.IsAny<Action<string>>(),
                        It.IsAny<string>()))
                .Throws(new Exception());
            NpmHelper.TryGetNpmPrefixPathFromNpmConfig(mock.Object)
                .Should()
                .BeNullOrEmpty();
        }
    }
}