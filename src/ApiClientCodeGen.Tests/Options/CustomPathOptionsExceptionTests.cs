using System;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class CustomPathOptionsExceptionTests
    {
        private CustomPathOptions sut;

        [TestInitialize]
        public void Init()
        {
            var fixture = new Fixture();
            var mock = new Mock<IGeneralOptions>();
            mock.Setup(c => c.JavaPath).Throws<Exception>();
            mock.Setup(c => c.NpmPath).Throws<Exception>();
            mock.Setup(c => c.NSwagPath).Throws<Exception>();
            mock.Setup(c => c.SwaggerCodegenPath).Throws<Exception>();
            mock.Setup(c => c.OpenApiGeneratorPath).Throws<Exception>();

            sut = new CustomPathOptions(mock.Object);
        }

        [TestMethod]
        public void JavaPath_NotNullOrWhiteSpace()
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void JavaPath_Reads_From_Options()
            => sut.JavaPath.Should().Be(PathProvider.GetJavaPath());

        [TestMethod]
        public void NpmPath_NotNullOrWhiteSpace()
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void NpmPath_Reads_From_Options()
            => sut.NpmPath.Should().Be(PathProvider.GetNpmPath());

        [TestMethod]
        public void NSwagPath_NotNullOrWhiteSpace()
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void NSwagPath_Reads_From_Options()
            => sut.NSwagPath.Should().Be(PathProvider.GetNSwagPath());

        [TestMethod]
        public void SwaggerCodegenPath_NotNullOrWhiteSpace()
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void SwaggerCodegenPath_Reads_From_Options()
            => sut.SwaggerCodegenPath
                .Should()
                .Be(PathProvider.GetSwaggerCodegenPath());

        [TestMethod]
        public void OpenApiGeneratorPath_NotNullOrWhiteSpace()
            => sut.OpenApiGeneratorPath
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod]
        public void OpenApiGeneratorPath_Reads_From_Options()
            => sut.OpenApiGeneratorPath
                .Should()
                .Be(PathProvider.GetOpenApiGeneratorPath());
    }
}