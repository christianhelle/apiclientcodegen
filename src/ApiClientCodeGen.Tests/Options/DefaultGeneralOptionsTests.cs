using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    
    public class DefaultGeneralOptionsTests
    {
        private readonly IGeneralOptions sut = new DefaultGeneralOptions();

        [Xunit.Fact]
        public void JavaPath_NotNull()
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void NpmPath_NotNull()
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void NSwagPath_NotNull()
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void SwaggerCodegenPath_NotNull()
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void OpenApiGeneratorPath_NotNull()
            => sut.OpenApiGeneratorPath.Should().NotBeNullOrWhiteSpace();

        [Xunit.Fact]
        public void InstallMissingPackages_BeTrue()
            => sut.InstallMissingPackages.Should().BeTrue();
    }
}