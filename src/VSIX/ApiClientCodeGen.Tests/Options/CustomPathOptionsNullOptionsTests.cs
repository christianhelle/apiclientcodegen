using Rapicgen.Core.Options.General;
using Rapicgen.Options.General;
using FluentAssertions;
using Rapicgen.Core.External;

namespace Rapicgen.Tests.Options
{
    
    public class CustomPathOptionsNullOptionsTests
    {
        private readonly CustomPathOptions sut = new CustomPathOptions();

        [Xunit.Fact]
        public void JavaPath_NotNullOrWhiteSpace()
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void JavaPath_Reads_From_Options()
            => sut.JavaPath.Should().Be(PathProvider.GetJavaPath());

        [Xunit.Fact]
        public void NpmPath_NotNullOrWhiteSpace()
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void NpmPath_Reads_From_Options()
            => sut.NpmPath.Should().Be(PathProvider.GetNpmPath());

        [Xunit.Fact]
        public void NSwagPath_NotNullOrWhiteSpace()
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void NSwagPath_Reads_From_Options()
            => sut.NSwagPath.Should().Be(PathProvider.GetNSwagStudioPath());

        [Xunit.Fact]
        public void SwaggerCodegenPath_NotNullOrWhiteSpace()
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void SwaggerCodegenPath_Reads_From_Options()
            => sut.SwaggerCodegenPath
                .Should()
                .Be(PathProvider.GetSwaggerCodegenPath());

        [Xunit.Fact]
        public void OpenApiGeneratorPath_NotNullOrWhiteSpace()
            => sut.OpenApiGeneratorPath
                .Should()
                .NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void OpenApiGeneratorPath_Reads_From_Options()
            => sut.OpenApiGeneratorPath
                .Should()
                .Be(PathProvider.GetOpenApiGeneratorPath());
    }
}