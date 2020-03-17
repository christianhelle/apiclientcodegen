using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class CustomPathOptionsNullOptionsTests
    {
        private readonly CustomPathOptions sut = new CustomPathOptions();

        [TestMethod, Xunit.Fact]
        public void JavaPath_NotNullOrWhiteSpace()
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void JavaPath_Reads_From_Options()
            => sut.JavaPath.Should().Be(PathProvider.GetJavaPath());

        [TestMethod, Xunit.Fact]
        public void NpmPath_NotNullOrWhiteSpace()
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void NpmPath_Reads_From_Options()
            => sut.NpmPath.Should().Be(PathProvider.GetNpmPath());

        [TestMethod, Xunit.Fact]
        public void NSwagPath_NotNullOrWhiteSpace()
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void NSwagPath_Reads_From_Options()
            => sut.NSwagPath.Should().Be(PathProvider.GetNSwagStudioPath());

        [TestMethod, Xunit.Fact]
        public void SwaggerCodegenPath_NotNullOrWhiteSpace()
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void SwaggerCodegenPath_Reads_From_Options()
            => sut.SwaggerCodegenPath
                .Should()
                .Be(PathProvider.GetSwaggerCodegenPath());

        [TestMethod, Xunit.Fact]
        public void OpenApiGeneratorPath_NotNullOrWhiteSpace()
            => sut.OpenApiGeneratorPath
                .Should()
                .NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void OpenApiGeneratorPath_Reads_From_Options()
            => sut.OpenApiGeneratorPath
                .Should()
                .Be(PathProvider.GetOpenApiGeneratorPath());
    }
}