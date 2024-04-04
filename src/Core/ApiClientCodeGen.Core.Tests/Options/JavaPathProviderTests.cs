using System;
using ApiClientCodeGen.Tests.Common;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Options.General;
using FluentAssertions;
using Moq;
using Rapicgen.Core.External;

namespace ApiClientCodeGen.Core.Tests.Options
{
    
    public class JavaPathProviderTests
    {
        private readonly Mock<IGeneralOptions> mock;
        private readonly string result;

        public JavaPathProviderTests()
        {
            mock = new Mock<IGeneralOptions>();
            mock.Setup(c => c.JavaPath)
                .Returns(Test.CreateAnnonymous<string>());

            result = new JavaPathProvider(
                    mock.Object,
                    Test.CreateDummy<IProcessLauncher>())
                .GetJavaExePath();
        }

        [Xunit.Fact]
        public void GetJavaExePath_Should_NotBeNull()
            => result.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void GetJavaExePath_Should_Read_JavaPath_Option()
            => mock.Verify(c => c.JavaPath);

        [Xunit.Fact]
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