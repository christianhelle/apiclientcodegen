using System;
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
    public class CustomPathOptionsNullOptionsTests
    {
        private readonly CustomPathOptions sut = new CustomPathOptions();

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
            => sut.NSwagPath.Should().Be(PathProvider.GetNSwagStudioPath());

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