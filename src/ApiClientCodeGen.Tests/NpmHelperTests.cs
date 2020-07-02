using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    
    public class NpmHelperTests
    {
        [Xunit.Fact]
        public void Can_GetNpmPath()
            => NpmHelper.GetNpmPath()
                .Should()
                .NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void GetNpmPath_IgnorePath_Returns_Npm()
            => NpmHelper.GetNpmPath(true)
                .Should()
                .Be("npm");

        [Xunit.Fact]
        public void FileExists_GetNpmPath()
            => File.Exists(NpmHelper.GetNpmPath())
                .Should()
                .BeTrue();

        [Xunit.Fact]
        public void Can_GetNpmPrefixPath()
            => NpmHelper.GetPrefixPath()
                .Should()
                .NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void DirectoryExists_GetNpmPrefixPath()
            => Directory.Exists(NpmHelper.GetPrefixPath())
                .Should()
                .BeTrue();

        [Xunit.Fact]
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