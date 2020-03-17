using System;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class JavaPathProviderTests
    {
        private Mock<IGeneralOptions> mock;
        private string result;

        [TestInitialize]
        public void Init()
        {
            mock = new Mock<IGeneralOptions>();
            mock.Setup(c => c.JavaPath)
                .Returns(Test.CreateAnnonymous<string>());

            result = new JavaPathProvider(
                    mock.Object,
                    Test.CreateDummy<IProcessLauncher>())
                .GetJavaExePath();
        }

        [TestMethod, Xunit.Fact]
        public void GetJavaExePath_Should_NotBeNull()
            => result.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void GetJavaExePath_Should_Read_JavaPath_Option()
            => mock.Verify(c => c.JavaPath);

        [TestMethod, Xunit.Fact]
        public void GetJavaExePath_Returns_Default_Path()
        {
            var launcher = new Mock<IProcessLauncher>();
            launcher.Setup(c => c.Start("java", "-version", It.IsAny<string>()))
                .Throws(new Exception());

            new JavaPathProvider(
                    Test.CreateDummy<IGeneralOptions>(),
                    launcher.Object)
                .GetJavaExePath()
                .Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}