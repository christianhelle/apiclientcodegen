using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class CustomPathOptionsTests
    {
        private CustomPathOptions sut;
        private Mock<IGeneralOptions> mock;

        [TestInitialize]
        public void Init()
        {
            var fixture = new Fixture();
            mock = new Mock<IGeneralOptions>();
            mock.Setup(c => c.JavaPath).Returns(fixture.Create<string>());
            mock.Setup(c => c.NpmPath).Returns(fixture.Create<string>());
            mock.Setup(c => c.NSwagPath).Returns(fixture.Create<string>());
            mock.Setup(c => c.SwaggerCodegenPath).Returns(fixture.Create<string>());
            mock.Setup(c => c.OpenApiGeneratorPath).Returns(fixture.Create<string>());

            sut = new CustomPathOptions(mock.Object);
        }

        [TestMethod]
        public void JavaPath_NotNullOrWhiteSpace()
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void JavaPath_Reads_From_Options()
            => sut.JavaPath.Should().Be(mock.Object.JavaPath);

        [TestMethod]
        public void NpmPath_NotNullOrWhiteSpace()
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void NpmPath_Reads_From_Options()
            => sut.NpmPath.Should().Be(mock.Object.NpmPath);

        [TestMethod]
        public void NSwagPath_NotNullOrWhiteSpace()
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void NSwagPath_Reads_From_Options()
            => sut.NSwagPath.Should().Be(mock.Object.NSwagPath);

        [TestMethod]
        public void SwaggerCodegenPath_NotNullOrWhiteSpace()
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void SwaggerCodegenPath_Reads_From_Options()
            => sut.SwaggerCodegenPath.Should().Be(mock.Object.SwaggerCodegenPath);

        [TestMethod]
        public void OpenApiGeneratorPath_NotNullOrWhiteSpace()
            => sut.OpenApiGeneratorPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void OpenApiGeneratorPath_Reads_From_Options()
            => sut.OpenApiGeneratorPath.Should().Be(mock.Object.OpenApiGeneratorPath);
    }
}
