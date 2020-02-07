using ApiClientCodeGen.CLI.Options;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests
{
    public class GeneralOptionsTests
    {
        [Theory, AutoData]
        public void JavaPath_NotNull(GeneralOptions sut)
            => sut.JavaPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void NpmPath_NotNull(GeneralOptions sut)
            => sut.NpmPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void NSwagPath_NotNull(GeneralOptions sut)
            => sut.NSwagPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void SwaggerCodegenPath_NotNull(GeneralOptions sut)
            => sut.SwaggerCodegenPath.Should().NotBeNullOrWhiteSpace();
        
        [Theory, AutoData]
        public void OpenApiGeneratorPath_NotNull(GeneralOptions sut)
            => sut.OpenApiGeneratorPath.Should().NotBeNullOrWhiteSpace();
    }
}